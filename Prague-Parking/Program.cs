// Prague Parking //

string[] parkingSpots = new string[101];
Random random = new Random();

void Main()
{
    ShowMenu();
}

void ShowMenu()
{
    bool running = true;
    while (running)
    {
        Console.Clear();

        Console.WriteLine("======================");
        Console.WriteLine("   PRAGUE PARKING");
        Console.WriteLine("======================\n");

        Console.WriteLine("Choose option:");
        Console.WriteLine("1. Park Vehicle:");
        Console.WriteLine("2. Retrieve/Remove Vehicle:");
        Console.WriteLine("3. Move Vehicle:");
        Console.WriteLine("4. Search:");
        Console.WriteLine("5. Show Parking Spots:");
        Console.WriteLine("6. Show all registered vehicles:");
        Console.WriteLine("7. Exit");

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

            /*case "1": // Random parkering
                Console.WriteLine("Enter license number:");
                string regNumber = Console.ReadLine();
                Console.WriteLine("Enter vehicle type (Car/MC):");
                string type = Console.ReadLine();
                ParkRandom(regNumber, type);
                break;*/
                
            case "2":
                Console.WriteLine("Enter license number:");
                string regNumberRemove = Console.ReadLine();
                RemoveVehicle(regNumberRemove);
                break;
            case "3":
                Console.WriteLine("Enter from spot:");
                int fromSpot = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter to spot:");
                int toSpot = int.Parse(Console.ReadLine());
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
                ShowRegisteredVehicles();
                break;
            case "7":
                running = false; // Close program
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }

        Console.WriteLine("\nPress enter to continue...");
        Console.ReadKey();
    }
}

void ParkVehicle(string regNumber, string type)
{
    if (regNumber.Length > 10)
    {
        Console.WriteLine("License number cannot be longer than 10 characters.");
        return;
    }

    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] == null)
        {
            parkingSpots[i] = $"{regNumber} ({type})";
            Console.WriteLine($"{regNumber} parked on spot {i + 1}");
            return;
        }
        else if (type.ToLower() == "mc" && parkingSpots[i].Contains("mc") && !parkingSpots[i].Contains(","))
        {
            parkingSpots[i] += $", {regNumber} ({type})";
            Console.WriteLine($"MC {regNumber} parked on spot {i + 1}");
            return;
        }
    }
    Console.WriteLine("No available parkingspots.");
}

// Extra method to random park
void ParkRandom(string regNumber, string type)
{
    if (regNumber.Length > 10)
    {
        Console.WriteLine("License number cannot be longer than 10 characters.");
        return;
    }

    bool isParked = false;
    int attempts = 0;

    while (!isParked && attempts < 101)
    {
        int randomSpot = random.Next(1, parkingSpots.Length);

        if (parkingSpots[randomSpot] == null)
        {
            parkingSpots[randomSpot] = $"{regNumber} ({type})";
            Console.WriteLine($"{regNumber} parked on {randomSpot + 1}.");
            isParked = true;
            return;
        }
        else if (type.ToLower() == "mc" && parkingSpots[randomSpot].Contains("mc") && !parkingSpots[randomSpot].Contains(","))
        {
            parkingSpots[randomSpot] += $", {regNumber} ({type})";
            Console.WriteLine($"MC {regNumber} parked on {randomSpot + 1}.");
            isParked = true;
            return;
        }
        attempts++;
    }

    if (!isParked)
    {
        Console.WriteLine("No available parkingspots.");
    }
}

void RemoveVehicle(string regNumber)
{
    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] != null && parkingSpots[i].Contains(regNumber))
        {
            if (parkingSpots[i].Contains(","))
            {
                parkingSpots[i] = parkingSpots[i].Replace($"{regNumber} (mc)", "").Trim().Trim(',');
            }
            else
            {
                parkingSpots[i] = null;
                Console.WriteLine($"{regNumber} has been retrieved from: Park-spot {i + 1}");
            }
            return;
        }
    }
    Console.WriteLine($"Vehicle with license {regNumber} not found.");
}

void MoveVehicle(int fromSpot, int toSpot)
{
    fromSpot--;
    toSpot--;

    if (fromSpot < 0 || fromSpot >= parkingSpots.Length || toSpot < 0 || toSpot >= parkingSpots.Length)
    {
        Console.WriteLine("Invalid licensenumber");
        return;
    }

    if (parkingSpots[fromSpot] == null)
    {
        Console.WriteLine("No vehicles at the park-spot.");
        return;
    }

    if (parkingSpots[toSpot] == null)
    {
        // Flyttar fordon till ny plats
        parkingSpots[toSpot] = parkingSpots[fromSpot];
        parkingSpots[fromSpot] = null;
        Console.WriteLine($"Vehicle moved from spot {fromSpot + 1} to spot {toSpot + 1}.");
    }
    else
    {
        Console.WriteLine("Park-spot is taken.");
    }
}


void FindVehicle(string regNumber)
{
    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] != null && parkingSpots[i].Contains(regNumber))
        {
            Console.WriteLine($"{regNumber} found: Park-spot {i + 1}.");
            return;
        }
    }
    Console.WriteLine($"{regNumber} not found.");
}

void ShowParkingSpots()
{
    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] == null)
        {
            Console.WriteLine($"Park-spot {i + 1}: Free");
        }
        else
        {
            Console.WriteLine($"Park-spot {i + 1}: {parkingSpots[i]}");
        }
    }
}

void ShowRegisteredVehicles()
{
    Console.WriteLine("Active parkings: ");
    bool foundAnyVehicle = false;

    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] != null)
        {
            Console.WriteLine(parkingSpots[i]);
            foundAnyVehicle = true;
        }
    }

    if (!foundAnyVehicle)
    {
        Console.WriteLine("No vehicle registered.");
    }
}
