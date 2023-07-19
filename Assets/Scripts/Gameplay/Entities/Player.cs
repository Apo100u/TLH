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
        }

        private void SetupStateMachine()
        {
            RunState runState = new(ChangeState, movement, inputReader);
            MobilityActionState mobilityActionState = new(ChangeState);

            runState.AddTransition(Command.MobilityAction, mobilityActionState);
            mobilityActionState.AddTransitionOnActionEnded(runState);

            currentState = runState;
        }

        private void Update()
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