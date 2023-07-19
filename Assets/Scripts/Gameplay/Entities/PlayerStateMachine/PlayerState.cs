using System;
using System.Collections.Generic;

namespace TLH.Gameplay.Entities.PlayerStateMachine
{
    public abstract class PlayerState
    {
        private Action<StateChangeEventArgs> stateChangeCallback;
        private Dictionary<Command, PlayerState> transitions = new();

        protected PlayerState(Action<StateChangeEventArgs> stateChangeCallback)
        {
            this.stateChangeCallback = stateChangeCallback;
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual void Process()
        {
            
        }

        public void AddTransition(Command command, PlayerState stateToActivate)
        {
            transitions.Add(command, stateToActivate);
        }

        public void ExecuteCommand(Command command)
        {
            if (transitions.TryGetValue(command, out PlayerState newState))
            {
                stateChangeCallback?.Invoke(new StateChangeEventArgs(newState));
            }
        }
    }
}