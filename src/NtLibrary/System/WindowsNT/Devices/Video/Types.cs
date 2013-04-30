namespace System.WindowsNT.Devices.Video
{
	public struct DISPLAY_BRIGHTNESS
	{
		public enum DisplayPolicy : byte
		{
			AC = 0x00000001,
			DC = 0x00000002,
			BOTH = (AC | DC)
		}
		public DisplayPolicy ucDisplayPolicy;
		public byte ucACBrightness;
		public byte ucDCBrightness;
	}
}