using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    class Dummy:Champion
    {
        public Dummy(int level):base(level)
        {
            this.oHealth = 5985.6;
            this.oAttackNumber = 60.04;
            this.oAttackSpeed = 0.679;
            this.oArmor = 24.04;
            this.oMana = 250.56;
            this.oManaReg = 7.255;
            this.oMagigResist = 32.1;

            this.nHealth = 92;
            this.nAttackNumber = 3;
            this.nAttackSpeed = 0.02;
            this.nArmor = 3;
            this.nMana = 42;
            this.nManaReg = 0.45;
            this.nMagigResist = 1.25;
        }
    }
}
