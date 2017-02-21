using System;
using System.IO;


namespace SimpleFileSorter.Library
{
    public class Sorter{
        private int _year;
        private string _directory;

        public Sorter(int year, string directory)
        {
           _year = year; 
           _directory = directory;
        }

        public void Execute()
        {
            DirectorySearch(_directory);

        }

        private void DirectorySearch(string currentDirectory)
        {
            try
            {
                foreach (string nextDirectory in Directory.GetDirectories(currentDirectory))
                {
                    foreach (string file in Directory.GetFiles(currentDirectory))
                    {
                    }
                    DirectorySearch(nextDirectory);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
    }
}