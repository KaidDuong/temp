using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Rikkonbi.WebAPI.Helpers
{
    public static class QRCodeHelper
    {
        public static string GenerateQrCodeImage(string qrCodeFileName, string qrCodeContent)
        {
            string rootResourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
            string qrCodeDir = Path.Combine(rootResourceDir, "QRCodes");
            string qrCodeFilePath = Path.Combine(qrCodeDir, qrCodeFileName);
            string qrCodeFileUrl = BaseURIs.STATIC_RESOURCES + qrCodeFilePath.Replace(rootResourceDir, "").Replace("\\", "/");

            if (!Directory.Exists(qrCodeDir))
            {
                Directory.CreateDirectory(qrCodeDir);
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeContent, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
            {
                qrCodeImage.Save(qrCodeFilePath, ImageFormat.Png);
            }

            return qrCodeFileUrl;
        }
    }
}