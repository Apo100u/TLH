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

        public override Vector3 GetAimWorldPosition(Vector3 playerPosition, Camera camera)
        {
            Vector3 aimWorldPosition = camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            aimWorldPosition.z = playerPosition.z;
            
            return aimWorldPosition;
        }

        public override bool GetMobilityActionDown()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space);
        }

        public override bool GetPrimaryActionDown()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);
        }
    }
}