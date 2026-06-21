namespace Order
{
    public interface IOrderService
    {
        bool ValidateOrder(Order order);

        Task<bool> SendOrderAsync(Order order);

        void NotifyUser(Order order, bool success);
    }
}
