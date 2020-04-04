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
        List<FuelUnits> AcceptableUnits();
        FuelType FuelType { get; }
    }
    class AvGasFuel : IFuel
    {
        private const double AVGAS_DENSITY_KG_M3 = 768.0;
        public List<FuelUnits> AcceptableUnits() { return new List<FuelUnits> { FuelUnits.l, FuelUnits.usgal, FuelUnits.impgal }; }

        public void SetQuantity(double quantity, FuelUnits unit)
        {
            Debug.Assert(AcceptableUnits().Contains(unit));
            switch (unit)
            {
                case FuelUnits.l: Volume.Litre = quantity; break;
                case FuelUnits.usgal: Volume.USGallon = quantity; break;
                case FuelUnits.impgal: Volume.ImperialGallon = quantity; break;
                default: throw new System.Exception();
            }
        }

        public double GetQuantity(FuelUnits unit)
        {
            Debug.Assert(AcceptableUnits().Contains(unit));
            switch (unit)
            {
                case FuelUnits.l: return Volume.Litre;
                case FuelUnits.usgal: return Volume.USGallon;
                case FuelUnits.impgal: return Volume.ImperialGallon;
                default: throw new System.Exception();
            }
        }

        public Volume Volume { get; set; } = new Volume();

        public Mass Mass { get => new Mass { KiloGrams = AVGAS_DENSITY_KG_M3 * Volume.CubicMetre }; }

        public FuelType FuelType => FuelType.AvGas;
    }
    class JetFuel : IFuel
    {
        public List<FuelUnits> AcceptableUnits() { return new List<FuelUnits> { FuelUnits.kg, FuelUnits.lb }; }

        public void SetQuantity(double quantity, FuelUnits unit)
        {
            Debug.Assert(AcceptableUnits().Contains(unit));
            switch (unit)
            {
                case FuelUnits.kg: Mass.KiloGrams = quantity; break;
                case FuelUnits.lb: Mass.Pounds = quantity; break;
                default: throw new System.Exception();
            }
        }

        public double GetQuantity(FuelUnits unit)
        {
            Debug.Assert(AcceptableUnits().Contains(unit));
            switch (unit)
            {
                case FuelUnits.kg: return Mass.KiloGrams;
                case FuelUnits.lb: return Mass.Pounds;
                default: throw new System.Exception();
            }
        }

        public Mass Mass { get; } = new Mass();

        public FuelType FuelType => FuelType.Jet;
    }
}
