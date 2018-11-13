using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuienEsQuien.Startup))]
namespace QuienEsQuien
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
