using System.Windows;
using Buying_Guide.ViewModel;

namespace Buying_Guide
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {


        private void Application_Startup(object sender, StartupEventArgs e)
        {
               Admin admin = new Admin();
        }
    }
}
