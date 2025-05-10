using System.Threading;
using Core.Extensions;
using Core.FSM;
using Core.FSM.Data;
using Core.FSM.State;
using Core.Pool;
using Core.Settings;
using Cysharp.Threading.Tasks;
using Modules.Figure;
using Modules.Figure.Container;
using Modules.Figure.Model;
using UnityEngine;
using VContainer;

namespace Modules.FSM.States
{
    public class InitializationState : BaseState
    {
        [Inject] private IFiguresContainerController _figuresContainerController;
        [Inject] private FiguresContainerView _figuresContainerView;

        [Inject] private PoolSimple<FigureView> _figureViewPool;
        [Inject] private GameplaySettings _gameplaySettings;
        [Inject] private FiguresModel _figuresModel;

        private Sprite[] _iconSprites;
        private Sprite[] _figureSprites;
        private Color[] _figureColors;

        private int _iconSpritesLoopCounter;
        private int _figureSpritesLoopCounter;
        private int _figureColorsLoopCounter;

        public override StateType StateType => StateType.Initialization;

        public InitializationState(IFiniteStateMachine fsm) : base(fsm)
        {
        }

        public override async UniTask EnterAsync(CancellationToken cancellationToken)
        {
            var settings = _gameplaySettings;
            _iconSprites = settings.FigureIconSprites.ShuffleToArray();
            _figureSprites = settings.FigureSprites.ShuffleToArray();
            _figureColors = settings.FigureColors.ShuffleToArray();

            for (var i = 0; i < settings.FiguresMaxCount; i += 3)
            {
                var settingsData = GetNextFigureSettingData();
                for (var j = 0; j < 3; j++)
                {
                    var view = _figureViewPool.Get();

                    view.InitializeView(settingsData);
                    view.SetPosition(_figuresContainerView.SpawnPoint + GetRandomOffset());

                    _figuresContainerController.ActiveFigures.Add(view);
                    _figuresModel.Add(new FigureData(view, i));
                }
            }

            var shuffledViews = _figuresContainerController.ActiveFigures.ShuffleToEnumerable();
            foreach (var view in shuffledViews)
            {
                view.gameObject.SetActive(true);
                view.EnablePhysics(true);
                await UniTask.WaitForSeconds(settings.TimeBetweenSpawnFigure, cancellationToken: cancellationToken);
            }
            cancellationToken.ThrowIfCancellationRequested();
            await base.EnterAsync(cancellationToken);
        }

        protected override void OnStateEnter()
        {
            ChangeState(StateType.Gameplay);
        }

        protected override void OnStateExit()
        {
        }

        private Vector3 GetRandomOffset()
        {
            var x = Random.Range(-0.3f, 0.3f);
            return new Vector3(x, 0, 0);
        }

        private FigureSettingData GetNextFigureSettingData()
        {
            ClampLoopCounter(ref _iconSpritesLoopCounter, _iconSprites.Length);
            ClampLoopCounter(ref _figureSpritesLoopCounter, _figureSprites.Length);
            ClampLoopCounter(ref _figureColorsLoopCounter, _figureColors.Length);

            var iconSprite = _iconSprites[_iconSpritesLoopCounter];
            var figureSprite = _figureSprites[_figureSpritesLoopCounter];
            var figureColor = _figureColors[_figureColorsLoopCounter];
            return new FigureSettingData(figureSprite, iconSprite, figureColor);
        }

        private void ClampLoopCounter(ref int value, int max)
        {
            if (value >= max - 1)
            {
                value = 0;
            }
            else
            {
                value++;
            }
        }
    }
}