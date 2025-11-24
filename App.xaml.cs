using LifetimeToolManage.Model.DB;
using LifetimeToolManage.Model;
using LifetimeToolManage.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;

using System.Windows;
using LifetimeToolManage.View;
using Microsoft.EntityFrameworkCore;

namespace LifetimeToolManage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; set; }

        public App()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<AppDbContext>();
            services.AddScoped<IToolsService, ToolsService>();
            services.AddScoped<ILifetimeService, LifetimeService>();
            services.AddScoped<IActiveToolService, ActiveToolService>();

            services.AddTransient<SerwisViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<HomeViewViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new MainWindow();
            {
                var context = ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
                mainWindow.DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            }
            ;
            mainWindow.Show();

        }
    }

}
