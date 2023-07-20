using System;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours
{
    public class DashInfo
    {
        public readonly float SqrDistanceToTravel;
        public readonly Vector2 Direction;
        public readonly Vector3 StartPosition;
        public Action EndCallback;

        public DashInfo(float sqrDistanceToTravel, Vector2 direction, Vector3 startPosition, Action endCallback)
        {
            SqrDistanceToTravel = sqrDistanceToTravel;
            Direction = direction;
            StartPosition = startPosition;
            EndCallback = endCallback;
        }
    }
}