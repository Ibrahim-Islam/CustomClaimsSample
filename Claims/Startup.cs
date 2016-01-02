using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Claims.Startup))]
namespace Claims
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
