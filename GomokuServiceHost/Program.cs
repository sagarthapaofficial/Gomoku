using System;
using System.ServiceModel;
using GomokuLibrary;

namespace GomokuServiceHost
{
    /**
      * Class Name: Program.cs		
      * Purpose: A multiplayer game application which incorporates multiple assemblies and WCF
      * Coders: Haris Khalid, Woojin Shin, Sterling Gault, Sagar Thapa
      * Date: Apr 3rd, 2022
      */
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost servHost = null;
            try
            {
                //servHost configs are implemented in app.config file
                servHost = new ServiceHost(typeof(Game));

                // Run the service
                servHost.Open();

                Console.WriteLine("Service started. Press any key to quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Wait for a keystroke
                Console.ReadKey();
                servHost?.Close();
            }
        }
    }
}