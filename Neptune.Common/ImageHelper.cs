/*-----------------------------------------------------------------------
<copyright file="ImageHelper.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Drawing;
using System.Drawing.Imaging;

namespace Neptune.Common
{
    public class ImageHelper
    {
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        public static byte[] ImageToByteArrayAndCompress(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            using var inStream = ms;
            using var outStream = new MemoryStream();
            var image = Image.FromStream(inStream);

            // if we aren't able to retrieve our encoder
            // we should just save the current image and
            // return to prevent any exceptions from happening
            if (jpgEncoder == null)
            {
                image.Save(outStream, ImageFormat.Jpeg);
            }
            else
            {
                var qualityEncoder = Encoder.Quality;
                var encoderParameters = new EncoderParameters(1);
                //defaulting to 80% quality seems like a good compromise in basic eye testing 
                encoderParameters.Param[0] = new EncoderParameter(qualityEncoder, 80L);
                image.Save(outStream, jpgEncoder, encoderParameters);
            }

            return outStream.ToArray();
        }

        public static Image ScaleImage(byte[] imageData, int maxWidth, int maxHeight)
        {
            using var ms = new MemoryStream(imageData);
            using var image = Image.FromStream(ms);
            var scaledImage = ScaleImage(image, maxWidth, maxHeight);

            return scaledImage;
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);


            return newImage;
        }
    }
}
