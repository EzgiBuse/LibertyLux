﻿using LibertyLux.Business.Services.Abstract;
using LibertyLux.DataAccess.Repository.Menu;
using LibertyLux.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Business.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _orderRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            try
            {
                return await _orderRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
       
        public async Task<Order> GetOrderByOrderItemIdAsync(int id)
        {
            try
            {
                return await _orderRepository.GetByOrderItemIdAsync(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableId)
        {
            try
            {
                return await _orderRepository.GetOrdersByTableAsync(tableId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.AddAsync(order);
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            try
            {
                await _orderRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
        
            public async Task<bool> DeleteOrderItemByOrderItemIdAsync(int id)
            {
            try
            {
                await _orderRepository.DeleteOrderItemByOrderItemIdAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
             }



        public async Task<IEnumerable<OrderItem>> GetAllOrderDetailsByTableAsync(int tableId)
        {
            try
            {
                 var result = await _orderRepository.GetAllOrderDetailsForTableAsync(tableId);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

}
