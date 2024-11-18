using AutoMapper;
using BookStore.Application.DTOs.Order;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public (IEnumerable<OrderDto> orderDtos, int FilteredCount, int TotalCount) Find(string search, int page, int pageSize)
        {
            if (page < 1) page = 1;
            IQueryable<Order> orderQuery = _unitOfWork.Orders.GetAll().OrderByDescending(s => s.OrderDate);
            var totalCount = orderQuery.Count();

            var key = orderQuery.ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                orderQuery = orderQuery.Where(order => (order.ReceivedName.Contains(search)));
            }

            var filteredCount = orderQuery.Count();

            // Apply paging
            var orders = orderQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Map to BookDto
            var orderDto = _mapper.Map<List<OrderDto>>(orders);

            return (orderDto, filteredCount, totalCount);
        }

        public async Task<List<OrderDetailDto>> GetOrderDetailsByOrderIdAsync(long orderId)
        {
            var orderDetails = await _unitOfWork.OrderDetails.GetOrderDetailsByOrderIdAsync(orderId);

            var orderDetailDtos = _mapper.Map<List<OrderDetailDto>>(orderDetails);
            orderDetailDtos.ForEach(orderDetailDto =>
            {
                orderDetailDto.BookImage = _fileService.GetImageUrl(orderDetailDto.BookImage);
            });
            return orderDetailDtos;
        }
    }
}
