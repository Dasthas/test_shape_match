using System.Threading;
using Core.FSM;
using Core.FSM.Data;
using Core.FSM.State;
using Core.Input;
using Core.Pool;
using Core.Settings;
using Cysharp.Threading.Tasks;
using Modules.Figure;
using Modules.Figure.Bar;
using Modules.Figure.Container;
using Modules.Figure.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Modules.FSM.States
{
    public class GameplayState : BaseState
    {
        [Inject] private IFiguresContainerController _figuresContainerController;
        [Inject] private FiguresContainerView _figuresContainerView;
        [Inject] private IFiguresBarController _figuresBarController;
        [Inject] private Camera _mainCamera;
        [Inject] private InputModel _inputModel;
        [Inject] private GameplaySettings _gameplaySettings;
        [Inject] private PoolSimple<FigureView> _figureViewPool;
        [Inject] private FiguresModel _figuresModel;

        private RaycastHit2D[] _hitResults = new RaycastHit2D[5];

        private CancellationTokenSource _cts;

        public override StateType StateType => StateType.Gameplay;

        private bool _canProcess;

        public GameplayState(IFiniteStateMachine fsm) : base(fsm)
        {
        }

        public override void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
            _inputModel.OnPointerDown -= OnPointerDown;
            _figuresBarController.ClearAll();
        }

        protected override void OnStateEnter()
        {
            _cts = new CancellationTokenSource();
            _inputModel.OnPointerDown += OnPointerDown;
            _canProcess = true;
        }

        protected override void OnStateExit()
        {
            Dispose();
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            if (!_canProcess)
            {
                return;
            }

            OnPointerDownAsync(eventData, _cts.Token).Forget();
        }

        private async UniTask OnPointerDownAsync(PointerEventData pointerEventData, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var ray = _mainCamera.ScreenPointToRay(pointerEventData.position);
            var hitsCount = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, _hitResults, float.PositiveInfinity,
                _gameplaySettings.FiguresLayerMask, float.NegativeInfinity, float.PositiveInfinity);

            for (int i = 0; i < hitsCount; i++)
            {
                var view = _hitResults[i].collider.GetComponent<FigureView>();

                // check if view is in active collection
                if (_figuresContainerController.ActiveFigures.Contains(view))
                {
                    view.EnablePhysics(false);
                    // move to bar
                    var data = _figuresModel.GetDataByView(view);
                    await _figuresBarController.MoveFigureToBar(data, view.GetCancellationTokenOnDestroy());
                    _figuresContainerController.ActiveFigures.Remove(view);
                    break;
                }
            }

            token.ThrowIfCancellationRequested();

            // clear matches
            _figuresBarController.ClearMatches();

            // check for lose
            if (_figuresBarController.IsFull())
            {
                ChangeState(StateType.Lose);
                return;
            }

            // check for win
            if (_figuresContainerController.ActiveFigures.Count == 0)
            {
                ChangeState(StateType.Win);
            }

            _canProcess = true;
        }
    }
}