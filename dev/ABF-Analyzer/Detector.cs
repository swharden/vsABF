using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF_Analyzer
{
    public class arrayExtrema
    {
        public double maxValue;
        public double minValue;
        public int maxI;
        public int minI;
        public double range;

        public arrayExtrema(double[] values, int i1, int i2)
        {
            minValue = values[i1];
            maxValue = values[i1];
            for (int i = i1 + 1; i < i2; i++)
            {
                if (values[i] < minValue)
                {
                    minValue = values[i];
                    minI = i;
                }
                if (values[i] > maxValue)
                {
                    maxValue = values[i];
                    maxI = i;
                }
            }
            range = maxValue - minValue;
        }

    }

    public static class Detector
    {
        private static double arrayExtrema(double[] values, int i1, int i2)
        {
            return 0;
        }

        public static List<EventPsc> Psc(double[] sweepY, double sampleRate, DetectionSettingsPsc settings)
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // prepare useful units
            double pointsPerMsec = (double)sampleRate / 1000;
            int windowSizePoints = (int)(settings.windowSizeMs * pointsPerMsec); 
            int windowStepPoints = (int)(settings.windowStepMs * pointsPerMsec);
            int baselineBackUpPoints = (int)(settings.baselineBackUp * pointsPerMsec);
            int baselineDurationPoints = (int)(settings.baselineDuration * pointsPerMsec);

            var events = new List<EventPsc>();
            int sweepI = baselineBackUpPoints + baselineDurationPoints;
            while (sweepI < sweepY.Length - windowSizePoints)
            {

                // abort if this window doesn't contain extrema greater than the threshold
                var windowExtrema = new arrayExtrema(sweepY, sweepI, sweepI + windowSizePoints);
                if (windowExtrema.range < settings.threshold)
                {
                    sweepI += windowStepPoints;
                    continue;
                }

                // record the peak peak
                var thisEvent = new EventPsc();
                if (settings.upward)
                    thisEvent.peakI = windowExtrema.maxI;
                else
                    thisEvent.peakI = windowExtrema.minI;

                // calculate baseline
                thisEvent.baselineEndI = sweepI - baselineBackUpPoints;
                thisEvent.baselineStartI = thisEvent.baselineEndI - baselineDurationPoints;
                double baselineSum = 0;
                for (int i = thisEvent.baselineStartI; i < thisEvent.baselineEndI; i++)
                    baselineSum += sweepY[i];
                thisEvent.baselineLevel = baselineSum / baselineDurationPoints;

                // if we made it this far, the event is valid
                events.Add(thisEvent);
                sweepI += windowStepPoints;
            }

            double elapsedMsec = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            System.Console.WriteLine(string.Format("Event detection finished in {0:0.000} ms", elapsedMsec));

            return events;
        }
    }
}
