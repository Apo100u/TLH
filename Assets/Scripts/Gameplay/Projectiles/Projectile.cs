using System;
using TLH.Extensions;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Interactions;
using UnityEngine;

namespace TLH.Gameplay.Projectiles
{
    public class Projectile : InteractionTrigger
    {
        public event Action<Projectile> Deactivated;
        public event Action<Projectile> Destroying;

        public ProjectileAttackData AttackData { get; private set; }
        
        private Vector3 velocity;
        private float shootTime;

        public void Init(ProjectileAttackData attackData)
        {
            AttackData = attackData;
        }
        
        public void Shoot(Vector2 directionNormalized)
        {
            transform.up = directionNormalized;
            velocity = directionNormalized * AttackData.Speed;
            shootTime = Time.time;
        }
        
        public void OnUpdate()
        {
            transform.position += velocity * Time.deltaTime;

            if (Time.time > shootTime + AttackData.LifeTimeInSec)
            {
                Deactivate();
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);
            HandleCollision(collider);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision.collider);
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