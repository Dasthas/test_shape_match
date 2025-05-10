using Core.FSM;
using Core.FSM.Data;
using Core.FSM.State;
using Modules.UI.Win;
using VContainer;

namespace Modules.FSM.States
{
    public class WinState : BaseState
    {
        [Inject] private WinScreen _winScreen;

        public override StateType StateType => StateType.Win;

        public WinState(IFiniteStateMachine fsm) : base(fsm)
        {
        }

        protected override void OnStateEnter()
        {
            _winScreen.gameObject.SetActive(true);
        }

        protected override void OnStateExit()
        {
            _winScreen.gameObject.SetActive(false);
        }
    }
}