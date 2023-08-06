using TLH.Gameplay.Entities.ActionData;
using TLH.Utility.StateMachine;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : EntityBehaviour
    {
        [SerializeField] private new CircleCollider2D collider;
        public CircleCollider2D Collider => collider;

        public RunData RunData { get; private set; }
        public DashData DashData  { get; private set; }
        public Vector2 NormalizedDirection { get; private set; }
        public Vector2 LastNonZeroNormalizedDirection { get; private set; } = Vector2.down;
        
        private StateMachine<MovementCommand, State<MovementCommand>> stateMachine;
        private Rigidbody2D entitiesRigidbody;

        private float remainingDashCooldown;

        protected override void Awake()
        {
            base.Awake();
            entitiesRigidbody = GetComponent<Rigidbody2D>();
            SetupStateMachine();
        }

        private void SetupStateMachine()
        {
            stateMachine = new StateMachine<MovementCommand, State<MovementCommand>>();

            State<MovementCommand> runState = new RunState(this, entitiesRigidbody)
                .AddTransition<DashState>(MovementCommand.Dash, CanPerformDash);

            State<MovementCommand> dashState = new DashState(this, entitiesRigidbody, OnDashEnded)
                .AddTransition<RunState>(MovementCommand.DashEnded);

            stateMachine.AddState(runState);
            stateMachine.AddState(dashState);
            stateMachine.Start(runState);
        }

        public void SetRunData(RunData runData)
        {
            RunData = runData;
        }

        public void SetDashData(DashData dashData)
        {
            DashData = dashData;
        }

        public void UpdateDirection(Vector2 direction)
        {
            NormalizedDirection = direction.normalized;

            if (NormalizedDirection != Vector2.zero)
            {
                LastNonZeroNormalizedDirection = NormalizedDirection;
            }
        }

        public void DemandDash()
        {
            stateMachine.ExecuteCommand(MovementCommand.Dash);
        }

        private void Update()
        {
            stateMachine.Process();
            UpdateCooldowns();
        }

        private void UpdateCooldowns()
        {
            if (remainingDashCooldown > 0)
            {
                remainingDashCooldown -= Time.deltaTime;

                if (remainingDashCooldown < 0)
                {
                    remainingDashCooldown = 0;
                }
            }
        }

        private bool CanPerformDash()
        {
            return remainingDashCooldown <= 0;
        }

        private void OnDashEnded()
        {
            remainingDashCooldown = DashData.Cooldown;
            stateMachine.ExecuteCommand(MovementCommand.DashEnded);
        }
    }
}