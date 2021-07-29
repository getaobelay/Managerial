// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using Application.ViewModels;
using AutoMapper;
using Domain.Entites;
using Managerial.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : BaseApiController<Customer, CustomerViewModel>
    {
        private readonly IEmailSender _emailSender;

        public CustomerController(IMediator mediator, IEmailSender emailSender) : base(mediator)
        {
            _emailSender = emailSender;
        }



        [HttpGet("email")]
        public async Task<string> Email()
        {
            string recepientName = "QickApp Tester"; //         <===== Put the recepient's name here
            string recepientEmail = "test@ebenmonney.com"; //   <===== Put the recepient's email here

            string message = EmailTemplates.GetTestEmail(recepientName, DateTime.UtcNow);

            (bool success, string errorMsg) = await _emailSender.SendEmailAsync(recepientName, recepientEmail, "Test Email from QuickApp", message);

            if (success)
                return "Success";

            return "Error: " + errorMsg;
        }
    }
     
}