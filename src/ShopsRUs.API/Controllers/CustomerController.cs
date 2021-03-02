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
using ShopsRus.Core;
using ShopsRus.Core.Entities;
using ShopsRUs.API.APIModels;
using ShopsRUs.API.Constants;

namespace ShopsRUs.API.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<CustomerController> log;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> log)
        {
            this.customerService = customerService;
            this.log = log;
        }


        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <remarks>
        /// Sample Request Body
        /// {
        ///     "firstName": "Dare",
        ///     "lastName": "Osewa",
        ///     "mobileNumber": "08123456789",
        ///     "CustomerType": "Employee"
        /// }
        ///
        /// Sample Response Body
        ///     {
        ///         "responseCode": "00",
        ///         "responseMessage": "Success",
        ///         "result": true
        ///     } 
        ///</remarks>
        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer([FromBody] CreateCustomerModel createCustomerModel)
        {
            var response = new BaseAPIResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = new Customer
                    {
                        FirstName = createCustomerModel.FirstName,
                        LastName = createCustomerModel.LastName,
                        DateCreated = DateTime.Now,
                        MobileNumber = createCustomerModel.MobileNumber,
                        CustomerType = createCustomerModel.CustomerType
                        
                    };

                    var result = customerService.CreateCustomer(customer);

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
                log.LogError("CreateCustomer Error: {ex}", ex);
                response.ResponseCode = ResponseConstants.ErrorCode;
                response.ResponseMessage = ResponseConstants.ErrorMessage;
                return Ok(response);
            }
        }

        /// <summary>
        /// Gets all customers
        /// </summary>
        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            var response = new BaseAPIResponse<List<CustomerAPIModel>>();
            try
            {     
                var customers = customerService.GetAllCustomers();
                var responseData = customers.Select(x => new CustomerAPIModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CustomerType = x.CustomerType,
                    MobileNumber = x.MobileNumber,
                    Id = x.Id.ToString()
                }).ToList();

                response.ResponseCode = ResponseConstants.SuccessCode;
                response.ResponseMessage = ResponseConstants.SuccessMessage;
                response.Result = responseData;
                return Ok(response);

            }
            catch (Exception ex)
            {
                log.LogError("GetAllCustomers Error: {ex}", ex);
                response.ResponseCode = ResponseConstants.ErrorCode;
                response.ResponseMessage = ResponseConstants.ErrorMessage;
                return Ok(response);
            }
        }

        /// <summary>
        /// Gets customer details by Id
        /// </summary>
        [HttpGet("GetCustomerById/{Id}")]
        public IActionResult GetCustomerById([FromRoute]string Id)
        {
            var response = new BaseAPIResponse<CustomerAPIModel>();
            try
            {
                var customer = customerService.GetCustomerById(Id);
                if(customer != null)
                {
                    var responseData = new CustomerAPIModel();
                    responseData.FirstName = customer.FirstName;
                    responseData.LastName = customer.LastName;
                    responseData.CustomerType = customer.CustomerType;
                    responseData.MobileNumber = customer.MobileNumber;
                    responseData.Id = customer.Id.ToString();
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
                log.LogError("GetCustomerById Error: {ex}", ex);
                response.ResponseCode = ResponseConstants.ErrorCode;
                response.ResponseMessage = ResponseConstants.ErrorMessage;
                return Ok(response);
            }
        }

        /// <summary>
        /// Gets discount details by Name
        /// </summary>
        [HttpGet("GetCustomerByName")]
        public IActionResult GetCustomerByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var response = new BaseAPIResponse<CustomerAPIModel>();
            try
            {
                var customer = customerService.GetCustomerByName(firstName, lastName);
                if (customer != null)
                {
                    var responseData = new CustomerAPIModel();
                    responseData.FirstName = customer.FirstName;
                    responseData.LastName = customer.LastName;;
                    responseData.CustomerType = customer.CustomerType;
                    responseData.MobileNumber = customer.MobileNumber;
                    responseData.Id = customer.Id.ToString();
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
                log.LogError("GetCustomerByName Error: {ex}", ex);
                response.ResponseCode = ResponseConstants.ErrorCode;
                response.ResponseMessage = ResponseConstants.ErrorMessage;
                return Ok(response);
            }
        }








    }
}
