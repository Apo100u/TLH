using System;
using System.Collections.Generic;

namespace TLH.Gameplay.Entities.PlayerStateMachine
{
    public abstract class PlayerState
    {
        protected Action<StateChangeEventArgs> stateChangeCallback;
        private Dictionary<Command, StateTransition> transitions = new();

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

        public StateTransition AddTransition(Command command, PlayerState stateToActivate)
        {
            StateTransition stateTransition = new(stateToActivate);
            transitions.Add(command, stateTransition);
            return stateTransition;
        }

        public void ExecuteCommand(Command command)
        {
            if (transitions.TryGetValue(command, out StateTransition stateTransition))
            {
                bool conditionSatisfied = stateTransition.Condition == null || stateTransition.Condition();

                if (conditionSatisfied)
                {
                    stateChangeCallback?.Invoke(new StateChangeEventArgs(stateTransition.NewState));
                }
            }
        }
    }
}