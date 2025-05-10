using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Main
{
    public class MainScreenView: MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;

        public Button RestartButton => _restartButton;
    }
}