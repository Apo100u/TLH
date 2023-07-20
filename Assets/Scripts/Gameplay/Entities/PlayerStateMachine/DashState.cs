using System;
using TLH.Gameplay.Entities.Behaviours;
using TLH.Input;

namespace TLH.Gameplay.Entities.PlayerStateMachine
{
    public class DashState : PlayerState
    {
        private PlayerState stateToActivateOnDashEnd;
        private Movement movement;
        private InputReader inputReader;
        
        public DashState(Action<StateChangeEventArgs> stateChangeCallback, Movement movement, InputReader inputReader)
            : base(stateChangeCallback)
        {
            this.movement = movement;
            this.inputReader = inputReader;
        }

        public void AddTransitionOnDashEnd(PlayerState stateToActivateOnDashEnd)
        {
            this.stateToActivateOnDashEnd = stateToActivateOnDashEnd;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            movement.PerformDash(inputReader.GetDirection(), OnDashEnded);
        }

        private void OnDashEnded()
        {
            stateChangeCallback?.Invoke(new StateChangeEventArgs(stateToActivateOnDashEnd));
        }
    }
}