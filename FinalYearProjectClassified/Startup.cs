using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalYearProjectClassified.Startup))]
namespace FinalYearProjectClassified
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
