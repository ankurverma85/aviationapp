namespace AviationApp.Utilities.Units
{
    enum MassUnits { kg, g, lb };
    class Mass
    {
        private const double LB_IN_KG = 2.204623;

        public double KiloGrams { get; set; } = 0.0;
        public double Pounds { get => KiloGrams * LB_IN_KG; set { KiloGrams = value / LB_IN_KG; } }
        public double Grams { get => KiloGrams * 1000; set { KiloGrams = value / 1000; } }
        public static Mass operator +(Mass a, Mass b) => new Mass { KiloGrams = a.KiloGrams + b.KiloGrams };
        public static bool operator <(Mass a, Mass b) => a.KiloGrams < b.KiloGrams;
        public static bool operator >(Mass a, Mass b) => a.KiloGrams > b.KiloGrams;
        public double GetQuantity(MassUnits unit)
        {
            switch (unit)
            {
                case MassUnits.g: return Grams;
                case MassUnits.kg: return KiloGrams;
                case MassUnits.lb: return Pounds;
                default: throw new System.Exception();
            }
        }
        public void SetQuantity(double amount, MassUnits unit)
        {
            switch (unit)
            {
                case MassUnits.g: Grams = amount; break;
                case MassUnits.kg: KiloGrams = amount; break;
                case MassUnits.lb: Pounds = amount; break;
                default: throw new System.Exception();
            }
        }
    }
}
