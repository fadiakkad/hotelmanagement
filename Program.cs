
class Program
{
    static void Main(string[] args)
    {

        string hotelsFilePath = null;
        string bookingsFilePath = null;

        // Parse arguments manually
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--hotels" && i + 1 < args.Length)
            {
                hotelsFilePath = args[i + 1];
            }
            else if (args[i] == "--bookings" && i + 1 < args.Length)
            {
                bookingsFilePath = args[i + 1];
            }
        }

        // Validate file paths
        if (string.IsNullOrEmpty(hotelsFilePath) || string.IsNullOrEmpty(bookingsFilePath))
        {
            Console.WriteLine("Error: You must provide file paths for both hotels and bookings.");
            return;
        }

        // Load data from the specified files
        Console.WriteLine("Loading data...");
        var hotels = FileReader.ReadJsonFile<List<Hotel>>(hotelsFilePath);
        var bookings = FileReader.ReadJsonFile<List<Booking>>(bookingsFilePath);
        Console.WriteLine("Data loaded successfully.");




        var availabilityService = new AvailabilityService(hotels, bookings);
        var roomAllocationService = new RoomAllocationService(hotels, bookings);

        Console.WriteLine("Enter commands:");
        string input;
        while (!string.IsNullOrEmpty(input = Console.ReadLine()))
        {
            try
            {
                if (input.StartsWith(InputCommandType.Availability.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    var (hotelId, startDate, endDate, roomType) = InputCommandReader.ParseAvailabilityCommand(input);
                    if (hotelId == null)
                    {
                        Console.WriteLine("Invalid Availability command format.");
                    }

                    var dateRange = ParseDateRange(startDate, endDate);


                    // Fetch room availability.
                    int availableRooms = availabilityService.GetAvailability(hotelId, dateRange, roomType);

                    Console.WriteLine(availableRooms);

                }
                else if (input.StartsWith(InputCommandType.RoomTypes.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    var (hotelId, startDate, endDate, numberOfPeople) = InputCommandReader.ParseRoomTypesCommand(input);
                    if (hotelId == null)
                    {
                        Console.WriteLine("Invalid RoomTypes command format.");
                    }
                    var dateRange = ParseDateRange(startDate, endDate);

                    // Allocate rooms based on the required number of people.
                    var allocatedRooms = roomAllocationService.AllocateRooms(hotelId, dateRange, numberOfPeople);

                    // Output the room allocation
                    Console.WriteLine(string.Join(", ", allocatedRooms));
                }


                else
                {
                    Console.WriteLine("Unrecognized command.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }


    // Parse the date range and return a DateRange object.
    static DateRange ParseDateRange(string startDate, string endDate)
    {
        // Assuming date format is "yyyyMMdd"
        DateTime start = DateTime.ParseExact(startDate, "yyyyMMdd", null);
        DateTime end = DateTime.ParseExact(endDate, "yyyyMMdd", null);

        // Return a DateRange object.
        return new DateRange { Start = start, End = end };
    }

}
