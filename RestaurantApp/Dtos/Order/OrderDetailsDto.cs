using Foolproof;
using RestaurantApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Dtos.Order
{
    public class OrderDetailsDto
    {
        public string Notes { get; set; }
        public int OrderTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public string? FullName { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
