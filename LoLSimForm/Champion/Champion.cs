using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Drawing;

/// <summary>
/// Creator:Qiushi Zhu  2016/5/10
/// Instrutor:将方法调用的时间点分为以下几个
/// 1.选择英雄   =>   此时也会以默认等级调用方法2
/// 2.英雄属性变化    
/// 3.模拟开始
/// 4.模拟进行中
/// </summary>







namespace LoLSimForm
{
    public class  Champion
    {
        //input stats
        public int Level { get; set; }
        public string Name { get; set; }
        public int Q_Level { get; set; }
        public int W_Level { get; set; }
        public int E_Level { get; set; }
        public int R_Level { get; set; }
        public string defaultAbility = "QWEQQRQEQEREEWWRWW";
        public string currentAbility;

        //internal reference
        public List<Item> championItems = new List<Item>(6);
        protected List<Ability> Abilities = new List<Ability>(5);
        protected AutoAttack aa;
        protected List<Ability> AbilityTitles = new List<Ability>(5);
        public List<Buff> Buffs = new List<Buff>();
        List<Label> BuffLabels = new List<Label>();
        public bool championRole;

        //Information Control
        PictureBox HealthBar;
        public RichTextBox FightLog;

        //external reference
        public Champion Enemy;
        public Form1 form1;

        //用于搭配CD机,BUFF机的计时器
        const double frameInterval = 0.1;
        const double frameRate = 10;
        int frameCount = 0;
        System.Timers.Timer t = new System.Timers.Timer(1000 * frameInterval);


        //初始输入属性暂存,用于取消BUFF,计算血量等
        //TODO:考虑将属性放在一起作为一个单独的类




        #region stats
        //current offence stats
        //在模拟过程中当前的状态,可以被技能影响,也是实际所调用的状态
        public double cAttackNumber { get; set; }
        public double cAttackSpeed { get; set; }
        public double cCritChance { get; set; }
        public double cCritDmg { get; set; }
        public double cPenetrate { get; set; }
        public double cPenetrateP { get; set; }
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

        //init stats 
        //在计算完英雄的等级,装备后得出
        public double iAttackNumber { get; set; }
        public double iAttackSpeed { get; set; }
        public double iCritChance { get; set; }
        public double iCritDmg { get; set; }
        public double iPenetrate { get; set; }
        public double iPenetrateP { get; set; }
        public double iHealth { get; set; }
        public double iArmor { get; set; }
        public double iHealthReg { get; set; }
        public double iMana { get; set; }
        public double iManaReg { get; set; }
        public double iMagicResist { get; set; }
        public double iLifeSteal { get; set; }
        public double iAP { get; set; }
        public double iCDR { get; set; }
        public double iMagPenetrate { get; set; }
        public double iSpellVamp { get; set; }

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
        protected double bPenetrateP { get; set; }
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

        //单位初始化
        public Champion(int level, Form1 form)       //Phase 1
        {
            form1 = form;
            if (form.myChampionSelect)
                championRole = true;
            else
                championRole = false;
            Level = level;
            controlInit();
        }

        //开始模拟时调用
        public void Sim()                               //Phase 3
        {
            //TODO:在这里确认敌人是能实现的么
            if (championRole)
                Enemy = form1.enemyChampion;
            else
                Enemy = form1.myChampion;
            simStatsInit();
            simAbilityInit();
            TimerInit();
        }

        //对应的控件初始化
        void controlInit()
        {
            if(championRole)
            {
                
                BuffLabels.Add(form1.MyChampionBuffLabel1);
                BuffLabels.Add(form1.MyChampionBuffLabel2);
                BuffLabels.Add(form1.MyChampionBuffLabel3);
                FightLog = form1.richTextBox1;
            }
            else
            {
                
                BuffLabels.Add(form1.EnemyChampionBuffLabel1);
                BuffLabels.Add(form1.EnemyChampionBuffLabel2);
                BuffLabels.Add(form1.EnemyChampionBuffLabel3);
                FightLog = form1.richTextBox2;
            }
        }

        //TODO:Phase 4属性变更,即Buff机制
        public void Change(int level)                   //Phase 2
        {
            Level = level;

            //战斗属性初始化
            statsInit();

            //技能初始化            
            currentAbility = defaultAbility.Substring(0, Level);
            Q_Level = currentAbility.Length - currentAbility.Replace("Q", "").Length;
            W_Level = currentAbility.Length - currentAbility.Replace("W", "").Length;
            E_Level = currentAbility.Length - currentAbility.Replace("E", "").Length;
            R_Level = currentAbility.Length - currentAbility.Replace("R", "").Length;
            AbilityInit();
            Console.WriteLine(level);
            Console.WriteLine(currentAbility);
            Console.WriteLine(Q_Level.ToString() + W_Level.ToString() + E_Level.ToString() + R_Level.ToString());

            //血条初始化
            HealthBarInit();

        }

        //每帧调用
        public void update(object sender, ElapsedEventArgs e)       //Phase 4
        {
            frameCount++;
            AbilityCDMachine();
            BuffMachinde();
            HealthBarUpdate();                   
        }

        public void stop()                                      //Phase X
        {
            t.Stop();
        }


        public void HealthBarInit()                         //Phase 2
        {
            //通过Champion内置的ChampionRole判断敌我英雄,再进一步判断将输出显示到Form中的哪个部分.
            if (championRole)
                HealthBar = form1.MyChampionHealthBar;
            else
                HealthBar = form1.EnemyHealthBar;



            int width = HealthBar.Width;
            int height = HealthBar.Height;


            int blocks = (int)(iHealth / 100);

            Bitmap bar = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bar);
            g.DrawRectangle(new Pen(Color.Black), 0, 0, width, height);
            g.FillRectangle(new SolidBrush(Color.Red), 0, 0, width, height);

            for (int i = 0; i < blocks; i++)
            {
                int anchor = width / blocks * (i + 1);
                g.DrawLine(new Pen(Color.Black), anchor, 0, anchor, height);
            }

            HealthBar.Image = bar;
        }

        public void TimerInit() //开始计时器             Phase 3
        {
            t.Start();
            t.Elapsed += update;
        }

        public virtual void AbilityInit()               //Phase 2
        {

        }

        public void simAbilityInit()                //Phase 3
        {
            for (int i = 0; i < Abilities.Count; i++)
            {
                Abilities[i].CDR(Enemy);
            }
            aa.CDR();
        }




        /// <summary>
        /// AutoAttack
        /// </summary>
        //TODO:Crit Hit (Use Expection or Ramdon?)
        public event EventHandler eAutoAttack;
        public void AutoAttack()                //Phase 4 暂时不用
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

                //eAutoAttack(this, EventArgs.Empty);
            }

        }

        public void HealthBarUpdate()       //Phase 4     该方法存在问题,由于没有销毁上一帧生成的血条,导致内存消耗不断增加
        {

            Bitmap b = new Bitmap(HealthBar.Image);
            Graphics g = Graphics.FromImage(b);
            Rectangle hpLose = new Rectangle((int)(cHealth / iHealth * HealthBar.Width), 0, (int)(HealthBar.Width - cHealth / iHealth * HealthBar.Width), HealthBar.Height);
            g.DrawRectangle(new Pen(Color.Black), hpLose);
            g.FillRectangle(new SolidBrush(Color.Black), hpLose);
            HealthBar.Image = b;
        }

        protected void Death()          //Phase 4
        {
            t.Stop();
            FightLog.AppendText("Enemy died!");
            FightLog.AppendText("Attack last for " + (frameCount / frameRate).ToString("F1") + " seconds.");
            FightLog.AppendText(Environment.NewLine);
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
            statsInit();
        }               //Phase 2

        double statsFormula(double nStats, int level)
        {
            return nStats * (level - 1) * (0.685 + 0.0175 * level);
        }

        public void statsInit()
        {
            iAttackNumber = oAttackNumber + statsFormula(nAttackNumber, this.Level) + bAttackNumber;
            iAttackSpeed = oAttackSpeed * (1 + statsFormula(nAttackSpeed, this.Level) + bAttackSpeed);
            iHealth = oHealth + statsFormula(nHealth, this.Level) + bHealth;
            iArmor = oArmor + statsFormula(nArmor, this.Level) + bArmor;
            iMagicResist = oMagigResist + statsFormula(nMagigResist, this.Level) + bMagicResist;
            iMana = oMana + statsFormula(nMana, this.Level) + bMana;
            iManaReg = oHealthReg + statsFormula(nManaReg, this.Level) + bManaReg;
            iHealthReg = oHealthReg + statsFormula(nHealthReg, this.Level) + bHealthReg;

            iCritChance = bCritChance;
            iCritDmg = bCritDmg;
            iPenetrate = bPenetrate;
            iLifeSteal = bLifeSteal;
            iAP = bAP;
            iCDR = bCDR;
            iMagPenetrate = bMagPenetrate;
            iSpellVamp = bSpellVamp;

            //aa_cd = 1 / cAttackSpeed;
        }      //Phase 2
            
        public void simStatsInit()
        {
            cAttackNumber = iAttackNumber;
            cAttackSpeed = iAttackSpeed;
            cHealth = iHealth;
            cArmor = iArmor;
            cMagicResist = iMagicResist;
            cMana = iMana;
            cManaReg = iManaReg;
            cHealthReg = iHealthReg;

            cCritChance = iCritChance;
            cCritDmg = iCritDmg;
            cPenetrate = iPenetrate;
            cLifeSteal = iLifeSteal;
            cAP = iAP;
            cCDR = iCDR;
            cMagPenetrate = iMagPenetrate;
            cSpellVamp = iSpellVamp;
        }       //Phase 3 

        public void AbilityCDMachine()  //CD机           Phase 4
        {
            for (int i = 0; i < Abilities.Count; i++)
            {
                Abilities[i].CDRemained -= frameInterval;
                if (Abilities[i].CDRemained <= 0)
                {
                    Abilities[i].effect();
                    Abilities[i].CDRemained = Abilities[i].CD;
                }

                //通过Champion内置的ChampionRole判断敌我英雄,再进一步判断将输出显示到Form中的哪个部分.
                if (championRole)
                    form1.Controls["MyChampionCD" + i.ToString()].Text = Abilities[i].CDRemained.ToString("F1");
                else
                    form1.Controls["EnemyChampionCD" + i.ToString()].Text = Abilities[i].CDRemained.ToString("F1");
            }
        }

        public void BuffMachinde()
        {
            for (int i=0;i<Buffs.Count;i++)
            {
                Buffs[i].countDown -= frameInterval;
                BuffLabels[i].Text = Buffs[i].countDown.ToString("F1");
                if(Buffs[i].countDown <=0)
                {
                    Buffs.RemoveAt(i);
                    BuffLabels[i].Text = "";
                }

            }
        }



        public double PhysicalDamage(double damage)
        {
            double fDamage;
            double armor;
            armor = Enemy.cArmor * (1 - cPenetrateP) - cPenetrate;
            fDamage = damage * 100 / (100 + armor);
            return fDamage;
        }

    }
}


