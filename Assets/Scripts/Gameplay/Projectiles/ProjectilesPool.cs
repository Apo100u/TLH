using UnityEngine;
using UnityEngine.Pool;

namespace TLH.Gameplay.Projectiles
{
    public class ProjectilesPool
    {
        private Projectile originalPrefab;
        private ObjectPool<Projectile> pool;

        public ProjectilesPool(Projectile originalPrefab)
        {
            this.originalPrefab = originalPrefab;
            pool = new ObjectPool<Projectile>(CreateNewProjectile, OnProjectileTakenFromPool, OnProjectileReturnedToPool, OnProjectileSurpassedPoolCapacity);
        }

        public Projectile TakeFromPool()
        {
            return pool.Get();
        }

        public void ReturnToPool(Projectile projectile)
        {
            pool.Release(projectile);
        }

        private Projectile CreateNewProjectile()
        {
            return Object.Instantiate(originalPrefab);
        }

        private void OnProjectileTakenFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private void OnProjectileReturnedToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnProjectileSurpassedPoolCapacity(Projectile projectile)
        {
            projectile.Destroy();
        }
    }
}