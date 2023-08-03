using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManagement.Models
{
    public class Response
    {
        public bool Error { get; set; }
        public List<string> Messages { get; set; }


        public Response()
        {
            Error = false;
            Messages = new List<string>();
        }
    }
}