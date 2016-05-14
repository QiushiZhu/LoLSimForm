using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    public class Item
    {
        public double bAttackNumber { get; set; }
        public double bAttackSpeed { get; set; }
        public double bCritChance { get; set; }
        public double bPenetrate { get; set; }
        public double bHealth { get; set; }
        public double bArmor { get; set; }
        public double bHealthReg { get; set; }
        public double bMana { get; set; }
        public double bManaReg { get; set; }
        public double bMagicResist { get; set; }
        public double bLifeSteal { get; set; }
        public double bAP { get; set; }
        public double bCDR { get; set; }
        public double bMagPenetrate { get; set; }
        public double bSpellVamp { get; set; }
        public double bCritDmg { get; set; }

        public string itemName { get; set; }
        public double  cost { get; set; }


        //TODO:Unique Oneshot Active
        public virtual string OnHitEffect(Champion enemy) { return ""; }
    }

    class BladeOfTheRuinedKing:Item
    {
        public BladeOfTheRuinedKing()
        {
            cost = 3400;
            bAttackSpeed = 0.4;
            bAttackNumber = 25;
            bLifeSteal = 0.1;
            //itemName = "BladeOfTheRuinedKing";
        }

        public override string OnHitEffect(Champion enemy)
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
            bAttackNumber = 10;
        }
    }

    class BFSword:Item
    {
        public BFSword()
        {
            cost = 1300;
            bAttackNumber = 40;
        }
    }

    class  CaufieldsWarhammer:Item
    {
        public CaufieldsWarhammer()
        {
            cost = 1100;
            bAttackNumber = 25;
            bCDR = 0.1;
        }
    }

    class ChainVset:Item
    {
        public ChainVset()
        {
            cost = 800;
            bArmor = 40;
        }
    }

    class ClothArmor:Item
    {
        public ClothArmor()
        {
            cost = 300;
            bArmor = 15;
        }
    }

    class CrystallineBracer:Item
    {
        public CrystallineBracer()
        {
            cost = 200;
            bHealthReg = 0.5;
        }
    }

    class Dagger:Item
    {
        public Dagger()
        {
            cost = 300;
            bAttackSpeed = 0.12;
        }
    }

    class DeadMansPlate:Item
    {
        public DeadMansPlate()
        {
            cost = 2900;
            bArmor = 50;
        }

        //TODO:Passive
    }
    
    class DeathsDance:Item
    {
        public DeathsDance()
        {
            cost = 3500;
            bAttackNumber = 75;
            bCDR = 0.1;
        }

        //TODO:Passive LifeSteal
    }

    class DoransBlade:Item
    {
        public DoransBlade()
        {
            cost = 450;
            bAttackNumber = 8;
            bHealth = 80;
            bLifeSteal = 0.03;
        }
    }

    class Entropy:Item
    {
        public Entropy()
        {
            cost = 2600;
            bAttackNumber = 55;
            bHealth = 275;
        }

        //TODO:Active
    }

    class EssenceReaver:Item
    {
        public EssenceReaver()
        {
            cost = 3600;
            bAttackNumber = 65;
            bCritChance = 0.2;
            bCDR = 0.1;
        }

        //TODO:Permenant Passive
    }

    class FrozenMallet:Item
    {
        public FrozenMallet()
        {
            cost = 3100;
            bHealth = 650;
            bAttackNumber = 40;
        }
    }

    class GiantsBelt:Item
    {
        public GiantsBelt()
        {
            cost = 1000;
            bHealth = 380;
        }
    }
}
