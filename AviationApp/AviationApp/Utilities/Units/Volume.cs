namespace AviationApp.Utilities.Units
{
    class Volume
    {
        private const double USGAL_IN_MCUBE = 264.17205124156;
        private const double IMPGAL_IN_MCUBE = 219.96924829909;
        public Volume() { volumeInMCubed = 0.0; }
        private double volumeInMCubed;
        public double CubicMetre { get => volumeInMCubed; set { volumeInMCubed = value; } }
        public double Litre { get => volumeInMCubed * 1000.0; set { volumeInMCubed = value / 1000.0; } }
        public double USGallon { get => volumeInMCubed * USGAL_IN_MCUBE; set { volumeInMCubed = value / USGAL_IN_MCUBE; } }
        public double ImperialGallon { get => volumeInMCubed * IMPGAL_IN_MCUBE; set { volumeInMCubed = value / IMPGAL_IN_MCUBE; } }
    }
}
