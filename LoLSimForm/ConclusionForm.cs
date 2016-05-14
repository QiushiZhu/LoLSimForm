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
    public partial class ConclusionForm : Form
    {
        public ConclusionForm()
        {
            InitializeComponent();
            
        }

        public void statLoad(SimConclusion conclusion)
        {
            //MyChampionHealthBar.Image = conclusion.MyChampionInfo.HeathBar;
            WinnerTextBox.Text = conclusion.winner;
            //HpRemainedTextbox.Text = conclusion.hpRemained.ToString("P");
            //FightTimeTextbox.Text = conclusion.duration.ToString("F1");
        }
    }
}
