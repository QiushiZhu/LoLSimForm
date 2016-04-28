using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Drawing;

namespace LoLSimForm
{
    class Champion
    {
        //input stats
        public int Level { get; set; }
        public string Name { get; set; }
        protected int Q_Level { get; set; }
        protected int W_Level { get; set; }
        protected int E_Level { get; set; }
        protected int R_Level { get; set; }

        //items
        public List<Item> championItems = new List<Item>(6);


        #region stats
        //current offence stats
        public double cAttackNumber { get; set; }
        public double cAttackSpeed { get; set; }
        public double cCritChance { get; set; }
        public double cCritDmg { get; set; }
        public double cPenetrate { get; set; }
        public double cHealth { get; set; }
        public double cArmor { get; set; }
        public double cHealthReg { get; set; }
        public double cMana { get; set; }
        public double cManaReg { get; set; }
        public double cMagicResist { get; set; }
        public double cLifeSteal { get; set; }
        public double cAP { get; set; }
        public double cCDR { get; set; }
        public double cMagPenetrate { get; set; }
        public double cSpellVamp { get; set; }

        //level0 stats
        protected double oAttackNumber { get; set; }
        protected double oAttackSpeed { get; set; }
        protected double oHealth { get; set; }
        protected double oArmor { get; set; }
        protected double oHealthReg { get; set; }
        protected double oMana { get; set; }
        protected double oManaReg { get; set; }
        protected double oMagigResist { get; set; }

        //leveln stats
        protected double nAttackNumber { get; set; }
        protected double nAttackSpeed { get; set; }
        protected double nHealth { get; set; }
        protected double nArmor { get; set; }
        protected double nHealthReg { get; set; }
        protected double nMana { get; set; }
        protected double nManaReg { get; set; }
        protected double nMagigResist { get; set; }

        //bonus stats
        protected double bAttackNumber { get; set; }
        protected double bAttackSpeed { get; set; }
        protected double bCritChance { get; set; }
        protected double bCritDmg { get; set; }
        protected double bPenetrate { get; set; }
        protected double bHealth { get; set; }
        protected double bArmor { get; set; }
        protected double bHealthReg { get; set; }
        protected double bMana { get; set; }
        protected double bManaReg { get; set; }
        protected double bMagicResist { get; set; }
        protected double bLifeSteal { get; set; }
        protected double bAP { get; set; }
        protected double bCDR { get; set; }
        protected double bMagPenetrate { get; set; }
        protected double bSpellVamp { get; set; }
        #endregion

        protected Champion(int level)
        {

            Level = level;
            currentAbility = defaultAbility.Substring(0, Level);
            Q_Level = currentAbility.Length - currentAbility.Replace("Q", "").Length - 1;
            W_Level = currentAbility.Length - currentAbility.Replace("W", "").Length - 1;
            E_Level = W_Level = currentAbility.Length - currentAbility.Replace("E", "").Length - 1;
            R_Level = W_Level = currentAbility.Length - currentAbility.Replace("R", "").Length - 1;
            UpdateTimer = new System.Timers.Timer(1000 / frameRate);

        }

        protected string defaultAbility = "QWEQQR,QEQER,EEWWR,WW";
        protected string currentAbility;
        public const double frameRate = 10;
        protected int frameCount = 0;

        double enemyTotalHealth;

        //logic
        public Champion Enemy;
        public Form1 form1;

        protected System.Timers.Timer UpdateTimer;

        //HealtnBarInit
        public void HealthBar()
        {
            int width = form1.EnemyHealthBar.Width;
            int height = form1.EnemyHealthBar.Height;

            enemyTotalHealth = Enemy.cHealth;
            int blocks = (int)(enemyTotalHealth / 100);

            Bitmap bar = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bar);
            g.DrawRectangle(new Pen(Color.Black), 0, 0, width, height);
            g.FillRectangle(new SolidBrush(Color.Red), 0, 0, width, height);

            for (int i = 0; i < blocks; i++)
            {
                int anchor = width / blocks * (i + 1);
                g.DrawLine(new Pen(Color.Black), anchor, 0, anchor, height);
            }

            form1.EnemyHealthBar.Image = bar;
        }


        /// <summary>
        /// AutoAttack
        /// </summary>
        //TODO:Crit Hit (Use Expection or Ramdon?)
        public event EventHandler eAutoAttack;
        protected void AutoAttack()
        {
            double damage = cAttackNumber * 100 / (100 + Enemy.cArmor);
            if (Enemy.cHealth > 0)
            {

                Enemy.cHealth -= damage;
                form1.richTextBox1.AppendText(damage.ToString("F0") + " damage from AutoAttack");
                form1.richTextBox1.AppendText(Environment.NewLine);
                if (this.championItems != null && this.championItems.Count > 0)
                {
                    for (int i = 0; i < this.championItems.Count; i++)
                    {
                        form1.richTextBox1.AppendText(championItems[i].iAutoAttackPassive(Enemy));
                        form1.richTextBox1.AppendText(Environment.NewLine);
                    }
                }

                eAutoAttack(this, EventArgs.Empty);
            }

        }

        /// <summary>
        /// AbilityCD
        /// </summary>
        protected double q_cd;
        protected double q_cd_Num;
        protected double q_startTime;
        protected double q_duration;
        protected double[] Q_Ability_CoolDown = new double[5];

        protected double w_cd;
        protected double w_cd_Num;
        protected double w_startTime;
        protected double w_duration;
        protected double[] W_Ability_CoolDown = new double[5];

        protected double e_cd;
        protected double e_cd_Num;
        protected double e_startTime;
        protected double e_duration;
        protected double[] E_Ability_CoolDown = new double[5];

        protected double r_cd;
        protected double r_cd_Num;
        protected double r_startTime;
        protected double r_duration;
        protected double[] R_Ability_CoolDown = new double[3];

        protected double aa_cd;
        protected double aa_cd_Num;

        protected enum AbilityType { Oneshot, Passive, Buff, Other }
        protected AbilityType QType;
        protected AbilityType WType;
        protected AbilityType EType;
        protected AbilityType RType;

        public virtual void SimStart()
        {

        }

        protected virtual void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Enemy.cHealth < 0)
                Death();

            aa_cd_Num -= 1 / frameRate;
            form1.MyChampionAACD.Text = aa_cd_Num.ToString("F1");
            if (aa_cd_Num <= 0)
            {
                AutoAttack();
                aa_cd_Num = aa_cd;
            }


            if (Q_Level >= 0)
            {
                q_cd_Num -= 1 / frameRate;
                form1.MyChampionQCD.Text = q_cd_Num.ToString("F1");
                if (q_cd_Num <= 0)
                {
                    Q_Ability();
                    q_cd_Num = q_cd;
                }
                if (QType == AbilityType.Buff && frameCount - q_startTime > q_duration)
                {
                    Q_AbilityCancel();
                }
            }

            if (W_Level >= 0)
            {
                w_cd_Num -= 1 / frameRate;
                form1.MyChampionWCD.Text = w_cd_Num.ToString("F1");
                if (w_cd_Num <= 0)
                {
                    W_Ability();
                    w_cd_Num = e_cd;
                }
                if (WType == AbilityType.Buff && frameCount - w_startTime > w_duration)
                {
                    W_AbilityCancel();
                }
            }

            if (E_Level >= 0)
            {
                e_cd_Num -= 1 / frameRate;
                form1.MyChampionECD.Text = e_cd_Num.ToString("F1");
                if (e_cd_Num <= 0)
                {
                    E_Ability();
                    e_cd_Num = e_cd;
                }
                if (EType == AbilityType.Buff && frameCount - e_startTime > e_duration)
                {
                    E_AbilityCancel();
                }
            }

            if (R_Level >= 0)
            {
                q_cd_Num -= 1 / frameRate;
                form1.MyChampionRCD.Text = r_cd_Num.ToString("F1");
                if (r_cd_Num <= 0)
                {
                    R_Ability();
                    r_cd_Num = r_cd;
                }
                if (RType == AbilityType.Buff && frameCount - r_startTime > r_duration)
                {
                    R_AbilityCancel();
                }
            }

            //Update Health Bar

            Bitmap b = new Bitmap(form1.EnemyHealthBar.Image);
            Graphics g = Graphics.FromImage(b);
            Rectangle hpLose = new Rectangle((int)(Enemy.cHealth / enemyTotalHealth * form1.EnemyHealthBar.Width), 0, (int)(form1.EnemyHealthBar.Width - Enemy.cHealth / enemyTotalHealth * form1.EnemyHealthBar.Width), form1.EnemyHealthBar.Height);
            g.DrawRectangle(new Pen(Color.Black), hpLose);
            g.FillRectangle(new SolidBrush(Color.Black), hpLose);
            form1.EnemyHealthBar.Image = b;
            frameCount++;
            Console.WriteLine(frameCount);

        }

        protected void Death()
        {
            UpdateTimer.Stop();
            form1.richTextBox1.AppendText("Enemy died!");
            form1.richTextBox1.AppendText("Attack last for " + (frameCount / frameRate).ToString("F1") + " seconds.");
            form1.richTextBox1.AppendText(Environment.NewLine);
        }


        public void LevelUp()
        {
            Level++;
        }

        public void ItemPurchase(Item item)
        {
            bAttackNumber += item.iAttackNumber;
            bAttackSpeed += item.iAttackSpeed;
            bArmor += item.iArmor;
            bHealth += item.iHealth;
            bCritChance += item.iCritChance;
            bPenetrate += item.iPenetrate;
            championItems.Add(item);
            UpdateStats();
        }

        double statsFormula(double nStats, int level)
        {
            return nStats * (level - 1) * (0.685 + 0.0175 * level);
        }

        public void UpdateStats()
        {
            cAttackNumber = oAttackNumber + statsFormula(nAttackNumber, this.Level) + bAttackNumber;
            cAttackSpeed = oAttackSpeed * (1 + statsFormula(nAttackSpeed, this.Level) + bAttackSpeed);
            cHealth = oHealth + statsFormula(nHealth, this.Level) + bHealth;
            cArmor = oArmor + statsFormula(nArmor, this.Level) + bArmor;
            cMagicResist = oMagigResist + statsFormula(nMagigResist, this.Level) + bMagicResist;
            cMana = oMana + statsFormula(nMana, this.Level) + bMana;
            cManaReg = oHealthReg + statsFormula(nManaReg, this.Level) + bManaReg;
            cHealthReg = oHealthReg + statsFormula(nHealthReg, this.Level) + bHealthReg;

            cCritChance = bCritChance;
            cCritDmg = bCritDmg;
            cPenetrate = bPenetrate;
            cLifeSteal = bLifeSteal;
            cAP = bAP;
            cCDR = bCDR;
            cMagPenetrate = bMagPenetrate;
            cSpellVamp = bSpellVamp;

            aa_cd = 1 / cAttackSpeed;
            if (Q_Level >= 0 && QType != AbilityType.Passive)
                q_cd = Q_Ability_CoolDown[Q_Level];
            if (W_Level >= 0 && WType != AbilityType.Passive)
                w_cd = W_Ability_CoolDown[W_Level];
            if (E_Level >= 0 && EType != AbilityType.Passive)
                e_cd = E_Ability_CoolDown[E_Level];
            if (R_Level >= 0 && QType != AbilityType.Passive)
                r_cd = R_Ability_CoolDown[R_Level];
            

        }

        public virtual void P_Ability()
        {

        }
        public virtual void Q_Ability()
        {

        }
        public virtual void W_Ability()
        {

        }
        public virtual void E_Ability()
        {

        }
        public virtual void R_Ability()
        {

        }

        public virtual void Q_AbilityCancel()
        {

        }
        public virtual void W_AbilityCancel()
        {

        }
        public virtual void E_AbilityCancel()
        {

        }
        public virtual void R_AbilityCancel()
        {

        }

    }
}
