using TLH.Gameplay.Entities.ActionData;

namespace TLH.Gameplay.Entities.Behaviours
{
    public class Combat : EntityBehaviour
    {
        private AttackData primaryAttackData;

        public void SetPrimaryAttackData(AttackData primaryAttackData)
        {
            this.primaryAttackData = primaryAttackData;
        }

        public void DemandPrimaryAttack()
        {
        }
    }
}