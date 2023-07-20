using System;

namespace TLH.Gameplay.Entities.PlayerStateMachine
{
    public class StateTransition
    {
        public PlayerState NewState { get; }
        public Func<bool> Condition { get; private set; }

        public StateTransition(PlayerState newState)
        {
            NewState = newState;
            Condition = null;
        }

        public void SetCondition(Func<bool> condition)
        {
            Condition = condition;
        }
    }
}