using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    class MasterYiE : Ability
    {
        int[] CDs = { 10,9, 8, 7, 6 };

        public override void init()
        {
            CD = CDs[caster.E_Level-1];
        }

        protected override string effectString()
        {
            MasterYiEBuff EBuff = new MasterYiEBuff(caster,target);
            caster.Buffs.Add(EBuff);
            return "Use MasterYiE";
        }

        public MasterYiE(Champion _caster):base(_caster)
        {

        }

        protected override void passiveEffect()
        {
            //base.passiveEffect();
            caster.cAttackNumber = caster.iAttackNumber * 1.1;
        }
    }

    class MasterYiEBuff : Buff
    {
        double[] damages = { 12, 19, 26, 30, 40 };
        
        public MasterYiEBuff(Champion _caster,Champion _target):base(_caster,_target)
        {
            duration = 5;
            name = "E";
            countDown = duration;
            type = Type.AutoAttack;
            //caster.cAttackNumber = caster.iAttackNumber * 1.1;
        }

        public override string effectString()     //实际技能效果的方法
        {
            //把oAtt,bAttackNumber拿出来,E有bonus
            caster.cAttackNumber = caster.iAttackNumber;
            double damage = damages[caster.E_Level-1]+0.25*caster.bAttackNumber;
            target.cHealth -= damage;
            return damage.ToString("F0")+ " damage from EBuff";
        }

        public override void BuffEnd()
        {
            base.BuffEnd();
            caster.cAttackNumber = caster.iAttackNumber * 1.1;
        }
    }
}
