using TLH.Gameplay.Entities.Actions;
using TLH.Gameplay.Entities.Behaviours;
using TLH.Input;
using UnityEngine;

namespace TLH.Gameplay.Entities
{
    [RequireComponent(typeof(Movement))]
    public class Player : Entity
    {
        [Header("Default Actions")]
        [SerializeField] private Run defaultRun;

        private InputReader inputReader;
        private Movement movement;

        public void Init(InputReader inputReader)
        {
            this.inputReader = inputReader;
        }
        
        private void Awake()
        {
            movement = GetComponent<Movement>();
            movement.SetRun(defaultRun);
        }
    }
}