using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LoLSimForm
{
    class MasterYi : Champion
    {

        public MasterYi(int level) : base(level)
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

            defaultAbility = "QWEQQRQEQEREEWWRWW";

            q_cd = Q_Ability_CoolDown[Q_Level];
            //coolDownArrangeMent();
        }

        int aaCount = -1;
        double P_Ability_Modifier = 0.5;
        double[] Q_Ability_Cost = { 70, 80, 90, 100, 110 };
        double[] Q_Ability_CoolDown = { 5, 17, 16, 15, 14 };
        double[] Q_Ability_Damage = { 25, 60, 95, 130, 165 };
        double Q_Ability_Bonus = 1;
        double q_damage;
        double q_cd;
        double q_cd_Num;

        int i;

        public void coolDownArrangeMent()
        {
            q_cd_Num = q_cd;
            UpdateTimer.Elapsed += UpdateTimer_Elapsed;
            UpdateTimer.Start();
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Enemy.cHealth < 0)
                Death();
            q_cd_Num = q_cd_Num - 0.1;
            if(q_cd_Num<0 && Q_Level>=0)
            {
                Q_Ablility();
            }
            Console.WriteLine(i++);
            
        }

        public override void P_Ablility()
        {
            eAutoAttack += OnP_Ablility;
        }

        private void OnP_Ablility(object sender, EventArgs e)
        {
            if (aaCount < 3)
            {
                aaCount++;
            }
            if (aaCount == 3)
            {
                double damage = P_Ability_Modifier * this.cAttackNumber * 100 / (100 + Enemy.cArmor);
                

                    Enemy.cHealth -= damage;
                    if (this.championItems != null && this.championItems.Count > 0)
                    {
                        for (int i = 0; i < this.championItems.Count; i++)
                        {
                            form1.richTextBox1.AppendText(Environment.NewLine);
                            form1.richTextBox1.AppendText(championItems[i].iAutoAttackPassive(Enemy));
                        }
                    }

                    form1.richTextBox1.AppendText(Environment.NewLine);
                    form1.richTextBox1.AppendText(damage.ToString("F0") + " damage from Passive");
                
                aaCount = 0;
            }

        }

        public override void Q_Ablility()
        {
            if (Q_Level >= 0)
            {
                q_damage = (Q_Ability_Damage[Q_Level] + Q_Ability_Bonus * cAttackNumber) * 100 / (100 + Enemy.cArmor);
                double cost = Q_Ability_Cost[Q_Level];
                if (q_cd_Num < 0)
                {
                    OnQ_Ability();
                    q_cd_Num = q_cd;
                }

            }
        }

        void OnQ_Ability()
        {
            AutoAttackTimer.Stop();
            Enemy.cHealth -= q_damage;
            form1.richTextBox1.AppendText(Environment.NewLine);
            form1.richTextBox1.AppendText(q_damage.ToString("F0") + " damage from Q_Ability");
            System.Threading.Thread.Sleep((int)(0.25 * 1000 / timeLevel));
            AutoAttackTimer.Start();
        }

    }
}
