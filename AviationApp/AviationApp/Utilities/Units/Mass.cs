namespace AviationApp.Utilities.Units
{
    class Mass
    {
        private const double LB_IN_KG = 2.204623;

        public double KiloGrams { get; set; } = 0.0;
        public double Pounds { get => KiloGrams * LB_IN_KG; set { KiloGrams = value / LB_IN_KG; } }
        public double Grams { get => KiloGrams * 1000; set { KiloGrams = value / 1000; } }
        public static Mass operator +(Mass a, Mass b) => new Mass { KiloGrams = a.KiloGrams + b.KiloGrams };
        public static bool operator <(Mass a, Mass b) => a.KiloGrams < b.KiloGrams;
        public static bool operator >(Mass a, Mass b) => a.KiloGrams > b.KiloGrams;
    }
}
