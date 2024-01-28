using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{

    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes, 
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            var date = new Date(day, month, year);
            var devicesList = new List<Device>();
            var dateList = new List<Date>();
            foreach(var item in devices)
                devicesList.Add(new Device((int) item["DeviceId"], item["Name"].ToString()));

            foreach(var item in times)
                dateList.Add(new Date((int)item[0], (int)item[1], (int)item[2]));

            return FindDevicesFailedBeforeDate(date, devicesList, dateList);
        }


        public static List<string> FindDevicesFailedBeforeDate(Date date, List<Device> devices, List<Date> dates)
        {
            var problematicDevices = new HashSet<int>();
            for (int i = 0; i < dates.Count; i++)
                if (i % 2 == 0 && dates[i] < date)
                    problematicDevices.Add(devices[i].Id);

            var result = new List<string>();
            foreach (var device in devices)
                if (problematicDevices.Contains(device.Id))
                    result.Add(device.Name);


            return result;
        }


    }

    public class Device
    {   
        public int Id { get; set; }
        public string Name { get; set; }

        public Device(int id, string name)
        {
            Id = id;
            Name = name;
        }   
    }


    public class Date
    {
        public int year;
        public int month;
        public int day;

        public Date(int day, int month, int year)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }

        public static bool operator<(Date d1, Date d2)
        {
            if (d1.year > d2.year) return false;
            if (d1.month > d2.month) return false;
            if (d1.day > d2.day) return false;
            return true;
        }
        public static bool operator >(Date d1, Date d2)
        {
            if (d1.year < d2.year) return false;
            if (d1.month < d2.month) return false;
            if (d1.day < d2.day) return false;
            return true;
        }
    }
}
