// Prague Parking

string[] parkingSpots = new string[101];

void Main()
{
    ShowMenu();
}

void ShowMenu() // Metoden för menyhantering
{
    bool running = true;
    while (running)
    {
        Console.WriteLine("Välj ett alternativ:");
        Console.WriteLine("1. Parkera");
        Console.WriteLine("2. Hämta ut ditt fordon");
        Console.WriteLine("3. Flytta till annan P-plats");
        Console.WriteLine("4. Sök");
        Console.WriteLine("5. Visa parkeringsplatser");
        Console.WriteLine("6. Avsluta");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Ange regnummer:");
                string regNumber = Console.ReadLine();
                Console.WriteLine("Ange fordonstyp (MC/Bil):");
                string type = Console.ReadLine();
                ParkVehicle(regNumber, type);
                break;
            case "2":
                Console.WriteLine("Ange regnummer:");
                // string regNumberRemove = Console.ReadLine();
                // RemoveVehicle(regNumberRemove);
                break;
            case "3":
                Console.WriteLine("Ange startplats:");
                int fromSpot = int.Parse(Console.ReadLine());
                Console.WriteLine("Ange slutplats:");
                //int toSpot = int.Parse(Console.ReadLine());
                // MoveVehicle(fromSpot, toSpot);
                break;
            case "4":
                Console.WriteLine("Ange regnummer:");
                // string searchRegNumber = Console.ReadLine();
                //FindVehicle(searchRegNumber);
                break;
            case "5":
                ShowParkingSpots(); // Metod för att visa både lediga och upptagna
                break;
            case "6":
                running = false;
                break;
            default:
                Console.WriteLine("Ogiltligt val.");
                break;
        }
    }
}


void ParkVehicle(string regNumber, string type)
{
    if (regNumber.Length > 10)
    {
        Console.WriteLine("Regnummer kan inte vara längre än 10 tecken.");
        return;
    }

    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] == null)
        {
            parkingSpots[i] = $"{regNumber} ({type})";
            Console.WriteLine($"Fordon {regNumber} parkerad på plats {i + 1}");
            return;
        }
        else if (type.ToLower() == "mc" && parkingSpots[i].Contains("mc") && !parkingSpots[i].Contains(","))
        {
            parkingSpots[i] += $", {regNumber} ({type})";
            Console.WriteLine($"MC {regNumber} parkerad på plats {i + 1}");
            return;
        }
    }
    Console.WriteLine("Alla P-platser är upptagna.");
}

void ShowParkingSpots()
{
    for (int i = 0; i < parkingSpots.Length; i++)
    {
        if (parkingSpots[i] == null)
        {
            Console.WriteLine($"Plats {i + 1}: Ledig");
        }
        else
        {
            Console.WriteLine($"Plats {i + 1}: {parkingSpots[i]}");
        }
    }
}