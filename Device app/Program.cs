using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Xml.Linq;
using System.Xml.XPath;

class Program
{
    //global variables
    static string mostExpensiveDevice = "";
    static List<string> DeviceCategories = new List<string> { "Laptop", "Desktop", "Other"};

    static void Main(string[] args)
    {
        //local variables 
        char Nextdevice = 'y';

        // Display app titile
        Console.WriteLine("██████╗ ███████╗██╗   ██╗██╗ ██████╗███████╗     █████╗ ██████╗ ██████╗ \r\n██╔══██╗██╔════╝██║   ██║██║██╔════╝██╔════╝    ██╔══██╗██╔══██╗██╔══██╗\r\n██║  ██║█████╗  ██║   ██║██║██║     █████╗      ███████║██████╔╝██████╔╝\r\n██║  ██║██╔══╝  ╚██╗ ██╔╝██║██║     ██╔══╝      ██╔══██║██╔═══╝ ██╔═══╝ \r\n██████╔╝███████╗ ╚████╔╝ ██║╚██████╗███████╗    ██║  ██║██║     ██║     \r\n╚═════╝ ╚══════╝  ╚═══╝  ╚═╝ ╚═════╝╚══════╝    ╚═╝  ╚═╝╚═╝     ╚═╝     ");
        // Display app description
        Console.WriteLine("\n\nA simple and easy-to-use app designed for schools to calculate the cost of insuring student devices. Staff can enter\n details such as device type, value, and coverage options to get an estimated insurance cost, helping schools manage budgets and protect their equipment more effectively.");
        Console.WriteLine("\n\nPress <ENTER> to continue...");
        Console.ReadLine();

        Console.Clear();

        // Loop untill all devices have been inpuuted
        while (Nextdevice.Equals('y'))
        {

            // call OneDeivce method
            OneDevice();


            Nextdevice = CheckProceed();

            Console.Clear();

        }


    }


    static void OneDevice()
    {
        //local variables 
        decimal quantityOfDevice = -1m;
        decimal devicePrice = -1m;
        decimal insuranceAmount = -1m;
        string deviceCatergory = "";
        string deviceName = "";
        string deprecationSummary = "";
        decimal deprecationValue;
        string deviceSummary = "";

        Console.WriteLine("---------- Log Device ----------]\n");

        //user input the device name
        Console.WriteLine("Please input device name:");
        deviceName = Console.ReadLine();

        //user inputs device quantity amount
        Console.WriteLine("Please input a quantity");
        quantityOfDevice = Convert.ToDecimal(Console.ReadLine());

        //device price 
        Console.WriteLine("Please input device price");
        devicePrice = Convert.ToDecimal(Console.ReadLine());

        //device catergory
        deviceCatergory = CheckDevicecatergory();
        //calculate insurance amount
        if (quantityOfDevice > 5)
        {
            // first five will be insured at full cost
            insuranceAmount = 5 * devicePrice;
            insuranceAmount += (quantityOfDevice - 5) * 0.9m * devicePrice;

            //remaining devices will be insured at 10% less of cost


        }
        else
        {
            insuranceAmount = devicePrice * quantityOfDevice;

        }


        //calculate 5% Deprecation Over 6 Months
        deprecationValue = devicePrice;
        for (int monthCount = 1; monthCount < 7; monthCount++)
        {
            deprecationValue = deprecationValue * 0.95m;
            deprecationSummary += $"Month {monthCount}:\t\t{deprecationValue:C}\n";


        }
        //create device summary
        deviceSummary += $"{deviceName}\nTotal cost for {quantityOfDevice}  {deviceName} devices is = to {devicePrice:C}\nMonth\t\t\tvalue Loss\n{deprecationSummary} {deviceCatergory}: {devicePrice:C}";
        Console.WriteLine(deviceSummary);






    }

    static char CheckProceed()
    {
        while (true)
        {
            string proceed;
            Console.WriteLine("\n\nDo you want to add another device? (y/n)");
            proceed = Console.ReadLine();

            if (Regex.IsMatch(proceed, @"^[YNyn]+$") && !string.IsNullOrEmpty(proceed) && proceed.Length == 1)
            {
                return char.Parse(proceed[0].ToString().ToLower());
            }
            else
            {
                Console.WriteLine("Error: please use Y or N");
            }

        }
    }

    static string CheckDevicecatergory()
    {
        while (true)
        {
            string Devicecatergory;
            Console.WriteLine("Please input device catergory");
            Devicecatergory = Console.ReadLine();
            if (DeviceCategories.Contains(Devicecatergory) && !string.IsNullOrEmpty(Devicecatergory))
            {
                return Devicecatergory;
            }
            else
            {
                Console.WriteLine("Error please use valid device type");
            }

        }



    }
}


