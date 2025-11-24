using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifetimeToolManage.Model;
using LifetimeToolManage.Model.DB;
using System.Windows;

namespace LifetimeToolManage.ViewModel
{
    partial class SerwisViewModel : ObservableObject
    {

        private readonly IToolsService _toolsService;
        private readonly IActiveToolService _activeToolService;
        private readonly ILifetimeService _lifetimeService;


        // Constructor
        public SerwisViewModel(
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

        // Observable Properties
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddToolsCommand))]
        private string? toolCode;

        [ObservableProperty]
        private string? activateToolCode;

        [ObservableProperty]
        private string? activeToolHeader;

        [ObservableProperty]
        private int activeLifetime;

        [ObservableProperty]
        private int newLifetime;


        // Commands
        // --------
        [RelayCommand(CanExecute = nameof(canAddTools))]
        private void AddTools()
        {
            try
            {
                var success = _toolsService.addTool(new Tools { Code = ToolCode });

                if (success)
                    MessageBox.Show($"Dodano narzędzie o kodzie: {ToolCode}");
                else
                    MessageBox.Show("Narzędzie o takim kodzie już istnieje!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}");
            }
            ToolCode = string.Empty;
        }

        [RelayCommand]
        private void ActivateTool()
        {
            try
            {
                var tool = _toolsService.checkIfExistTool(ActivateToolCode);
                if (tool == null)
                {
                    MessageBox.Show("Narzędzie o podanym kodzie nie istnieje!");
                    return;
                }
                var success = _activeToolService.updateActiveTool(tool.Code);
                if (success)
                {
                    // update Current Active Tool Header
                    ActiveToolHeader = tool.Code;
                    // Update Active Lifetime (get from LifetimeService)
                    ActiveLifetime = _toolsService.checkIfExistTool(tool.Code) != null ?
                        (int)(_lifetimeService.getLifetimeByToolId(tool.Id)?.quantity ?? 0) : 0;
                    MessageBox.Show($"Aktywowano narzędzie o kodzie: {ActivateToolCode}");
                }
                else
                    MessageBox.Show("Narzędzie jest już aktywne!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd aktywacji narzedzia: {ex.Message}");
            }
            ActivateToolCode = string.Empty;
        }


        [RelayCommand(CanExecute=nameof(canChangeLifetimeQuantity))]
        private void ChangeLifetimeQuantity()
        {
            string activeToolCode = _activeToolService.getActiveTool();
            if (string.IsNullOrWhiteSpace(activeToolCode))
            {
                MessageBox.Show("Brak aktywnego narzędzia w bazie danych");
                return;
            }
            var activeTool = _toolsService.checkIfExistTool(activeToolCode);
            if (activeTool == null)
            {
                MessageBox.Show("Brak takiego narzędzia w bazie danych");
                return;
            }

            try
            {
                var successEdit = _lifetimeService.editLastQuantity(activeTool.Id, NewLifetime);
                if (successEdit)
                {
                    ActiveLifetime = NewLifetime;
                    MessageBox.Show("Pomyślnie zmieniono wartość Lifetime");
                    NewLifetime = 0;
                }
                else
                {
                    MessageBox.Show("Nie udało się zmienić wartości Lifetime");
                    var successRegister = _lifetimeService.registerQuantity(activeTool.Id, DateTime.Now, NewLifetime);
                    if(successRegister)
                    {
                        ActiveLifetime = NewLifetime;
                        MessageBox.Show("Pomyślnie dodano wartość Lifetime");
                        NewLifetime = 0;
                    }
                    else
                    {
                        MessageBox.Show("Nie udało się dodać wartości Lifetime");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd zmiany wartości Lifetime: {ex.Message}");
            }
        }

        // Methods for local loading data
        // ------------------------------
        private void LoadActiveTool()
        {
            try
            {
                var activeToolCode = _activeToolService.getActiveTool();
                if (activeToolCode != string.Empty)
                {
                    ActiveToolHeader = activeToolCode;
                }
                else
                {
                    ActiveToolHeader = "Brak aktywnego narzędzia";
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
                } else
                {
                    var activeToolLifetime = _lifetimeService.getLifetimeByToolId(tool.Id);
                    if(activeToolLifetime == null)
                    {
                        ActiveLifetime = 0;
                        return;
                    }
                    int tempActiveLifetime = (int)activeToolLifetime.quantity;
                    ActiveLifetime = tempActiveLifetime;
                }

            } catch (Exception ex)
            {
                MessageBox.Show($"Błąd zaladowania Lifetime Data: {ex.Message}");
            }
        }

        private bool canChangeLifetimeQuantity()
        {
            if (NewLifetime < 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        private bool canAddTools()
        {
            return !string.IsNullOrWhiteSpace(ToolCode);
        }
    }
}
