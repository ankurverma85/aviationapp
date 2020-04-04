namespace AviationApp.Utilities.Units
{
    class Volume
    {
        private const double USGAL_IN_MCUBE = 264.17205124156;
        private const double IMPGAL_IN_MCUBE = 219.96924829909;

        public double CubicMetre { get; set; } = 0.0;
        public double Litre { get => CubicMetre * 1000.0; set { CubicMetre = value / 1000.0; } }
        public double USGallon { get => CubicMetre * USGAL_IN_MCUBE; set { CubicMetre = value / USGAL_IN_MCUBE; } }
        public double ImperialGallon { get => CubicMetre * IMPGAL_IN_MCUBE; set { CubicMetre = value / IMPGAL_IN_MCUBE; } }
    }
}
