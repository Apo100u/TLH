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

        private UnobstructedPlaceFinder unobstructedPlaceFinder;
        private Rigidbody2D entitiesRigidbody;
        private RunData runData;
        private DashData dashData;

        private Vector2 lastNonZeroRunDirection = Vector2.down;
        
        private bool isDashing;
        private float remainingDashCooldown;
        private DashInfo currentDashInfo;

        protected override void Awake()
        {
            base.Awake();
            entitiesRigidbody = GetComponent<Rigidbody2D>();
            unobstructedPlaceFinder = new UnobstructedPlaceFinder(layersBlockingMovement);
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
            MoveRigidbody(direction, runData.Speed);

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
            
            StartDash(direction, dashEndCallback);
        }

        private void MoveRigidbody(Vector2 direction, float speed)
        {
            // TODO: Remake into ProcessVelocity method called from FixedUpdate
            // with velocity calculations based on velocity "wanted" by the entity and "independent" velocities like attack pushback or explosion for example.
            // Consider introducing helper class like MovementForce with Direction, Speed and possibly optional Curve with speed over time and a variable
            // holding current time for that force.
            entitiesRigidbody.velocity = direction.normalized * speed;
        }

        private void Update()
        {
            if (isDashing)
            {
                ProcessDash();
            }

            UpdateCooldowns();
        }

        private void UpdateCooldowns()
        {
            if (remainingDashCooldown > 0)
            {
                remainingDashCooldown -= Time.deltaTime;

                if (remainingDashCooldown < 0)
                {
                    remainingDashCooldown = 0;
                }
            }
        }
        
        public bool IsDashAvailable()
        {
            return remainingDashCooldown <= 0;
        }

        private void ProcessDash()
        {
            float sqrDashedDistance = (transform.position - currentDashInfo.StartPosition).sqrMagnitude;

            if (sqrDashedDistance >= currentDashInfo.SqrDistanceToTravel)
            {
                EndDash();
            }
            else
            {
                MoveRigidbody(currentDashInfo.Direction, dashData.Speed);
            }
        }

        private void StartDash(Vector2 direction, Action dashEndCallback = null)
        {
            Vector2 targetPosition = (Vector2)transform.position + direction.normalized * dashData.Distance;
            Vector2 unobstructedTargetPosition = unobstructedPlaceFinder.FindFurthestOnPath(transform.position, targetPosition, movementCollider.radius);
            float sqrDashDistanceToTravel = (unobstructedTargetPosition - (Vector2)transform.position).sqrMagnitude;
            
            movementCollider.enabled = false;
            isDashing = true;
            currentDashInfo = new DashInfo(sqrDashDistanceToTravel, direction, transform.position, dashEndCallback);
        }

        private void EndDash()
        {
            movementCollider.enabled = true;
            isDashing = false;
            currentDashInfo.EndCallback?.Invoke();
            currentDashInfo.EndCallback = null;
            currentDashInfo = null;
            remainingDashCooldown = dashData.Cooldown;
        }
    }
}