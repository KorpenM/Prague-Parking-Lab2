string[] parking = new string[100]; //A string-array that will be loaded with the registration plates that the user types in

int[] spotNr = new int[101]; //An array to help make sure that the first parking space is 1 and not 0
for (int i = 0; i < parking.Length; i++)
{
    spotNr[i] = i + 1;
}

int menu = 0; //Used with the menu to help determine which function to execute

//Menu
do
{
    Console.Clear();
    Console.WriteLine("=== Welcome to Prague Park ===");
    Console.WriteLine("\n[0] End Program");
    Console.WriteLine("[1] Add vehicle");
    Console.WriteLine("[2] Remove vehicle");
    Console.WriteLine("[3] Relocate vehicle");
    Console.WriteLine("[4] Find vehicle");
    Console.WriteLine("[5] Show Parking\n");
    Console.WriteLine("To use program:\nType in the number of one of the above functions,\nand then press the [ENTER] key to run it\n");

    menu = int.Parse(Console.ReadLine()); //Determines which switch-case to access depending on what value the user loads the int with
    switch (menu)
    {
        case 1:
            addVehicle();
            break;
        case 2:
            removeVehicle();
            break;
        case 3:
            relocateVehicle();
            break;
        case 4:
            findVehicle();
            break;
        case 5:
            showParking();
            break;
        case 0:
            Console.Clear();
            Console.WriteLine("Ending program...");
            break;
    }
} while (menu != 0); //The do-while loop and program ends when the user loads the int with a 0

//Park
void addVehicle()
{
    Console.Clear();
    Console.Write("Type in the registration plate for the veichle you'd like to park: ");
    string regPlate = Console.ReadLine();

    //Makes sure that user doesn't use more than 10 characters to type in a registration plate
    if (regPlate.Length > 10) 
    {
        Console.WriteLine("\nThe registration plate can't have more than 10 characters in it");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("\nIs the veichle that you'd like to park a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct veichle type: ");
    int veichleType = int.Parse(Console.ReadLine());
    
    //Joins the string for the registration plate with one of two strings depending on user input
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

    //Senses whether the veichle is a motorcycle
    if (veichleType == 2)
    {
        //A for-loop used to park motorcycles, and makes it possible to place two of them at the same spot
        for (int i = 0; i < parking.Length; ++i)
        {
            if (spotNr[i] == moveTo && parking[i] != null)
            {
                string subString = parking[i].ToString(); //Creates a substring from the parking-array
                int searchMotorcycle = subString.IndexOf("MC#"); //Used to find whether the substring is a motorcycel or not
                int searchSeperator = subString.IndexOf("|"); //Used to find whether the substring has a seperator or not

                if (searchMotorcycle == -1)
                {
                    break;
                }
                if (searchSeperator > -1)
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
                    string joinedRegPlate = subString + "|" + regPlate; //Creates a new string representing two motorcycles
                    parking[i] = joinedRegPlate; //Fill the current array position with the newlycreated string, parking the two motorcycles at the chosen spot
                    Console.WriteLine(" ");
                    Console.WriteLine($"{regPlate} has been parked at P-Spot {spotNr[i]} with {subString}");
                    Console.Write("\n\nPress random key to continue...");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }

    //A for-loop used to park cars, and can not be used to place more than one veichle at a spot
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
void removeVehicle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the veichle that you'd like to remove: ");
    string regPlate = Console.ReadLine();

    Console.WriteLine("\nIs the veichle you'd like to remove a car [1] or a motorcycle [2]?");
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
        Console.WriteLine("\nInvalid input");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (parking[i] != null)
        {
            string subString = parking[i].ToString();
            int searchSeperator = subString.IndexOf("|");

            if (searchSeperator > -1)
            {
                string[] splitStrings = subString.Split("|");
                if (splitStrings[0] == regPlate)
                {
                    parking[i] = splitStrings[1];
                    Console.WriteLine(" ");
                    Console.WriteLine($"{regPlate} has been removed from P-Spot {spotNr[i]}");
                    Console.Write("\n\nPress random key to return to continue...");
                    Console.ReadKey();
                    return;
                }
                else if (splitStrings[1] == regPlate)
                {
                    parking[i] = splitStrings[0];
                    Console.WriteLine(" ");
                    Console.WriteLine($"{regPlate} has been removed from P-Spot {spotNr[i]}");
                    Console.Write("\n\nPress random key to return to continue...");
                    Console.ReadKey();
                    return;
                }
            }
        }

        if (parking[i] == regPlate)
        {
            parking[i] = null; //Assings the spot in the array where the veichle is parked at with the value null

            Console.WriteLine(" ");
            Console.WriteLine($"{regPlate} has been removed from P-Spot {spotNr[i]}");
            Console.Write("\n\nPress random key to continue...");
            Console.ReadKey();
            break;
        }
    }
    Console.WriteLine("\nThere is no veichle with that registration plate");
    Console.Write("\n\nPress random key to continue...");
    Console.ReadKey();
}

//Move
void relocateVehicle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the vehicle that you'd like to move: ");
    string regPlate = Console.ReadLine();

    Console.WriteLine("\nIs the vehicle you'd like to move a car [1] or a motorcycle [2]?");
    Console.Write("Type in the number of the correct vehicle type: ");
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

    Console.Write("\nNow type in the number of the P-Spot that you'd like to move it to: ");
    int moveTo = int.Parse(Console.ReadLine());

    if (veichleType == 2)
    {
        for (int i = 0; i < parking.Length; ++i)
        {
            if (parking[i] != null)
            {
                string subString = parking[i].ToString();
                int searchSeperator = subString.IndexOf("|");

                if (searchSeperator > -1)
                {
                    string[] splitStrings = subString.Split("|");
                    string joinStrings = splitStrings[0] + "|" + splitStrings[1];

                    if (splitStrings[0] == regPlate)
                    {
                        for (int j = 0; j < parking.Length; j++)
                        {
                            if (spotNr[j] == moveTo)
                            {
                                parking[j] = splitStrings[0];
                            }
                        }
                        parking[i] = splitStrings[1];
                        Console.WriteLine(" ");
                        Console.WriteLine($"{regPlate} has been moved to P-Spot {spotNr[moveTo-1]}");
                        Console.Write("\n\nPress random key to continue...");
                        Console.ReadKey();
                        return;
                    }
                    else if (splitStrings[1] == regPlate)
                    {
                        for (int j = 0; j < parking.Length; j++)
                        {
                            if (spotNr[j] == moveTo)
                            {
                                parking[j] = splitStrings[1];
                            }
                        }
                        parking[i] = splitStrings[0];
                        Console.WriteLine(" ");
                        Console.WriteLine($"{regPlate} has been moved to P-Spot {spotNr[moveTo-1]}");
                        Console.Write("\n\nPress random key to continue...");
                        Console.ReadKey();
                        return;
                    }
                }
            }
            
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
            return;
        }
    }
    Console.WriteLine("\nThere is no vehicle with that registration plate");
    Console.Write("\n\nPress random key to continue...");
    Console.ReadKey();
}

//Find
void findVehicle()
{
    Console.Clear();
    Console.Write("Type in the registration plate of the vehicle that you'd like to find: ");
    string regPlate = Console.ReadLine();

    Console.WriteLine("\nIs the vehicle you'd like to find a car [1] or a motorcycle [2]?");
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
        Console.WriteLine("\nInvalid input");
        Console.Write("\n\nPress random key to continue...");
        Console.ReadKey();
        return;
    }

    for (int i = 0; i < parking.Length; i++)
    {
        if (parking[i] != null)
        {
            string subString = parking[i].ToString();
            int searchSeperator = subString.IndexOf("|");

            if (searchSeperator > -1)
            {
                string[] splitStrings = subString.Split("|");
                if (splitStrings[0] == regPlate)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine($"{regPlate} can be found at P-Spot {spotNr[i]} with {splitStrings[1]}");
                    Console.Write("\n\nPress random key to return to continue...");
                    Console.ReadKey();
                    return;
                }
                else if (splitStrings[1] == regPlate)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine($"{regPlate} can be found at P-Spot {spotNr[i]} with {splitStrings[0]}");
                    Console.Write("\n\nPress random key to return to continue...");
                    Console.ReadKey();
                    return;
                }
            }
        }
        
        if (regPlate == parking[i])
        {
            Console.WriteLine(" ");
            Console.WriteLine($"{regPlate} can be found at P-Spot {spotNr[i]}");
            Console.Write("\n\nPress random key to return to continue...");
            Console.ReadKey();
            return;
        }
    }
    Console.WriteLine("\nThere is no vehicle with that registration plate");
    Console.Write("\n\nPress random key to continue...");
    Console.ReadKey();
}

//Show
void showParking()
{
    Console.Clear();

    int rowLength = 10; //Used to help make sure that a new row is made when 10 values are shown

    Console.WriteLine("Parking: \n");

    for (int i = 0; i < parking.Length; i++)
    {
        if (i % rowLength == 0 && i != 0)
        {
            Console.WriteLine();
        }

        if (parking[i] == null) //Assigns open spots the color green
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
        else if (parking[i] != null) //Assigns parked spots either the color red or yellow, depending on whether there is room for other veichles to be parked there or not
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