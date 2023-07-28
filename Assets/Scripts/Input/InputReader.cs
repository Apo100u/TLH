using UnityEngine;

namespace TLH.Input
{
    public abstract class InputReader
    {
        public abstract Vector2 GetDirection();
        public abstract bool GetMobilityActionDown();
        public abstract bool GetPrimaryActionDown();
    }
}