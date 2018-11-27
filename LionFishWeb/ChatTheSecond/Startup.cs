using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatTheSecond.Startup))]
namespace ChatTheSecond
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
