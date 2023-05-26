using StepIn.Services.CouponAPI.Models.Dto;
using StepIn.Services.CuponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StepIn.MessageBus;

namespace StepIn.Services.CouponAPI.Controllers
{
  [ApiController]
  [Route("/api/coupon")]
  public class CouponAPIController : Controller
  {

    private readonly IMessageBus _messageBus;
    private ResponseDto _response;

    private readonly IConfiguration _configuration;

    public CouponAPIController(IMessageBus messageBus, IConfiguration configuration)
    {
      _response = new ResponseDto();
      _messageBus = messageBus;
      _configuration = configuration;
    }

    [Authorize]
    [HttpPost]
    public async Task<Object> AskCoupon(CouponDto couponDto)
    {
      string topicToMessageBus = _configuration.GetValue<string>("TopicToMessageBus");
      string connectionstringToMessageBus = _configuration.GetValue<string>("ConnectionstringToMessageBus");

      try
      {
        await _messageBus.PuplishMessage(couponDto, topicToMessageBus, connectionstringToMessageBus);
        _response.IsSuccess = true;
      }
      catch (Exception ex)
      {
        _response.ErrorMessages = new List<string> { ex.ToString() };
      }

      return _response;
    }

  }
}
