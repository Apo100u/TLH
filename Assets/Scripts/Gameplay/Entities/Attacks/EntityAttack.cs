using System;
using TLH.Extensions;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Interactions;
using UnityEngine;

namespace TLH.Gameplay.Entities.Attacks
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EntityAttack : InteractionInitiator
    {
        public event Action<EntityAttack> Deactivated;
        public event Action<EntityAttack> Destroying;

        public AttackData AttackData { get; private set; }
        public Entity CurrentSource { get; private set; }

        protected Rigidbody2D entityAttackRigidbody;
        protected Vector2 velocity;

        private LayerMask originalExcludeLayers;
        private Vector3 lastPosition;
        private Vector3 lastSourcePosition;
        private bool isActive;
        private float activationTime;
        
        private void Awake()
        {
            entityAttackRigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void Init(AttackData attackData)
        {
            AttackData = attackData;
            originalExcludeLayers = entityAttackRigidbody.excludeLayers;
        }
        
        public void Activate(Vector2 directionNormalized, Entity source)
        {
            isActive = true;
            activationTime = Time.time;
            lastSourcePosition = source.transform.position;
            gameObject.SetActive(true);
            SetSource(source);
            SetVelocity(directionNormalized, AttackData.Speed);
        }
        
        protected void SetSource(Entity source)
        {
            CurrentSource = source;
            entityAttackRigidbody.excludeLayers = originalExcludeLayers.WithLayer(source.gameObject.layer);
        }

        protected void SetVelocity(Vector2 directionNormalized, float speed)
        {
            velocity = directionNormalized * speed;
            transform.up = directionNormalized;
        }

        public void OnFixedUpdate()
        {
            ProcessMovement();
            
            lastSourcePosition = CurrentSource.transform.position;
            lastPosition = transform.position;
        }
        
        public void OnUpdate()
        {
            ProcessLifetime();
            ProcessPointingDirection();
        }

        private void ProcessMovement()
        {
            Vector3 targetPosition = (Vector2)transform.position + velocity * Time.fixedDeltaTime;

            if (AttackData.AttachToEntity)
            {
                targetPosition += CurrentSource.transform.position - lastSourcePosition;
            }

            entityAttackRigidbody.MovePosition(targetPosition);
        }

        private void ProcessLifetime()
        {
            if (Time.time > activationTime + AttackData.LifeTimeInSec)
            {
                Deactivate();
            }
        }

        private void ProcessPointingDirection()
        {
            if (AttackData.UpdatePointingDirection)
            {
                transform.up = transform.position - lastPosition;
            }
        }
        
        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            if (!IsTriggerEnterIgnoredWithCollider(collider))
            {
                base.OnTriggerEnter2D(collider);

                if (isActive)
                {
                    HandleUnignoredTrigger(collider);
                }
            }
        }
        
        private bool IsTriggerEnterIgnoredWithCollider(Collider2D colliderEnteringTrigger)
        {
            EntityAttack entityAttack = colliderEnteringTrigger.GetComponentInParent<EntityAttack>();
            return entityAttack != null && entityAttack.CurrentSource == CurrentSource;
        }
        
        private void HandleUnignoredTrigger(Collider2D collider)
        {
            if (!AttackData.LayersToPierce.ContainsLayer(collider.gameObject.layer))
            {
                Deactivate();
            }
        }
        
        protected void Deactivate()
        {
            isActive = false;
            gameObject.SetActive(false);
            velocity = Vector2.zero;
            Deactivated?.Invoke(this);
        }

        public void Destroy()
        {
            Destroying?.Invoke(this);
            Destroy(gameObject);
        }
    }
}