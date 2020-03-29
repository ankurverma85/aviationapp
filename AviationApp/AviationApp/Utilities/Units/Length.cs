using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.Utilities.Units
{
    class Length
    {
        private const double METRES_IN_SM = 1609.3440000000103;
        private const double METRES_IN_NM = 1852.0;
        Length() { lengthInM = 0.0; }
        private double lengthInM;
        public double Metre { get => lengthInM; set { lengthInM = value; } }
        public double KiloMetre { get => lengthInM / 1000.0; set { lengthInM = value * 1000.0; } }
        public double StatuteMile { get => lengthInM * METRES_IN_SM; set { lengthInM = value / METRES_IN_SM; } }
        public double NauticalMile { get => lengthInM * METRES_IN_NM; set { lengthInM = value / METRES_IN_NM; } }
    }
}
