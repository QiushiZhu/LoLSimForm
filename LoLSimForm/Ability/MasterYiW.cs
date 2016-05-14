using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    class MasterYiW : Ability
    {
        int[] CDs = { 60, 59, 58, 57, 56 };
        public override void init()
        {
            CD = CDs[caster.W_Level-1];
        }

        public MasterYiW(Champion _caster):base(_caster)
        {

        }

        protected override string effectString()
        {            
            return "Use MasterYiW";
        }

    }
}
