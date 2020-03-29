using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AviationApp.WeightAndBalance
{
    enum FuelQuantityUnits { kg, lb, l, gal };
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FuelStationView : ContentView
    {
        public FuelStationView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            foreach (FuelQuantityUnits u in Enum.GetValues(typeof(FuelQuantityUnits)))
            {
                unitsPicker.Items.Add(u.ToString());
            }
        }
    }

    class FuelStationViewModel : INotifyPropertyChanged
    {
        public FuelStationViewModel()
        { arm = 0.0f; capacity = 0.0f; fuelWeightInKg = 0.0f; }
        public FuelStationViewModel(float arm, float capacity, int displayUnits)
        {
            this.arm = arm;
            this.capacity = capacity;
            fuelWeightInKg = 0.0f;
            this.displayUnits = displayUnits;
        }
        private FuelQuantityUnits _displayUnits;
        public int displayUnits
        {
            get
            {
                return (int)_displayUnits;
            }
            set
            {
                _displayUnits = (FuelQuantityUnits)value;
                var unitChangeArgs = new PropertyChangedEventArgs(nameof(displayUnits));
                PropertyChanged?.Invoke(this, unitChangeArgs);
                var displayQuantityChangeArgs = new PropertyChangedEventArgs(nameof(displayFuel));
                PropertyChanged?.Invoke(this, displayQuantityChangeArgs);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private float _fuelWeightInKg;
        public float fuelWeightInKg
        {
            get => _fuelWeightInKg;
            set
            {
                _fuelWeightInKg = value;
                var args = new PropertyChangedEventArgs(nameof(displayFuel));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public float arm { get; }
        public float capacity { get; }
        public float displayFuel
        {
            get
            {
                switch ((FuelQuantityUnits)displayUnits)
                {
                    case FuelQuantityUnits.kg: return fuelWeightInKg;
                    case FuelQuantityUnits.lb: return fuelWeightInKg * 2.204623f;
                    case FuelQuantityUnits.gal: return fuelWeightInKg * 2.204623f / 6.01f;
                    case FuelQuantityUnits.l: return fuelWeightInKg * 2.204623f / 6.01f * 3.7854f;
                }
                // Code should never reach here
                return float.NaN;
            }
        }
    }
}