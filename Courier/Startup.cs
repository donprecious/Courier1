using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Courier.Startup))]
namespace Courier
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }



    }
}
