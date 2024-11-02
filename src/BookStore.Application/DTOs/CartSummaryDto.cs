using BookStore.Domain.Entities;

namespace BookStore.Application.DTOs
{
    public class CartSummaryDto
    {
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public List<CartDetail> CartDetails { get; set; }
        public CartSummaryDto()
        {
            CartDetails = new List<CartDetail>();
        }
        public CartSummaryDto(double totalPrice, double totalDiscount, List<CartDetail> cartDetails)
        {
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
            CartDetails = cartDetails;
        }
    }
}
