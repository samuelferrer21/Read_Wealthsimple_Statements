using Read_Wealthsimple_Statements.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Read_Wealthsimple_Statements
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
                
            };
            MainWindow.Show();
      
            base.OnStartup(e);
        }
    }

}
