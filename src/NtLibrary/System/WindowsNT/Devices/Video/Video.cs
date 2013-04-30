using System.WindowsNT.IO;
using System.WindowsNT.Devices.PrivateImplementationDetails;
using System.WindowsNT.Devices.Video;
namespace System.WindowsNT.Devices.Video
{
	public static class Video
	{
		public static DISPLAY_BRIGHTNESS GetDisplayBrightness(NtNonDirectoryFile display)
		{
			return Wrapper.GetBrightness(display.Handle);
		}

		public static byte[] GetSupportedDisplayBrightness(NtNonDirectoryFile display)
		{
			return Wrapper.GetSupportedBrightness(display.Handle);
		}
	}
}