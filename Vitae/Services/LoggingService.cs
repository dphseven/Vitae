namespace Vitae.Services
{
    using System;
    using System.IO;

    public class LoggingService : ILoggingService
    {
        string logFilePath = @"C:\ProgramData\Vitae\Log.txt";

        public LoggingService() 
        {

        }

        public void Log(Exception e, string message = "") 
        {
            if (!File.Exists(logFilePath))
            {
                if (!Directory.Exists(@"C:\ProgramData\Vitae\"))
                {
                    Directory.CreateDirectory(@"C:\ProgramData\Vitae\");
                }
                using (StreamWriter sw = File.CreateText(logFilePath))
                {
                    sw.WriteLine("VITAE LOG FILE");
                    sw.WriteLine("--------------");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine();
                }
            }
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine();

                DateTime now = DateTime.Now;
                sw.WriteLine("Date/Time: " + now.ToLongDateString() + " at " + now.ToLongTimeString());
                sw.WriteLine("Message: " + message);
                sw.WriteLine("Exception Message: " + e.Message);
                sw.WriteLine("Exception Source: " + e.Source);
                sw.WriteLine("Exception Stack Trace: " + e.StackTrace);
                sw.WriteLine("Exception Target Site Name: " + e.TargetSite.Name);
                sw.WriteLine(); 
            }
        }
    }
}