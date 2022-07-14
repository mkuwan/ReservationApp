using System;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.IServices;
using Reservation.Application.Models;
using Reservation.Domain.Repositories;
using Reservation.SharedLibrary;

namespace Reservation.Application.Services
{
    public class SchedulerService : ISchedulerService
    {
        private readonly ISchedulerRepository _schedulerRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IManagerRepository _managerRepository;

        public SchedulerService(ISchedulerRepository schedulerRepository, IClientRepository clientRepository, IManagerRepository managerRepository)
        {
            _schedulerRepository = schedulerRepository;
            _clientRepository = clientRepository;
            _managerRepository = managerRepository;
        }


        /// <summary>
        /// パネルの取得
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IActionResult> GetPanelAsync(string scheduleId)
        {
            var panel = await _schedulerRepository.GetScheduleAsync(scheduleId);

            if (panel == null) return new NotFoundResult();

            return new OkObjectResult(new ScheduleForClientViewModel().DomainModelToViewModelDTO(panel));
        }

        /// <summary>
        /// 指定日範囲の予定表取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IActionResult> GetSchedulesAsync(DateTime startDate, DateTime endDate)
        {
            // Task<ActionResult<List<ScheduleForClientViewModel>>>

            var schedules = await _schedulerRepository.GetSchedulesAsync(DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate));

            if (schedules == null || schedules.Count() == 0)
                return new NotFoundResult();

            List<ScheduleForClientViewModel> scheduleForClientViewModels = new();

            schedules.ForEach(x => scheduleForClientViewModels.Add(new ScheduleForClientViewModel().DomainModelToViewModelDTO(x)));

            
            return new OkObjectResult(scheduleForClientViewModels);
        }

        /// <summary>
        /// 予約する
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="clientId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IActionResult> TakeReservationAsync(string scheduleId, string clientId)
        {
            try
            {
                var panel = await _schedulerRepository.GetScheduleAsync(scheduleId);

                // ドメインサービスの使用


                // ここをMediatR, INotificationなどを利用してDomain Eventの実装に変更する予定
                var responsePanel = panel?.MakeTemporaryReservation(clientId);
                await _schedulerRepository.MakeTemporaryReservation(responsePanel!); 

                return new OkObjectResult(responsePanel!.PanelStatus.GetEmumDescriptionFromValue());
            }
            catch (Exception err)
            {
                var message = err.Message;
                return new ConflictObjectResult(message);
            }
        }

        /// <summary>
        /// 予約をキャンセルする
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="clientId"></param>
        public async Task<IActionResult> CancelReservationAsync(string scheduleId, string clientId)
        {
            try
            {
                var panel = await _schedulerRepository.GetScheduleAsync(scheduleId);
                var client = await _clientRepository.GetClientAsync(clientId);

                // ここをMediatR, INotificationなどを利用してDomain Eventの実装に変更する予定
                var responsePanel = panel?.CancelReservation(clientId);
                await _schedulerRepository.CancelTemporaryReservation(responsePanel!);

                return new OkObjectResult(responsePanel!.PanelStatus.GetEmumDescriptionFromValue());
            }
            catch (Exception err)
            {
                var message = err.Message;
                return new BadRequestObjectResult(message);
            }

        }

        /// <summary>
        /// 予約を承認する
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="managerId"></param>
        public async Task<IActionResult> ApproveReservationAsync(string scheduleId, string managerId)
        {
            try
            {
                var panel = await _schedulerRepository.GetScheduleAsync(scheduleId);

                var responsePanel = panel?.ApproveReservation(managerId);
                return new OkObjectResult(responsePanel!.PanelStatus.GetEmumDescriptionFromValue());
            }
            catch (Exception err)
            {
                var message = err.Message;
                return new BadRequestObjectResult(message);
            }
        }

        /// <summary>
        /// 予約を却下する
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="managerId"></param>
        public async Task<IActionResult> NotApproveReservationAsync(string scheduleId, string managerId)
        {
            try
            {
                var panel = await _schedulerRepository.GetScheduleAsync(scheduleId);

                var responsePanel = panel?.RejectReservation(managerId);
                return new OkObjectResult(responsePanel!.PanelStatus.GetEmumDescriptionFromValue());
            }
            catch (Exception err)
            {
                var message = err.Message;
                return new BadRequestObjectResult(message);
            }
        }


    }
}

