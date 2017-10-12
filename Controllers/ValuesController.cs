using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_mvc_master.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace gb_mvc_master.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return getCartDetailsJson()+ "\r\n" + 
            getShippingDetailsJson() + "\r\n"+ 
            getContactDetailsJson();

        }
        
        private String getCartDetailsJson(){
            CartDetail cartDetail = new CartDetail();            
            cartDetail.items = new Item[2];
            
            for(int item = 0 ; item < cartDetail.items.Count() ;item++){
                    //cartDetail.items[item] = new Item();
                    //cartDetail.items[item].itemId = "itemId" + item.ToString();
                    //cartDetail.items[item].qty = item.ToString();
            }  
            
            var json = JsonConvert.SerializeObject(cartDetail);
    
            return json;
        }

        private String getShippingDetailsJson(){
            ShippingDetail shippingDetails = new ShippingDetail();

            shippingDetails = new ShippingDetail();
            shippingDetails.homeAddress = new Address();            
            shippingDetails.homeAddress.country = "country";
            shippingDetails.homeAddress.streetAddress1 = "StAddr1" ;
            shippingDetails.homeAddress.streetAddress2 = "StAddr2";
            shippingDetails.homeAddress.streetAddress3 = "StAddr3" ;
            shippingDetails.homeAddress.zip = "zip" ;
            shippingDetails.homeAddress.city = "city";

            //shippingDetails.officeAddress = new Address(); 
            //shippingDetails.officeAddress.country = "country";
            //shippingDetails.officeAddress.streetAddress1 = "StAddr1" ;
            //shippingDetails.officeAddress.streetAddress2 = "StAddr2";
            //shippingDetails.officeAddress.streetAddress3 = "StAddr3" ;
            //shippingDetails.officeAddress.zip = "zip" ;
            //shippingDetails.officeAddress.city = "city";

            var json = JsonConvert.SerializeObject(shippingDetails);
            return json;
        }

        private String getContactDetailsJson(){
            ContactDetail contactDetails = new ContactDetail();

            contactDetails.email = null;
            contactDetails.handphone = "handphone" ;
            contactDetails.landline = "landline" ;

            var json = JsonConvert.SerializeObject(contactDetails);
            return json;
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
