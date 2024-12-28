using System.Text.Json.Serialization;
using System.Collections.Generic;

public class Hotel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("roomTypes")]
    public List<RoomType> RoomTypes { get; set; }

    [JsonPropertyName("rooms")]
    public List<Room> Rooms { get; set; }
}

public class RoomType
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string RoomId { get; set; }

    [JsonPropertyName("roomType")]
    public string RoomType { get; set; }
}
