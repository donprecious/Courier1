using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Courier;

namespace Infrastructure
{
    //Custom attribute validations 
    //VALIDATE IF AN EMAIL ALREADY EXIST
    public class CheckEmail : ValidationAttribute
    {
        public CheckEmail() : base("{0} Already Exit")
        {
           
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                var result = UserManager.FindByEmail(value.ToString());
                if(result != null)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    //return error that user already exit with that email
                    return new ValidationResult(ErrorMessage);
                }
            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
            return ValidationResult.Success;
        }
      //  private readonly string _email;

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                // return HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
  
}