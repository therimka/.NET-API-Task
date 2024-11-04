namespace OrderManagementAPI.DTOs
{
    public class CreateOrderDto
    {
        public int CustomerID { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }

    //orders can contain more than 1 orderitem so something like this in the request body works
    /*
      {
         "customerID": 12,
         "orderItems": [
           {
         "productID": 1,
         "quantity": 1
           },
      {
         "productID": 2,
         "quantity": 1
       }
      ]
     }
     */
}
