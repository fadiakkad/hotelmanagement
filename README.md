To run the applcation, all what you need to do is to pull the code and run the app using this command in dotnet enviornment:

dotnet run -- --hotels hotels.json --bookings bookings.json


##Assumptions
SGL (Single Room) is assumed to accommodate 1 guest.
DBL (Double Room) is assumed to accommodate 2 guests.
If a room type is not SGL or DBL, the service does not support it. Any additional or unsupported room types will not be allocated or considered in the room assignment.
