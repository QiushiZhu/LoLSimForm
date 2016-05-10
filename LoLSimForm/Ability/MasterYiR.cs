using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSimForm
{
    class MasterYiR : Ability
    {
        public override void init()
        {
            CD = 60;
        }

        public MasterYiR(Champion _caster):base(_caster)
        {

        }
    }
}
