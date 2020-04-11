using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrelCodesChallengeCore
{
    class Program
    {
        static StreamReader local;
        static StreamReader server;
        static StreamWriter output;
        static StreamWriter missing;

        static int currentL;
        static int currentS;
        static void Main(string[] args)
        {
            DateTime timestamp = DateTime.Now;
            local = new StreamReader(File.Open("input_local.txt", FileMode.Open));
            server = new StreamReader(File.Open("input_server.txt", FileMode.Open));
            output = new StreamWriter(File.Create("output_result.txt"));
            missing = new StreamWriter(File.Create("output_missing.txt"));
            currentL = int.Parse(local.ReadLine());
            currentS = int.Parse(server.ReadLine());
            while(currentL < currentS)
            {
                output.WriteLine(currentL);
                currentL = int.Parse(local.ReadLine());
            }
            output.WriteLine(currentS);
            if(currentL == currentS)
                currentL = int.Parse(local.ReadLine());
            while(!server.EndOfStream)
            {
                currentS = int.Parse(server.ReadLine());
                output.WriteLine(currentS);
                if(currentS == currentL)
                {
                    if(!local.EndOfStream)
                        currentL = int.Parse(local.ReadLine());
                }
                else
                {
                    missing.WriteLine(currentS);
                }
            }
            TimeSpan timegone = DateTime.Now - timestamp;
            Console.WriteLine(timegone.TotalMilliseconds + "ms");
        }

    }
}
