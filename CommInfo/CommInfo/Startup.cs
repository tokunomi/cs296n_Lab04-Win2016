using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CommInfo.Startup))]
namespace CommInfo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
