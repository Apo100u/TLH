namespace TLH.Gameplay.Entities.PlayerStateMachine
{
    public class StateChangeEventArgs
    {
        public readonly PlayerState NewState;

        public StateChangeEventArgs(PlayerState newState)
        {
            NewState = newState;
        }
    }
}