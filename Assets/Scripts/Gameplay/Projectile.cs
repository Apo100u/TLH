using TLH.Extensions;
using TLH.Gameplay.Interactions;
using UnityEngine;

namespace TLH.Gameplay
{
    public class Projectile : InteractionTrigger
    {
        private Vector3 velocity;
        private float lifeTimeInSec;
        private float shootTime;
        private LayerMask layersToDestroyOn;
        
        public void Shoot(Vector2 velocity, float lifeTimeInSec, LayerMask layersToDestroyOn)
        {
            this.velocity = velocity;
            this.lifeTimeInSec = lifeTimeInSec;
            this.layersToDestroyOn = layersToDestroyOn;
            shootTime = Time.time;
        }
        
        public void OnUpdate()
        {
            transform.position += velocity * Time.deltaTime;

            if (Time.time > shootTime + lifeTimeInSec)
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
            if (layersToDestroyOn.ContainLayer(collider.gameObject.layer))
            {
                Deactivate();
            }
        }

        private void Deactivate()
        {
            velocity = Vector2.zero;
            // TODO: Return projectile to pool.
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}