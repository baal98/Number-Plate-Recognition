# Number Plates Recognition

This repository houses the code for the Number Plates Recognition system, which is designed to process vehicle number plates from the Øresundsbroen bridge connecting Denmark and Sweden. The system reads number plates from text files, analyzes them to identify the country of origin, and writes subsets of the data to output files.

## Running the Solution

To run the solution:
1. Ensure all text files are located in the `data/cameras` directory as specified in the application.
2. Execute the `Program.cs` file, which will process the plates using the `PlateReader`, `PlateAnalyzer`, and `PlateWriter` components.

## Structure

The solution is divided into several namespaces:
- `PlateReader`: Handles reading number plates from text files.
- `PlateAnalyzer`: Analyzes plates to determine the number of unique plates and their countries.
- `PlateWriter`: Writes processed plate data to text files.

## Documentation

Further documentation is provided in the code comments, explaining the functionality of each component and method.

## Testing

The `Tests` project contains unit tests that validate the functionality of the system. To execute these tests, run the unit test cases provided within the project.

## Concurrency

The system is designed to process up to 500 text files concurrently, simulating a live data feed from multiple cameras. This is demonstrated in the `ProcessCameraFilesConcurrently` method in the `Program` class.

## Optional Tasks

- Task 5 details the testing requirements. A small dataset for testing edge cases is provided, and unit tests are included for solution integrity.
- Task 6 simulates concurrent data addition from multiple cameras to the analyzer.

## Contributing

Contributions are welcome. Please fork the repository, make your changes, and submit a pull request with your enhancements.

## License

This project is open-source and available under the [MIT License](LICENSE).
