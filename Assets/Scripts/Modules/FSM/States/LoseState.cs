using Core.FSM;
using Core.FSM.Data;
using Core.FSM.State;
using Modules.UI.Lose;
using VContainer;

namespace Modules.FSM.States
{
    public class LoseState : BaseState
    {
        public override StateType StateType => StateType.Lose;
        
        [Inject]
        private LoseScreen _loseScreen;

        public LoseState(IFiniteStateMachine fsm) : base(fsm)
        {
        }

        protected override void OnStateEnter()
        {
            _loseScreen.gameObject.SetActive(true);
        }

        protected override void OnStateExit()
        {
            _loseScreen.gameObject.SetActive(false);
        }
    }
}