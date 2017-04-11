using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERPOLD.Startup))]
namespace ERPOLD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
