using Core.FSM;
using Core.FSM.Data;
using Core.FSM.State;
using Core.Pool;
using Modules.Figure;
using Modules.Figure.Container;
using Modules.Figure.Model;
using VContainer;

namespace Modules.FSM.States
{
    public class RestartState : BaseState
    {
        public override StateType StateType => StateType.Restart;

        [Inject] private PoolSimple<FigureView> _figureViewPool;

        [Inject] private IFiguresContainerController _figuresContainerController;
        [Inject] private FiguresModel _figuresModel;

        public RestartState(IFiniteStateMachine fsm) : base(fsm)
        {
        }

        protected override void OnStateEnter()
        {
            //clear scene
            foreach (var figureData in _figuresModel.GetEnumerable())
            {
                figureData.FigureView.gameObject.SetActive(false);
                _figureViewPool.Add(figureData.FigureView);
            }

            _figuresModel.Clear();
            _figuresContainerController.ActiveFigures.Clear();
            ChangeState(StateType.Initialization);
        }

        protected override void OnStateExit()
        {
        }
    }
}