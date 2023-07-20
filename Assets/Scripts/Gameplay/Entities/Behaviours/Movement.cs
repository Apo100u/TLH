using System;
using TLH.Gameplay.Entities.ActionData;
using TLH.Utility;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : EntityBehaviour
    {
        [SerializeField] private CircleCollider2D movementCollider;
        [SerializeField] private LayerMask layersBlockingMovement;
        
        private Rigidbody2D entitiesRigidbody;
        private RunData runData;
        private DashData dashData;

        private Vector2 lastNonZeroRunDirection = Vector2.down;
        
        private bool isDashing;
        private float sqrDashDistanceToTravel;
        private Vector2 dashDirection;
        private Vector3 dashStartPosition;
        private Action dashEndCallback;

        protected override void Awake()
        {
            base.Awake();
            entitiesRigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetRunData(RunData runData)
        {
            this.runData = runData;
        }

        public void SetDashData(DashData dashData)
        {
            this.dashData = dashData;
        }
        
        public void PerformRun(Vector2 direction)
        {
            entitiesRigidbody.velocity = direction.normalized * runData.Speed;

            if (direction != Vector2.zero)
            {
                lastNonZeroRunDirection = direction;
            }
        }

        public void PerformDash(Vector2 direction, Action dashEndCallback = null)
        {
            if (direction == Vector2.zero)
            {
                direction = lastNonZeroRunDirection;
            }
            
            UnobstructedPlaceFinder placeFinder = new(layersBlockingMovement);
            Vector2 dashTargetPosition = (Vector2)transform.position + direction.normalized * dashData.Distance;
            Vector2 adjustedDashTargetPosition = placeFinder.FindFurthestOnPath(transform.position, dashTargetPosition, movementCollider.radius);

            movementCollider.enabled = false;
            
            isDashing = true;
            sqrDashDistanceToTravel = (adjustedDashTargetPosition - (Vector2)transform.position).sqrMagnitude;
            dashDirection = direction;
            dashStartPosition = transform.position;
            this.dashEndCallback = dashEndCallback;
        }

        private void Update()
        {
            if (isDashing)
            {
                float sqrDashedDistance = (transform.position - dashStartPosition).sqrMagnitude;

                if (sqrDashedDistance >= sqrDashDistanceToTravel)
                {
                    movementCollider.enabled = true;
                    isDashing = false;
                    dashEndCallback?.Invoke();
                    dashEndCallback = null;
                }
                else
                {
                    entitiesRigidbody.velocity = dashDirection.normalized * dashData.Speed;
                }
            }
        }
    }
}