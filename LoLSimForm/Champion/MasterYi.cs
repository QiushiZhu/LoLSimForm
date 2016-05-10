using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace LoLSimForm
{
    class MasterYi : Champion
    {

        public MasterYi(int level, Form1 form) : base(level, form)
        {
            this.oHealth = 598.56;
            this.oHealthReg = 7.59;
            this.oAttackNumber = 60.04;
            this.oAttackSpeed = 0.679;
            this.oArmor = 24.04;
            this.oMana = 250.56;
            this.oManaReg = 7.255;
            this.oMagigResist = 32.1;

            this.nHealth = 92;
            this.nHealthReg = 0.65;
            this.nAttackNumber = 3;
            this.nAttackSpeed = 0.02;
            this.nArmor = 3;
            this.nMana = 42;
            this.nManaReg = 0.45;
            this.nMagigResist = 1.25;


            statsInit();

            defaultAbility = "QWEQQRQEQEREEWWRWW";

        }

        int aaCount = -1;
        double P_Ability_Modifier = 0.5;

        public override void AbilityInit()          //Phase 2
        {
            Abilities.Clear();
            aa = new AutoAttack(this);
            Abilities.Add(aa);

            if (Q_Level > 0)
                Abilities.Add(new MasterYiQ(this));
            if (W_Level > 0)
                Abilities.Add(new MasterYiW(this));
            if (E_Level > 0)
                Abilities.Add(new MasterYiE(this));
            if (R_Level > 0)
                Abilities.Add(new MasterYiR(this));
        }


        private void OnP_Ability(object sender, EventArgs e)
        {
            if (aaCount < 3)
            {
                aaCount++;
            }
            if (aaCount == 3)
            {
                double damage = P_Ability_Modifier * this.cAttackNumber * 100 / (100 + Enemy.cArmor);

                Enemy.cHealth -= damage;

                form1.richTextBox1.AppendText(damage.ToString("F0") + " damage from Passive");
                form1.richTextBox1.AppendText(Environment.NewLine);
                if (this.championItems != null && this.championItems.Count > 0)
                {
                    for (int i = 0; i < this.championItems.Count; i++)
                    {
                        form1.richTextBox1.AppendText(championItems[i].iAutoAttackPassive(Enemy));
                        form1.richTextBox1.AppendText(Environment.NewLine);
                    }
                }
                aaCount = 0;
            }
        }

    }


}



