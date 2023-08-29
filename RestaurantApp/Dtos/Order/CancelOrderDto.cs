using RestaurantApp.Models;

namespace RestaurantApp.Dtos.Order
{
    public class CancelOrderDto
    {

       
        public string OrderCode { get; set; }
        public string ReturnCause { get; set; }
    }
}
