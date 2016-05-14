using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    class MasterYiR : Ability
    {

        int[] CDs = { 85,85, 85 };
        public override void init()
        {
            CD = CDs[caster.E_Level - 1];
        }

        public MasterYiR(Champion _caster):base(_caster)
        {
          
        }

        protected override string effectString()
        {
            MasterYiRBuff RBuff = new MasterYiRBuff(caster, target);
            caster.Buffs.Add(RBuff);
            return "Use MasterYiR";
        }
    }

    class MasterYiRBuff:Buff
    {
        double[] ASs = { 0.3, 0.55, 0.8 };
        public MasterYiRBuff(Champion _caster, Champion _target):base(_caster,_target)
        {
            duration = 7;
            countDown = duration;
            type = Type.Stats;
            caster.cAttackSpeed = (caster.cAttackSpeed / caster.oAttackSpeed + ASs[caster.R_Level - 1]) * caster.oAttackSpeed;
            caster.aa.CDR();
        }

        public override void BuffEnd()
        {
            base.BuffEnd();
            caster.cAttackSpeed = caster.iAttackSpeed;
            caster.aa.CDR();
        }
    }
}
