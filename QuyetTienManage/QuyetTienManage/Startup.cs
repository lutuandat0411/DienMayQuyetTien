using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuyetTienManage.Startup))]
namespace QuyetTienManage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
