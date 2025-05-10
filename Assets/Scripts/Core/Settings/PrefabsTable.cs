using Core.Input;
using Modules.Figure;
using Modules.Figure.Bar;
using Modules.Figure.Container;
using Modules.UI.Lose;
using Modules.UI.Main;
using Modules.UI.Win;
using UnityEngine;

namespace Core.Settings
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Create/Setting/PrefabsTable")]
    public class PrefabsTable : ScriptableObject
    {
        [Header("UI")] [SerializeField] private MainScreenView _mainScreenPrefab;
        [SerializeField] private WinScreen _winScreenPrefab;
        [SerializeField] private LoseScreen _loseScreenPrefab;
        [SerializeField] private InputView _inputViewPrefab;

        [Header("Physic")] [SerializeField] private FigureView _figureViewPrefab;
        [SerializeField] private FiguresBarView _figuresBarViewPrefab;
        [SerializeField] private FiguresContainerView _figuresContainerPrefab;

        public MainScreenView MainScreenPrefab => _mainScreenPrefab;

        public LoseScreen LoseScreenPrefab => _loseScreenPrefab;

        public WinScreen WinScreenPrefab => _winScreenPrefab;

        public FigureView FigureViewPrefab => _figureViewPrefab;

        public FiguresContainerView FiguresContainerPrefab => _figuresContainerPrefab;

        public FiguresBarView FiguresBarViewPrefab => _figuresBarViewPrefab;

        public InputView InputViewPrefab => _inputViewPrefab;
    }
}