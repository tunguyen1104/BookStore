namespace BookStore.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        ICategoryRepository Categories { get; }
        IUserRepository Users { get; }
        ICartRepository Carts { get; }
        ICartDetailRepository CartDetails { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
