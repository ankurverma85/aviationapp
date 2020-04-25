using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AviationApp.FAADataParser.Aff
{
    public static class AffFileParser
    {
        public static List<ArtccFacility> ParseFile(FileStream file)
        {
            List<ArtccFacility> facilitiesList = new List<ArtccFacility>();
            using (StreamReader streamReader = new StreamReader(file, true))
            {
                string line;
                ArtccFacility facility = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    switch (line.Substring(0, 4))
                    {
                        case "AFF1":
                            if (!Aff1.TryParse(line, out Aff1 aff1))
                            {
                                throw new ApplicationException("Could not parse AFF1\n" + line);
                            }
                            if (!(facility is null))
                            {
                                facilitiesList.Add(facility);
                            }
                            facility = new ArtccFacility
                            {
                                ArtccIdent = aff1.ArtccIdent,
                                ArtccName = aff1.ArtccName,
                                SiteLocation = aff1.SiteLocation,
                                FacilityType = aff1.FacilityType,
                                EffectiveDate = aff1.EffectiveDate,
                                StateName = aff1.StateName,
                                StateCode = aff1.StateCode,
                                Latitude = aff1.Latitude,
                                Longitude = aff1.Longitude,
                                IcaoId = aff1.IcaoId
                            };
                            break;
                        case "AFF2":
                            if (!Aff2.TryParse(line, out Aff2 aff2))
                            {
                                throw new ApplicationException("Could not parse AFF2\n" + line);
                            }
                            if (aff2.ArtccIdent != facility.ArtccIdent
                                || aff2.SiteLocation != facility.SiteLocation
                                || aff2.FacilityType != facility.FacilityType)
                            {
                                throw new ApplicationException("Mismatch in AFF2 and AFF1\n" + line);
                            }
                            foreach ((int number, string remark) remark in facility.FacilityRemarks)
                            {
                                if (remark.number == aff2.RemarksNumber)
                                {
                                    throw new ApplicationException("Duplicate remarks number in line\n" + line);
                                }
                            }
                            facility.FacilityRemarks.Add((aff2.RemarksNumber, aff2.RemarksText));
                            break;
                        case "AFF3":
                            if (!Aff3.TryParse(line, out Aff3 aff3))
                            {
                                throw new ApplicationException("Could not parse AFF3\n" + line);
                            }
                            if (aff3.ArtccIdent != facility.ArtccIdent
                                || aff3.SiteLocation != facility.SiteLocation
                                || aff3.FacilityType != facility.FacilityType)
                            {
                                throw new ApplicationException("Mismatch in AFF3 and AFF1\n" + line);
                            }
                            ArtccFrequencyInformation frequencyInformation = new ArtccFrequencyInformation
                            {
                                Frequency = aff3.Frequency,
                                AltitudeSector = aff3.AltitudeSector,
                                FrequencySpecialUsage = aff3.FrequencySpecialUsage,
                                RCAGFrequencyCharted = aff3.RCAGFrequencyCharted,
                                AirportIdent = aff3.AirportIdent,
                                AirportState = aff3.AirportState,
                                AirportStatePOCode = aff3.AirportStatePOCode,
                                AirportCity = aff3.AirportCity,
                                AirportName = aff3.AirportName,
                                AirportLatitude = aff3.AirportLatitude,
                                AirportLongitude = aff3.AirportLongitude
                            };
                            facility.Frequencies.Add(frequencyInformation);
                            break;
                        case "AFF4":
                            if (!Aff4.TryParse(line, out Aff4 aff4))
                            {
                                throw new ApplicationException("Could not parse AFF4\n" + line);
                            }
                            if (aff4.ArtccIdent != facility.ArtccIdent
                                || aff4.SiteLocation != facility.SiteLocation
                                || aff4.FacilityType != facility.FacilityType)
                            {
                                throw new ApplicationException("Mismatch in AFF4 and AFF1\n" + line);
                            }
                            IEnumerable<int> frequencyQuery = from freq in facility.FrequencyRemarks
                                                              where freq.frequency == aff4.Frequency
                                                              select freq.number;
                            foreach (int num in frequencyQuery)
                            {
                                if (aff4.RemarksNumber == num)
                                {
                                    throw new ApplicationException("Duplicate remarks number in line\n" + line);
                                }
                            }
                            facility.FrequencyRemarks.Add((aff4.Frequency, aff4.RemarksNumber, aff4.RemarksText));
                            break;
                        default: throw new ApplicationException("Funny line\n" + line); // continue;
                    }
                }
            }
            return facilitiesList;
        }
    }
}
