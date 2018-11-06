﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Windows.Media.Imaging;

namespace NotContra
{
    class ImageSelector
    {
        public const int IMAGE_WIDTH = 80;
        public const int IMAGE_HEIGHT = 80;

        public Dictionary<string, BitmapImage> Images { get; private set; }

        public ImageSelector()
        {
            this.Images = new Dictionary<string, BitmapImage>();

            ResourceManager manager = new ResourceManager(typeof(ImageResources));
            ResourceSet set =
                manager.GetResourceSet(
                    CultureInfo.CurrentUICulture, true, true);

            ConvertBitmapToBitmapImage(set);
        }

        private void ConvertBitmapToBitmapImage(ResourceSet set)
        {
            foreach (DictionaryEntry entry in set)
            {
                string key = entry.Key.ToString();
                Bitmap value = (Bitmap)entry.Value;

                var scaled = new Bitmap(value, new Size(IMAGE_WIDTH, IMAGE_HEIGHT));

                using (var memory = new MemoryStream())
                {
                    scaled.Save(memory, ImageFormat.Png);

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();

                    //Add image to Dictionary
                    Images.Add(key, bitmapImage);
                }
            }
        }

        public BitmapImage Get(string key)
        {
            if(!Images.ContainsKey(key))
            {
                throw new KeyNotFoundException("This image cannot be found: " + key);
            }

            return Images[key];
        }
    }
}
