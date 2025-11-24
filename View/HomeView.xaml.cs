using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using LifetimeToolManage.ViewModel;

namespace LifetimeToolManage.View
{
    /// <summary>
    /// Logika interakcji dla klasy HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<HomeViewViewModel>();
        }
    }
}
