using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Appocal.Startup))]
namespace Appocal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
