using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using ShopRUs.Services.Interfaces.Interfaces;
using ShopsRus.Core.Entities;
using ShopsRUs.API.APIModels;
using ShopsRUs.API.Constants;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopsRUs.API.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService discountService;
        private readonly ILogger<DiscountController> log;

        public DiscountController(IDiscountService discountService, ILogger<DiscountController> log)
        {
            this.discountService = discountService;
            this.log = log;
        }

        /// <summary>
        /// Creates a discount
        /// </summary>
        /// <remarks>
        /// Sample Request Body
        /// {
        ///  "name": "Main",
        ///   "percentage": "20",
        /// }
        ///
        /// Sample Response Body
        ///     {
        ///         "responseCode": "00",
        ///         "responseMessage": "Success",
        ///         "result": "true"
        ///         
        ///     }
        ///     </remarks>
        [HttpPost("CreateDiscount")]
        public IActionResult CreateDiscount([FromBody]DiscountAPIModel discountAPIModel)
        {
            var response = new BaseAPIResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var discount = new Discount
                    {
                        Name = discountAPIModel.Name,
                        Percentage = discountAPIModel.Percentage,
                        DateCreated = DateTime.Now
                    };

                    var result = discountService.CreateDiscount(discount);
                    response.ResponseCode = ResponseConstants.SuccessCode;
                    response.ResponseMessage = ResponseConstants.SuccessMessage;
                    response.Result = result;
                    return Ok(response);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                log.LogError("CreateDiscount Error: {ex}", ex);
                response.ResponseCode = ResponseConstants.ErrorCode;
                response.ResponseMessage = ResponseConstants.ErrorMessage;
                return Ok(response);
            }
        }

        /// <summary>
        /// Gets all discounts
        /// </summary>
        [HttpGet("GetAllDiscounts")]
        public IActionResult GetAllDiscounts()
        {
            var response = new BaseAPIResponse<List<DiscountAPIModel>>();
            try
            {     
                var discounts = discountService.GetAllDiscounts();
                var responseData = discounts.Select(x => new DiscountAPIModel
                {
                   Name = x.Name,
                   Percentage = x.Percentage
                }).ToList();

                response.ResponseCode = ResponseConstants.SuccessCode;
                response.ResponseMessage = ResponseConstants.SuccessMessage;
                response.Result = responseData;
                return Ok(response);

            }
            catch (Exception ex)
            {
                log.LogError("GetAllDiscounts Error: {ex}", ex);
                response.ResponseCode = ResponseConstants.ErrorCode;
                response.ResponseMessage = ResponseConstants.ErrorMessage;
                return Ok(response);
            }
        }

        /// <summary>
        /// Gets discount details by Id
        /// </summary>
        [HttpGet("GetDiscountDetails/{Id}")]
        public IActionResult GetDiscountDetails([FromRoute]string Id)
        {
            var response = new BaseAPIResponse<DiscountAPIModel>();
            try
            {
                var discount = discountService.GetDiscountDetails(Id);
                if(discount != null)
                {
                    var responseData = new DiscountAPIModel();
                    responseData.Name = discount.Name;
                    responseData.Percentage = discount.Percentage;
                    response.Result = responseData;
                }
                else
                {
                    response.Result = null;
                }
                response.ResponseCode = ResponseConstants.SuccessCode;
                response.ResponseMessage = ResponseConstants.SuccessMessage;
                return Ok(response);

            }
            catch (Exception ex)
            {
                log.LogError("GetDiscountDetails Error: {ex}", ex);
                response.ResponseCode = ResponseConstants.ErrorCode;
                response.ResponseMessage = ResponseConstants.ErrorMessage;
                return Ok(response);
            }
        }




    }
}
