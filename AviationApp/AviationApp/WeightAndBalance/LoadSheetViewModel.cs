using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

using AviationApp.Utilities.Quantities;
using AviationApp.Utilities.Units;

using Xamarin.Forms;

namespace AviationApp.Pages
{
    class LoadSheetViewModel : BindableObject
    {
        public LoadSheetViewModel()
        {
            LoadSheet = new LoadSheet
            {
                AircraftIdentifier = "N7991U",
                AircraftType = "C150",
                EmptyMass = new Mass { KiloGrams = 693.1 },
                EmptyCG = new Length { Millimetre = 828 },
                LoadStations = new List<LoadStation>
                {
                    new LoadStation("Pilot & Passenger", new Length { Millimetre = 991 }, new Mass { KiloGrams = 500 }),
                    new LoadStation("Area 1 Baggage", new Length { Millimetre = 1626}, new Mass {Pounds=120}),
                    new LoadStation("Area 2 Baggage", new Length{Millimetre= 2134 }, new Mass{Pounds=50})
                },
                FuelStations = new List<FuelStation>
                {
                    new FuelStation("Main Tanks", new Length { Millimetre=1067}, new AvGasFuel {Volume = new Volume{USGallon=22.5 } })
                }
            };
            var loadSheetCopy = LoadSheet;
            loadSheetCopy.LoadStations[0].StationItems.Add(new StationItem { ItemName = "Pilot", Mass = new Mass { KiloGrams = 65 } });
            loadSheetCopy.LoadStations[0].StationItems.Add(new StationItem { ItemName = "Passenger", Mass = new Mass { Pounds = 190 } });
            LoadSheet = loadSheetCopy;
        }
        public ObservableCollection<LoadSheetGroup> LoadSheetGroups { get; set; } = new ObservableCollection<LoadSheetGroup>();
        public LoadSheet LoadSheet
        {
            get => loadSheet;
            set
            {
                loadSheet = value;
                LoadSheetGroups.Clear();
                LoadSheetGroup aircraftIdentification = new LoadSheetGroup("Aircraft Information")
                {
                    new LoadSheetTitleDescription("Identifier", loadSheet.AircraftIdentifier),
                    new LoadSheetTitleDescription("Type", loadSheet.AircraftType)
                };
                LoadSheetGroup weightStations = new LoadSheetGroup("Weight");
                foreach (var weightStation in loadSheet.LoadStations)
                {
                    weightStations.Add(new LoadSheetWeightStationHeader(weightStation.StationName, weightStation.StationArm.Millimetre, LengthUnits.mm));
                    foreach (var weightStationItem in weightStation.StationItems)
                    {
                        weightStations.Add(new LoadSheetWeightStationItem { Name = weightStationItem.ItemName, MassDisplayUnit = MassUnits.kg, MassDisplay = weightStationItem.Mass.KiloGrams }); ;
                    }
                }
                LoadSheetGroup fuelStations = new LoadSheetGroup("Fuel");
                foreach (var fuelStation in loadSheet.FuelStations)
                {
                    fuelStations.Add(new LoadSheetFuelStation(fuelStation.Name, fuelStation.Arm.Millimetre, LengthUnits.mm, fuelStation.Capacity));
                }
                LoadSheetGroups.Add(aircraftIdentification);
                LoadSheetGroups.Add(weightStations);
                LoadSheetGroups.Add(fuelStations);
            }
        }
        private LoadSheet loadSheet;
    }
    public class LoadSheetDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TitleDescriptionTemplate { get; set; }
        public DataTemplate WeightStationHeaderTemplate { get; set; }
        public DataTemplate WeightStationItemTemplate { get; set; }
        public DataTemplate FuelStationTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case LoadSheetTitleDescription _:
                    return TitleDescriptionTemplate;
                case LoadSheetWeightStationHeader _:
                    return WeightStationHeaderTemplate;
                case LoadSheetWeightStationItem _:
                    return WeightStationItemTemplate;
                case LoadSheetFuelStation _:
                    return FuelStationTemplate;
                default:
                    throw new System.Exception();
            }
        }
    }
    internal interface ILoadSheetRow { }
    internal class LoadSheetGroup : ObservableCollection<ILoadSheetRow>
    {
        public LoadSheetGroup(string title) { Title = title; }
        public string Title { get; }
    }
    internal class LoadSheetTitleDescription : ILoadSheetRow
    {
        public LoadSheetTitleDescription(string title, string description) { Title = title; Description = description; }
        public string Title { get; }
        public string Description { get; }
    }
    internal abstract class LoadSheetStation : ILoadSheetRow
    {
        public LoadSheetStation(string title, double arm_length, LengthUnits armunits)
        {
            Title = title;
            ArmDisplay = arm_length;
            ArmDisplayUnit = armunits;
        }
        public string Title { get; }
        public double ArmDisplay { get; set; }
        public List<LengthUnits> ArmDisplayUnits { get; } = new List<LengthUnits> { LengthUnits.mm, LengthUnits.cm, LengthUnits.inch }; // Should be readonly, don't know how to set
        public LengthUnits ArmDisplayUnit { get; set; }
    }
    internal class LoadSheetWeightStationHeader : LoadSheetStation
    {
        public LoadSheetWeightStationHeader(string title, double arm_length, LengthUnits armunits) : base(title, arm_length, armunits) { }
    }
    internal class LoadSheetWeightStationItem : ILoadSheetRow, INotifyPropertyChanged
    {
        public string Name { get => name; set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name))); } }
        public double MassDisplay
        {
            get => mass.GetQuantity(MassDisplayUnit);
            set
            {
                mass.SetQuantity(value, MassDisplayUnit);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MassDisplay)));
            }
        }
        public MassUnits MassDisplayUnit
        {
            get => massDisplayUnit;
            set
            {
                massDisplayUnit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MassDisplayUnit)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MassDisplay)));
            }
        }
        public List<MassUnits> MassDisplayUnits { get => new List<MassUnits> { MassUnits.kg, MassUnits.lb }; }
        private string name = string.Empty;
        private Mass mass = new Mass();
        private MassUnits massDisplayUnit = MassUnits.kg;

        public event PropertyChangedEventHandler PropertyChanged;
    }
    internal class LoadSheetFuelStation : LoadSheetStation, INotifyPropertyChanged
    {
        public LoadSheetFuelStation(string title, double arm_length, LengthUnits armunits, IFuel capacity) : base(title, arm_length, armunits)
        {
            switch (capacity)
            {
                case AvGasFuel _:
                    fuel = new AvGasFuel();
                    break;
                case JetFuel _:
                    fuel = new JetFuel();
                    break;
                default:
                    throw new System.Exception();
            }
            FuelDisplayUnit = FuelDisplayUnits[0];
            fuelCapacity = capacity.GetQuantity(FuelDisplayUnits[0]);
        }

        public double FuelQuantity { get => fuel.GetQuantity(FuelDisplayUnit); }
        public double FuelFraction
        {
            get => fuel.GetQuantity(FuelDisplayUnits[0]) / fuelCapacity;
            set
            {
                fuel.SetQuantity(value * fuelCapacity, FuelDisplayUnits[0]);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FuelQuantity)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FuelQuantity)));
            }
        }
        public List<FuelUnits> FuelDisplayUnits => fuel?.AcceptableUnits();
        public FuelUnits FuelDisplayUnit
        {
            get => fuelDisplayUnit;
            set
            {
                fuelDisplayUnit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FuelDisplayUnit)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FuelQuantity)));
            }
        }
        private IFuel fuel = null;
        double fuelCapacity = 0;
        private FuelUnits fuelDisplayUnit;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
