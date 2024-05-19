using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Models
{
    internal class Timer
    {
        int hours;
        int minutes;
        int seconds;
        Stopwatch sw;
        long totalMilliseconds;

        public Timer(int hours, int minutes, int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
            // Konvertieren Sie hours, minutes und seconds in Millisekunden und speichern Sie den Wert
            totalMilliseconds = ((hours * 60 * 60) + (minutes * 60) + seconds) * 1000;
            sw = Stopwatch.StartNew();
        }

        public double PercentileTimeElapsed()
        {
            // Berechnen Sie den Prozentsatz der bereits vergangenen Zeit
            double elapsed = (double)sw.ElapsedMilliseconds / totalMilliseconds * 100;
            return elapsed;
        }
    }
}
