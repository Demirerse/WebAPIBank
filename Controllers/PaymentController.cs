using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIBank.DesignPattern.SingletonPattern;
using WebAPIBank.DTOClasses;
using WebAPIBank.Models.Context;
using WebAPIBank.Models.Entities;

namespace WebAPIBank.Controllers
{
    public class PaymentController : ApiController
    {
        MyContext _db;

        public PaymentController()
        {
            _db = DBTool.DBInstance;
        }



        [HttpPost]
        public IHttpActionResult ReceivePayment(PaymentDTO item)
        {


            CustomerCardInfo cif = _db.CustomerCardInfos.FirstOrDefault(x => x.CardNumber == item.CardNumber && x.SecurityNumber == item.SecurityNumber && x.CardUserName == item.CardUserName && x.CardExpiryYear == item.CardExpiryYear && x.CardExpiryMonth == item.CardExpiryMonth);



            if (cif != null)
            {
                if (cif.CardExpiryYear < DateTime.Now.Year)
                {
                    return BadRequest("Expired Card");
                }
                else if (cif.CardExpiryYear == DateTime.Now.Year)
                {
                    if (cif.CardExpiryMonth < DateTime.Now.Month)
                    {
                        return BadRequest("Expired Card");
                    }


                    if (cif.Balance >= item.ShoppingPrice)
                    {
                        cif.Balance -= item.ShoppingPrice;
                        _db.SaveChanges();

                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Balance Exceeded");
                    }


                }

                if (cif.Balance >= item.ShoppingPrice)
                {
                    cif.Balance -= item.ShoppingPrice;
                    _db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Balance Exceeded");
                }

            }
            else
            {
                //Kart bulunamazsa
                return BadRequest("Card Not Found");
            }


        }
    }
}
