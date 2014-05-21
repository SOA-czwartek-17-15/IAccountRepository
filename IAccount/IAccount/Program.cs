using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Configuration;
using Contracts;
using System.Timers;
using System.IO;
using log4net;


namespace AccountRepository
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            log.Info("Program started");
            try
            {
                var runner = new ServiceRunner();
                runner.RunService();
            }
            catch (Exception ex)
            {
                log.Error("fatal error ", ex);
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }


    }
}


