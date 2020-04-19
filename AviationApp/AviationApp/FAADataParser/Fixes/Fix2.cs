namespace AviationApp.FAADataParser.Fixes
{
    class Fix2
    {
        /// <summary>
        /// Foreign key, record id from the Fix1 table
        /// </summary>
        public ulong FixRecordID { get; set; }
        /// <summary>
        /// Foreign key, record if of the associated Navaid
        /// </summary>
        public ulong NavID { get; set; }
        public double Radial { get; set; }
        public double DMEInKm { get; set; }
        public static bool TryParse(string recordString, ulong fixRecordId, string fixID, string state, string ICAOCode, out Fix2 fix2)
        {
            fix2 = new Fix2();
            if (recordString.Length != LOGICAL_RECORD_LENGTH) { return false; }
            if (recordString.Substring(0, 4) != "FIX2") { return false; }
            fix2.FixRecordID = fixRecordId;
            if (recordString.Substring(FIXID_START, FIXID_LEN).Trim() != fixID) { return false; }
            if (recordString.Substring(STATE_NAME_START, STATE_NAME_LEN).Trim() != state) { return false; }
            if (recordString.Substring(ICAO_CODE_START, ICAO_CODE_LEN).Trim() != ICAOCode) { return false; }
            return true;
        }
        private const int FIXID_START = 4;
        private const int FIXID_LEN = 30;
        private const int STATE_NAME_START = 34;
        private const int STATE_NAME_LEN = 30;
        private const int ICAO_CODE_START = 64;
        private const int ICAO_CODE_LEN = 2;
        private const int LOGICAL_RECORD_LENGTH = 466;
    }
}
