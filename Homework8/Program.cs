using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Homework8
{
    
    class Program
    {
        //failinimi
        public const string FileName = "resolutions.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            var resolutions = GetResolutionsInitially();

            var cmd = "";
            
            //teeb antud käsku kuni inimene vajutab x (exit)
            while (cmd != "x")
            {
                Console.Clear();
                PrintResolutions(resolutions);
                Console.WriteLine("Write a to add, r to remove and x to exit");
                cmd = Console.ReadLine()?.ToLower();

                //kui inimene vajutab a (add), saab ta lubadusi lisada
                if (cmd == "a")
                {
                    Console.WriteLine("Enter new resolution: ");
                    var newResolutionStr = Console.ReadLine();
                    resolutions.Add(new Resolution(newResolutionStr));
                    SaveListToFile(resolutions);
                }
                
                //kui inimene vajutab r (remove), siis saab valitud lubaduse eemaldada
                else if (cmd == "r")
                {
                    //inimene saab oma lubaduste seast valida numbriga millist lubadust eemaldada
                    Console.WriteLine($"Enter resolution number (1 - {resolutions.Count}):");
                    var toRemove = Console.ReadLine();

                    //kui inimene sisestab numbri asemel tähe, siis käivitub see käsklus
                    if (!int.TryParse(toRemove, out int toRemoveInt))
                    {
                        Console.WriteLine("That is not a number!");
                        Console.ReadKey();
                        continue;
                    }
                    
                    //numbri sisetamise puhul eemaldatakse valitud lubadus
                    if (resolutions.Count >= toRemoveInt)
                    {
                        resolutions.RemoveAt(toRemoveInt-1);
                        SaveListToFile(resolutions);
                    }
                }
                
            }
            
            //kõik info sisestatakse/salvestatakse faili
            var fromFile = LoadFromFile();
            Console.WriteLine("Saved to file:");
            PrintResolutions(fromFile);

        }    
        
        //näitab, mis resolutioonid listis on kirjas
        public static void PrintResolutions(List<Resolution> resolutions)
        {
            Console.WriteLine("Your new year resolutions:");
            var i = 1;
            foreach (var resolution in resolutions)
            {
                Console.WriteLine(i + ". " +resolution.Description);
                i++;
            }

            if (resolutions.Count == 0)
            {
                Console.WriteLine("No resolutions!");
            }
        }
        
        //inimesed sisestavad uue aasta lubadused
        public static List<Resolution> GetResolutionsInitially()
        {
            Console.WriteLine("Enter resolutions separated with a comma.");
            var resolutions = Console.ReadLine();
            var list = resolutions.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            var resolutionsList = new List<Resolution>();
            foreach (var line in list)
            {
                resolutionsList.Add(new Resolution(line));
            }
            SaveListToFile(resolutionsList);
            return resolutionsList;
        }
        
        //listi failist alla laadimine
        public static List<Resolution> LoadFromFile()
        {
            if (File.Exists(FileName))
            {
                var lines = File.ReadAllLines(FileName).ToList();
                var resolutions = new List<Resolution>();
                foreach (var line in lines)
                {
                    resolutions.Add(new Resolution(line));
                }

                return resolutions;
            }
            return new List<Resolution>();
        }
        
        //listi faili salvestamine
        public static void SaveListToFile(List<Resolution> list)
        {
            using (var writer = File.CreateText(FileName))
            {
                foreach (var resolution in list)
                {
                    writer.WriteLine(resolution.Description);
                }
            }
        }
        
        
    }
}