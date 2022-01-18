using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoIdentity.Startup))]
namespace ToDoIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
