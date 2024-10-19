//Prague parking

string[] parking = new string[100];

int[] spotNr = new int[101];
for (int i = 0; i < parking.Length; i++)
{
    spotNr[i] = i + 1;
}

int menu = 0;

/************** Main program loop ****************/

do
{
    Console.Clear();
    Console.WriteLine("=== Welcome to Prague Park ===");
    Console.WriteLine("\n[0] End Program");
    Console.WriteLine("[1] Add veichle");
    Console.WriteLine("[2] Remove veichle");
    Console.WriteLine("[3] Relocate veichle");
    Console.WriteLine("[4] Find veichle");
    Console.WriteLine("[5] Show Parking\n");
    Console.WriteLine("To use program:\nType in the number of one of the above functions,\nand then press the [ENTER] key to run it\n");

    menu = int.Parse(Console.ReadLine());
    switch (menu)
    {
        case 1:
            addVeichle();
            break;
        case 2:
            removeVeichle();
            break;
        case 3:
            relocateVeichle();
            break;
        case 4:
            findVeichle();
            break;
        case 5:
            showParking();
            break;
        case 0:
            Console.Clear();
            Console.WriteLine("Ending program...");
            break;
    }
} while (menu != 0);

/*************** Methods for operations *************/

//Park
void addVeichle()
{
    Console.Clear();
    Console.Write("Type in the registration plate for the veichle you'd like to park: ");
    string regPlate = Console.ReadLine();

    if (regPlate.Length > 10)
    {
        Console.WriteLine("\nThe registration plate can't have more than 10 characters in it");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("\nIs the veichle that you'd like to park a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type and then press the [ENTER] key: ");
    int veichleType = int.Parse(Console.ReadLine());
    if (veichleType == 1)
    {
        string car = "CAR";
        regPlate = car + "#" + regPlate;
    }
    else if (veichleType == 2)
    {
        string motorcycle = "MC";
        regPlate = motorcycle + "#" + regPlate;
    }
    else
    {
        Console.WriteLine("\nInvalid input");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    Console.Write("\nNow type in the number of the P-Spot that you'd like to park the car at: ");
    int moveTo = int.Parse(Console.ReadLine());

    if (veichleType == 2)
    {
        for (int i = 0; i < parking.Length; ++i)
        {
            if (spotNr[i] == moveTo && parking[i] != null)
            {
                string subString = parking[i].ToString();
                int searchMotorcycle = subString.IndexOf("MC#");
                int searchSeperator = subString.IndexOf("|");

                if (searchMotorcycle == -1)
                {
                    break;
                }
                else if (searchSeperator > -1)
                {
                    Console.WriteLine("\nThere's already two motorcycles parked at this spot");
                    Console.Write("\n\nPress random key to continue...");
                    Console.ReadKey();
                    return;
                }
                else if (searchSeperator == -1)
                {
                    for (int j = 0; j < parking.Length; j++)
                    {
                        if (parking[j] == regPlate)
                        {
                            parking[j] = null;
                        }
                    }
                    string joinedRegPlate = subString + "|" + regPlate;
                    parking[i] = joinedRegPlate;
                    Console.WriteLine(" ");
                    Console.WriteLine($"{regPlate} has been parked at P-Spot {spotNr[i]} with {subString}");
                    Console.Write("\n\nPress random key to continue...");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (spotNr[i] == moveTo && parking[i] != null)
        {
            Console.WriteLine("\nThere's already a car parked at this P-Spot");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
        else if (spotNr[i] == moveTo && parking[i] == null)
        {
            parking[i] = regPlate;
            Console.WriteLine(" ");
            Console.WriteLine($"{regPlate} has been parked at P-Spot {spotNr[i]}");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            break;
        }
    }
}

//Remove
void removeVeichle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the veichle that you'd like to remove: ");
    string regPlate = Console.ReadLine();

    Console.WriteLine("\nIs the veichle you'd like to remove a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type and then press the [ENTER] key: ");
    int veichleType = int.Parse(Console.ReadLine());
    if (veichleType == 1)
    {
        string car = "CAR";
        regPlate = car + "#" + regPlate;
    }
    else if (veichleType == 2)
    {
        string motorcycle = "MC";
        regPlate = motorcycle + "#" + regPlate;
    }
    else
    {
        Console.WriteLine("\nInvalid input");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    int plateFound = 0;
    for (int i = 0; i < parking.Length; ++i)
    {
        for (int j = 0; j < parking.Length; j++)
        {
            if (parking[j] == regPlate)
            {
                plateFound++;
            }
        }
        if (plateFound == 0)
        {
            Console.WriteLine("\nThere is no veichle with that registration plate");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (parking[i] == regPlate)
        {
            parking[i] = null;

            Console.WriteLine(" ");
            Console.WriteLine($"{regPlate} has been removed from P-Spot {spotNr[i]}");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            break;
        }
    }
}

//Move
void relocateVeichle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the veichle that you'd like to move: ");
    string regPlate = Console.ReadLine();

    Console.WriteLine("\nIs the veichle you'd like to move a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type and then press the [ENTER] key: ");
    int veichleType = int.Parse(Console.ReadLine());
    if (veichleType == 1)
    {
        string car = "CAR";
        regPlate = car + "#" + regPlate;
    }
    else if (veichleType == 2)
    {
        string motorcycle = "MC";
        regPlate = motorcycle + "#" + regPlate;
    }
    else
    {
        Console.WriteLine("\nInvalid input");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    int plateFound = 0;
    for (int i = 0; i < parking.Length; ++i)
    {
        for (int j = 0; j < parking.Length; j++)
        {
            if (parking[j] == regPlate)
            {
                plateFound++;
            }
        }
        if (plateFound == 0)
        {
            Console.WriteLine("\nThere is no veichle with that registration plate");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
    }

    Console.Write("\nNow type in the number of the P-Spot that you'd like to move it to: ");
    int moveTo = int.Parse(Console.ReadLine());

    if (veichleType == 2)
    {
        for (int i = 0; i < parking.Length; ++i)
        {
            if (spotNr[i] == moveTo && parking[i] != null)
            {
                string subString = parking[i].ToString();
                int searchMotorcycle = subString.IndexOf("MC#");
                int searchSeperator = subString.IndexOf("|");

                if (searchMotorcycle == -1)
                {
                    break;
                }
                else if (searchSeperator > -1)
                {
                    Console.WriteLine("\nThere's already two motorcycles parked at this P-Spot");
                    Console.Write("\n\nPress random key to continue...");
                    Console.ReadKey();
                    return;
                }
                else if (searchSeperator == -1)
                {
                    for (int j = 0; j < parking.Length; j++)
                    {
                        if (parking[j] == regPlate)
                        {
                            parking[j] = null;
                        }
                    }
                    string joinedRegPlate = subString + "|" + regPlate;
                    parking[i] = joinedRegPlate;
                    Console.WriteLine(" ");
                    Console.WriteLine($"{regPlate} has been moved to P-Spot {spotNr[i]} with {subString}");
                    Console.Write("\n\nPress random key to continue...");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }

    for (int i = 0; i < parking.Length; i++)
    {
        
        
        if (spotNr[i] == moveTo && parking[i] != null)
        {
            Console.WriteLine("\nThere's already a car parked at this P-Spot");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
        else if (spotNr[i] == moveTo && parking[i] == null)
        {
            for (int j = 0; j < parking.Length; j++)
            {
                if (parking[j] == regPlate)
                {
                    parking[j] = null;
                }
            }
            parking[i] = regPlate;
            Console.WriteLine(" ");
            Console.WriteLine($"{regPlate} has been parked at P-Spot {spotNr[i]}");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            break;
        }
    }
}

//Find
void findVeichle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the veichle that you'd like to find: ");
    string regPlate = Console.ReadLine();

    Console.WriteLine("\nIs the veichle you'd like to find a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type and then press the [ENTER] key: ");
    int veichleType = int.Parse(Console.ReadLine());
    if (veichleType == 1)
    {
        string car = "CAR";
        regPlate = car + "#" + regPlate;
    }
    else if (veichleType == 2)
    {
        string motorcycle = "MC";
        regPlate = motorcycle + "#" + regPlate;
    }
    else
    {
        Console.WriteLine("\nInvalid input");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    int plateFound = 0;
    for (int i = 0; i < parking.Length; ++i)
    {
        for (int j = 0; j < parking.Length; j++)
        {
            if (parking[j] == regPlate)
            {
                plateFound++;
            }
        }
        if (plateFound == 0)
        {
            Console.WriteLine("\nThere is no veichle with that registration plate");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (regPlate == parking[i])
        {
            Console.WriteLine(" ");
            Console.WriteLine($"{regPlate} can be found at P-Spot {spotNr[i]}");
            Console.Write("\n\nPress random key to return to continue...");
            Console.ReadKey();
            break;
        }
    }
}

//Show
void showParking()
{
    Console.Clear();

    int rowLength = 10;

    Console.WriteLine("Parking: \n");
    for (int i = 0; i < parking.Length; i++)
    {
        if (i % rowLength == 0 && i != 0)
        {
            Console.WriteLine();
        }

        if (parking[i] == null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (spotNr[i] < 10)
            {
                Console.Write("P-Spot " + "00" + spotNr[i]);
            }
            else if (spotNr[i] > 9 && spotNr[i] < 100)
            {
                Console.Write("P-Spot " + "0" + spotNr[i]);
            }
            else if (spotNr[i] == 100)
            {
                Console.Write("P-Spot " + spotNr[i]);
            }
            Console.ResetColor();
        }
        else if (parking[i] != null)
        {
            string subString = parking[i].ToString();
            int searchMotorcycle = subString.IndexOf("MC#");
            int searchSeperator = subString.IndexOf("|");

            if (searchMotorcycle == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(parking[i]);
                Console.ResetColor();
            }
            else if (searchSeperator > -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(parking[i]);
                Console.ResetColor();
            }
            else if (searchSeperator == -1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(parking[i]);
                Console.ResetColor();
            }
        }
        Console.Write(" ");
    }

    Console.Write("\n\nPress random key to continue...");
    Console.ReadKey();
}