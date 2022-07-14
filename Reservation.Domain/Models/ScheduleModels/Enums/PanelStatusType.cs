using System;
using System.ComponentModel;

namespace Reservation.Domain.Models.ScheduleModels.Enums
{
    public enum PanelStatusType
    {
        [Description("予約なし")]
        Blank,

        [Description("仮予約")]
        TemporaryReservation,

        [Description("予約")]
        Reserved,

        [Description("終了")]
        Finished
    }
}

