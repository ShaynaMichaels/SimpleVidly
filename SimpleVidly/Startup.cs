using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleVidly.Startup))]
namespace SimpleVidly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
