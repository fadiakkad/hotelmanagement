public class RoomAllocationService
{
    private List<Hotel> _hotels;
    private List<Booking> _bookings;

    public RoomAllocationService(List<Hotel> hotels, List<Booking> bookings)
    {
        _hotels = hotels;
        _bookings = bookings;
    }

    public List<string> AllocateRooms(string hotelId, DateRange dateRange, int numberOfPeople)
    {
        var hotel = _hotels.FirstOrDefault(h => h.Id == hotelId);
        if (hotel == null) throw new Exception("Hotel not found!");

        var roomTypes = hotel.RoomTypes.OrderByDescending(rt => GetCapacityForRoomType(rt.Code)).ToList(); // Sort by capacity
        var allocatedRooms = new List<string>();
        int remainingPeople = numberOfPeople;

        foreach (var roomType in roomTypes)
        {
            // Count booked rooms for this room type and date range
            int bookedRooms = _bookings
                .Where(b => b.RoomType == roomType.Code)
                .Count(b => new DateRange { Start = b.Arrival, End = b.Departure }.Overlaps(dateRange));

            // Calculate available rooms by subtracting booked rooms from total available rooms
            int availableRooms = hotel.Rooms.Count(r => r.RoomType == roomType.Code) - bookedRooms;

            while (availableRooms > 0 && remainingPeople > 0)
            {
                int roomCapacity = GetCapacityForRoomType(roomType.Code);
                if (remainingPeople >= roomCapacity)
                {
                    allocatedRooms.Add(roomType.Code);  // Full room
                    remainingPeople -= roomCapacity;
                }
                else
                {
                    allocatedRooms.Add(roomType.Code + "!");  // Partial room
                    remainingPeople = 0;
                }
                availableRooms--;
            }
        }

        if (remainingPeople > 0)
            throw new Exception("Unable to allocate rooms for all guests.");

        return allocatedRooms;
    }
    private int GetCapacityForRoomType(string roomType)
    {
        return roomType switch
        {
            "SGL" => 1,
            "DBL" => 2,
            _ => 1 // Default capacity
        };
    }

}
