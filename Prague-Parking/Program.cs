//Prague parking

string[] parking = new string[100];

int[] slotNr = new int[101];
for (int i = 0; i < parking.Length; i++)
{
    slotNr[i] = i + 1;
}

int menu = 0;

/************** Main program loop ****************/

do
{
    Console.Clear();
    Console.WriteLine("[0] End Program");
    Console.WriteLine("[1] Add veichle");
    Console.WriteLine("[2] Remove veichle");
    Console.WriteLine("[3] Relocate veichle");
    Console.WriteLine("[4] Find veichle");
    Console.WriteLine("[5] Show Parking");

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
            Console.WriteLine("Ending program");
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
    
    Console.WriteLine("Is the veichle you'd like to park a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type: ");
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
        Console.WriteLine("Invalid input");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    Console.Write("Now type in the number of the slot that you'd like to park the car at: ");
    int moveTo = int.Parse(Console.ReadLine());

    if (veichleType == 2)
    {
        for (int i = 0; i < parking.Length; ++i)
        {
            if (slotNr[i] == moveTo && parking[i] != null)
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
                    Console.WriteLine("There's already two motorcycles parked at this spot");
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
                    Console.WriteLine($"{regPlate} has been parked at slot {slotNr[i]} with {subString}");
                    Console.Write("\n\nPress random key to continue...");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (slotNr[i] == moveTo && parking[i] != null)
        {
            Console.WriteLine("There's already a car parked at this spot");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
        else if (slotNr[i] == moveTo && parking[i] == null)
        {
            parking[i] = regPlate;
            Console.WriteLine($"{regPlate} has been parked at slot {slotNr[i]}");
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

    Console.WriteLine("Is the veichle you'd like to remove a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type: ");
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
        Console.WriteLine("Invalid input");
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
            Console.WriteLine("There is no veichle with that registration plate");
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

            Console.WriteLine($"{regPlate} has been removed from slot {slotNr[i]}");
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

    Console.WriteLine("Is the veichle you'd like to move a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type: ");
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
        Console.WriteLine("Invalid input");
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
            Console.WriteLine("There is no veichle with that registration plate");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
    }

    Console.Write("Now type in the number of the spot that you'd like to move it to: ");
    int moveTo = int.Parse(Console.ReadLine());

    if (veichleType == 2)
    {
        for (int i = 0; i < parking.Length; ++i)
        {
            if (slotNr[i] == moveTo && parking[i] != null)
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
                    Console.WriteLine("There's already two motorcycles parked at this spot");
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
                    Console.WriteLine($"{regPlate} has been moved to slot {slotNr[i]} with {subString}");
                    Console.Write("\n\nPress random key to continue...");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }

    for (int i = 0; i < parking.Length; i++)
    {
        
        
        if (slotNr[i] == moveTo && parking[i] != null)
        {
            Console.WriteLine("There's already a car parked at this spot");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
        else if (slotNr[i] == moveTo && parking[i] == null)
        {
            for (int j = 0; j < parking.Length; j++)
            {
                if (parking[j] == regPlate)
                {
                    parking[j] = null;
                }
            }
            parking[i] = regPlate;
            Console.WriteLine($"{regPlate} has been parked at slot {slotNr[i]}");
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

    Console.WriteLine("Is the veichle you'd like to find a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type: ");
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
        Console.WriteLine("Invalid input");
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
            Console.WriteLine("There is no veichle with that registration plate");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            return;
        }
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (regPlate == parking[i])
        {
            Console.WriteLine($"{regPlate} can be found at Slot {slotNr[i]}");
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
    Console.WriteLine("Parking: ");
    Console.WriteLine(" ");
    for (int i = 0; i < parking.Length; i++)
    {   
        //Console.ForegroundColor = ConsoleColor.Red;
        //Console.Write(parking[i]);
        //Console.ResetColor();

        if (parking[i] == null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Slot " + slotNr[i]);
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