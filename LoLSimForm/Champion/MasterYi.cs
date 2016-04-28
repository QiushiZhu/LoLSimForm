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

            QType = AbilityType.Oneshot;
            WType = AbilityType.Passive;
            EType = AbilityType.Buff;
            RType = AbilityType.Buff;

            Q_Ability_CoolDown = new double[] { 18, 17, 16, 15, 14 };
            E_Ability_CoolDown = new double[] { 18, 17, 16, 15, 14 };
            R_Ability_CoolDown = new double[] { 85, 85, 85 };

            UpdateStats();

            defaultAbility = "QWEQQR,QEQER,EEWWR,WW";

            e_duration = 5 * frameRate;
            r_duration = 7 * frameRate;



        }

        int aaCount = -1;
        double P_Ability_Modifier = 0.5;

        double[] Q_Ability_Cost = { 70, 80, 90, 100, 110 };
        //double[] Q_Ability_CoolDown = { 18, 17, 16, 15, 14 };
        double[] Q_Ability_Damage = { 25, 60, 95, 130, 165 };
        double Q_Ability_Bonus = 1;

        //double[] E_Ability_CoolDown = { 18, 17, 16, 15, 14 };
        double[] E_Ability_Damage = { 12, 19, 26, 33, 40 };
        double E_Ability_Bonus = 0.25;

        //double[] R_Ability_CoolDown = { 85, 85, 85 };
        double[] R_Ability_AS = { 0.3, 0.5, 0.8 };






        int i;


        double currentAD;
        double currentAS;

        public override void SimStart()
        {
            currentAD = cAttackNumber;
            currentAS = cAttackNumber;
            q_cd_Num = 0;
            e_cd_Num = 0;
            r_cd_Num = 85;
            aa_cd_Num = aa_cd;

            P_Ability();

            UpdateTimer.Elapsed += UpdateTimer_Elapsed;
            UpdateTimer.Start();
        }

        protected override void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            base.UpdateTimer_Elapsed(sender, e);
        }



        public override void P_Ability()
        {
            eAutoAttack += OnP_Ability;
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
                if (E_Level >= 0)
                    OnE_Ability(this, EventArgs.Empty);
                aaCount = 0;
            }
            q_cd_Num -= 1;
        }

        public override void Q_Ability()
        {
            double cost = Q_Ability_Cost[Q_Level];
            OnQ_Ability();
        }

        void OnQ_Ability()
        {
            double q_damage = (Q_Ability_Damage[Q_Level] + Q_Ability_Bonus * cAttackNumber) * 100 / (100 + Enemy.cArmor);
            Enemy.cHealth -= q_damage;

            form1.richTextBox1.AppendText(q_damage.ToString("F0") + " damage from Q_Ability");
            form1.richTextBox1.AppendText(Environment.NewLine);
        }



        public override void W_Ability()
        {
            base.W_Ability();
        }

        public override void E_Ability()
        {
            e_startTime = frameCount;
            eAutoAttack += OnE_Ability;
        }

        void OnE_Ability(object sender, EventArgs e)
        {
            if (frameCount - e_startTime < e_duration)
            {
                cAttackNumber = currentAD;
                double e_damage = E_Ability_Damage[E_Level] + E_Ability_Bonus * bAttackNumber;
                Enemy.cHealth -= e_damage;

                form1.richTextBox1.AppendText(e_damage.ToString("F0") + " damage from E_Ability");
                form1.richTextBox1.AppendText(Environment.NewLine);
            }
        }

        public override void E_AbilityCancel()
        {
            base.E_AbilityCancel();
            cAttackNumber = currentAD * 1.1;
        }

        public override void R_Ability()
        {
            r_startTime = frameCount;
        }

        void OnR_Ability()
        {
            //TODO:Complete this ability, AS stats update
            cAttackSpeed = (currentAS / oAttackSpeed + R_Ability_AS[R_Level]) * oAttackSpeed;
        }

        public override void R_AbilityCancel()
        {
            base.R_AbilityCancel();
            cAttackSpeed = currentAS;
        }
    }
}
