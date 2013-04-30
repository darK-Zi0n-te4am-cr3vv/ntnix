using System.WindowsNT.PrivateImplementationDetails;
using DllImport = System.Runtime.InteropServices.DllImportAttribute;
using In = System.Runtime.InteropServices.InAttribute;
using Out = System.Runtime.InteropServices.OutAttribute;

namespace System.WindowsNT.Security.PrivateImplementationDetails
{
	internal partial class Native
	{
		[DllImport("Advapi32.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus LsaRetrievePrivateData(System.IntPtr PolicyHandle, [In] ref LSAUnicodeString KeyName, [Out] out System.IntPtr PrivateData);


		[DllImport("Advapi32.dll", EntryPoint = "LsaFreeMemory", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus LsaFreeUnicodeString([In] ref LSAUnicodeString Buffer);


		[DllImport("Advapi32.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus LsaOpenPolicy([In] ref LSAUnicodeString SystemName, [In] ref LSAObjectAttributes ObjectAttributes, [In] AccessMask DesiredAccess, [Out] out System.IntPtr PolicyHandle);

		[DllImport("Advapi32.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus LsaOpenPolicy([In] System.IntPtr SystemName, [In] ref LSAObjectAttributes ObjectAttributes, [In] AccessMask DesiredAccess, [Out] out System.IntPtr PolicyHandle);


		[DllImport("Advapi32.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus LsaClose([In] System.IntPtr ObjectHandle);


	}
}