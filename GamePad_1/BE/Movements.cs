using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePad_1.BE
{
    public class Movements
    {
        private static Movements instancia;       

        public static Movements getInstancia()
        { 
            if (instancia == null)
            {
                instancia = ConfigButtons.getSavedMovements();

            }
            return instancia;        
        }
        public static void setInstancia(Movements mov)
        {
            instancia = mov;
        
        }
         
        public string Up { get; set; }
        public string Down { get; set; }
        public string Right { get; set; }
        public string Left { get; set; }
    }
}
