using Core.FSM.Data;

namespace Core.FSM
{
    public interface IFiniteStateMachine
    {
        StateType CurrentStateType { get; }
        void ChangeState(StateType newStateType);
    }
}