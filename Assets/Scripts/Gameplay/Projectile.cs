using TLH.Extensions;
using UnityEngine;

namespace TLH.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        [Tooltip("Layers on which this object will be destroyed. It will pierce other layers and still interact with them if possible.")]
        [SerializeField] private LayerMask layersToDestroyOn;
        [Tooltip("Time in seconds after which this object will be destroyed, if it wasn't already destroyed.")]
        [SerializeField][Min(0f)] private float lifeTimeInSec = 10f;

        private Vector3 velocity;
        private float shootTime;
        
        public void Shoot(Vector2 velocity)
        {
            this.velocity = velocity;
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

        private void OnTriggerEnter2D(Collider2D collider)
        {
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
            // TODO: Return projectile to pool.
        }
    }
}