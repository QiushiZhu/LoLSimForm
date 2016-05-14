using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    class MasterYiP : Ability
    {
        
        public override void init()
        {
            type = Type.Passive;
            MasterYiPBuff PBuff = new MasterYiPBuff(caster, target);
            caster.Buffs.Add(PBuff);
        }

        public MasterYiP(Champion _caster):base(_caster)
        {
            
        }

        protected override string effectString()
        {            
            return "Use MasterYiW";
        }

        protected override void passiveEffect()
        {
            //base.passiveEffect();
            
        }
    }

    class MasterYiPBuff:Buff
    {
        public MasterYiPBuff(Champion _caster, Champion _target):base(_caster,_target)
        {            
            type = Type.AutoAttack;
        }

        public override string effectString()     //实际技能效果的方法
        {
                     //为何出现了4个0?,醉了,只能把P和别的Ability彻底分开
            stack++;
            if (stack >= 0 && stack < 3)
            {
                stack++;
                return "";
            }
            if(stack == 3)
            {
                stack++;
                //caster.aa.AAEvent(this, EventArgs.Empty);         //发出事件,平A,对于MasterYi通过这个事件来减少QCD
                foreach (Buff buff in caster.Buffs)     //通过这个BUFF来触发E,这么做的坏处是OnHitEffect这个在游戏中统一的概念被分离了,当然可以考虑把这个BUFF写到事件里面去
                {
                    if (buff.type == Buff.Type.AutoAttack)
                        buff.effect();
                }
                //stack = 0;
                double damage = caster.PhysicalDamage(caster.cAttackNumber);
                target.cHealth -= damage;
                return damage.ToString("F0") + " damage from AutoAttack";
                
            }
            if(stack == 4)
            {
                stack++;
                foreach (Buff buff in caster.Buffs)     //通过这个BUFF来触发E,这么做的坏处是OnHitEffect这个在游戏中统一的概念被分离了,当然可以考虑把这个BUFF写到事件里面去
                {
                    if (buff.type == Buff.Type.AutoAttack)
                        buff.effect();
                }
                double damage = caster.PhysicalDamage(caster.cAttackNumber)/2;
                target.cHealth -= damage/2;
                return damage.ToString("F0") + " damage from P";
            }
            if (stack > 4)
                return "";
            else return "";
            
        }
    }
}
