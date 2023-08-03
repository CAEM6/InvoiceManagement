using InvoiceManagement.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;

namespace InvoiceManagement.Utils
{
    public class ExceptionHandler
    {
        public static void ResolveException(DbEntityValidationException ex, ref Response response)
        {
            response.Error = true;
            foreach (var eve in ex.EntityValidationErrors)
            {
                foreach (var ve in eve.ValidationErrors)
                    response.Messages.Add(ve.ErrorMessage.Replace("'", "").Replace("\"", "").Replace("\r\n", "  "));
            }
        }

        public static void ResolveException(DbUpdateException ex, ref Response response)
        {
            response.Error = true;
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");
            try
            {
                foreach (var result in ex.Entries)
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }
            response.Messages.Add(builder.ToString());
        }

        public static void ResolveException(Exception ex, ref Response response)
        {
            response.Error = true;
            if (ex.InnerException != null)
                response.Messages.Add(ex.InnerException.InnerException.Message);
            else
                response.Messages.Add(ex.Message);
        }
    }
}