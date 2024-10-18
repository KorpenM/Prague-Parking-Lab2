using System;

public class ParkingSpot
{
    public string RegNumber { get; set; }
    public string VehicleType { get; set; }
    public DateTime CheckInTime { get; set; }

    public ParkingSpot(string regNumber, string vehicleType)
    {
        RegNumber = regNumber;
        VehicleType = vehicleType;
        CheckInTime = DateTime.Now;
    }

    public TimeSpan GetParkDuration()
    {
        return DateTime.Now - CheckInTime;
    }

    public override string ToString()
    {
        return $"{RegNumber} ({VehicleType}) - Check in: {CheckInTime:yyyy-MM-dd HH:mm:ss}";
    }
}

class Program
{
    static ParkingSpot[] parkingSpots = new ParkingSpot[100];

    static void Main(string[] args)
    {
        ShowMenu();
    }

    static void ShowMenu()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Choose option:");
            Console.WriteLine("1. Park Vehicle");
            Console.WriteLine("2. Retrieve/Remove Vehicle");
            Console.WriteLine("3. Move Vehicle");
            Console.WriteLine("4. Search");
            Console.WriteLine("5. Show Parking Spots");
            Console.WriteLine("6. Show Parking Spots | COLOURED-GRID |");
            Console.WriteLine("7. Show all registered vehicles");
            Console.WriteLine("8. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter license plate:");
                    string regNumber = Console.ReadLine();
                    Console.WriteLine("Enter vehicle type (Car/MC):");
                    string type = Console.ReadLine();
                    ParkVehicle(regNumber, type);
                    break;
                case "2":
                    Console.WriteLine("Enter license plate:");
                    string regNumberRemove = Console.ReadLine();
                    RemoveVehicle(regNumberRemove);
                    break;
                case "3":
                    Console.WriteLine("Enter from spot:");
                    int fromSpot;
                    if (!int.TryParse(Console.ReadLine(), out fromSpot) || fromSpot <= 0 || fromSpot > parkingSpots.Length)
                    {
                        Console.WriteLine("Invalid spot number. Please try again.");
                        continue;
                    }

                    Console.WriteLine("Enter to spot:");
                    int toSpot;
                    if (!int.TryParse(Console.ReadLine(), out toSpot) || toSpot <= 0 || toSpot > parkingSpots.Length)
                    {
                        Console.WriteLine("Invalid spot number. Please try again.");
                        continue;
                    }

                    MoveVehicle(fromSpot, toSpot);
                    break;
                case "4":
                    Console.WriteLine("Enter license plate:");
                    string searchRegNumber = Console.ReadLine();
                    FindVehicle(searchRegNumber);
                    break;
                case "5":
                    ShowParkingSpots();
                    break;
                case "6":
                    ShowColorParkingSpots();
                    break;
                case "7":
                    ShowRegisteredVehicles();
                    break;
                case "8":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadKey();
        }
    }

    static void ParkVehicle(string regNumber, string type)
    {
        if (regNumber.Length > 10)
        {
            Console.WriteLine("License plate cannot be longer than 10 characters.");
            return;
        }

        while (type.ToLower() != "mc" && type.ToLower() != "car")
        {
            Console.WriteLine("Invalid vehicle type. Enter Car or MC: ");
            type = Console.ReadLine();
        }

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] == null)  // Om platsen är ledig
            {
                parkingSpots[i] = new ParkingSpot(regNumber, type);
                Console.WriteLine($"{(type.ToLower() == "mc" ? "MC" : "Car")} {regNumber} parked on spot {i + 1}");
                Console.WriteLine($"Check in: {parkingSpots[i].CheckInTime:yyyy-MM-dd HH:mm:ss}");
                return;
            }
        }

        Console.WriteLine("No available parking spots.");
    }

    static void RemoveVehicle(string regNumber)
    {
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null && parkingSpots[i].RegNumber == regNumber)
            {
                TimeSpan duration = parkingSpots[i].GetParkDuration();
                Console.WriteLine($"{regNumber} has been parked for {duration.TotalMinutes:F2} minutes.");
                parkingSpots[i] = null;
                Console.WriteLine($"{regNumber} has been retrieved from spot {i + 1}.");
                return;
            }
        }

        Console.WriteLine($"{regNumber} license - not found.");
    }

    static void MoveVehicle(int fromSpot, int toSpot)
    {
        fromSpot--;
        toSpot--;

        if (parkingSpots[fromSpot] == null)
        {
            Console.WriteLine("No vehicle registered on spot.");
            return;
        }

        if (parkingSpots[toSpot] == null)
        {
            parkingSpots[toSpot] = parkingSpots[fromSpot];
            parkingSpots[fromSpot] = null;
            Console.WriteLine($"Vehicle moved from spot {fromSpot + 1} to spot {toSpot + 1}.");
        }
        else
        {
            Console.WriteLine("Parking spot is taken.");
        }
    }

    static void FindVehicle(string regNumber)
    {
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null && parkingSpots[i].RegNumber == regNumber)
            {
                Console.WriteLine($"{regNumber} found: P-spot {i + 1}.");
                return;
            }
        }
        Console.WriteLine($"{regNumber} license - not found.");
    }

    static void ShowParkingSpots()
    {
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] == null)
            {
                Console.WriteLine($"Spot {i + 1} | Available");
            }
            else
            {
                Console.WriteLine($"Spot {i + 1}: {parkingSpots[i].ToString()}");
            }
        }
    }

    static void ShowRegisteredVehicles()
    {
        Console.WriteLine("Active parkings:");
        bool foundAnyVehicle = false;

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null)
            {
                Console.WriteLine($"{parkingSpots[i]}: P-spot {i + 1}");
                foundAnyVehicle = true;
            }
        }

        if (!foundAnyVehicle)
        {
            Console.WriteLine("No registered vehicles.");
        }
    }

    static void ShowColorParkingSpots()
    {
        int spotsPerRow = 10;

        Console.WriteLine("Compact view over parking grid (Green = Available, Yellow = 1/2 full, Red = Busy/Taken)\n");

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (i % spotsPerRow == 0 && i != 0)
            {
                Console.WriteLine();
            }

            if (parkingSpots[i] == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (parkingSpots[i].VehicleType.ToLower() == "mc")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.Write($"[{i + 1:D3}] ");
            Console.ResetColor();
        }

        Console.WriteLine("\n");
    }
}
