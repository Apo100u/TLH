using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Entities.Behaviours.Movement;
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

        public void Init(InputReader inputReader)
        {
            this.inputReader = inputReader;
            SetupBehaviours();
        }

        private void SetupBehaviours()
        {
            movement = GetComponent<Movement>();
            movement.SetRunData(defaultRunData);
            movement.SetDashData(defaultDashData);
        }

        private void Update()
        {
            movement.UpdateDirection(inputReader.GetDirection());

            if (inputReader.GetMobilityActionDown())
            {
                movement.DemandDash();
            }
        }
    }
}