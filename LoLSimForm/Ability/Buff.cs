using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    public class Buff
    {
        public int duration;
        public string name;
        public double countDown;
        public int stack;
        
        public Champion caster;
        public Champion target;

        public enum Type { AutoAttack,Stats,Permenant}
        public Type type;

        public Buff(Champion _caster,Champion _target)
        {
            caster = _caster;
            target = _target;
        }

        public void effect()    //调用时间为CDRemained=0,由AbilityMachine每帧进行判断是否调用
        {
            caster.FightLog.AppendText(effectString());      //需要多一个参数来判断是否显示文本
            caster.FightLog.AppendText(Environment.NewLine);
        }

        public virtual string effectString()     //实际技能效果的方法
        {
            return "Buff Unoverrided Error";
        }

        public virtual void BuffEnd()
        {

        }

    }
}
