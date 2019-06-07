using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF_Analyzer
{

    public abstract class Event
    {

    }

    public class EventPsc : Event
    {
        public int sweepNumber;

        // index positions to aid in graphing
        public int baselineStartI;
        public int baselineEndI;
        public int riseStartI;
        public int peakI;
        public int decayI;

        // positions to aid in charting
        public double baselineLevel;
        public double riseVel;
        public double riseTime;
        public double peakAmp;
        public double decayTime;
        public double fallVel;

    }

    public abstract class DetectionSettings
    {
        // settings are sample-rate independent
    }

    public class DetectionSettingsPsc : DetectionSettings
    {
        public double windowSizeMs = 5;
        public double windowStepMs = 3;

        // set by the event settings editor GUI
        public double threshold;
        public double timeToPeak;
        public double baselineBackUp;
        public double baselineDuration;
        public double decayFraction;
        public double area;
        public bool upward;

        // set by the analysis-calling function
        public double sampleRate;
        public double pointsPerMs;
        public double msPerPoint;
    }
}
