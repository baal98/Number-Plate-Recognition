using Number_Plates_Recognition;

namespace Tests
{
    [TestFixture]
    public class PlateAnalyzerTests
    {
        private PlateAnalyzer _analyzer;

        [SetUp]
        public void Setup()
        {
            _analyzer = new PlateAnalyzer();
        }

        [Test]
        public void AddPlate_ValidPlate_AddsPlate()
        {
            _analyzer.AddPlate("ABC 123");
            Assert.Contains("ABC 123", _analyzer.GetUniquePlates().ToList());
        }

        [Test]
        public void GetPlates_ValidCountry_ReturnsCorrectPlates()
        {
            _analyzer.AddPlate("ABC 123"); // Swedish plate
            _analyzer.AddPlate("AB 12345"); // Danish plate

            var swedishPlates = _analyzer.GetPlatesByCountry(Country.Sweden).ToList();
            Assert.AreEqual(1, swedishPlates.Count);
            Assert.Contains("ABC 123", swedishPlates);
        }

        [Test]
        public void GetPlates_WithOffset_ReturnsCorrectSubset()
        {
            for (int i = 0; i < 150; i++)
            {
                _analyzer.AddPlate($"ABC {i:000}");
            }

            var plates = _analyzer.GetPlatesByCountry(Country.Sweden, 100, 50).ToList();
            Assert.AreEqual(50, plates.Count);
            Assert.Contains("ABC 100", plates);
            Assert.Contains("ABC 149", plates);
        }

        [Test]
        public void GetPlates_NonExistingCountry_ReturnsEmpty()
        {
            _analyzer.AddPlate("ABC 123"); // Swedish plate
            var plates = _analyzer.GetPlatesByCountry(Country.Unknown).ToList();
            Assert.IsEmpty(plates);
        }

        [Test]
        public void Reset_ClearsAllPlates()
        {
            _analyzer.AddPlate("XYZ 789");
            _analyzer.Reset();
            Assert.IsEmpty(_analyzer.GetUniquePlates());
        }

        [Test]
        public void AddPlate_DuplicatePlates_OnlyAddsUnique()
        {
            _analyzer.AddPlate("ABC 123");
            _analyzer.AddPlate("ABC 123"); // Duplicate
            var plates = _analyzer.GetUniquePlates().ToList();
            Assert.AreEqual(1, plates.Count);
        }

        [Test]
        public void GetPlates_LargeNumberOfPlates_HandlesCorrectly()
        {
            for (int i = 0; i < 1000; i++)
            {
                _analyzer.AddPlate($"DEF {i:000}");
            }
            var plates = _analyzer.GetPlatesByCountry(Country.Sweden, 0, 1000).ToList();
            Assert.AreEqual(1000, plates.Count);
        }

        [Test]
        public void GetPlates_MalformedPlates_ExcludesFromResults()
        {
            _analyzer.AddPlate("ABCD 123"); // Malformed plate
            _analyzer.AddPlate("XYZ 789"); // Correct format
            var plates = _analyzer.GetPlatesByCountry(Country.Sweden).ToList();
            Assert.AreEqual(1, plates.Count);
            Assert.Contains("XYZ 789", plates);
        }
    }
}