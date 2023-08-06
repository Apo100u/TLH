using System;
using System.Collections.Generic;
using TLH.Gameplay.Entities.ActionData;
using UnityEngine;
using UnityEngine.Pool;

namespace TLH.Gameplay.Projectiles
{
    public class ProjectileAttacksManager : MonoBehaviour
    {
        private Dictionary<ProjectileAttackData, ObjectPool<Projectile>> poolsByAttackData = new();
        private List<Projectile> activeProjectiles = new();

        public void RegisterHandledProjectileAttack(ProjectileAttackData attackData)
        {
            if (!poolsByAttackData.ContainsKey(attackData))
            {
                ObjectPool<Projectile> pool = new(
                    GetCreateProjectileFunc(attackData),
                    OnProjectileTakenFromPool,
                    OnProjectileReturnedToPool,
                    OnProjectileSurpassedPoolCapacity);

                poolsByAttackData.Add(attackData, pool);
            }
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
            Projectile projectile = poolsByAttackData[attackData].Get();
            projectile.transform.position = spawnPoint;
            projectile.Shoot(directionNormalized);
        }
        
        private Func<Projectile> GetCreateProjectileFunc(ProjectileAttackData attackData)
        {
            return () =>
            {
                Projectile createdProjectile = Instantiate(attackData.ProjectilePrefab);
                createdProjectile.Init(attackData);
                createdProjectile.Deactivated += OnProjectileDeactivated;
                createdProjectile.Destroying += OnProjectileDestroying;

                for (int i = 0; i < attackData.Interactions.Length; i++)
                {
                    createdProjectile.AddInteraction(attackData.Interactions[i]);
                }
                
                return createdProjectile;
            };
        }
        
        private void OnProjectileTakenFromPool(Projectile projectile)
        {
            activeProjectiles.Add(projectile);
        }

        private void OnProjectileReturnedToPool(Projectile projectile)
        {
            activeProjectiles.Remove(projectile);
        }

        private void OnProjectileSurpassedPoolCapacity(Projectile projectile)
        {
            projectile.Destroy();
        }

        private void OnProjectileDeactivated(Projectile projectile)
        {
            poolsByAttackData[projectile.AttackData].Release(projectile);
        }
        
        private void OnProjectileDestroying(Projectile projectile)
        {
            activeProjectiles.Remove(projectile);
        }
    }
}