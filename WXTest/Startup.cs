using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WXTest.Startup))]
namespace WXTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
