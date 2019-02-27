using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AllYouMedia.Startup))]
namespace AllYouMedia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
