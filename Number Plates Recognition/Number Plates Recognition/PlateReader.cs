namespace Number_Plates_Recognition
{
    public class PlateReader
    {
        private string _filePath;

        public PlateReader(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<string> ReadPlates()
        {
            string line;
            using (var file = new StreamReader(_filePath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}