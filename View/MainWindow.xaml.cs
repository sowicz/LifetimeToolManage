using LifetimeToolManage.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LifetimeToolManage.View
{
 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
            //DataContext = new MainWindowViewModel();
        }
    }
}