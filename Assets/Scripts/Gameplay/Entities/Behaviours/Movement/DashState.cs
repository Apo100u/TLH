using System;
using TLH.Utility;
using TLH.Utility.StateMachine;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours.Movement
{
    public class DashState : State<MovementCommand>
    {
        private Movement movement;
        private Rigidbody2D entitiesRigidbody;
        private Action onDashEnded;
        private Vector2 direction;
        private Vector3 startPosition;
        private float sqrDistanceToTravel;

        public DashState(Movement movement, Rigidbody2D entitiesRigidbody, Action onDashEnded)
        {
            this.movement = movement;
            this.entitiesRigidbody = entitiesRigidbody;
            this.onDashEnded = onDashEnded;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            direction = movement.LastNonZeroNormalizedDirection;
            StartDash();
        }
        
        private void StartDash()
        {
            UnobstructedPlaceFinder unobstructedPlaceFinder = new(movement.LayersBlockingMovement);
            Vector2 wantedPosition = (Vector2)movement.transform.position + direction * movement.DashData.Distance;
            Vector2 unobstructedPosition = unobstructedPlaceFinder.FindFurthestOnPath(movement.transform.position, wantedPosition, movement.Collider.radius);

            startPosition = movement.transform.position;
            sqrDistanceToTravel = (unobstructedPosition - (Vector2)movement.transform.position).sqrMagnitude;

            movement.Collider.enabled = false;
            entitiesRigidbody.velocity = direction * movement.DashData.Speed;
        }

        public override void Process()
        {
            base.Process();
            float sqrDashedDistance = (movement.transform.position - startPosition).sqrMagnitude;
        
            if (sqrDashedDistance >= sqrDistanceToTravel)
            {
                EndDash();
            }
        }
        
        private void EndDash()
        {
            entitiesRigidbody.velocity = movement.NormalizedDirection * movement.RunData.MaxSpeed;
            movement.Collider.enabled = true;
            onDashEnded?.Invoke();
        }
    }
}