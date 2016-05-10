using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoLSimForm
{
    public partial class Form1 : Form
    {

        Form2 ChampionSelect = new Form2();
        string currentSelectedChampion;
        string currentSelectedEnemy;
        public bool myChampionSelect;
        public Champion myChampion;
        public Champion enemyChampion;

        int myDefaultLevel = 1;
        int enemyDefaultLevel = 1;


        public Form1()
        {

            InitializeComponent();
            formComponentsComplement();

            //SimTest();
        }




        void formComponentsComplement()
        {
            CheckForIllegalCrossThreadCalls = false;

            //订阅Form2中的点击选择英雄事件
            for (int i = 0; i < ChampionSelect.Controls.Count; i++)
            {
                if (ChampionSelect.Controls[i] is PictureBox)
                {
                    ChampionSelect.Controls[i].Click += championClick;
                }
            }
        }

        void Sim()
        {                        
            myChampion.Sim();
            
            enemyChampion.Sim(); 

        }

        void ChampionKeyInit(bool my)
        {
            string champion = "Null";
            Champion target;
            int defaultLevel;

            if (my)
            {
                champion = currentSelectedChampion;
                defaultLevel = myDefaultLevel;
            }
            else
            {
                champion = currentSelectedEnemy;
                defaultLevel = enemyDefaultLevel;
            }

            switch (champion)
            {
                case "MasterYi":
                    target = new MasterYi(defaultLevel,this);
                    break;
                default:
                    throw new Exception("Select your champion please!");
                case "Ashe":
                    target = new Ashe(defaultLevel,this);
                    break;
            }

            if (my)
            {
                myChampion = target;
                //myChampion.championRole = true;
                myChampion.Change(defaultLevel);
                MyChampionAbilitys.Text = myChampion.defaultAbility.Substring(0, MyChampionLevelNumber.SelectedIndex + 1);
                //myChampion.HealthBarInit();
            }
            else
            {
                enemyChampion = target;
                //enemyChampion.championRole = false;
                enemyChampion.Change(defaultLevel);
                EnemyChampionAbilitys.Text = enemyChampion.defaultAbility.Substring(0, EnemyChampionLevelNumber.SelectedIndex + 1);
                //enemyChampion.HealthBarInit();
            }
        }       //Phase 1
        


        #region championSelect  //点击图片选择英雄
        private void MyChampionIcon_Click(object sender, EventArgs e)
        {
            myChampionSelect = true;
            ChampionSelect.Show();
        }

        private void EnemyChampionIcon_Click(object sender, EventArgs e)
        {
            myChampionSelect = false;
            ChampionSelect.Show();
        }

        void championClick(object sender, EventArgs e)
        {
            if (myChampionSelect)
            {
                MyChampionIcon.Image = ((PictureBox)sender).Image;
                currentSelectedChampion = ((PictureBox)sender).Name.Substring(0, ((PictureBox)sender).Name.Length - 4);
                MyChampionText.Text = currentSelectedChampion;
                ChampionKeyInit(true);
            }
            else
            {
                EnemyChampionIcon.Image = ((PictureBox)sender).Image;
                currentSelectedEnemy = ((PictureBox)sender).Name.Substring(0, ((PictureBox)sender).Name.Length - 4);
                EnemyChampionText.Text = currentSelectedEnemy;
                ChampionKeyInit(false);
            }
            ChampionSelect.Hide();
        }


        #endregion

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyChampionLevelNumber.SelectedIndex = 0;
            EnemyChampionLevelNumber.SelectedIndex = 0;

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void ItemsRadioManual_CheckedChanged(object sender, EventArgs e)
        {

        }

        /*void SimTest()
        {


            MasterYi myMasterYi = new MasterYi(5);
            MasterYi enemyMasterYi = new MasterYi(2);


            myMasterYi.form1 = this;
            myMasterYi.Enemy = enemyMasterYi;
            myMasterYi.ItemPurchase(new BladeOfTheRuinedKing());
            myMasterYi.HealthBar();

            myMasterYi.SimStart();
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            Sim();
        }

        private void MyChampionLevelNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myChampion != null)
            {
               
                myDefaultLevel = MyChampionLevelNumber.SelectedIndex+1;
                myChampion.Change(myDefaultLevel);
                MyChampionAbilitys.Text = myChampion.defaultAbility.Substring(0, MyChampionLevelNumber.SelectedIndex + 1);
            }
        }

        private void EnemyChampionLevelNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myChampion != null)
            {
              
                enemyDefaultLevel = EnemyChampionLevelNumber.SelectedIndex+1;                
                enemyChampion.Change(enemyDefaultLevel);
                EnemyChampionAbilitys.Text = enemyChampion.defaultAbility.Substring(0, EnemyChampionLevelNumber.SelectedIndex + 1);
            }
        }

        private void EnemyChampionCD3_Click(object sender, EventArgs e)
        {

        }
    }
}
