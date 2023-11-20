using System.Text.RegularExpressions;

namespace Number_Plates_Recognition
{
    public enum Country
    {
        Sweden,
        Denmark,
        Unknown
    }
    public class PlateAnalyzer
    {
        private List<string> _plates;
        private readonly object _lock = new object();

        public PlateAnalyzer()
        {
            _plates = new List<string>();
        }

        public void AddPlate(string plate)
        {
            lock (_lock)
            {
                _plates.Add(plate);
            }
        }

        public void Reset()
        {
            _plates.Clear();
        }

        public IEnumerable<string> GetUniquePlates()
        {
            return _plates.Distinct();
        }

        public IEnumerable<string> GetPlatesByCountry(Country country, int offset = 0, int limit = 100)
        {
            return _plates
                .Where(plate => IdentifyCountry(plate) == country)
                .Skip(offset)
                .Take(limit);
        }

        private Country IdentifyCountry(string plate)
        {
            // Swedish numbers: three letters followed by three digits (e.g.: ABC 123)
            if (Regex.IsMatch(plate, "^[A-Za-z]{3} [0-9]{3}$"))
            {
                return Country.Sweden;
            }
            // Danish numbers: two letters followed by five digits (e.g.: AB 12345)
            else if (Regex.IsMatch(plate, "^[A-Za-z]{2} [0-9]{5}$"))
            {
                return Country.Denmark;
            }

            return Country.Unknown;
        }
    }
}