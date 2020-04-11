using System;
using NihFix.PgProfiler.LogProcessing;

namespace NihFix.PgProfiler.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            using var logReader = new LogChangeTracker(@"C:\Program Files\PostgreSQL\10\data\log");
            logReader.OnLogChange+=LogReaderOnOnLogChange;
            while (Console.ReadLine()!="q")
            {
                
            }

            logReader.OnLogChange -= LogReaderOnOnLogChange;
        }

        private static void LogReaderOnOnLogChange(object? sender, OnLogAddEventArgs e)
        {
            Console.WriteLine(e.FilePath);
            Console.Write(e.NewData);
        }
    }
}