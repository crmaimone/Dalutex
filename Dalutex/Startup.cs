using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dalutex.Startup))]
namespace Dalutex
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
