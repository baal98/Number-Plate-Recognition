namespace Number_Plates_Recognition
{
    public class PlateWriter
    {
        public void WriteToFile(string filename, IEnumerable<string> plates)
        {
            using (var file = new StreamWriter(filename))
            {
                foreach (var plate in plates)
                {
                    file.WriteLine(plate);
                }
            }
        }
    }
}