using System;

public class ParkingSpot
{
    public string RegNumber { get; set; }
    public string VehicleType { get; set; }
    public string CheckInTime { get; set; }

    public ParkingSpot(string regNumber, string vehicleType, DateTime checkInTime)
    {
 
        this.RegNumber = regNumber;
        this.VehicleType = vehicleType;
        this.CheckInTime = checkInTime.ToString();
    }

 

    public TimeSpan GetParkDuration()
    {
        return DateTime.Now - DateTime.Parse(CheckInTime);
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

            Console.WriteLine("======================");
            Console.WriteLine("   PRAGUE PARKING");
            Console.WriteLine("======================\n");

            Console.WriteLine("Choose option:");
            Console.WriteLine("1. Park Vehicle");
            Console.WriteLine("2. Retrieve/Remove Vehicle");
            Console.WriteLine("3. Move Vehicle");
            Console.WriteLine("4. Search");
            Console.WriteLine("5. Show Parking Spots");
            Console.WriteLine("6. Show Parking Spots | COLOURED-GRID |");
            Console.WriteLine("7. Show all registered vehicles");
            Console.WriteLine("8. Optimise parking");            
            Console.WriteLine("9. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter license number:");
                    string regNumber = Console.ReadLine();
                    Console.WriteLine("Enter vehicle type (Car/MC):");
                    string type = Console.ReadLine();
                    ParkVehicle(regNumber, type);
                    break;
                case "2":
                    Console.WriteLine("Enter license number:");
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
                    Console.WriteLine("Enter license number:");
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
                    OptimiseParking();
                    break;
                case "9":
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
            Console.WriteLine("License number cannot be longer than 10 characters.");
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

                parkingSpots[i] = new ParkingSpot(regNumber, type, DateTime.Now);
                Console.WriteLine($"{(type.ToLower() == "mc" ? "MC" : "Car")} {regNumber} parked on spot {i + 1}");
                Console.WriteLine($"Check in: {parkingSpots[i].CheckInTime:yyyy-MM-dd HH:mm:ss}");
                return;
            }
            else if (type.ToLower() == "mc" && parkingSpots[i].VehicleType == "mc" && i + 1 < parkingSpots.Length && parkingSpots[i + 1] == null)
            {

                parkingSpots[i + 1] = new ParkingSpot(regNumber, type, DateTime.Now);
                Console.WriteLine($"MC {regNumber} parked on spot {i + 2}");
                Console.WriteLine($"Check in: {parkingSpots[i + 1].CheckInTime:yyyy-MM-dd HH:mm:ss}");
                return;
            }
        }

        Console.WriteLine("No available parkingspots.");
    }

    static void RemoveVehicle(string regNumber)
    {
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null && parkingSpots[i].RegNumber == regNumber)
            {
                TimeSpan duration = parkingSpots[i].GetParkDuration();

                int days = duration.Days;
                int hours = duration.Hours;
                int minutes = duration.Minutes;


                string durationString = "";

                if (days > 0)
                {
                    durationString += $"{days} day(s) ";
                }

                if (hours > 0)
                {
                    durationString += $"{hours} hour(s) ";
                }

                durationString += $"{minutes} minute(s)";

                Console.WriteLine($"{regNumber} has been parked for {durationString}.");
                parkingSpots[i] = null;
                Console.WriteLine($"{regNumber} has been retrieved from spot {i + 1}.");
                return;
            }
            else if (parkingSpots[i] != null && parkingSpots[i].VehicleType == "mc" && parkingSpots[i].RegNumber.Contains(","))
            {
                var mcList = parkingSpots[i].RegNumber.Split(',');

                if (mcList[0].Contains(regNumber))
                {
                    parkingSpots[i].RegNumber = mcList[1].Trim();
                    Console.WriteLine("TRIMMED MC LIST 1 " + mcList);
                }
                else if (mcList[1].Contains(regNumber))
                {
                    parkingSpots[i].RegNumber = mcList[0].Trim();
                    Console.WriteLine("TRIMMED MC LIST 2 " + mcList);
                }

                TimeSpan duration = parkingSpots[i].GetParkDuration();

                int days = duration.Days;
                int hours = duration.Hours;
                int minutes = duration.Minutes;

                string durationString = "";

                if (days > 0)
                {
                    durationString += $"{days} day(s) ";
                }

                if (hours > 0)
                {
                    durationString += $"{hours} hour(s) ";
                }

                durationString += $"{minutes} minute(s)";

                Console.WriteLine($"{regNumber} has been parked for {durationString}.");
                Console.WriteLine($"MC {regNumber} has been retrieved from spot {i + 1}.");
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
            return;
        }

        if (parkingSpots[toSpot].VehicleType.ToLower() == "car")
        {
            Console.WriteLine("Park-spot busy. Cannot move to this spot.");
            return;
        }

        if (parkingSpots[toSpot].VehicleType.ToLower() == "mc" && parkingSpots[toSpot].RegNumber.Contains(","))
        {
            Console.WriteLine("Cannot move to this spot. There are already 2 MC or 1 car parked.");
            Console.WriteLine($"FROM {fromSpot}");
            Console.WriteLine($"T {toSpot}");
            return;
        }

        if (parkingSpots[toSpot].VehicleType.ToLower() == "mc" && parkingSpots[fromSpot].VehicleType.ToLower() == "mc")
        {
            Console.WriteLine($"Matching MC {parkingSpots[fromSpot].RegNumber} with MC {parkingSpots[toSpot].RegNumber}");
            parkingSpots[toSpot].RegNumber += $", {parkingSpots[fromSpot].RegNumber}";
            parkingSpots[fromSpot] = null;
            Console.WriteLine($"Moved MC to spot {toSpot + 1}.");
            Console.WriteLine($"FROM {fromSpot}");
            Console.WriteLine($"T {toSpot}");
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
                Console.WriteLine($"Spot {i + 1} | Free");
            }
            else
            {
                Console.WriteLine($"Spot {i + 1}: {parkingSpots[i].ToString()}");
            }
        }
    }

    static void ShowRegisteredVehicles()
    {
        Console.WriteLine("Active parkings: ");
        bool foundAnyVehicle = false;

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null)
            {
                Console.WriteLine($" {parkingSpots[i]}: Park-spot {i + 1}");
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

        Console.WriteLine("Parking grid - Compact view (Green = Free," +
            "Yellow = 1/2 full, Red = Busy)\n");

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
            else if (parkingSpots[i].VehicleType.ToLower() == "mc" && !parkingSpots[i].RegNumber.Contains(","))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.Write($"[{i + 1:D3}] "); // Formatting integer
            Console.ResetColor();
        }

        Console.WriteLine("\n");
    }

    static void OptimiseParking()
    {
        int spaceOne = 0;
        int spaceTwo = 0;
        string mcOne = "";
        string mcTwo = "";
        ParkingSpot[] carPark = parkingSpots;

        for (int i = 0; i < carPark.Length; i++)
        {
            if (carPark[i] == null)
            {
                continue;
            }
            else if (carPark[i].VehicleType.Contains("mc") && !carPark[i].RegNumber.Contains(','))
            {
                spaceOne = i;
                mcOne = carPark[i].RegNumber;

                for (int j = i + 1 ; j < carPark.Length; j++)
                {
                    if (carPark[j] == null)
                    {
                        break;
                    } 
                    else
                    {
                        if (carPark[j].VehicleType.Contains("mc") && !carPark[j].RegNumber.Contains(','))
                        {
                            spaceTwo = j;
                            mcTwo = carPark[j].RegNumber;
                            break;
                        }
                        else if (carPark[j].VehicleType.Contains("mc") && carPark[j].RegNumber.Contains(',') || carPark[j] == null)
                        {
                            Console.WriteLine("none have been moved");
                            break;
                        }
                    }
                }
                break;
            }
        }

       

        if (mcTwo != "")
        {
            string optimisedParking = carPark[spaceOne].RegNumber + ", " + carPark[spaceTwo];
            carPark[spaceOne].RegNumber = optimisedParking;
            carPark[spaceOne].VehicleType = "mc";
            carPark[spaceOne].CheckInTime = DateTime.Now.ToString();
            Console.WriteLine($"You have moved mc with registration {mcTwo}");
            Console.WriteLine($"From space: {spaceTwo + 1} to space: {spaceOne + 1}.");
            carPark[spaceTwo] = null;
        }
        else
        {
            Console.WriteLine("No mc's have been moved...");
          
        }
    }
}
