using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvoiceManagement.Startup))]
namespace InvoiceManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
