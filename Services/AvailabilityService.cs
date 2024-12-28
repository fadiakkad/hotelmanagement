public class AvailabilityService
{
    private List<Hotel> _hotels;
    private List<Booking> _bookings;

    public AvailabilityService(List<Hotel> hotels, List<Booking> bookings)
    {
        _hotels = hotels;
        _bookings = bookings;
    }

    public int GetAvailability(string hotelId, DateRange dateRange, string roomType)
    {
        Console.WriteLine($"Checking availability for hotel {hotelId}, room type {roomType}, from {dateRange.Start} to {dateRange.End}");

        var hotel = _hotels.FirstOrDefault(h => h.Id == hotelId);
        if (hotel == null) throw new Exception("Hotel not found!");

        // Count rooms of the requested type
        int totalRooms = hotel.Rooms.Count(r => r.RoomType == roomType);

        // Count booked rooms
        int bookedRooms = _bookings
            .Where(b => b.HotelId == hotelId && b.RoomType == roomType)
            .Count(b => new DateRange { Start = b.Arrival, End = b.Departure }.Overlaps(dateRange));
        
        return totalRooms - bookedRooms;
    }
}
