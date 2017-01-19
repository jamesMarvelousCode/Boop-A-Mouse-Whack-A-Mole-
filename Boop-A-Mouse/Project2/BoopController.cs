using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class BoopController
    {
        Random RANDOM = new Random();

        int TEMP_RANDOM = 0;
        int RANDOM_MIN = 0;
        int RANDOM_MAX = 16;
       
        public int RandomGen()
        {
            TEMP_RANDOM = RANDOM.Next(RANDOM_MIN, RANDOM_MAX);
            return TEMP_RANDOM;
        }

        public int MoleSelect
        {
            set { TEMP_RANDOM = value; }
            get { return RandomGen(); }
        }

        public bool Mole0 { set; get; }

        public bool Mole1 { set; get; }

        public bool Mole2 { set; get; }     

        public bool Mole3 { set; get; }

        public bool Mole4 { set; get; }

        public bool Mole5 { set; get; }

        public bool Mole6 { set; get; }

        public bool Mole7 { set; get; }

        public bool Mole8 { set; get; }

        public bool Mole9 { set; get; }

        public bool Mole10 { set; get; }

        public bool Mole11 { set; get; }

        public bool Mole12 { set; get; }

        public bool Mole13 { set; get; }

        public bool Mole14 { set; get; }

        public bool Mole15 { set; get; }
    }
}
