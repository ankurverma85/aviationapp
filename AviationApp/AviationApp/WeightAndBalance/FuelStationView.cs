using System;
using System.ComponentModel;

using Xamarin.Forms;

namespace AviationApp.WeightAndBalance
{
    public class FuelStationView : ContentView
    {
        public FuelStationView()
        {
            viewModel = new FuelStationViewModel(1.0, 100.0, 50.0, (int)FuelQuantityUnits.kg);
            CreateGridView();
        }
        public FuelStationView(double arm, double capacity, double initialFuel, int units)
        {
            viewModel = new FuelStationViewModel(arm, capacity, initialFuel, (FuelQuantityUnits)units);
            CreateGridView();
        }
        private void CreateGridView()
        {
            var grid = new Grid { Padding = new Thickness(15, 0) };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });

            var label = new Label { Text = "Main Tanks", VerticalOptions = LayoutOptions.CenterAndExpand };
            var fuelQuantitySlider = new Slider { VerticalOptions = LayoutOptions.CenterAndExpand };
            var fuelQuantityDisplay = new Label { VerticalOptions = LayoutOptions.CenterAndExpand };
            var fuelUnitsPicker = new Picker { VerticalOptions = LayoutOptions.CenterAndExpand };

            grid.Children.Add(label, 0, 0);
            grid.Children.Add(fuelQuantitySlider, 1, 0);
            grid.Children.Add(fuelQuantityDisplay, 2, 0);
            grid.Children.Add(fuelUnitsPicker, 3, 0);

            fuelQuantitySlider.Maximum = viewModel.capacity;
            fuelQuantitySlider.ValueChanged += (sender, args) =>
            {
                viewModel.fuelWeightInKg = fuelQuantitySlider.Value;
                fuelQuantityDisplay.Text = viewModel.displayFuelFormatted;
            };
            fuelQuantitySlider.Value = viewModel.fuelWeightInKg;
            foreach (FuelQuantityUnits u in Enum.GetValues(typeof(FuelQuantityUnits)))
            {
                fuelUnitsPicker.Items.Add(u.ToString());
            }
            fuelUnitsPicker.SelectedIndex = 0;
            fuelUnitsPicker.SelectedIndexChanged += (sender, args) =>
            {
                viewModel.displayUnits = fuelUnitsPicker.SelectedIndex;
                fuelQuantityDisplay.Text = viewModel.displayFuelFormatted;
            };

            Content = grid;
        }

        private FuelStationViewModel viewModel;
    }

    class FuelStationViewModel : INotifyPropertyChanged
    {
        public FuelStationViewModel(double arm, double capacity, double initialFuel, FuelQuantityUnits units)
        {
            this.arm = arm;
            this.capacity = capacity;
            this.fuelWeightInKg = initialFuel;
            this.displayUnits = (int)units;
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
        private double _fuelWeightInKg;
        public double fuelWeightInKg
        {
            get => _fuelWeightInKg;
            set
            {
                _fuelWeightInKg = value;
                var args = new PropertyChangedEventArgs(nameof(displayFuel));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public string displayFuelFormatted => string.Format("{0:0.0}", displayFuel);
        public double arm { get; }
        public double capacity { get; }
        public double momentInKgM
        {
            get
            {
                return arm * fuelWeightInKg;
            }
        }
        public double displayFuel
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
                return double.NaN;
            }
        }
    }
    enum FuelQuantityUnits { kg, lb, l, gal };
}