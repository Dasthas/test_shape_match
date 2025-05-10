using System.Threading;
using Core.FSM.Data;
using Cysharp.Threading.Tasks;

namespace Core.FSM.State
{
    public interface IState
    {
        StateType StateType { get; }
        UniTask EnterAsync(CancellationToken cancellationToken);
        UniTask ExitAsync(CancellationToken cancellationToken);
        void Dispose();
        void ChangeState(StateType newState);
    }
}