using Reservation.Domain.Models.ScheduleModels.Enums;
using Reservation.Domain.Models.ScheduleModels;
using Reservation.Domain.Models.ScheduleModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Test
{
    public class DomainScheuleTest
    {
        Client client = new Client(
            "C001",
            "患者1号"
            );
        Client client2 = new Client(
            "C002",
            "患者2号"
            );
        Manager manager = new Manager(
            "M001",
            "受付"
            );

        [Fact]
        public void 仮予約できる()
        {
            // arrange
            ScheduleModel scheduleModel = new ScheduleModel(
                "S001",
                DateTime.Today, 
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 0, 0),
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 30, 0),
                null,
                null,
                null,
                PanelStatusType.Blank
                );

            // act
            scheduleModel.MakeTemporaryReservation(client.clientId);

            // assert
            Assert.Equal(PanelStatusType.TemporaryReservation, scheduleModel.PanelStatus);
        }

        [Fact]
        public void 仮予約を承認する()
        {
            // Arrange
            ScheduleModel scheduleModel = new ScheduleModel(
                "S001",
                DateTime.Today,
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 0, 0),
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 30, 0),
                client.clientId,
                null,
                null,
                PanelStatusType.TemporaryReservation
                );
            // Act
            scheduleModel.ApproveReservation(manager.managerId);

            // Assertion
            Assert.Equal(PanelStatusType.Reserved, scheduleModel.PanelStatus);
        }

        [Fact]
        public void 仮予約を却下する()
        {
            // Arrange
            ScheduleModel scheduleModel = new ScheduleModel(
                "S001",
                DateTime.Today,
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 0, 0),
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 30, 0),
                client.clientId,
                null,
                null,
                PanelStatusType.TemporaryReservation
                );
            // Act
            scheduleModel.RejectReservation(manager.managerId);

            // Assertion
            Assert.Equal(PanelStatusType.Blank, scheduleModel.PanelStatus);
        }

        [Fact]
        public void 仮予約したら他の人が予約していてダメ()
        {
            // Arrange
            ScheduleModel scheduleModel = new ScheduleModel(
                "S001",
                DateTime.Today,
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 0, 0),
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 30, 0),
                client2.clientId,
                manager.managerId,
                null,
                PanelStatusType.Reserved
                );

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                scheduleModel.MakeTemporaryReservation(client.clientId);
            });

            // Assert
            Assert.Contains("すでに予約が入っています", exception.Message);
        }

        [Fact]
        public void 仮予約したけどキャンセルする()
        {
            // Arrange
            ScheduleModel scheduleModel = new ScheduleModel(
                "S001",
                DateTime.Today,
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 0, 0),
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 30, 0),
                client2.clientId,
                manager.managerId,
                null,
                PanelStatusType.TemporaryReservation
                );

            // Act
            scheduleModel.CancelReservation(client2.clientId);

            // Assert
            Assert.Equal(PanelStatusType.Blank, scheduleModel.PanelStatus);
        }

        [Fact]
        public void 他人のキャンセルしようとしたら怒られた()
        {
            // Arrange
            ScheduleModel scheduleModel = new ScheduleModel(
                "S001",
                DateTime.Today,
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 0, 0),
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 30, 0),
                client2.clientId,
                manager.managerId,
                null,
                PanelStatusType.Reserved
                );

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                scheduleModel.CancelReservation(client.clientId);
            });

            // Assert
            Assert.Contains("他人の診療のキャンセルはできません", exception.Message);
        }
    }
}
