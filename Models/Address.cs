using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AddressBookPro.Utilities;
using Microsoft.AspNetCore.Http;

namespace AddressBookPro.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ABPUser User { get; set; }

        public int LabelId { get; set; }
        public Label Label { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string AvatarFileName { get; set; }
        public byte[] Avatar { get; set; }
  
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string City { get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string State { get; set; }

        [DataType(DataType.PostalCode, ErrorMessage = "Not a valid Zip Code.")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Please only include numbers.")]
        public string ZipCode { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Not a valid phone number.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        public string Notes { get; set; }

        public DateTime DateAdded { get; set; }

        public void SetAvatar (IFormFile file)
        {
            Avatar = EncoderDecoder.EncodeFile(file);
            AvatarFileName = file.FileName;
        }
        public string GetAvatar()
        {
            return EncoderDecoder.DecodeFile(Avatar, AvatarFileName);
        }     

        public string GetFullAddress()
        {
            if (Address2 != null)
            {
                return $"{Address1} ({Address2}), {City}, {State} {ZipCode}";
                
            } else
            {
                return $"{Address1}, {City}, {State} {ZipCode}";
            }
        }
    }
}
