using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    public class AutoAttack : Ability
    {
        public event EventHandler AAEvent;

        public override void init()
        {
            CD = 1 / caster.iAttackSpeed;
        }

        //TODO:暴击
        protected override string effectString()
        {
            AAEvent(this, EventArgs.Empty);         //发出事件,平A,对于MasterYi通过这个事件来减少QCD

            foreach (Buff buff in caster.Buffs)     //通过这个BUFF来触发E,这么做的坏处是OnHitEffect这个在游戏中统一的概念被分离了,当然可以考虑把这个BUFF写到事件里面去
            {
                if (buff.type == Buff.Type.AutoAttack)
                    buff.effect();
            }

            foreach (Item item in caster.championItems)
            {
                caster.FightLog.AppendText(item.OnHitEffect(target));
                caster.FightLog.AppendText(Environment.NewLine);
            }

            double damage = caster.PhysicalDamage(caster.cAttackNumber);
            target.cHealth -= damage;
            return damage.ToString("F0") + " damage from AutoAttack";
        }
        //易大师的被动太他妈蛋疼了
        //1.不能在那里发送事件,要把事件包在方法力 =>第一行要变
        //2.P的BUFF要求伤害减半,下面的方法倒好写
        //3.由于AA会触发On-hit Buff,因此Buff要调用,但要保证不陷入无限调用循环

        new public void CDR()
        {
            CD = 1 / caster.cAttackSpeed;
        }

        public AutoAttack(Champion _caster) : base(_caster)
        {

        }
    }
}

