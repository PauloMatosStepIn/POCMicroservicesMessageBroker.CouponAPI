using StepIn.MessageBus;

namespace StepIn.Services.CouponAPI.Models.Dto
{
  public class CouponDto : BaseMessage
  {
    public string CouponCode { get; set; }
    public DateTime CouponDate { get; set; }
  }
}
