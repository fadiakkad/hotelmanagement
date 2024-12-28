using System;
using System.Text.RegularExpressions;

public class InputCommandReader
{
    public static (string hotelId, string startDate, string endDate, string roomType) ParseAvailabilityCommand(string input)
    {
        // Define the regex pattern 
        var pattern = @"Availability\(([^,]+),\s*([\d-]+)(?:\s*-\s*([\d-]+))?,\s*([A-Za-z]+)\)";
        var match = Regex.Match(input, pattern);

        // Check if the regex pattern matches the input string
        if (match.Success)
        {
            string hotelId = match.Groups[1].Value.Trim();
            string startDate = match.Groups[2].Value.Trim();
            string endDate = match.Groups[3].Success ? match.Groups[3].Value.Trim() : startDate;
            string roomType = match.Groups[4].Value.Trim();

            // Handle date range 
            if (startDate.Contains("-"))
            {
                var dates = startDate.Split('-');
                startDate = dates[0].Trim();
                endDate = dates[1].Trim();
            }

            return (hotelId, startDate, endDate, roomType);
        }

        // Return null values if the regex doesn't match
        return (null, null, null, null);
    }

    public static (string hotelId, string startDate, string endDate, int numberOfPeople) ParseRoomTypesCommand(string input)
    {
        // Define the regex pattern 
        var pattern = @"RoomTypes\(([^,]+),\s*([\d-]+)(?:\s*-\s*([\d-]+))?,\s*(\d+)\)";
        var match = Regex.Match(input, pattern);

        // Check if the regex pattern matches the input string
        if (match.Success)
        {
            string hotelId = match.Groups[1].Value.Trim();
            string startDate = match.Groups[2].Value.Trim();
            string endDate = match.Groups[3].Success ? match.Groups[3].Value.Trim() : startDate;
            int numberOfPeople = int.Parse(match.Groups[4].Value.Trim());

            // Handle date range 
            if (startDate.Contains("-"))
            {
                var dates = startDate.Split('-');
                startDate = dates[0].Trim();
                endDate = dates[1].Trim();
            }

            return (hotelId, startDate, endDate, numberOfPeople);
        }

        // Return null values if the regex doesn't match
        return (null, null, null, -1); 
    }
}
