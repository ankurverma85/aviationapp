namespace AviationApp.Utilities.Units
{
    class Mass
    {
        private const double LB_IN_KG = 2.204623;
        private double weightInKg = 0.0;
        public double KiloGrams { get => weightInKg; set { weightInKg = value; } }
        public double Pounds { get => weightInKg * LB_IN_KG; set { weightInKg = value / LB_IN_KG; } }
        public double Grams { get => weightInKg * 1000; set { weightInKg = value / 1000; } }
        public static Mass operator +(Mass a, Mass b) => new Mass { weightInKg = a.weightInKg + b.weightInKg };
        public static bool operator <(Mass a, Mass b) => a.weightInKg < b.weightInKg;
        public static bool operator >(Mass a, Mass b) => a.weightInKg > b.weightInKg;
    }
}
