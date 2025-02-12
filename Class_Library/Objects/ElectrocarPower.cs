using System;

namespace Class_Library
{
    public class ElectrocarPower
    {
        // String fields.
        int _Id;
        string _Name;
        string _AdmArea;
        string _District;
        string _Address;
        double _Longitude_WGS84;
        double _Latitude_WGS84;
        string _global_id;

        public ElectrocarPower()
        {

        }
        public ElectrocarPower(int Id, string Name, string AdmArea, string District, string Address,
                               double Longitude_WGS84, double Latitude_WGS84, string global_id)
        {
            _Id = Id;
            _Name = Name;
            _AdmArea = AdmArea;
            _District = District;
            _Address = Address;
            _Longitude_WGS84 = Longitude_WGS84;
            _Latitude_WGS84 = Latitude_WGS84;
            _global_id = global_id;
        }
        public int Id { get { return _Id; } set { _Id = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string AdmArea { get { return _AdmArea; } set { _AdmArea = value; } }
        public string District { get { return _District; } set { _District = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public double Longitude_WGS84 { get { return _Longitude_WGS84; } set { _Longitude_WGS84 = value; } }
        public double Latitude_WGS84 { get { return _Latitude_WGS84; } set { _Latitude_WGS84 = value; } }
        public string global_id { get { return _global_id; } set { _global_id = value; } }

        public static string ToStr(ElectrocarPower n)
        {
            string str = "";
            str += $";{n.Id.ToString()};{n.Name};{n.AdmArea};{n.District};{n.Address};{n.Longitude_WGS84.ToString()};{n.Latitude_WGS84.ToString()};{n.global_id};;";
            return str;
        }

    }
}

