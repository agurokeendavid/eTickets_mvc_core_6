﻿using eTickets.Models;

namespace eTickets.Data.Services;

public interface IOrdersService
{
    Task StoreOrderAsync(List<ShoppingCartItem> shoppingCartItems, string userId, string userEmailAddress);
    Task<List<Order>> GetOrdersByUserIdAsync(string userId);
}