using System;

using Reservation.SharedLibrary;
using Reservation.Domain.Models.ScheduleModels.ValueObjects;
using Reservation.Domain.Models.ScheduleModels.Enums;
using Reservation.Domain.Models.ScheduleModels;

namespace Reservation.Application.Models
{
    public class ScheduleForClientViewModel
    {

        public string ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public string? Doctor { get; set; }
        public string? Status { get; set; }

        public ScheduleForClientViewModel() { }



        public ScheduleForClientViewModel DomainModelToViewModelDTO(ScheduleModel? scheduleModel)
        {
            ScheduleId = scheduleModel.ScheduleId;
            Date = scheduleModel.Date;
            StartTime = scheduleModel.StartTime;
            EndTime = scheduleModel.EndTime;
            //Doctor = scheduleModel.PanelInfo.Doctor;
            Status = scheduleModel.PanelStatus.GetEmumDescriptionFromValue<PanelStatusType>();

            return this;
        }

    }
}

