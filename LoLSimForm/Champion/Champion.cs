using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

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
            UpdateTimer = new System.Timers.Timer(0.1 * 1000 / timeLevel);
        }

        protected string defaultAbility = "QWEQQR,QEQER,EEWWR,WW";
        protected string currentAbility;


        //logic
        public Champion Enemy;
        public Form1 form1;

        protected System.Timers.Timer UpdateTimer;
        protected System.Timers.Timer AutoAttackTimer;
        protected System.Timers.Timer QCD_Timer;
        protected System.Timers.Timer QAni_Timer;
        protected System.Timers.Timer WCD_Timer;
        protected System.Timers.Timer ECD_Timer;
        protected System.Timers.Timer RCD_Timer;

        public double timeLevel = 1;
        protected DateTime startTime;



        public void autoAttack()
        {
            AutoAttackTimer = new System.Timers.Timer(1 / this.cAttackSpeed * 1000 / this.timeLevel);
            AutoAttackTimer.Start();
            AutoAttackTimer.Elapsed += new ElapsedEventHandler(OnAutoAttack);
        }

        public event EventHandler eAutoAttack;
        void OnAutoAttack(object sender, ElapsedEventArgs e)
        {
            double damage = cAttackNumber * 100 / (100 + Enemy.cArmor);
            if (Enemy.cHealth > 0)
            {

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
                form1.richTextBox1.AppendText(damage.ToString("F0") + " damage from AutoAttack");
                eAutoAttack(this, EventArgs.Empty);
            }
            else
            {
                Death();
            }
        }

        protected void Death()
        {
            AutoAttackTimer.Stop();
            //QCD_Timer.Stop();
            UpdateTimer.Stop();
            form1.richTextBox1.AppendText(Environment.NewLine);
            form1.richTextBox1.AppendText("Enemy died!");
            form1.richTextBox1.AppendText("Attack last for " + (DateTime.Now - startTime).TotalSeconds * this.timeLevel + " seconds.");
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

    }
}
