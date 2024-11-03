namespace BookStore.Application.DTOs
{
    public class CartSummaryDto
    {
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public List<CartDetailDto> CartDetailDtos { get; set; }
        public CartSummaryDto()
        {
            CartDetailDtos = new List<CartDetailDto>();
        }
        public CartSummaryDto(double totalPrice, double totalDiscount, List<CartDetailDto> cartDetailDtos)
        {
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
            CartDetailDtos = cartDetailDtos;
        }
    }
}
