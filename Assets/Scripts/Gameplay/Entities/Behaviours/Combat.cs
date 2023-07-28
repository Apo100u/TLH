namespace TLH.Gameplay.Entities.Behaviours
{
    public class Combat : EntityBehaviour
    {
        private AttackData attackData;

        public void SetAttackData(AttackData attackData)
        {
            this.attackData = attackData;
        }
    }
}