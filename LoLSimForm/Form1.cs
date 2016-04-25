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
        bool myChampionSelect;



        public Form1()
        {
            
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            //订阅Form2中的点击选择英雄事件
            for (int i = 1; i < ChampionSelect.Controls.Count; i++)
            {
                if (ChampionSelect.Controls[i] is PictureBox)
                {
                    ChampionSelect.Controls[i].Click += championClick;
                }
            }

            MasterYi myMasterYi = new MasterYi(1);
            
            myMasterYi.UpdateStats();            
            MasterYi enemyMasterYi = new MasterYi(1);
            
            enemyMasterYi.UpdateStats();
            myMasterYi.form1 = this;
            myMasterYi.Enemy = enemyMasterYi;
            //myMasterYi.ItemPurchase(new BladeOfTheRuinedKing());

            myMasterYi.coolDownArrangeMent();
            myMasterYi.P_Ablility();
            myMasterYi.Q_Ablility();
            myMasterYi.autoAttack();
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

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void ItemsRadioManual_CheckedChanged(object sender, EventArgs e)
        {

        }

        //有没有办法简化代码,让所有pictureboxclick点击所要写的代码尽量少
        #region championSelect
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
                currentSelectedChampion = ((PictureBox)sender).Name.Substring(0, ((PictureBox)sender).Name.Length-4);
                MyChampionText.Text = currentSelectedChampion;                
            }
            else
            {
                EnemyChampionIcon.Image = ((PictureBox)sender).Image;
                currentSelectedEnemy = ((PictureBox)sender).Name.Substring(0, ((PictureBox)sender).Name.Length - 4);
                EnemyChampionText.Text = currentSelectedEnemy;                
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
    }
}
