using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Helper
{
    public class TimeSlotHelper
    {
        private static readonly Dictionary<int, (TimeSpan Start, TimeSpan End)> Slots = new Dictionary<int, (TimeSpan Start, TimeSpan End)>
    {
        { 1, (new TimeSpan(8, 0, 0), new TimeSpan(8, 45, 0)) },
        { 2, (new TimeSpan(8, 45, 0), new TimeSpan(9, 30, 0)) },
        { 3, (new TimeSpan(9, 30, 0), new TimeSpan(10, 15, 0)) },
        { 4, (new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0)) },
        { 5, (new TimeSpan(11, 0, 0), new TimeSpan(11, 45, 0)) },
        { 6, (new TimeSpan(14, 0, 0), new TimeSpan(14, 45, 0)) },
        { 7, (new TimeSpan(14, 45, 0), new TimeSpan(15, 30, 0)) },
        { 8, (new TimeSpan(15, 30, 0), new TimeSpan(16, 15, 0)) },
        { 9, (new TimeSpan(16, 15, 0), new TimeSpan(17, 0, 0)) },
        { 10, (new TimeSpan(17, 0, 0), new TimeSpan(17, 45, 0)) }
    };
        public static string GetTimeRange(int slot)
        {
            if (Slots.TryGetValue(slot, out var timeRange))
            {
                return $"{timeRange.Start:hh\\:mm} - {timeRange.End:hh\\:mm}";
            }
            return "Invalid Slot";
        }

        public static bool IsValidSlot(int slot, DateTime appointmentDate)
        {
            if (Slots.TryGetValue(slot, out var timeRange))
            {
                DateTime startDateTime = appointmentDate.Date + timeRange.Start;
                return startDateTime > DateTime.Now;
            }
            return false;
        }
    }
}
