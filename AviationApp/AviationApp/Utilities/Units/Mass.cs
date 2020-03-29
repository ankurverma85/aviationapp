﻿namespace AviationApp.Utilities.Units
{
    class Mass
    {
        private const double LB_IN_KG = 2.204623;
        public Mass() { weightInKg = 0.0; }
        private double weightInKg;
        public double KiloGrams { get => weightInKg; set { weightInKg = value; } }
        public double Pounds { get => weightInKg * LB_IN_KG;  set { weightInKg = value / LB_IN_KG; } }
        public double Grams { get => weightInKg * 1000; set { weightInKg = value / 1000; } }
    }
}