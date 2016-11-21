using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_NotesDeFrais.Startup))]
namespace Project_NotesDeFrais
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
