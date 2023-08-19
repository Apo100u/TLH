using System;
using System.Collections.Generic;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Entities.Attacks;
using UnityEngine.Pool;

namespace TLH.Gameplay.ObjectPools
{
    public class ProjectilesPool
    {
        private Dictionary<ProjectileAttackData, ObjectPool<Projectile>> poolsByAttackData = new();

        public ObjectPool<Projectile> this[ProjectileAttackData projectileAttackData] => poolsByAttackData[projectileAttackData];

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
        
        private Func<Projectile> GetCreateProjectileFunc(ProjectileAttackData attackData)
        {
            return () =>
            {
                Projectile createdProjectile = UnityEngine.Object.Instantiate(attackData.ProjectilePrefab);
                createdProjectile.Init(attackData);
                createdProjectile.Deactivated += OnProjectileDeactivated;

                for (int i = 0; i < attackData.Interactions.Length; i++)
                {
                    createdProjectile.AddInteraction(attackData.Interactions[i]);
                }
                
                return createdProjectile;
            };
        }
        
        private void OnProjectileTakenFromPool(Projectile projectile)
        {
            
        }

        private void OnProjectileReturnedToPool(Projectile projectile)
        {
        }

        private void OnProjectileSurpassedPoolCapacity(Projectile projectile)
        {
            projectile.Destroy();
        }
        
        private void OnProjectileDeactivated(Projectile projectile)
        {
            poolsByAttackData[projectile.AttackData].Release(projectile);
        }
    }
}