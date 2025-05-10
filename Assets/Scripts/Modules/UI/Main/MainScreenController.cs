using System;

namespace Modules.UI.Main
{
    public class MainScreenController : IMainScreenController, IDisposable
    {
        private readonly MainScreenView _mainScreenView;
        public Action OnRestartClicked { get; set; }

        public MainScreenController(MainScreenView mainScreenView)
        {
            _mainScreenView = mainScreenView;
            mainScreenView.RestartButton.onClick.AddListener(() => OnRestartClicked?.Invoke());
        }

        public void Dispose()
        {
            _mainScreenView.RestartButton.onClick.RemoveAllListeners();
        }
    }
}