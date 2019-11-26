using System;
using System.Collections.Generic;
using System.Text;

namespace TileLayout
{
    public static class Print
    {
        public static Action<object, string> CustomWriteLine;

        public static void Line(object sender, string message)
        {
            if (CustomWriteLine != null)
            {
                CustomWriteLine.Invoke(sender, message);
            }
            else
            {
                System.Console.WriteLine($"log||{sender.GetType().Name,-23}{sender.GetHashCode(),-11}|{Xamarin.Essentials.MainThread.IsMainThread,-6}|: {message}");
            }
        }
    }
}
