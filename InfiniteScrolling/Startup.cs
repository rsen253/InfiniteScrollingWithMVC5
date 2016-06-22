using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InfiniteScrolling.Startup))]
namespace InfiniteScrolling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
