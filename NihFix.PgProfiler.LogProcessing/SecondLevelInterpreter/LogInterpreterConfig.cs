namespace NihFix.PgProfiler.LogProcessing.SecondLevelInterpreter
{
    public class LogInterpreterConfig
    {
        public string QueryPrefix { get; set; } = "оператор: ";

        public string ParseRegExPattern =
            @"(?<duration>(?<=продолжительность: )[\d.]+)|(?<query>(?<=разбор <unnamed>: )[\w"".,= $]+)";

        public string DurationPrefix { get; set; } = "продолжительность:";

        public string ParamsPrefix { get; set; } = "";
    }
}