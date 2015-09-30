using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductSurvey.Startup))]
namespace ProductSurvey
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
