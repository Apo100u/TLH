using System;
using TLH.Extensions;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Interactions;
using UnityEngine;

namespace TLH.Gameplay.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : InteractionInitiator
    {
        public event Action<Projectile> Deactivated;
        public event Action<Projectile> Destroying;

        public LayerMask SourceLayer { get; private set; }
        public ProjectileAttackData AttackData { get; private set; }

        private Rigidbody2D projectilesRigidbody;
        private float shootTime;
        private bool isActive;

        private void Awake()
        {
            projectilesRigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void Init(ProjectileAttackData attackData, LayerMask sourceLayer)
        {
            SourceLayer = sourceLayer;
            AttackData = attackData;
            
            projectilesRigidbody.excludeLayers = projectilesRigidbody.excludeLayers.WithLayer(SourceLayer);
        }
        
        public void Shoot(Vector2 directionNormalized)
        {
            isActive = true;
            gameObject.SetActive(true);
            transform.up = directionNormalized;
            projectilesRigidbody.velocity = directionNormalized * AttackData.Speed;
            shootTime = Time.time;
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
            base.OnTriggerEnter2D(collider);

            if (isActive)
            {
                HandleCollision(collider);
            }
        }

        private void HandleCollision(Collider2D collider)
        {
            if (AttackData.LayersToDestroyOn.ContainsLayer(collider.gameObject.layer))
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