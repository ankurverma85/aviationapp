using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AviationApp.WeightAndBalance
{
    class FuelViewModel : INotifyPropertyChanged
    {
        public FuelViewModel()
        { arm = 0.0f; capacity = 0.0f; fuelWeightInKg = 0.0f; }
        public FuelViewModel(float arm, float capacity)
        {
            this.arm = arm;
            this.capacity = capacity;
            fuelWeightInKg = 0.0f;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private float _fuelWeightInKg;
        public float fuelWeightInKg {
            get => _fuelWeightInKg;
            set
            {
                _fuelWeightInKg = value;
                var args = new PropertyChangedEventArgs(nameof(FuelVolumeGal));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public float arm { get; }
        public float capacity { get; }
        public float FuelWeightInLb
        {
            get => (fuelWeightInKg * 2.204623f);
        }
        public float FuelVolumeGal
        {
            get => (fuelWeightInKg / 6.0f);
        }
    }
}
