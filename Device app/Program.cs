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
    static List<string> DEVICECATEGORIES = new List<string> { "Laptop", "Desktop", "Other"};
    static int laptopCounter = 0;
    static int desktopCounter = 0;
    static int otherCounter = 0;
    static string mostExpensiveDevice = "";
    static decimal mostExpensiveCost = -1;
    static void Main(string[] args)
    {
        //local variables 
        char Nextdevice = 'y';
        DEVICECATEGORIES.AsReadOnly();

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
        //display total device summary
        string totalDeviceSummary = "$$$$$$$$$ Final Devices Summary $$$$$$$$$\n";
        totalDeviceSummary += $"Total Number Of Laptops: {laptopCounter}\n";
        totalDeviceSummary += $"Total Number Of Desktops: {desktopCounter}\n";
        totalDeviceSummary += $"Total Number Of Others: {otherCounter}\n";
        totalDeviceSummary += $"Most Expensive Device Name: {mostExpensiveDevice}\n";
        totalDeviceSummary += $"Most Expensive Device Cost: {mostExpensiveCost}\n";
        Console.WriteLine(totalDeviceSummary);

        
        
    }
    static void OneDevice()
    {
        //local variables 
        int quantityOfDevice = -1;
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
        quantityOfDevice = Convert.ToInt32(Console.ReadLine());

        //device price 
        Console.WriteLine("Please input device price");
        devicePrice = Convert.ToDecimal(Console.ReadLine());

        //device catergory
        deviceCatergory = CheckDevicecatergory();

        //increase device counters
        if (deviceCatergory == "Laptop")
        {
         laptopCounter += quantityOfDevice;
        }
        else
        if (deviceCatergory == "Desktop")
        {
         desktopCounter += quantityOfDevice;
        }
        else
        if (deviceCatergory == "Other")
        {
         otherCounter += quantityOfDevice;
        }

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

        //determine if the device is most expensive device
        if (insuranceAmount > mostExpensiveCost)
        {
            mostExpensiveDevice = $"{deviceName}";
            mostExpensiveCost = insuranceAmount;
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
            if (DEVICECATEGORIES.Contains(Devicecatergory) && !string.IsNullOrEmpty(Devicecatergory))
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


