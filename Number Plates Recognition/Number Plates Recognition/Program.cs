namespace Number_Plates_Recognition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Single file processing
                ProcessSingleFile();

                // Concurrent processing of multiple files
                ProcessCameraFilesConcurrently();
            }
            catch (IOException e)
            {
                Console.WriteLine("Error while reading file: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        static void ProcessSingleFile()
        {
            var reader = new PlateReader(@"..\..\..\..\..\data\plates.txt");
            var analyzer = new PlateAnalyzer();
            var writer = new PlateWriter();

            foreach (var plate in reader.ReadPlates())
            {
                analyzer.AddPlate(plate);
            }

            var uniqueSwedishPlates = analyzer.GetPlatesByCountry(Country.Sweden);
            writer.WriteToFile(@"..\..\..\..\..\data\swedish_plates.txt", uniqueSwedishPlates);
        }

        static void ProcessCameraFilesConcurrently()
        {
            var analyzer = new PlateAnalyzer();
            string camerasDir = @"..\..\..\..\..\data\cameras\";

            var tasks = new Task[500];
            for (int i = 0; i < 500; i++)
            {
                string filePath = Path.Combine(camerasDir, $"camera_{i}.txt");
                tasks[i] = Task.Run(() => ProcessFile(filePath, analyzer));
            }

            Task.WhenAll(tasks).Wait();
        }

        static void ProcessFile(string filePath, PlateAnalyzer analyzer)
        {
            var reader = new PlateReader(filePath);
            foreach (var plate in reader.ReadPlates())
            {
                analyzer.AddPlate(plate);
            }
        }
    }
}
