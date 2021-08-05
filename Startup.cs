using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtGalleryWebsite.Startup))]
namespace ArtGalleryWebsite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
