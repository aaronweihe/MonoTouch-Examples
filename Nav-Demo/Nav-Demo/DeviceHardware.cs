using System;
using System.Runtime.InteropServices;
using MonoTouch;
using MonoTouch.UIKit;

namespace NavDemo
{
	public enum HardwareVersion
	{
		iPhone1G,
		iPhone3G,
		iPhone3GS,
		iPhone4,
		iPhone4S,
		iPhone4Verizon,
		iPod1G,
		iPod2G,
		iPod3G,
		iPod4G,
		iPad,
		iPad2Wifi,
		iPad2GSM,
		iPad2CDMA,
		iPhoneSimulator,
		iPhone4Simulator,
		iPadSimulator,
		Unknown
	}

	public class DeviceHardware
	{
		public DeviceHardware()
		{
		}

		private const string HardwareProperty = "hw.machine";

		[DllImport(Constants.SystemLibrary, CharSet = CharSet.Auto)]
		private extern static int sysctlbyname(string prop, IntPtr output, IntPtr oldLen, IntPtr newP, uint newLen);

		public static HardwareVersion Version {
			get {
				// get the length of the string that will be returned  
				var pLen = Marshal.AllocHGlobal(sizeof(int));
				try
				{
					sysctlbyname(DeviceHardware.HardwareProperty, IntPtr.Zero, pLen, IntPtr.Zero, 0);
				
					var length = Marshal.ReadInt32(pLen);
				
					// check to see if we got a length  
					if (length == 0)
						return HardwareVersion.Unknown;					
				
					// get the hardware string  
					var pStr = Marshal.AllocHGlobal(length);
					try
					{
						sysctlbyname(DeviceHardware.HardwareProperty, pStr, pLen, IntPtr.Zero, 0);
				
						// convert the native string into a C# string  
						var hardwareStr = Marshal.PtrToStringAnsi(pStr);
				
						// determine which hardware we are running  
						switch (hardwareStr)
						{
							case "iPhone1,1":
								return HardwareVersion.iPhone1G;
							case "iPhone1,2":
								return HardwareVersion.iPhone3G;
							case "iPhone2,1":
								return HardwareVersion.iPhone3GS;
							case "iPhone3,1":
								return HardwareVersion.iPhone4;
							case "iPhone3,2":
							case "iPhone3,3":
								return HardwareVersion.iPhone4Verizon;
							case "iPhone4,1":
								return HardwareVersion.iPhone4S;
							case "iPad1,1":
								return HardwareVersion.iPad;
							case "iPad2,1":
								return HardwareVersion.iPad2Wifi;
							case "iPad2,2":
								return HardwareVersion.iPad2GSM;
							case "iPad2,3":
								return HardwareVersion.iPad2CDMA;
							case "iPod1,1":
								return HardwareVersion.iPod1G;
							case "iPod2,1":
								return HardwareVersion.iPod2G;
							case "iPod3,1":
								return HardwareVersion.iPod3G;
							case "iPod4,1":
								return HardwareVersion.iPod3G;
							case "i386":
							case "x86_64":
								if (UIDevice.CurrentDevice.Model.Contains("iPhone"))
									return UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale == 960 || UIScreen.MainScreen.Bounds.Width * UIScreen.MainScreen.Scale == 960 ? HardwareVersion.iPhone4Simulator : HardwareVersion.iPhoneSimulator;
								else
									return HardwareVersion.iPadSimulator;
							default:
								return HardwareVersion.Unknown;
						}
					}
					finally
					{
						Marshal.FreeHGlobal(pStr);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(pLen);
				}				
			}
		}

		public static string VersionString {
			get {
				switch (Version) {
				case HardwareVersion.iPhone1G:
					return "iPhone";
				case HardwareVersion.iPhone3G:
					return "iPhone 3G";
				case HardwareVersion.iPhone3GS:
					return "iPhone 3GS";
				case HardwareVersion.iPhone4:
				case HardwareVersion.iPhone4Verizon:
					return "iPhone 4";
				case HardwareVersion.iPhone4S:
					return "iPhone 4S";
				case HardwareVersion.iPod1G:
					return "iPhone 1G";
				case HardwareVersion.iPod2G:
					return "iPhone 2G";
				case HardwareVersion.iPod3G:
					return "iPhone 3G";
				case HardwareVersion.iPod4G:
					return "iPhone 4G";
				case HardwareVersion.iPad:
					return "iPad";
				case HardwareVersion.iPad2Wifi:
					return "iPad 2 (WiFi)";
				case HardwareVersion.iPad2GSM:
					return "iPad 2 (GSM)";
				case HardwareVersion.iPad2CDMA:
					return "iPad 2 (CDMA)";
				case HardwareVersion.iPhoneSimulator:
					return "iPhone Simulator";
				case HardwareVersion.iPhone4Simulator:
					return "iPhone 4 Simulator";
				case HardwareVersion.iPadSimulator:
					return "iPad Simulator";
				default:
					return "Unknown device";
				}
			}
		}

		public static bool InSimulator {
			get {
				HardwareVersion ver = Version;
				return (ver == HardwareVersion.iPhoneSimulator) || 
					   (ver == HardwareVersion.iPhone4Simulator) || 
					   (ver == HardwareVersion.iPadSimulator);
			}
		}
	}
}

