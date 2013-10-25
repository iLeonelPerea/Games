using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarritaGirando
{
    class ComparaScore: IComparer <Score>
    {
        public int Compare(Score sc1, Score sc2) {
            int returnValue = 1;
            if (sc1 != null && sc2 != null) {
                returnValue = sc2.Puntos.CompareTo(sc1.Puntos);
            }
            return returnValue;
        }
    }
}
