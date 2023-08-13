using System.Collections.Generic;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.ObjectPools;
using UnityEngine;

namespace TLH.Gameplay.Projectiles
{
    public class ProjectileAttacksManager : MonoBehaviour
    {
        private LayerMask attacksSourceLayer;
        private ProjectilesPool projectilesPool;
        private List<Projectile> activeProjectiles = new();

        public void Init(LayerMask attacksSourceLayer, ProjectilesPool projectilesPool)
        {
            this.attacksSourceLayer = attacksSourceLayer;
            this.projectilesPool = projectilesPool;
        }

        private void Update()
        {
            for (int i = activeProjectiles.Count - 1; i >= 0; i--)
            {
                activeProjectiles[i].OnUpdate();
            }
        }
        
        public void ShootProjectileFromAttackData(ProjectileAttackData attackData, Vector3 spawnPoint, Vector2 directionNormalized)
        {
            Projectile projectile = projectilesPool[attackData].Get();
            
            projectile.Deactivated += OnProjectileDeactivated;
            projectile.Destroying += OnProjectileDestroying;
            
            activeProjectiles.Add(projectile);
            projectile.transform.position = spawnPoint;
            projectile.Shoot(directionNormalized, attacksSourceLayer);
        }

        private void OnProjectileDeactivated(Projectile projectile)
        {
            projectile.Deactivated -= OnProjectileDeactivated;
            activeProjectiles.Remove(projectile);
        }

        private void OnProjectileDestroying(Projectile projectile)
        {
            projectile.Destroying -= OnProjectileDestroying;
            activeProjectiles.Remove(projectile);
        }
    }
}