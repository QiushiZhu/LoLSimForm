using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace LoLSimForm
{
    public class DeathInfo
    {
        public string Champion;
        public int ChampionLevel;
        public bool ChampionRole;

        public int ChampionQLevel;
        public int ChampionWLevel;
        public int ChampionELevel;
        public int ChampionRLevel;

        public List<Item> championItems = new List<Item>(6);

        public Image HeathBar;

        public DeathInfo(Champion champion)
        {
            this.Champion = champion.Name;
            ChampionLevel = champion.Level;
            ChampionRole = champion.championRole;

            ChampionQLevel = champion.Q_Level;
            ChampionWLevel = champion.W_Level;
            ChampionELevel = champion.E_Level;
            ChampionRLevel = champion.R_Level;

            championItems = champion.championItems;

            HeathBar = champion.HealthBar.Image;
        }

    }

   public  class SimConclusion
    {
        public DeathInfo WinnerChampionInfo;
        public DeathInfo LoserChampionInfo;
        public DeathInfo MyChampionInfo;
        public DeathInfo EnemyChampionInfo;

        public string winner;
        public double duration;
        public double hpRemained;

        public SimConclusion(DeathInfo WinnerChampionInfo, DeathInfo LoserChampionInfo, double duration, double hpRemained)
        {
            this.WinnerChampionInfo = WinnerChampionInfo;
            this.LoserChampionInfo = LoserChampionInfo;
            this.duration = duration;
            this.hpRemained = hpRemained;

            if (WinnerChampionInfo.ChampionRole)
            {
                winner = "Winner is my champion - " + WinnerChampionInfo.Champion;
                MyChampionInfo = WinnerChampionInfo;
                EnemyChampionInfo = LoserChampionInfo;
            }
            else
            {
                winner = "Winner is enemy champion - " + WinnerChampionInfo.Champion;
                EnemyChampionInfo = WinnerChampionInfo;
                MyChampionInfo = LoserChampionInfo;
            }

        }
    }
}
