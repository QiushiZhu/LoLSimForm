using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoLSimForm
{
    public class Ability
    {
        public Champion caster;
        public Champion target;

        //protected RichTextBox textBox;      //判断显示文本的控件为哪个TextBox

        public double CD;
        public double CDRemained;
        public virtual void init()  //调用时间为修改英雄参数后的初步技能初始化
        {

        }

        public void effect()    //调用时间为CDRemained=0,由AbilityMachine每帧进行判断是否调用
        {
            caster.FightLog.AppendText(effectString());
            caster.FightLog.AppendText(Environment.NewLine);
        }
        
        public void CDR(Champion _target)       //调用时间为开始模拟后的再次技能初始化
        {
            target = _target;
            CD = CD * (1 - caster.cCDR);
        }

        public  Ability(Champion _caster)      //调用时间为修改英雄参数后的初步技能初始化
        {            
            caster = _caster;                  //注意,最尴尬的地方就是确认敌方这一行为只能在Phase 3确认,那好像就有逻辑问题
            
            init();
        }

        protected virtual string effectString()     //实际技能效果的方法
        {
            return "Ability Unoverrided Error";
        }


    }
}
