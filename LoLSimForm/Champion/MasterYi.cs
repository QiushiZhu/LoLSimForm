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

            Name = "MasterYi";

            statsInit();

            defaultAbility = "QEQWQRQEQEREEWWRWW";

        }
        

        public override void AbilityInit()          //Phase 2
        {
            Abilities.Clear();
            aa = new AutoAttack(this);
            Abilities.Add(aa);
            //MasterYiP p = new MasterYiP(this);
            //p.init();
            if (Q_Level > 0)
                Abilities.Add(new MasterYiQ(this));
            if (W_Level > 0)
                Abilities.Add(new MasterYiW(this));
            if (E_Level > 0)
                Abilities.Add(new MasterYiE(this));
            if (R_Level > 0)
                Abilities.Add(new MasterYiR(this));
        }


       

    }


}



