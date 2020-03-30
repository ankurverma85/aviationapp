using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.Utilities.Units
{
    class Length
    {
        private const double METRES_IN_SM = 1609.3440000000103;
        private const double METRES_IN_NM = 1852.0;
        private const double METRES_IN_FEET = 0.30480000;

        public double Metre { get; set; } = 0.0;
        public double KiloMetre { get => Metre / 1000.0; set { Metre = value * 1000.0; } }
        public double StatuteMile { get => Metre / METRES_IN_SM; set { Metre = value * METRES_IN_SM; } }
        public double NauticalMile { get => Metre / METRES_IN_NM; set { Metre = value * METRES_IN_NM; } }
        public double Foot { get => Metre / METRES_IN_FEET; set { Metre = value * METRES_IN_FEET; } }
        public double Inch { get => Foot * 12.0; set { Foot = value / 12.0; } }
    }
}
