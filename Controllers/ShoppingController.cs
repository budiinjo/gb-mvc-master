using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gb_mvc_master.Models;
using Newtonsoft.Json.Linq;

namespace gb_mvc_master.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingController : Controller
    {
        // POST api/shopping
        [HttpPost]
        public IActionResult Post([FromForm] String cartDetails,[FromForm] String shippingDetails , [FromForm] String contactDetails ){
            CartDetail cardDetailsObj=null;
            ShippingDetail shippingDetailObj=null;
            ContactDetail contactDetailObj=null;

            //process cardDetails
            try{                
                JObject a = JObject.Parse(cartDetails);
                cardDetailsObj =  a.ToObject<CartDetail>();        
            }catch(Exception e){                
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                cardDetailsObj = null;
            }
 
            //process shippingDetails
            try{
                JObject a = JObject.Parse(shippingDetails);
                shippingDetailObj = a.ToObject<ShippingDetail>();                            
            }catch(Exception e){                
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                shippingDetailObj = null;
            }
          


            //process contactDetails
            try{
                JObject a = JObject.Parse(contactDetails);
                contactDetailObj = a.ToObject<ContactDetail>();   

            }catch(Exception e){                
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                contactDetailObj = null;
            }

            //validate request
            // carddetails Y, shippingDetails Y , contactDetails 
            //Console.WriteLine("count:" + cardDetailsObj.items.Count());

           if (validate(cardDetailsObj, shippingDetailObj, contactDetailObj)== true){
               return  StatusCode(200);
            }else
                return StatusCode(422);

        }
       
       private Boolean validate(CartDetail cartDetails ,ShippingDetail shippingDetails , ContactDetail contactDetails){
           //validate request
           // carddetails Y, shippingDetails Y , contactDetails 
            
           Boolean value = true;
           if (cartDetails==null) value= false ;
           else{
  
                if (cartDetails.items== null){
                    value = false;
                }else{
                

                    for(int ii = 0 ; ii< cartDetails.items.Count() ; ii++){
                        if (cartDetails.items[ii] == null){
                            value = false;
                            break;
                        }
                        else{
                            if(String.IsNullOrWhiteSpace (cartDetails.items[ii].itemId)){
                                value = false;
                                break;
                            }    

                            if(String.IsNullOrWhiteSpace (cartDetails.items[ii].qty)){                        
                                value = false;
                                break;
                            }    
                        }     
                                                  
                    }
                }
            }
           


            if (shippingDetails == null ) {
                Console.WriteLine("Shipping details is null");
                value = false;
            }
            else{
                if(shippingDetails.homeAddress == null && shippingDetails.officeAddress == null) {
                    Console.WriteLine("all address is null");
                    value = false;
                }else{
                    Boolean homeAddress = false, officeAddress = false;

                    if (shippingDetails.homeAddress!= null)
                        homeAddress = isAddressValid(shippingDetails.homeAddress);

                    if (shippingDetails.officeAddress != null)    
                        officeAddress = isAddressValid(shippingDetails.officeAddress);

                    if (homeAddress == true || officeAddress == true){
                        //do nothing
                    }else{
                        value = false;
                    }                  

                }
           
            }


            if (contactDetails == null){
                Console.WriteLine("contact details is null");
            }else{
                if (!String.IsNullOrWhiteSpace(contactDetails.email) || 
                    !String.IsNullOrWhiteSpace(contactDetails.handphone) ||
                    !String.IsNullOrWhiteSpace(contactDetails.landline)){
                        // do nothing
                    }else{
                        value = false;
                    }
            }
                
 
 
           return value;
           
       }

       public Boolean isAddressValid(Address address){
           Boolean isAddressValid = true;

            if (String.IsNullOrWhiteSpace(address.streetAddress1)){
                Console.WriteLine("streetAddress1 is null or whitespace ");
                isAddressValid = false;
            }

            if (String.IsNullOrEmpty(address.zip)){
                Console.WriteLine("zip is null or whitespace ");
                isAddressValid = false;                
            }                                  

            if (String.IsNullOrEmpty(address.city)){
                Console.WriteLine("city is null or whitespace ");
                isAddressValid = false;
            }

            if (String.IsNullOrEmpty(address.country)){
                Console.WriteLine("country is null or whitespace ");
                isAddressValid = false;
            }   

            return isAddressValid;  
       }

    
    }
}
