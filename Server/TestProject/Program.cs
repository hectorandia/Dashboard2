using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server;
using System.ServiceProcess;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            FileShare file = new FileShare("Hector-Notebook");
            Worker worker = new Worker("Hector-Notebook");
            Capture capture = new Capture("Hector-Notebook");

            //FileShare Test
            Console.WriteLine(file.GetMessage("D:\\Archivo\\"));
            Console.WriteLine(file.GetEmptyFolder("D:\\Archivo\\"));
            
            //Worker Performance Test
            Console.WriteLine(worker.GetCpuUsage());
            Console.WriteLine(worker.GetRamUsage());

            //Worker HDD Test
            foreach(string hd in worker.GetLogicalDisk())
            {
                Console.WriteLine(hd);
            }

            //Worker Services Test
            worker.LoadAllServices();
            Console.WriteLine(worker.CheckStoppedServices());
            foreach(ServiceController service in worker.GetNamesStoppedServices())
            {
                Console.WriteLine(service.ServiceName + " " + service.MachineName );
            }


            //Worker Ping Test
            Console.WriteLine(worker.CheckServerConnection());
            Console.WriteLine(worker.GetServerIP());

            //Capture Event Test
            Console.WriteLine(capture.CheckPrio0());


            Console.ReadKey();
        }
    }
}
