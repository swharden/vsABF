﻿using System;

namespace vsABF
{
    public class AbfVersion
    {
        public int major;
        public int minor;
        public int bugfix;

        public AbfVersion(int major, int minor, int bugfix)
        {
            this.major = major;
            this.minor = minor;
            this.bugfix = bugfix;
        }

        public override string ToString()
        {
            return $"{major}.{minor}.{bugfix}";
        }
    }

    public class AbfInfo
    {
        // info that does not change as sweeps/channels change

        public readonly string filePath;
        public readonly string fileName;
        public readonly string id;
        public readonly long fileSize;
        public readonly double fileSizeMb;
        //public readonly string[] adcNames;
        //public readonly string[] adcUnits;
        //public readonly string[] dacNames;
        //public readonly string[] dacUnits;

        // data
        public readonly int channelCount;
        public readonly int sweepCount;
        public readonly int tagCount;
        public readonly Tag[] tags;

        public readonly int sampleRate;
        public readonly double pointsPerMs;

        public readonly double totalLengthMin;
        public readonly double totalLengthSec;
        public readonly int totalLengthPoints;

        public readonly double sweepLengthMin;
        public readonly double sweepLengthSec;
        public readonly int sweepLengthPoints;

        public readonly double sweepIntervalSec;
        public readonly double sweepIntervalMin;

        // details from the header
        //public readonly DateTime dateTime;
        //public readonly AbfVersion version;
        //public readonly string comment;
        //public readonly string creator;
        //public readonly string creatorVersion;
        public readonly string protocolFilePath;
        public readonly string protocol;

        public AbfInfo(string abfFilePath, ABFFIOstructs.ABFFileHeader header)
        {
            filePath = System.IO.Path.GetFullPath(abfFilePath);
            fileName = System.IO.Path.GetFileName(abfFilePath);
            id = System.IO.Path.GetFileNameWithoutExtension(abfFilePath);
            fileSize = new System.IO.FileInfo(abfFilePath).Length;
            fileSizeMb = (double)fileSize / 1e6;

            if (header.fADCSequenceInterval == 0)
                throw new Exception("abf header hasn't been properly initialized");

            sampleRate = (int)(1e6 / header.fADCSequenceInterval);
            pointsPerMs = (double)sampleRate / 1000;
            channelCount = header.nADCNumChannels;

            sweepCount = header.lActualEpisodes;
            sweepLengthPoints = header.lNumSamplesPerEpisode / channelCount;
            sweepLengthSec = (double)sweepLengthPoints / sampleRate;
            sweepLengthMin = sweepLengthSec / 60;
            sweepIntervalSec = header.fEpisodeStartToStart;
            if (sweepIntervalSec == 0)
                sweepIntervalSec = sweepLengthSec;
            sweepIntervalMin = sweepIntervalSec / 60;

            totalLengthSec = sweepIntervalSec * sweepCount;
            totalLengthMin = totalLengthSec / 60;
            totalLengthPoints = sweepLengthPoints * sweepCount;

            tagCount = header.lNumTagEntries;
            tags = new Tag[tagCount];

            protocolFilePath = header.sProtocolPath.Trim();
            if (protocolFilePath == "(untitled)")
                protocolFilePath = "";
            protocol = System.IO.Path.GetFileNameWithoutExtension(protocolFilePath);
        }

        public override string ToString()
        {
            string info = "";

            // use reflection to iterate through all public variables of this class
            var fields = this.GetType().GetFields();
            foreach (var field in fields)
            {
                // get the basic information for each item
                string type = field.FieldType.ToString();
                string name = field.Name;
                string value = field.GetValue(this).ToString();

                // customize string value of specific types
                if (field.FieldType == typeof(double))
                    value = string.Format("{0:0.0000}", (double)field.GetValue(this));
                else if (
                        field.FieldType == typeof(int) ||
                        field.FieldType == typeof(Int16) ||
                        field.FieldType == typeof(long)
                        )
                    value = string.Format("{0}", field.GetValue(this));
                else if (field.FieldType == typeof(string))
                    value = $"\"{field.GetValue(this)}\"";
                else if (field.FieldType == typeof(Tag[]))
                    foreach (Tag tag in (Tag[])field.GetValue(this))
                        value += "\n" + tag.ToString();
                else
                    name = $"({type}) {name}";

                if (value == "vsABF.Tag[]")
                    continue;

                info += $"{name}: {value}\n";
            }

            info = info.Trim();
            return info;
        }
    }
}
