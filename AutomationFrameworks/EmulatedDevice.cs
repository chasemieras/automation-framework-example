using System;
using System.Collections.Generic;

namespace AutomationFramework
{
    /// <remarks>
    /// Class to handle the devices that will be emulated
    /// </remarks>
    /// <param name="name">the name of the device</param>
    /// <param name="width">the width of the device</param>
    /// <param name="height">the height of the device</param>
    public class EmulatedDevice(string name, int width, int height)
    {
        /// <summary>
        /// The name of the device
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// The width of the device
        /// </summary>
        public int Width { get; } = width;

        /// <summary>
        /// The height of the device
        /// </summary>
        public int Height { get; } = height;
    }

    /// <summary>
    /// The OS of the device
    /// </summary>
    public enum DeviceOperatingSystem
    {
        /// <summary>
        /// Apple/iOS
        /// </summary>
        Apple,

        /// <summary>
        /// Like Samsung
        /// </summary>
        Android
    }

    /// <summary>
    /// The type of device
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// a mobile device like iPhone or Galaxy
        /// </summary>
        Mobile,

        /// <summary>
        /// A tablet device like iPad or TabS
        /// </summary>
        Tablet
    }

    /// <summary>
    /// The lists of emulated devices
    /// </summary>
    public static class EmulatedDevices
    {
        private static readonly List<EmulatedDevice> AppleTablets = new()
        {
            new EmulatedDevice("iPad Pro 12.9", 2048, 2732),
            new EmulatedDevice("iPad Air", 1640, 2360),
            new EmulatedDevice("iPad Mini", 1536, 2048),
            new EmulatedDevice("iPad 10.2", 1620, 2160)
        };

        private static readonly List<EmulatedDevice> AppleMobiles = new()
        {
            new EmulatedDevice("iPhone 14 Pro Max", 1290, 2796),
            new EmulatedDevice("iPhone 13", 1170, 2532),
            new EmulatedDevice("iPhone SE", 750, 1334),
            new EmulatedDevice("iPhone 12 Mini", 1080, 2340)
        };

        private static readonly List<EmulatedDevice> AndroidTablets = new()
        {
            new EmulatedDevice("Samsung Galaxy Tab S8", 1752, 2800),
            new EmulatedDevice("Lenovo Tab P11", 1200, 2000),
            new EmulatedDevice("Amazon Fire HD 10", 1200, 1920),
            new EmulatedDevice("Huawei MatePad Pro", 1600, 2560)
        };

        private static readonly List<EmulatedDevice> AndroidMobiles = new()
        {
            new EmulatedDevice("Samsung Galaxy S23 Ultra", 1440, 3088),
            new EmulatedDevice("Google Pixel 7", 1080, 2400),
            new EmulatedDevice("OnePlus 11", 1440, 3216),
            new EmulatedDevice("Xiaomi Mi 11", 1440, 3200)
        };


        /// <summary>
        /// Gets a random device based on <paramref name="os"/> and <paramref name="type"/>
        /// </summary>
        /// <param name="os">The <see cref="DeviceOperatingSystem"/> you want to select from</param>
        /// <param name="type">The <see cref="DeviceType"/></param>
        /// <returns>An <see cref="EmulatedDevice"/></returns>
        /// <exception cref="ArgumentException">If a bad <see cref="DeviceType"/> or <see cref="DeviceOperatingSystem"/> is given</exception>
        public static EmulatedDevice GetRandomDevice(DeviceOperatingSystem os, DeviceType type)
        {
            List<EmulatedDevice> devices = os switch
            {
                DeviceOperatingSystem.Apple when type == DeviceType.Tablet => AppleTablets,
                DeviceOperatingSystem.Apple when type == DeviceType.Mobile => AppleMobiles,
                DeviceOperatingSystem.Android when type == DeviceType.Tablet => AndroidTablets,
                DeviceOperatingSystem.Android when type == DeviceType.Mobile => AndroidMobiles,
                _ => throw new ArgumentException("Invalid OS or device type")
            };

            int index = new Random().Next(devices.Count);
            return devices[index];
        }
    }
}