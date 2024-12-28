using System;
using System.Text.Json.Serialization;

public class Booking
{
    [JsonPropertyName("hotelId")]
    public string HotelId { get; set; }

    [JsonPropertyName("arrival")]
    [JsonConverter(typeof(DateTimeConverter))]

    public DateTime Arrival { get; set; }

    [JsonPropertyName("departure")]
    [JsonConverter(typeof(DateTimeConverter))]

    public DateTime Departure { get; set; }

    [JsonPropertyName("roomType")]
    public string RoomType { get; set; }

    [JsonPropertyName("roomRate")]
    public string RoomRate { get; set; }
}
