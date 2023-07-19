using System;

namespace TLH.Gameplay.Entities.PlayerStateMachine
{
    public class MobilityActionState : PlayerState
    {
        private PlayerState stateToActivateOnActionEnd;
        
        public MobilityActionState(Action<StateChangeEventArgs> stateChangeCallback) : base(stateChangeCallback)
        {
        }

        public void AddTransitionOnActionEnded(PlayerState stateToActivateOnActionEnd)
        {
            this.stateToActivateOnActionEnd = stateToActivateOnActionEnd;
        }
    }
}