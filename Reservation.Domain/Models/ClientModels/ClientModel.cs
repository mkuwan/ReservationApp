using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Models.ClientModels
{
    public class ClientModel
    {
        public string ClientId { get; private set; }
        public string ClientMemberId { get; private set; }
        public string ClientName { get; private set; }
        public DateTime? Birthday { get; private set; }
        public string Gender { get; private set; }
        public string HealthInsuranceNumber { get; private set; }
        public string Address { get; private set; }


        public ClientModel(string clientId, string clientMemberId, string clientName, DateTime? birthday, string gender, string healthInsuranceNumber, string address)
        {
            ClientId = clientId;
            ClientMemberId = clientMemberId;
            ClientName = clientName;
            Birthday = birthday;
            Gender = gender;
            HealthInsuranceNumber = healthInsuranceNumber;
            Address = address;
        }

        /// <summary>
        /// 新規患者情報登録
        /// </summary>
        /// <param name="clientMemberId"></param>
        /// <param name="clientName"></param>
        /// <param name="birthday"></param>
        /// <param name="gender"></param>
        /// <param name="healthInsuranceNumber"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public ClientModel CreateClient(string clientMemberId, string clientName, DateTime? birthday, string gender, string healthInsuranceNumber, string address)
        {
            ClientId = Guid.NewGuid().ToString();
            ClientMemberId = clientMemberId;
            ClientName = clientName;
            Birthday = birthday;
            Gender = gender;
            HealthInsuranceNumber = healthInsuranceNumber;
            Address = address;

            return this;
        }

        /// <summary>
        /// 名前の変更
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ClientModel ChangeName(string clientId, string clientName)
        {
            if (ClientId != clientId || clientId == null)
                throw new InvalidOperationException("名前を変更することはできません");

            ClientName = clientId;
            return this;
        }

        /// <summary>
        /// 保険証番号を登録する
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="healthInsuranceNumber"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ClientModel RegisterHealthInsuranceNumber(string clientId, string healthInsuranceNumber)
        {
            if (ClientId != clientId || clientId == null)
                throw new InvalidOperationException("番号を変更することはできません");

            HealthInsuranceNumber = healthInsuranceNumber;
            return this;
        }

    }
}
