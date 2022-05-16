using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeiraSP.Web.API.TEST.Util
{
    internal class FeiraUtil
    {
        public static int CriarID()
        {            
            return CriarID(1000,05000);
        }

        public static int CriarID(int inicio, int final)
        {
            Random random = new Random();
            int value = random.Next(inicio, final);
            return value;
        }

    }
}
