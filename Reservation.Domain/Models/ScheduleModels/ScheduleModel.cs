using System;
using Reservation.Domain.Models.ScheduleModels.Enums;
using Reservation.Domain.Models.ScheduleModels.ValueObjects;


namespace Reservation.Domain.Models.ScheduleModels
{
    public class ScheduleModel
    {

        public string ScheduleId { get; private set; }

        public DateTime Date { get; private set; }
        public DateTime StartTime {get;set;}
        public DateTime EndTime {get;set;}

        public string? DoctorId { get; private set; }
        //public Panel? PanelInfo { get; private set; }   // => ここは別のクラスにする必要はない。PanalのDoctorほ別オブジェクトとするほうがいい

        public string? ClientId { get; private set; }

        public string? ManagerId { get; private set; }

        public PanelStatusType PanelStatus { get; private set; }


        //public ScheduleModel(string scheduleId, Panel? panelInfo, Client? clientInfo, Manager? manager, PanelStatusType? panelStatus)
        //{
        //    ScheduleId = scheduleId;
        //    PanelInfo = panelInfo;
        //    ClientInfo = clientInfo;
        //    PanelStatus = panelStatus ?? PanelStatusType.Blank;
        //    ManagerInfo = manager;
        //}

        public ScheduleModel(string scheduleId, DateTime date, DateTime startTime, DateTime endTime,
            string? clientId, string? doctorId, string? managerId, PanelStatusType? panelStatus)
        {
            ScheduleId = scheduleId;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            ClientId = clientId;
            DoctorId = doctorId;
            ManagerId = managerId;
            PanelStatus = panelStatus ?? PanelStatusType.Blank;
        }


        /// <summary>
        /// 仮予約する
        /// </summary>
        /// <param name="client"></param>
        public ScheduleModel MakeTemporaryReservation(string? clientId)
        {
            if (clientId == null)
                throw new InvalidOperationException("あなたは誰？");

            if (PanelStatus == PanelStatusType.Reserved || PanelStatus == PanelStatusType.TemporaryReservation)
                throw new InvalidOperationException("すでに予約が入っています");


            if (PanelStatus == PanelStatusType.Finished)
                throw new InvalidOperationException("診療は終了しています");

            ClientId = clientId;
            PanelStatus = PanelStatusType.TemporaryReservation;

            return this;
        }

        /// <summary>
        /// キャンセルする
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public ScheduleModel CancelReservation(string? clientId)
        {
            if (clientId == null)
                throw new InvalidOperationException("あなたは誰？");

            if (ClientId != null && !clientId.Equals(ClientId))
                throw new InvalidOperationException("他人の診療のキャンセルはできません");


            if (PanelStatus == PanelStatusType.Finished)
                throw new InvalidOperationException("終了済みをキャンセルはできません");

            if (PanelStatus == PanelStatusType.Blank)
                throw new InvalidOperationException("予約がありません");

            ClientId = clientId;
            PanelStatus = PanelStatusType.Blank;

            return this;
        }

        /// <summary>
        /// 予約の却下(管理者によって)
        /// </summary>
        /// <param name="manager"></param>
        public ScheduleModel RejectReservation(string? managerId)
        {
            if (managerId == null)
                throw new InvalidOperationException("あなたは誰？");

            if (PanelStatus != PanelStatusType.TemporaryReservation && PanelStatus != PanelStatusType.Reserved)
                throw new InvalidOperationException("予約ではありません");

            ManagerId = managerId;
            PanelStatus = PanelStatusType.Blank;

            return this;
        }


        /// <summary>
        /// 仮予約を承認する
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public ScheduleModel ApproveReservation(string? managerId)
        {
            if (managerId == null)
                throw new InvalidOperationException("あなたは誰？");

            if (PanelStatus != PanelStatusType.TemporaryReservation)
                throw new InvalidOperationException("仮予約ではありません");

            ManagerId = managerId;
            PanelStatus = PanelStatusType.Reserved;

            return this;
        }


    }
}

