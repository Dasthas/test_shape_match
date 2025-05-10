using System.Threading;
using Core.FSM.Data;
using Cysharp.Threading.Tasks;
using Modules.UI.Main;
using VContainer;

namespace Core.FSM.State
{
    public abstract class BaseState : IState
    {
        [Inject] private IMainScreenController _mainScreenController;

        private readonly IFiniteStateMachine _fsm;

        public abstract StateType StateType { get; }

        protected BaseState(IFiniteStateMachine fsm)
        {
            _fsm = fsm;
        }

        protected abstract void OnStateEnter();
        protected abstract void OnStateExit();

        public virtual UniTask EnterAsync(CancellationToken cancellationToken)
        {
            _mainScreenController.OnRestartClicked += OnRestartClicked;
            OnStateEnter();
            return UniTask.CompletedTask;
        }

        public virtual UniTask ExitAsync(CancellationToken cancellationToken)
        {
            _mainScreenController.OnRestartClicked -= OnRestartClicked;
            OnStateExit();
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// Custom Dispose for currently active state
        /// </summary>
        public virtual void Dispose()
        {
            _mainScreenController.OnRestartClicked -= OnRestartClicked;
        }

        public virtual void ChangeState(StateType newState)
        {
            _fsm.ChangeState(newState);
        }


        public virtual void OnRestartClicked()
        {
            ChangeState(StateType.Restart);
        }
    }
}