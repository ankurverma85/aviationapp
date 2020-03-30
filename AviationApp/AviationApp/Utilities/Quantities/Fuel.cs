using AviationApp.Utilities.Units;
using System.Collections.Generic;
using System.Diagnostics;

namespace AviationApp.Utilities.Quantities
{
    enum FuelUnits { kg, lb, l, usgal, impgal };
    enum FuelType { AvGas, Jet };
    interface IFuel
    {
        Mass Mass { get; }
        void SetQuantity(double quantity, FuelUnits unit);
        double GetQuantity(FuelUnits unit);
        public static List<FuelUnits> AcceptableUnits();
    }
    class AvGasFuel : IFuel
    {
        private const double AVGAS_DENSITY_KG_M3 = 768.0;
        public static List<FuelUnits> AcceptableUnits() { return new List<FuelUnits> { FuelUnits.l, FuelUnits.usgal, FuelUnits.impgal }; }

        public void SetQuantity(double quantity, FuelUnits unit)
        {
            Debug.Assert(AcceptableUnits().Contains(unit));
            switch (unit)
            {
                case FuelUnits.l: volume.Litre = quantity; break;
                case FuelUnits.usgal: volume.USGallon = quantity; break;
                case FuelUnits.impgal: volume.ImperialGallon = quantity; break;
                default: throw new System.Exception();
            }
        }

        public double GetQuantity(FuelUnits unit)
        {
            Debug.Assert(AcceptableUnits().Contains(unit));
            switch (unit)
            {
                case FuelUnits.l: return volume.Litre;
                case FuelUnits.usgal: return volume.USGallon;
                case FuelUnits.impgal: return volume.ImperialGallon;
                default: throw new System.Exception();
            }
        }

        private Volume volume = new Volume();

        public Mass Mass { get => new Mass { KiloGrams = AVGAS_DENSITY_KG_M3 * volume.CubicMetre }; }
    }
    class JetFuel : IFuel
    {
        public static List<FuelUnits> AcceptableUnits() { return new List<FuelUnits> { FuelUnits.kg, FuelUnits.lb }; }

        public void SetQuantity(double quantity, FuelUnits unit)
        {
            Debug.Assert(AcceptableUnits().Contains(unit));
            switch(unit)
            {
                case FuelUnits.kg: mass.KiloGrams = quantity; break;
                case FuelUnits.lb: mass.Pounds = quantity; break;
                default: throw new System.Exception();
            }
        }

        public double GetQuantity(FuelUnits unit)
        {
            Debug.Assert(AcceptableUnits().Contains(unit));
            switch (unit)
            {
                case FuelUnits.kg: return mass.KiloGrams;
                case FuelUnits.lb: return mass.Pounds;
                default: throw new System.Exception();
            }
        }

        private Mass mass = new Mass();

        public Mass Mass => mass;
    }
}
