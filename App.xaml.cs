using System.Media;
using System.Windows;

namespace CyberSecurityChatbot
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                new SoundPlayer("greeting.wav").Play();
            }
            catch { }
        }
    }
}
