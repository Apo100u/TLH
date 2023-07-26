using TLH.Utility.StateMachine;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours.Movement
{
    public class RunState : State<MovementCommand>
    {
        private Movement movement;
        private Rigidbody2D entitiesRigidbody;
        private Vector2 currentVelocity;

        public RunState(Movement movement, Rigidbody2D entitiesRigidbody)
        {
            this.movement = movement;
            this.entitiesRigidbody = entitiesRigidbody;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            currentVelocity = entitiesRigidbody.velocity;
        }

        public override void Process()
        {
            base.Process();

            currentVelocity = UpdateMovementVelocity(currentVelocity);
            entitiesRigidbody.velocity = currentVelocity;
        }

        private Vector2 UpdateMovementVelocity(Vector2 currentVelocity)
        {
            Vector2 updatedVelocity = currentVelocity;

            if (movement.RunData.Sliding != 0f)
            {
                updatedVelocity = ApplySlidingToVelocity(updatedVelocity);
            }
            else
            {
                updatedVelocity = movement.NormalizedDirection * movement.RunData.MaxSpeed;
            }

            return updatedVelocity;
        }

        private Vector2 ApplySlidingToVelocity(Vector2 velocity)
        {
            Vector2 slidingVelocity = velocity;
            float slideMultiplier = movement.RunData.MaxSpeed / movement.RunData.Sliding;
            
            if (movement.NormalizedDirection == Vector2.zero)
            {
                slidingVelocity = Vector2.MoveTowards(slidingVelocity, Vector2.zero, Time.deltaTime * slideMultiplier);
            }
            else
            {
                float targetSpeed = Mathf.MoveTowards(velocity.magnitude, movement.RunData.MaxSpeed, Time.deltaTime * slideMultiplier);
                slidingVelocity = movement.NormalizedDirection * targetSpeed;
            }
            
            return slidingVelocity;
        }
    }
}