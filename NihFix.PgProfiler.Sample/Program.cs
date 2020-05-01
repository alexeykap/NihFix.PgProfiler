using System;
using NihFix.PgProfiler.LogProcessing;

namespace NihFix.PgProfiler.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            using var logReader = new LogChangeTracker(@"D:\postgres\data\log");
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
            foreach (var postgresLogRecord in e.NewData)
            {
                var r = postgresLogRecord;
            }
        }
    }
}