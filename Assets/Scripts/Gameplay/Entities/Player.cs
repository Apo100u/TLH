using TLH.Gameplay.Entities.Actions;
using TLH.Gameplay.Entities.Behaviours;
using TLH.Input;
using UnityEngine;

namespace TLH.Gameplay.Entities
{
    [RequireComponent(typeof(Movement))]
    public class Player : Entity
    {
        private InputReader inputReader;
        private Movement movement;

        [Header("Default Actions")]
        [SerializeField] private Run defaultRun;

        public void Init(InputReader inputReader)
        {
            this.inputReader = inputReader;
        }
        
        private void Awake()
        {
            movement = GetComponent<Movement>();
            movement.SetRun(defaultRun);
        }

        private void Update()
        {
            Vector2 directionInput = inputReader.GetDirection();
            bool mobilityActionInput = inputReader.GetMobilityAction();

            movement.UpdateDirection(directionInput);
        }
    }
}