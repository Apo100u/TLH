using TLH.Gameplay.Entities.ActionData;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : EntityBehaviour
    {
        private Rigidbody2D entitiesRigidbody;
        
        private RunData runData;
        // private DashData dashData; // TODO

        protected override void Awake()
        {
            base.Awake();
            entitiesRigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetRunData(RunData runData)
        {
            this.runData = runData;
        }

        public void SetDashData(/*DashData dashData*/)
        {
            // this.dashData = dashData;
        }
        
        public void PerformRun(Vector2 direction)
        {
            entitiesRigidbody.velocity = direction.normalized * runData.Speed;
        }
    }
}