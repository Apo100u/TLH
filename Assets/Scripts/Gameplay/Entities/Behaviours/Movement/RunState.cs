using TLH.Utility.StateMachine;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours.Movement
{
    public class RunState : State<MovementCommand>
    {
        private Movement movement;
        private Rigidbody2D entitiesRigidbody;

        public RunState(Movement movement, Rigidbody2D entitiesRigidbody)
        {
            this.movement = movement;
            this.entitiesRigidbody = entitiesRigidbody;
        }

        public override void Process()
        {
            base.Process();
            entitiesRigidbody.velocity = movement.NormalizedDirection * movement.RunData.Speed;
        }
    }
}
