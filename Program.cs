using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BinPack
{
    class Program
    {

        static void BinPack(int maxBinSize, int[] packets){
            // Dictionary where key is the number of spaces left and values are the number of bins with that amount of space
            Dictionary<int, int> binMap = new Dictionary<int, int>(); // allows memory use to be really low
            int spaceNeeded = 0;
            int neededSpace = 0;
            bool found  = false;
            int checkBin = 0; int NewSize = 0;
            // Variable definment done above so only done once

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            Console.WriteLine("Algo has started");

            for(int i = 0; i <= maxBinSize; i++)
                binMap.Add(i, 0);

            for(int current = 0; current < packets.Length; current++){
                spaceNeeded = packets[current];
                neededSpace = maxBinSize - spaceNeeded;

                if(spaceNeeded == maxBinSize)
                    binMap[0] += 1;
                else{
                    found = false;
                    for(checkBin = spaceNeeded; checkBin < binMap.Count; checkBin++){
                        if(binMap[checkBin] != 0){
                            NewSize = checkBin - spaceNeeded;
                            binMap[checkBin] -= 1;
                            binMap[NewSize] += 1;

                            found = true;
                            break;
                        }
                    }
                    if(found == false)
                        binMap[neededSpace] += 1;
                }
            }
            Console.WriteLine("Complete");

            DisplayBins(binMap);
        }

        static void DisplayBins( Dictionary<int, int> binMap){
            long total = 0;
            foreach (var item in binMap){
                Console.WriteLine($"KEY (space left): {item.Key} VALUE (number of bins): {item.Value}");
                total += item.Value;
            }
            Console.WriteLine("Bins used: " + total);
        }
        static void Main(string[] args)
        {

            //2,3,4,1,3,2,3,3,2
            Console.WriteLine("Enter Max bin size");
            int maxBinSize = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("Enter packet string");

            int[] packets = ReadFileData();

            Console.WriteLine(packets.Max());
            if(packets.Max() > maxBinSize){
                Console.WriteLine("one of the inputs was to big");
                System.Environment.Exit(0);
            }

            BinPack(maxBinSize, packets);
        }

        static int[] ReadFileData(){
            string packetData = File.ReadAllText("numbers.txt");
            return packetData.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        }
    }
}