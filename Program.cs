using System;
using System.IO;
using System.IO.Compression;

namespace awosLoader
{
    class Program
    {
        static Char[] recordType_001_05 = new Char[05];
        static Char[] sensorId_006_04 = new Char[04];

        static Char[] type_010_10 = new Char[10];
        static Char[] latitude_032_14 = new Char[14];
        static Char[] longitude_046_15 = new Char[15];
        static Char[] elevation_061_07 = new Char[07];
        static Char[] freq1_069_07 = new Char[07];
        static Char[] freq2_076_07 = new Char[07];
        static Char[] phone1_083_14 = new Char[14];
        static Char[] phone2_097_14 = new Char[14];

        static Char[] remarks_020_236 = new Char[236];

        static StreamWriter ofileAWOS1 = new StreamWriter("awosStation.txt");
        static StreamWriter ofileAWOS2 = new StreamWriter("awosRemarks.txt");

        static void Main(String[] args)
        {
            String userprofileFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            String[] fileEntries = Directory.GetFiles(userprofileFolder + "\\Downloads\\", "28DaySubscription*.zip");

            ZipArchive archive = ZipFile.OpenRead(fileEntries[0]);
            ZipArchiveEntry entry = archive.GetEntry("AWOS.txt");
            entry.ExtractToFile("AWOS.txt", true);

            StreamReader file = new StreamReader("AWOS.txt");

            String rec = file.ReadLine();

            while (!file.EndOfStream)
            {
                ProcessRecord(rec);
                rec = file.ReadLine();
            }

            ProcessRecord(rec);

            file.Close();

            ofileAWOS1.Close();
            ofileAWOS2.Close();
        }

        static void ProcessRecord(String record)
        {
            recordType_001_05 = record.ToCharArray(0, 5);

            String rt = new String(recordType_001_05);

            Int32 r = String.Compare(rt, "AWOS1");
            if (r == 0)
            {
                sensorId_006_04 = record.ToCharArray(5, 4);
                String s = new String(sensorId_006_04).Trim();
                ofileAWOS1.Write(s);
                ofileAWOS1.Write('~');

                type_010_10 = record.ToCharArray(9, 10);
                s = new String(type_010_10).Trim();
                ofileAWOS1.Write(s);
                ofileAWOS1.Write('~');

                latitude_032_14 = record.ToCharArray(31, 14);
                s = new String(latitude_032_14).Trim();
                ofileAWOS1.Write(s);
                ofileAWOS1.Write('~');

                longitude_046_15 = record.ToCharArray(45, 15);
                s = new String(longitude_046_15).Trim();
                ofileAWOS1.Write(s);
                ofileAWOS1.Write('~');

                elevation_061_07 = record.ToCharArray(60, 7);
                s = new String(elevation_061_07).Trim();
                if(s != "")
                {
                    s = s.PadLeft(7, '0');
                }
                ofileAWOS1.Write(s);
                ofileAWOS1.Write('~');

                freq1_069_07 = record.ToCharArray(68, 7);
                s = new String(freq1_069_07).Trim();
                if(s.Length > 3)
                {
                    s = s.PadRight(7, '0');
                }
                ofileAWOS1.Write(s);
                ofileAWOS1.Write('~');

                freq2_076_07 = record.ToCharArray(75, 7);
                s = new String(freq2_076_07).Trim();
                if (s.Length > 3)
                {
                    s = s.PadRight(7, '0');
                }
                ofileAWOS1.Write(s);
                ofileAWOS1.Write('~');

                phone1_083_14 = record.ToCharArray(82, 14);
                s = new String(phone1_083_14).Trim();
                if(s != "")
                {
                    s = s.Replace("(", "");
                    s = s.Replace(")", "");
                    s = s.Replace(" ", "-");
                    s = s.Replace("--", "-");
                    s = s.Replace("C", "");
                    s = s.Replace("-", "");

                    Char[] sa = s.ToCharArray();
                    Int32 count = 0;
                    s = "";
                    foreach(Char c in sa)
                    {
                        if((count == 3) || (count == 6))
                        {
                            s += "-";
                        }

                        s += c;

                        count++;
                    }
                }
                ofileAWOS1.Write(s);
                ofileAWOS1.Write('~');

                phone2_097_14 = record.ToCharArray(96, 14);
                s = new String(phone2_097_14).Trim();
                if (s != "")
                {
                    s = s.Replace("(", "");
                    s = s.Replace(")", "");
                    s = s.Replace(" ", "-");
                    s = s.Replace("--", "-");
                    s = s.Replace("C", "");
                    s = s.Replace("-", "");

                    Char[] sa = s.ToCharArray();
                    Int32 count = 0;
                    s = "";
                    foreach (Char c in sa)
                    {
                        if ((count == 3) || (count == 6))
                        {
                            s += "-";
                        }

                        s += c;

                        count++;
                    }
                }
                ofileAWOS1.Write(s);
                ofileAWOS1.Write(ofileAWOS1.NewLine);
            }

            r = String.Compare(rt, "AWOS2");
            if (r == 0)
            {
                sensorId_006_04 = record.ToCharArray(5, 4);
                String s = new String(sensorId_006_04).Trim();
                ofileAWOS2.Write(s);
                ofileAWOS2.Write('~');

                remarks_020_236 = record.ToCharArray(19, 236);
                s = new String(remarks_020_236).Trim();
                ofileAWOS2.Write(s);
                ofileAWOS2.Write(ofileAWOS2.NewLine);
            }
        }

    }
}