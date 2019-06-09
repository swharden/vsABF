﻿/* 
 *   The AbffioInterface class is a .NET interface to ABFFIO.DLL
 *     written by Scott Harden as part of the vsABF Project
 *     (loosely inspired by the pyABF Project)
 * 
 */

using System;
using System.Runtime.InteropServices;

namespace vsABF
{
    public class AbffioInterface : IDisposable
    {
        private UInt32 sweepPointCount;
        private UInt32 sweepCount;
        private string abfFilePath;
        private bool fileIsOpen = false;
        public float[] sweepBuffer;
        public ABFFIOstructs.ABFFileHeader header;

        public AbffioInterface(string abfFilePath)
        {
            if (!System.IO.File.Exists(abfFilePath))
                throw new ArgumentException($"file does not exist: {abfFilePath}");

            this.abfFilePath = System.IO.Path.GetFullPath(abfFilePath);

            if (!IsABFFile())
                throw new ArgumentException($"ABFFIO says not an ABF file: {abfFilePath}");

            OpenAndReadHeader();
        }

        public void Dispose()
        {
            Close();
        }

        #region ABFFIO.dll calls

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_IsABFFile(String szFileName, ref Int32 pnDataFormat, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadOpen(String szFileName, ref Int32 phFile, UInt32 fFlags, ref ABFFIOstructs.ABFFileHeader pFH, ref UInt32 puMaxSamples, ref UInt32 pdwMaxEpi, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadChannel(Int32 nFile, ref ABFFIOstructs.ABFFileHeader pFH, Int32 nChannel, Int32 dwEpisode, ref float pfBuffer, ref UInt32 puNumSamples, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_Close(Int32 nFile, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadTags(Int32 nFile, ref ABFFIOstructs.ABFFileHeader pFH, UInt32 dwFirstTag, ref ABFFIOstructs.ABFTag pTagArray, UInt32 uNumTags, ref Int32 pnError);

        [DllImport("ABFFIO.dll", CharSet = CharSet.Ansi)]
        private static extern bool ABF_ReadTags(Int32 nFile, ref ABFFIOstructs.ABFFileHeader pFH, UInt32 dwFirstTag, ref ABFFIOstructs.ABFTag[] pTagArray, UInt32 uNumTags, ref Int32 pnError);

        public bool IsABFFile()
        {
            Int32 dataFormat = 0;
            Int32 errorCode = 0;
            ABF_IsABFFile(abfFilePath, ref dataFormat, ref errorCode);
            return (errorCode == 0);
        }

        public void OpenAndReadHeader()
        {
            if (!fileIsOpen)
            {
                Int32 fileHandle = 0;
                Int32 errorCode = 0;
                uint loadFlags = 0;
                ABF_ReadOpen(abfFilePath, ref fileHandle, loadFlags, ref header, ref sweepPointCount, ref sweepCount, ref errorCode);
                AbfError.AssertSuccess(errorCode);
                sweepBuffer = new float[sweepPointCount];
                fileIsOpen = true;
            }
        }

        private void AssertOpen()
        {
            if (!fileIsOpen)
                throw new Exception("ABFFIO must be Open() to use this method");
        }

        public void Close()
        {
            if (fileIsOpen)
            {
                Int32 fileHandle = 0;
                Int32 errorCode = 0;
                ABF_Close(fileHandle, ref errorCode);
                AbfError.AssertSuccess(errorCode);
                fileIsOpen = false;
            }
        }

        public ABFFIOstructs.ABFTag[] ReadTags()
        {
            AssertOpen();
            Int32 fileHandle = 0;
            Int32 errorCode = 0;
            ABFFIOstructs.ABFTag[] abfTags = new ABFFIOstructs.ABFTag[(UInt32)header.lNumTagEntries];
            for (uint i = 0; i < abfTags.Length; i++)
            {
                ABF_ReadTags(fileHandle, ref header, i, ref abfTags[i], 1, ref errorCode);
                AbfError.AssertSuccess(errorCode);
            }
            return abfTags;
        }

        public void ReadChannel(int sweepNumber, int channelNumber)
        {
            AssertOpen();
            Int32 errorCode = 0;
            Int32 fileHandle = 0;
            int physicalChannel = header.nADCSamplingSeq[channelNumber];
            ABF_ReadChannel(fileHandle, ref header, physicalChannel, sweepNumber, ref sweepBuffer[0], ref sweepPointCount, ref errorCode);
            AbfError.AssertSuccess(errorCode);
        }

        #endregion


    }
}
