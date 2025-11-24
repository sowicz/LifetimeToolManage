using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifetimeToolManage.View;

namespace LifetimeToolManage.ViewModel
{
    partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserControl currentView;

        private readonly HomeView _homeView = new HomeView();
        private readonly ListaView _listView = new ListaView();
        private readonly SerwisView _serwisView = new SerwisView();

        public MainWindowViewModel()
        {
            CurrentView = _homeView;
        }

        [RelayCommand]
        private void Navigate(string param)
        {
            switch (param)
            {
                case "Home":
                    CurrentView = _homeView;
                    break;
                case "Lista":
                    CurrentView = _listView;
                    break;
                case "Serwis":
                    CurrentView = _serwisView;
                    break;
                default:
                    CurrentView = _homeView;
                    break;
            }
        }


    }
}
