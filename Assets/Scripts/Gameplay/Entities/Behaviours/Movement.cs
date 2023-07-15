using TLH.Gameplay.Entities.Actions;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : EntityBehaviour
    {
        private Rigidbody2D entitiesRigidbody;
        private Vector2 direction;
        
        private Run run;
        private Dash dash;

        protected override void Awake()
        {
            base.Awake();
            entitiesRigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetRun(Run run)
        {
            this.run = run;
        }

        public void SetDash(Dash dash)
        {
            this.dash = dash;
        }
        
        public void UpdateDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        private void Update()
        {
            entitiesRigidbody.velocity = direction.normalized * run.Speed;
        }
    }
}