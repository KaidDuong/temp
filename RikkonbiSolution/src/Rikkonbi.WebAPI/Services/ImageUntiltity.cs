using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rikkonbi.WebAPI.Services
{
    public class ImageUntiltity
    {
       
        public static bool isRealImageFile(byte[] bytes)
        {
            List<byte[]> signatureFormats = new List<byte[]>();
            //	JPG
            signatureFormats.Add(ImageUntiltity.ToByteArray("FFD8FFE0"));
            signatureFormats.Add(ImageUntiltity.ToByteArray("FFD8FFE1"));
            signatureFormats.Add(ImageUntiltity.ToByteArray("FFD8FFE8"));
            //	BMP
            signatureFormats.Add(ImageUntiltity.ToByteArray("424D"));
            // GIF
            signatureFormats.Add(ImageUntiltity.ToByteArray("47494638"));
            // TIFF
            signatureFormats.Add(ImageUntiltity.ToByteArray("492049"));
            signatureFormats.Add(ImageUntiltity.ToByteArray("49492A00"));
            signatureFormats.Add(ImageUntiltity.ToByteArray("4D4D002A"));
            signatureFormats.Add(ImageUntiltity.ToByteArray("4D4D002B"));
            // PNG
            signatureFormats.Add(ImageUntiltity.ToByteArray("89504E470D0A1A0A"));

            foreach (var item in signatureFormats)
            {
                if (item.SequenceEqual(bytes.Take(item.Length)))
                    return true;
            }


            return false;
        }
        public static byte[] ToByteArray(String hexString)
        {
            byte[] retval = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
                retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            return retval;
        }
    }
}
