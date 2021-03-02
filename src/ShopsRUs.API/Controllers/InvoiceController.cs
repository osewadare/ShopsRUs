using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopRUs.Services.Interfaces.Interfaces;
using ShopsRUs.API.APIModels;
using ShopsRUs.API.Constants;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopsRUs.API.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService invoiceService;
        private readonly ILogger<InvoiceController> log;

        public InvoiceController(IInvoiceService invoiceService, ILogger<InvoiceController> log)
        {
            this.invoiceService = invoiceService;
            this.log = log;
        }


        /// <summary>
        /// Computes the customer's invoice amount
        /// </summary>
        /// <remarks>
        /// Sample Request Body
        /// {
        ///  "customerId": "1",
        ///      "items": [
        /// {
        ///     "Amount": "50",
        ///     "Category": "Books",
        ///     "Name": "Atomic Habits" 
        /// }
        /// ]}
        ///
        /// Sample Response Body
        ///     {
        ///         "responseCode": "00",
        ///         "responseMessage": "Success",
        ///         "result": {
        ///         "invoiceAmount": "50"
        ///         }
        ///     }
        ///     </remarks>
        [HttpPost("GetInvoiceAmount")]
        public IActionResult GetInvoiceAmount([FromBody]InvoiceRequestModel invoiceRequestModel)
        {
            var response = new BaseAPIResponse<InvoiceResponseModel>();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = invoiceService.ComputeInvoiceAmount(invoiceRequestModel.Items, invoiceRequestModel.CustomerId);
                    response.ResponseCode = ResponseConstants.SuccessCode;
                    response.ResponseMessage = ResponseConstants.SuccessMessage;
                    response.Result = new InvoiceResponseModel
                    {
                        InvoiceAmount = result
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                log.LogError("GetInvoiceAmount Error: {ex}", ex);
                response.ResponseCode = ResponseConstants.ErrorCode;
                response.ResponseMessage = ResponseConstants.ErrorMessage;
                return Ok(response);
            }
        }

    }
}
