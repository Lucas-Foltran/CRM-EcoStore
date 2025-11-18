using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Email
{
    public class EmailModel
    {
            public string email { get; set; }
            public string senha { get; set; }
            public string subDominio { get; set; }
            public string nome { get; set; }
            public string erro { get; set; }
            public string codigo { get; set; }
            public string revisao { get; set; }
            public string urlSistema { get; set; }
            public string template { get; set; }
            public string titulo { get; set; }
            public string content { get; set; }
            public string content2 { get; set; }
            public string cabecalhoEmail { get; set; }
            public string cliente { get; set; }
            public string status { get; set; }
    }
}
