using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    class Item
    {
        public double iAttackNumber { get; set; }
        public double iAttackSpeed { get; set; }
        public double iCritChance { get; set; }
        public double iPenetrate { get; set; }
        public double iHealth { get; set; }
        public double iArmor { get; set; }
        public double iHealthReg { get; set; }
        public double iMana { get; set; }
        public double iManaReg { get; set; }
        public double iMagicResist { get; set; }
        public double iLifeSteal { get; set; }
        public double iAP { get; set; }
        public double iCDR { get; set; }
        public double iMagPenetrate { get; set; }
        public double iSpellVamp { get; set; }
        public double iCritDmg { get; set; }

        public string itemName { get; set; }
        public double  cost { get; set; }

        public virtual string iAutoAttackPassive(Champion enemy) { return ""; }
    }

    class BladeOfTheRuinedKing:Item
    {
        public BladeOfTheRuinedKing()
        {
            cost = 3400;
            iAttackSpeed = 0.4;
            iAttackNumber = 25;
            iLifeSteal = 0.1;
            //itemName = "BladeOfTheRuinedKing";
        }

        public override string iAutoAttackPassive(Champion enemy)
        {
            double damage = 0.06 * enemy.cHealth * 100 / (100 + enemy.cArmor);
            enemy.cHealth -= damage;
            return damage.ToString("F0") + " damage from BladeOfTheRuinedKing";
        }
    }

    class LongSword:Item
    {
        public LongSword()
        {
            cost = 350;
            iAttackNumber = 10;
        }
    }

    
}
