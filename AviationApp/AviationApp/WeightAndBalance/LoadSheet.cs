using System.Collections.Generic;

using AviationApp.Utilities.Quantities;
using AviationApp.Utilities.Units;

namespace AviationApp.Pages
{
    class LoadSheet
    {
        public string AircraftIdentifier { get; set; } = string.Empty;
        public string AircraftType { get; set; } = "Unknown";
        public Mass EmptyMass { get; set; } = new Mass();
        public Length EmptyCG { get; set; } = new Length();
        public List<LoadStation> LoadStations { get; set; } = new List<LoadStation> { };
        public List<FuelStation> FuelStations { get; set; } = new List<FuelStation> { };
    }
    class LoadStation
    {
        public LoadStation(string stationName, Length stationArm, Mass maximumMass)
        {
            StationName = stationName;
            StationArm = stationArm;
            MaximumMass = maximumMass;
        }
        public string StationName { get; } = string.Empty;
        public Length StationArm { get; } = new Length();
        public List<StationItem> StationItems { get; set; } = new List<StationItem> { };
        public Mass MaximumMass { get; } = new Mass { KiloGrams = double.PositiveInfinity };
        public Mass TotalMass { get { Mass m = new Mass(); foreach (StationItem si in StationItems) { m += si.Mass; } return m; } }
        public bool MaximumMassExceeded { get => TotalMass > MaximumMass; }
    }
    class StationItem
    {
        public string ItemName { get; set; } = string.Empty;
        public Mass Mass { get; set; } = new Mass();
    }
    class FuelStation
    {
        public FuelStation(string name, Length arm, IFuel capacity)
        {
            Name = name;
            Arm = arm;
            Capacity = capacity;
            switch (capacity.FuelType)
            {
                case FuelType.AvGas: Fuel = new AvGasFuel(); break;
                case FuelType.Jet: Fuel = new JetFuel(); break;
                default: throw new System.NotImplementedException();
            }
        }
        public string Name { get; } = string.Empty;
        public Length Arm { get; } = new Length();
        public IFuel Fuel { get; } = null;
        public IFuel Capacity { get; } = null;
        public Mass Mass => Fuel.Mass;
    }
}
