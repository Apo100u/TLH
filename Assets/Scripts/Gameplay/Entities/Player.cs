using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Entities.Behaviours;
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
        [SerializeField] private AttackData defaultAttackData;

        private Camera mainCamera;
        private InputReader inputReader;
        private Movement movement;
        private Combat combat;

        public void Init(InputReader inputReader, Camera mainCamera)
        {
            this.mainCamera = Camera.main;;
            this.inputReader = inputReader;
            SetupBehaviours();
        }

        private void SetupBehaviours()
        {
            movement = GetComponent<Movement>();
            movement.SetRunData(defaultRunData);
            movement.SetDashData(defaultDashData);

            combat = GetComponent<Combat>();
            combat.SetPrimaryAttackData(defaultAttackData);
        }

        private void Update()
        {
            movement.UpdateDirection(inputReader.GetDirection());
            combat.UpdateAimPoint(inputReader.GetAimWorldPosition(transform.position, mainCamera));

            if (inputReader.GetMobilityActionDown())
            {
                movement.DemandDash();
            }

            if (inputReader.GetPrimaryActionDown())
            {
                combat.DemandPrimaryAttack();
            }
        }
    }
}