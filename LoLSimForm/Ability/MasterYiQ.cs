using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    class MasterYiQ : Ability
    {
        int[] CDs = { 18, 17, 16, 15, 14 };
        double[] damages = { 25, 60, 95, 130, 165 };

        public override void init()
        {
            CD = CDs[caster.Q_Level-1];
        }

        public MasterYiQ(Champion _caster):base(_caster)
        {
           
        }

        protected override string effectString()
        {
            double idamage = damages[caster.Q_Level - 1] + caster.cAttackNumber;
            double damage = caster.PhysicalDamage(idamage);
            target.cHealth -= damage;
            return damage.ToString("F0") + " damage from Q";
        }
    }
}
