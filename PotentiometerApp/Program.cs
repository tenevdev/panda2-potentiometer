using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Idiot.Net;
using Idiot.DataTypes;

namespace PotentiometerApp
{
    public class Program
    {
        public static void Main()
        {
            // InitializeDisplay();
            // DisplayStatus(0, "Potentiometer value:");

            AnalogIn pot = new AnalogIn((AnalogIn.Pin)FEZ_Pin.AnalogIn.An2);

            Credentials auth = new Credentials("admin", "admin");
            string hubId = "54f239b1801d90881e9d15be";
            DataPointService dataPoints = new DataPointService(auth, "adminProject", hubId, 5000);
            dataPoints.ConfigureForDevelopment("");

            // Start communications
            dataPoints.Start();

            while (true)
            {
                // Measure potentiometre value
                int potValue = pot.Read();

                // Display current value on screen
                // DisplayStatus(1, FormatValue(potValue));

                // Push current value to the data service
                Number number = new Number(potValue);
                dataPoints.PushDataPoint(number);

                // Wait before next measurement
                Thread.Sleep(3000);
            }
        }
    }
}
