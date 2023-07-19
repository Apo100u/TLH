using System;
using TLH.Gameplay.Entities.Behaviours;
using TLH.Input;

namespace TLH.Gameplay.Entities.PlayerStateMachine
{
    public class RunState : PlayerState
    {
        private Movement movement;
        private InputReader inputReader;

        public RunState(Action<StateChangeEventArgs> stateChangeCallback, Movement movement, InputReader inputReader)
            : base(stateChangeCallback)
        {
            this.movement = movement;
            this.inputReader = inputReader;
        }

        public override void Process()
        {
            base.Process();
            
            movement.PerformRun(inputReader.GetDirection());
        }
    }
}