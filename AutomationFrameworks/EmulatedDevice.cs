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
        private static readonly List<EmulatedDevice> AppleTablets =
        [
            new EmulatedDevice("iPad Pro", 1024, 1366),
            new EmulatedDevice("iPad Air", 820, 1180),
            new EmulatedDevice("iPad Mini", 768, 1024),
        ];

        private static readonly List<EmulatedDevice> AppleMobiles =
        [
            new EmulatedDevice("iPhone 14 Pro Max", 430, 932),
            new EmulatedDevice("iPhone 12 Pro", 390, 844),
            new EmulatedDevice("iPhone SE", 375, 667),
            new EmulatedDevice("iPhone XR", 414, 869)
        ];

        private static readonly List<EmulatedDevice> AndroidTablets =
        [
            new EmulatedDevice("Galaxy Tab S4", 712, 1138),
            new EmulatedDevice("Fire HDX", 800, 1280)
        ];

        private static readonly List<EmulatedDevice> AndroidMobiles =
        [
            new EmulatedDevice("Samsung Galaxy S2 Ultra", 412, 915),
            new EmulatedDevice("Pixel 7", 412, 915),
            new EmulatedDevice("Samsung Galaxy S8+", 360, 740),
        ];


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