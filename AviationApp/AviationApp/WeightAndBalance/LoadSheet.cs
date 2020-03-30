using System;
using System.Collections.Generic;
using System.Text;

using AviationApp.Utilities.Units;
using AviationApp.Utilities.Quantities;

namespace AviationApp.WeightAndBalance
{

    class LoadSheet
    {
        public string AircraftIdentifier { get; set; } = "";
        public string AircraftType { get; set; } = "Unknown";
        public List<LoadStation> LoadStations { get; set; } = new List<LoadStation> { };
        public List<FuelStation> FuelStations { get; set; } = new List<FuelStation> { };
    }
    class LoadStation
    {
        public string StationName { get; set; } = "";
        public Length StationArm { get; set; } = new Length();
        public List<StationItem> StationItems { get; set; } = new List<StationItem> { };
        public Mass MaximumMass { get; set; } = new Mass { KiloGrams = Double.PositiveInfinity };
        public Mass TotalMass { get { Mass m = new Mass(); foreach (var si in StationItems) { m += si.Mass; } return m; } }
        public bool MaximumMassExceeded { get => TotalMass > MaximumMass; }
    }
    class StationItem
    {
        public string ItemName { get; set; } = "";
        public Mass Mass { get; set; } = new Mass();
    }
    class FuelStation
    {
        public string Name { get; set; } = "";
        public Length Arm { get; } = new Length();
        public IFuel Fuel { get; set; }
        public IFuel Capacity { get; }
        public Mass Mass { get => Fuel.Mass; }
    }
}
