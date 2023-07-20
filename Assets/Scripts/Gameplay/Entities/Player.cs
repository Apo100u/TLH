using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Entities.Behaviours;
using TLH.Gameplay.Entities.PlayerStateMachine;
using TLH.Input;
using UnityEngine;

namespace TLH.Gameplay.Entities
{
    [RequireComponent(typeof(Movement))]
    public class Player : Entity
    {
        [Header("Default Actions")]
        [SerializeField] private RunData defaultRunData;
        [SerializeField] private DashData defaultDashData;

        private InputReader inputReader;
        private Movement movement;
        private PlayerState currentState;

        public void Init(InputReader inputReader)
        {
            this.inputReader = inputReader;
            SetupBehaviours();
            SetupStateMachine();
        }

        private void SetupBehaviours()
        {
            movement = GetComponent<Movement>();
            movement.SetRunData(defaultRunData);
            movement.SetDashData(defaultDashData);
        }

        private void SetupStateMachine()
        {
            RunState runState = new(ChangeState, movement, inputReader);
            DashState dashState = new(ChangeState, movement, inputReader);

            runState.AddTransition(Command.MobilityAction, dashState);
            dashState.AddTransitionOnDashEnd(runState);

            currentState = runState;
        }

        private void Update()
        {
            UpdateStateMachine();
        }

        private void UpdateStateMachine()
        {
            currentState.Process();

            if (inputReader.GetMobilityAction())
            {
                currentState.ExecuteCommand(Command.MobilityAction);
            }
        }

        private void ChangeState(StateChangeEventArgs args)
        {
            currentState.OnExit();
            currentState = args.NewState;
            currentState.OnEnter();
        }
    }
}