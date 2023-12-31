﻿using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Services
{
    public interface IOrder
    {
        bool OrderVehicle(int userId, int vehicleId);
        public IList<Orders> GetUserOrder(int userId);
        public IList<Orders> GetOrderById(int id);
        public IList<Orders> GetAllOrders();

        public IList<Orders> VerifyOrder();
        public bool AcceptOrder(Orders order);
        public bool RejectOrder(Orders order);


    }
}
