using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Class_Library
{
    public class CsvProcessing
    {
        /// <summary>
        /// The method for reading data from a file.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public List<ElectrocarPower> Read(FileStream stream)
        {
            List<ElectrocarPower> list = new List<ElectrocarPower>();
            try
            {
                using (var reader = stream)
                {
                    byte[] arr = new byte[reader.Length];
                    reader.Read(arr, 0, arr.Length);
                    string text = Encoding.Default.GetString(arr);
                    string[] data = text.Split('\n');
                    for (int i = 2; i < data.Length; i++)
                    {
                        string[] cur = data[i].Replace("\"", "").Split(";");
                        if (cur.Length >= 11)
                        {
                            ElectrocarPower n;
                            try
                            {
                                n = new ElectrocarPower(int.Parse(cur[1]), cur[2], cur[3], cur[4], cur[5],
                                                        double.Parse(cur[6].Replace(".", ",")), double.Parse(cur[7].Replace(".", ",")), cur[8]);
                                list.Add(n);
                            }
                            catch (Exception ex)
                            {
                                n = new ElectrocarPower(0, "Wrang data", " ", " ", " ", 0.0, 0.0, " ");
                                list.Add(n);
                            }
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                list = null;
            }
            return list;
        }

        public Stream Write(List<ElectrocarPower> Data)
        {
            string line = "";

            line += "object_category_Id;ID;Name;AdmArea;District;Address;Longitude_WGS84;Latitude_WGS84;global_id;geodata_center;geoarea;\n"+
                    "object_category_Id;Код;Наименование;Административный округ;Район;Адрес;Долгота в WGS-84;Широта в WGS-84;global_id;geodata_center;geoarea;\n";
            for (int i = 0; i < Data.Count; i++)
            {
                line += ElectrocarPower.ToStr(Data[i]) + "\n";
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(line);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;

        }
    }
}
