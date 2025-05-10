using System;
using System.Collections.Generic;
using System.Threading;
using Core.FSM.Data;
using Core.FSM.State;
using Cysharp.Threading.Tasks;
using Modules.FSM.States;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.FSM
{
    public class FiniteStateMachine : IFiniteStateMachine, IInitializable, IDisposable
    {
        private IState _currentState;

        private Dictionary<StateType, IState> _statesMap = new Dictionary<StateType, IState>();

        public StateType CurrentStateType => _currentState.StateType;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public FiniteStateMachine(IObjectResolver resolver)
        {
            CreateState(resolver, new InitializationState(this), StateType.Initialization);
            CreateState(resolver, new GameplayState(this), StateType.Gameplay);
            CreateState(resolver, new RestartState(this), StateType.Restart);
            CreateState(resolver, new WinState(this), StateType.Win);
            CreateState(resolver, new LoseState(this), StateType.Lose);
        }

        private void CreateState(IObjectResolver resolver, IState state, StateType stateType)
        {
            resolver.Inject(state);
            _statesMap.Add(stateType, state);
        }

        public void Initialize()
        {
            Debug.Log("FiniteStateMachine initialized " + _statesMap.Count);
            ChangeState(StateType.Initialization);
        }

        public void ChangeState(StateType newStateType)
        {
            ChangeStateAsync(newStateType).Forget();
        }

        private async UniTask ChangeStateAsync(StateType newStateType)
        {
            if (_currentState != null)
            {
                await _currentState.ExitAsync(_cts.Token);
            }

            if (_statesMap.TryGetValue(newStateType, out var nextState))
            {
                _currentState = nextState;
                Debug.Log($"[FSM]: ENTER {newStateType} state");
                await nextState.EnterAsync(_cts.Token);
            }
            else
            {
                throw new KeyNotFoundException($"[FSM]: State type {newStateType} not found");
            }
        }

        public void Dispose()
        {
            _cts?.Dispose();
            _currentState?.Dispose();
        }
    }
}