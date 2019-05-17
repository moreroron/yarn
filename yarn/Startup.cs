using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(yarn.Startup))]
namespace yarn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
