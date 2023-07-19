using System;

namespace TLH.Gameplay.Entities.PlayerStateMachine
{
    public class RunState : PlayerState
    {
        public RunState(Action<StateChangeEventArgs> stateChangeCallback) : base(stateChangeCallback)
        {
        }
    }
}