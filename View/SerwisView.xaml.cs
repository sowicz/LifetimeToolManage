using LifetimeToolManage.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace LifetimeToolManage.View
{
    /// <summary>
    /// Logika interakcji dla klasy SerwisView.xaml
    /// </summary>
    public partial class SerwisView : UserControl
    {
        public SerwisView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<SerwisViewModel>();
        }
    }
}
