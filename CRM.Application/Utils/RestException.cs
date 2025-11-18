using CRM.Application.ModelResponse;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Utils
{
    public class RestException : Exception
    {
        public string nome { get; set; }
        public Exception innerException { get; set; }

        public RestException(Type tipo, Exception innerException)
        {
            var display = tipo
                    .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() as DisplayNameAttribute;

            nome = tipo.Name;
            this.innerException = innerException;
        }

        public ResponseModel TratarErro()
        {
            ResponseModel model = new ResponseModel();

            if (innerException is DbUpdateException exception)
            {
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.Message.Contains("REFERENCE constraint"))
                    {
                        model.Message = string.Format("{0} possui registros vinculado e não pode ser removido", nome);
                    }
                    else if (exception.InnerException.Message.Contains("Cannot insert duplicate key"))
                    {
                        var exceptionEntry = exception.Entries.Single();
                        var clientValues = exceptionEntry.Entity;


                        //if (!string.IsNullOrEmpty(nome))
                        //model.Message = string.Format("{0} já existe", nome);
                        //else
                        model.Message = "Operação cancelada, esse registro já existe: " + exception.InnerException.Message;
                    }
                }

                model.Success = false;
                model.Error = false;
            }

            //não funciona, não sei pq
            Log.Debug("TratarErro: " + innerException.Message + " - " + innerException.StackTrace);

            if (string.IsNullOrEmpty(model.Message))
            {
                if (innerException.InnerException != null)
                    model.Message = innerException.InnerException.Message;
                else
                    model.Message = innerException.Message;

                model.Success = false;
                model.Error = true;
            }

            return model;
        }

    }
}
