using UnityEngine;

namespace TLH.Input
{
    public class TestInputReader : InputReader
    {
        public override Vector2 GetDirection()
        {
            Vector2 direction = Vector2.zero;

            if (UnityEngine.Input.GetKey(KeyCode.W)) direction.y += 1;
            if (UnityEngine.Input.GetKey(KeyCode.S)) direction.y -= 1;
            if (UnityEngine.Input.GetKey(KeyCode.D)) direction.x += 1;
            if (UnityEngine.Input.GetKey(KeyCode.A)) direction.x -= 1;
            
            return direction;
        }

        public override bool GetMobilityActionDown()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space);
        }
    }
}