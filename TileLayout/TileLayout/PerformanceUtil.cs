using System.Diagnostics;

namespace TileLayout
{
    public static class PerformanceUtil
    {
        public static Stopwatch Start(object sender, string message)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Print.Line(sender, $"{"START",6} {message}");
            return stopwatch;
        }

        public static void Step(this Stopwatch stopwatch, object sender, string message)
        {
            Print.Line(sender, $"{"STEP",6} {message} t: {stopwatch.ElapsedMilliseconds}ms");
        }

        public static void Stop(this Stopwatch stopwatch, object sender, string message)
        {
            stopwatch.Stop();
            Print.Line(sender, $"{"STOP",6} {message} t: {stopwatch.ElapsedMilliseconds}ms.");
        }
    }
}
