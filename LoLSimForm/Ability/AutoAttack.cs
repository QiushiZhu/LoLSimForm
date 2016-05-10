using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    public class AutoAttack : Ability
    {
        public override void init()
        {
            CD = 1 / caster.iAttackSpeed;
        }

        protected override string effectString()
        {
            double damage = caster.PhysicalDamage(caster.cAttackNumber);
            target.cHealth -= damage;
            foreach (Buff buff in caster.Buffs)
            {
                if (buff.type == Buff.Type.AutoAttack)
                    buff.effect();
            }
            return damage.ToString("F0") + " damage from AutoAttack";
        }

        new public void CDR()
        {
            CD = 1 / caster.cAttackSpeed;
        }

        public AutoAttack(Champion _caster) : base(_caster)
        {

        }
    }
}

