//Prague parking

string car = "";
string mc = "";
bool systemOn = true;
string[] carPark = new string[101];

/************** Main program loop ****************/

do
{
    //Menu choices and operations


    int choice = 0;

    int Menu(int choice)
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine("Park");
                Park();
                break;
            case 2:
                Console.WriteLine("Search");
                Search();
                break;
            case 3:
                Console.WriteLine("Show All");
                break;
            case 4:
                Console.WriteLine("Move");
                break;
            case 5:
                Console.WriteLine("Get / remove");
                break;
            case 6:
                Console.WriteLine("Shutdown");
                systemOn = false;
                Console.WriteLine("Good bye!");
                break;
            default:
                Console.WriteLine("nothing");
                break;
        }
       
        return choice;
    }



    Console.WriteLine("Please choose an option (1 - 6)");
    choice = int.Parse(Console.ReadLine());
    
    Menu(choice);

    /*************** Methods for operations *************/

    //Park
    void Park()
    {
        //Get vehicle registration number and type
        string reg = "";
        string type = "";
        string vehicle = "";

        Console.WriteLine("Please enter registration number:");
        reg = Console.ReadLine();
        Console.WriteLine("Is the vehicle a car or a mc? (type car or mc)");
        type = Console.ReadLine();

        //Concat strings with separator

        vehicle = reg + "#" + type;

        //Find space and park

        for (int i = 0; i < carPark.Length; i++)
        {
            if (carPark[i] == null)
            {
                carPark[i] = vehicle;
           
                Console.WriteLine($"You parked {type} with reg {reg} at space: {i}");
                break;
            }
        }

       


    }

    //Search car

    void Search()
    {

    }
    
} while (systemOn);

for (int i = 0;i < carPark.Length;i++)
{
    Console.WriteLine(i + " | " + carPark[i]);
}

