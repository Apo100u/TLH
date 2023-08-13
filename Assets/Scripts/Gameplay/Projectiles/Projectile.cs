using System;
using TLH.Extensions;
using TLH.Gameplay.Entities;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Interactions;
using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Interactable))]
    public class Projectile : InteractionInitiator, IInteractionReceiver<KnockbackInteraction>
    {
        public event Action<Projectile> Deactivated;
        public event Action<Projectile> Destroying;

        public ProjectileAttackData AttackData { get; private set; }

        private Rigidbody2D projectilesRigidbody;
        private LayerMask originalExcludeLayers;
        private Entity currentSource;
        private bool isActive;
        private float shootTime;

        private void Awake()
        {
            projectilesRigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void Init(ProjectileAttackData attackData)
        {
            AttackData = attackData;
            originalExcludeLayers = projectilesRigidbody.excludeLayers;
        }
        
        public void HandleInteraction(KnockbackInteraction interaction, InteractionInitiator initiator)
        {
            Vector2 directionNormalized = (transform.position - initiator.transform.position).normalized;

            float speed = interaction.MultiplyExistingVelocity
                ? projectilesRigidbody.velocity.magnitude * interaction.Power
                : interaction.Power;

            projectilesRigidbody.velocity = directionNormalized * speed;
        }
        
        public void Shoot(Vector2 directionNormalized, Entity source)
        {
            currentSource = source;
            isActive = true;
            shootTime = Time.time;
            gameObject.SetActive(true);
            transform.up = directionNormalized;
            projectilesRigidbody.excludeLayers = originalExcludeLayers.WithLayer(source.gameObject.layer);
            projectilesRigidbody.velocity = directionNormalized * AttackData.Speed;
        }

        public void OnUpdate()
        {
            if (Time.time > shootTime + AttackData.LifeTimeInSec)
            {
                Deactivate();
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            if (!IsTriggerEnterIgnoredWithCollider(collider))
            {
                base.OnTriggerEnter2D(collider);

                if (isActive)
                {
                    HandleCollision(collider);
                }
            }
        }

        private bool IsTriggerEnterIgnoredWithCollider(Collider2D colliderEnteringTrigger)
        {
            Projectile projectile = colliderEnteringTrigger.GetComponentInParent<Projectile>();
            return projectile != null && projectile.currentSource == currentSource;
        }

        private void HandleCollision(Collider2D collider)
        {
            if (!AttackData.LayersToPierce.ContainsLayer(collider.gameObject.layer))
            {
                Deactivate();
            }
        }

        private void Deactivate()
        {
            isActive = false;
            gameObject.SetActive(false);
            projectilesRigidbody.velocity = Vector2.zero;
            Deactivated?.Invoke(this);
        }

        public void Destroy()
        {
            Destroying?.Invoke(this);
            Destroy(gameObject);
        }
    }
}