using System.Collections.Generic;
using TLH.Gameplay.Entities.ActionData;
using UnityEngine;

namespace TLH.Gameplay.Projectiles
{
    public class ProjectilesManager : MonoBehaviour
    {
        private Dictionary<Projectile, ProjectilesPool> poolsByPrefabs = new();
        private List<Projectile> activeProjectiles = new();

        public void ShootProjectileFromAttackData(ProjectileAttackData attackData)
        {
            if (!poolsByPrefabs.ContainsKey(attackData.ProjectilePrefab))
            {
                poolsByPrefabs.Add(attackData.ProjectilePrefab, new ProjectilesPool(attackData.ProjectilePrefab));
            }

            Projectile projectile = poolsByPrefabs[attackData.ProjectilePrefab].TakeFromPool();
            
            projectile.Shoot(Vector2.up, attackData.LifeTimeInSec, attackData.LayersToDestroyOn);
            activeProjectiles.Add(projectile);
            
            projectile.Deactivated += OnProjectileDeactivated;
        }

        private void OnProjectileDeactivated(Projectile projectile)
        {
            activeProjectiles.Remove(projectile);
        }

        private void Update()
        {
            for (int i = 0; i < activeProjectiles.Count; i++)
            {
                activeProjectiles[i].OnUpdate();
            }
        }
    }
}