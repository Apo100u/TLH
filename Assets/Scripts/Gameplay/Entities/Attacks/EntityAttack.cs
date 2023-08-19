using System;
using TLH.Extensions;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Interactions;
using UnityEngine;

namespace TLH.Gameplay.Entities.Attacks
{
    public abstract class EntityAttack : InteractionInitiator
    {
        public event Action<EntityAttack> Deactivated;
        public event Action<EntityAttack> Destroying;

        public AttackData AttackData { get; private set; }
        public Entity CurrentSource { get; private set; }

        protected Rigidbody2D entityAttackRigidbody;
        
        private LayerMask originalExcludeLayers;
        private bool isActive;
        private float shootTime;
        
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
            shootTime = Time.time;
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
            entityAttackRigidbody.velocity = directionNormalized * speed;
            transform.up = directionNormalized;
        }
        
        public void OnUpdate()
        {
            if (Time.time > shootTime + AttackData.LifeTimeInSec)
            {
                Deactivate();
            }
        }
        
        private bool IsTriggerEnterIgnoredWithCollider(Collider2D colliderEnteringTrigger)
        {
            EntityAttack entityAttack = colliderEnteringTrigger.GetComponentInParent<EntityAttack>();
            return entityAttack != null && entityAttack.CurrentSource == CurrentSource;
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
        
        protected virtual void HandleUnignoredTrigger(Collider2D collider)
        {
        }
        
        protected void Deactivate()
        {
            isActive = false;
            gameObject.SetActive(false);
            entityAttackRigidbody.velocity = Vector2.zero;
            Deactivated?.Invoke(this);
        }

        public void Destroy()
        {
            Destroying?.Invoke(this);
            Destroy(gameObject);
        }
    }
}