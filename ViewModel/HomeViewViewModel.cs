using CommunityToolkit.Mvvm.ComponentModel;
using LifetimeToolManage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LifetimeToolManage.ViewModel
{
    partial class HomeViewViewModel : ObservableObject
    {

        private readonly IToolsService _toolsService;
        private readonly IActiveToolService _activeToolService;
        private readonly ILifetimeService _lifetimeService;

        [ObservableProperty]
        private string? activeToolName;

        [ObservableProperty]
        private int activeLifetime;

        public HomeViewViewModel(
            IToolsService toolsService,
            IActiveToolService activeToolService,
            ILifetimeService lifetimeService
            )
        {
            _toolsService = toolsService;
            _activeToolService = activeToolService;
            _lifetimeService = lifetimeService;

            LoadActiveTool();
            LoadLifetimeData();
        }

        private void LoadActiveTool()
        {
            try
            {
                var activeToolCode = _activeToolService.getActiveTool();
                if (activeToolCode != string.Empty)
                {
                    ActiveToolName = activeToolCode;
                }
                else
                {
                    ActiveToolName = "Brak aktywnego narzędzia";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd zaladowania Active Tool: {ex.Message}");
            }
        }

        private void LoadLifetimeData()
        {
            try
            {
                var activeToolCode = _activeToolService.getActiveTool();
                var tool = _toolsService.checkIfExistTool(activeToolCode);
                if (tool == null)
                {
                    ActiveLifetime = 0;
                    return;
                }
                else
                {
                    var activeToolLifetime = _lifetimeService.getLifetimeByToolId(tool.Id);
                    if (activeToolLifetime == null)
                    {
                        ActiveLifetime = 0;
                        return;
                    }
                    int tempActiveLifetime = (int)activeToolLifetime.quantity;
                    ActiveLifetime = tempActiveLifetime;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd zaladowania Lifetime Data: {ex.Message}");
            }
        }

    }
}
