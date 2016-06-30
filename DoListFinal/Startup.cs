using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoListFinal.Startup))]
namespace DoListFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
