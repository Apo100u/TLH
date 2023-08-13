using System.Collections.Generic;
using TLH.Gameplay.Entities;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.ObjectPools;
using UnityEngine;

namespace TLH.Gameplay.Projectiles
{
    public class ProjectileAttacksManager : MonoBehaviour
    {
        private Entity attacksSource;
        private ProjectilesPool projectilesPool;
        private List<Projectile> activeProjectiles = new();

        public void Init(Entity attacksSource, ProjectilesPool projectilesPool)
        {
            this.attacksSource = attacksSource;
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
            projectile.Shoot(directionNormalized, attacksSource);
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