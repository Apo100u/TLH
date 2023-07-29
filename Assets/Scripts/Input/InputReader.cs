using UnityEngine;

namespace TLH.Input
{
    public abstract class InputReader
    {
        public abstract Vector2 GetDirection();
        public abstract Vector3 GetAimWorldPosition(Vector3 playerPosition, Camera camera);
        public abstract bool GetMobilityActionDown();
        public abstract bool GetPrimaryActionDown();
    }
}