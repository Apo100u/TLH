using TLH.Gameplay.Entities.ActionData;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours.Combat
{
    public class AttackInfo
    {
        public AttackData Data { get; }
        public Transform[] SpawnPoints { get; }
        public float RemainingCooldown { get; set; }

        private int currentSpawnPointIndex = 0;

        public AttackInfo(AttackData data, params Transform[] spawnPoints)
        {
            Data = data;
            SpawnPoints = spawnPoints;
        }

        public Transform MoveNextSpawnPoint()
        {
            Transform spawnPoint = SpawnPoints[currentSpawnPointIndex];

            currentSpawnPointIndex++;

            if (currentSpawnPointIndex >= SpawnPoints.Length)
            {
                currentSpawnPointIndex = 0;
            }
            
            return spawnPoint;
        }
    }
}