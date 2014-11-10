using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MessManagementSystem.Startup))]
namespace MessManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
