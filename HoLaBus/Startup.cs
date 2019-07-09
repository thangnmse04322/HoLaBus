using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HoLaBus.Startup))]
namespace HoLaBus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
