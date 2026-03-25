using Discount.Core.Entities;

namespace Discount.Core.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UPdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
}
