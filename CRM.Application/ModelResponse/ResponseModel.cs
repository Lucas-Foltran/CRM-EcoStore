using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.ModelResponse
{
    public class ResponseModel
    {
        public ResponseModel()
        {

        }
        public ResponseModel(ResponseModel parametro)
        {
            Success = parametro.Success;
            Error = parametro.Error;
            Message = parametro.Message;
        }

        public ResponseModel(bool sucesso, string message, bool erro)
        {
            Success = sucesso;
            Error = erro;
            Message = message;
        }

        public bool Success { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
