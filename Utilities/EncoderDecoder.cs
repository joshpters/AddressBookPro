using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AddressBookPro.Utilities
{
    public static class EncoderDecoder
    {
        public static byte[] EncodeFile(IFormFile image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);
            var output = ms.ToArray();

            ms.Close();
            ms.Dispose();

            return output;
        }

        public static string DecodeFile(byte[] image, string fileName)
        {
            var binary = Convert.ToBase64String(image);
            var ext = Path.GetExtension(fileName);
            return $"data:image/{ext};base64,{binary}";
        }
    }
}
