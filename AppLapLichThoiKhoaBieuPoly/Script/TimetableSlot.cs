using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLapLichThoiKhoaBieuPoly
{
    public class TimetableSlot
    {
        public List<TimetableEvent> Events { get; set; } = new List<TimetableEvent>();

        // Thêm một sự kiện vào slot
        public void AddEvent(TimetableEvent timetableEvent)
        {
            Events.Add(timetableEvent);
        }
    }

}
