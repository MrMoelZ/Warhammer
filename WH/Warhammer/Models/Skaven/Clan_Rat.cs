using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Warhammer.Models
{
    public class Clan_Rat : _Skaven
    {
        public new string Name = "Clan Rat";
        public new string Description = "Clan Rat - Description"; 

        public Clan_Rat(int i, Canvas maincanvas)
            : base(i,maincanvas)
        {

        }


    }
}
