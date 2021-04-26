using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadFilePlaySound
{
    class Program
    {
        static SoundPlayer player = null;

        static void Main(string[] args)
        {
            string FileName = @"VocalItems.txt";            
            try
            {
                player = new SoundPlayer();
            }
            catch (Exception)
            {
                Console.Out.WriteLine("Error opening sound file");
                Environment.Exit(1);
            }
            

            System.IO.StreamReader sr = null;
            try
            {
                sr = new StreamReader(FileName);
            }
            catch (Exception)
            {
                Console.Out.WriteLine("Error opening file");
                Environment.Exit(1);
            }

            int lineCount = 0;
            string line = null;
            line = sr.ReadLine();
            while (line != null)
            {
                lineCount++;
                line = sr.ReadLine();
            };
            sr.Close();

            Console.Out.WriteLine("Total of {0} lines already exist", lineCount);

            while (true)
            {
                Thread.Sleep(5000);
                
                try
                {
                    sr = new StreamReader(FileName);
                }
                catch (Exception)
                {
                    Console.Out.WriteLine("Error opening file");
                    Environment.Exit(1);
                }

                int newLineCount = 0;
                line = sr.ReadLine();
                while (line != null)
                {
                    newLineCount++;
                    if (newLineCount > lineCount)
                    {
                        lineCount = newLineCount;
                        DoNotification(line);
                    }
                    line = sr.ReadLine();
                };
                sr.Close();
            }

        }

        private static void DoNotification(string line)
        {
            Console.Out.WriteLine("Playing sound \"{0}\"", line);

            try
            {
                //SoundPlayer player = new SoundPlayer("kappa");          
                player.SoundLocation = line;
                player.PlaySync();
            }
            catch (Exception)
            {
                Console.Out.WriteLine("Error playing sound \"{0}\"", line);
            }
        }
    }
}
