using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
using Marshal = System.Runtime.InteropServices.Marshal;
using System.Runtime.InteropServices;

namespace System.WindowsNT.Memory.PrivateImplementationDetails
{
	internal static class Wrapper
	{
		private static unsafe SafeNtSectionHandle NtCreateSection(string name, AllowedObjectAttributes attributes, long* maxSize, PageProtection protection, SectionAllocationAttributes allocationAttributes, SafeNtFileHandle fileHandle)
		{
			System.IntPtr handle;
			UnicodeString wName = (UnicodeString)name;
			try
			{
				NtStatus result;
				ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wName, attributes, null, null);
				result = Native.NtCreateSection(out handle, SectionAccessMask.MaximumAllowed, ref oa, maxSize, protection, allocationAttributes, NTInternal.CreateHandleRef(fileHandle));
				NtException.CheckAndThrowException(result);
				return new SafeNtSectionHandle(handle);
			}
			finally
			{
				wName.Dispose();
			}
		}

		private static unsafe SafeNtSectionHandle NtCreateSection(long* maxSize, PageProtection protection, SectionAllocationAttributes allocationAttributes, SafeNtFileHandle fileHandle)
		{
			System.IntPtr handle;
			NtStatus result;
			result = Native.NtCreateSection(out handle, SectionAccessMask.MaximumAllowed, System.IntPtr.Zero, maxSize, protection, allocationAttributes, NTInternal.CreateHandleRef(fileHandle));
			NtException.CheckAndThrowException(result);
			return new SafeNtSectionHandle(handle);
		}

		public static SafeNtSectionHandle NtCreateSection(string name, AllowedObjectAttributes attributes, long maxSize, PageProtection protection, SectionAllocationAttributes allocationAttributes, SafeNtFileHandle fileHandle)
		{
			unsafe
			{
				if (name != null)
				{
					return NtCreateSection(name, attributes, &maxSize, protection, allocationAttributes, fileHandle);
				}
				else
				{
					return NtCreateSection(&maxSize, protection, allocationAttributes, fileHandle);
				}
			}
		}

		public static SafeNtSectionHandle NtCreateSection(string name, AllowedObjectAttributes attributes, PageProtection protection, SectionAllocationAttributes allocationAttributes, SafeNtFileHandle fileHandle)
		{
			unsafe
			{
				if (name != null)
				{
					return NtCreateSection(name, attributes, null, protection, allocationAttributes, fileHandle);
				}
				else
				{
					return NtCreateSection(null, protection, allocationAttributes, fileHandle);
				}
			}
		}


		public static System.IntPtr NtAllocateVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr? baseAddress, byte zeroBits, ref System.UIntPtr regionSize, MemoryAllocationType allocationType, PageProtection protection)
		{
			System.IntPtr @base = baseAddress ?? System.IntPtr.Zero;
			unsafe
			{
				System.IntPtr* pBase = baseAddress != null ? &@base : null;
				NtStatus result = Native.NtAllocateVirtualMemory(NTInternal.CreateHandleRef(processHandle), pBase, zeroBits, ref regionSize, allocationType, protection);
				NtException.CheckAndThrowException(result);
				return *pBase;
			}
		}

		private static unsafe System.IntPtr NtFreeVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address, uint* regionSize, MemoryFreeType freeType)
		{
			NtStatus result = Native.NtFreeVirtualMemory(NTInternal.CreateHandleRef(processHandle), ref address, regionSize, freeType);
			NtException.CheckAndThrowException(result);
			return address;
		}

		public static System.IntPtr NtFreeVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address)
		{ unsafe { return NtFreeVirtualMemory(processHandle, address, null, MemoryFreeType.RELEASE); } }

		public static System.IntPtr NtFreeVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address, uint regionSize)
		{ unsafe { return NtFreeVirtualMemory(processHandle, address, &regionSize, MemoryFreeType.DECOMMIT); } }

		public static uint NtFlushVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address, ref uint bytesToFlush)
		{
			uint bytesFlushed;
			NtStatus result = Native.NtFlushVirtualMemory(NTInternal.CreateHandleRef(processHandle), address, ref bytesToFlush, out bytesFlushed);
			NtException.CheckAndThrowException(result);
			return bytesFlushed;
		}

		public static PageProtection NtProtectVirtualMemory(SafeNtProcessHandle processHandle, ref System.IntPtr address, ref uint bytesToProtect, PageProtection protection)
		{
			PageProtection old;
			IntPtr address2 = address;
			uint bytesToProtect2 = bytesToProtect;
			unsafe
			{
				NtStatus result = Native.NtProtectVirtualMemory(NTInternal.CreateHandleRef(processHandle), &address2, &bytesToProtect2, protection, out old);
				NtException.CheckAndThrowException(result);
				address = address2;
				bytesToProtect = bytesToProtect2;
				return old;
			}
		}

		public static MemoryBasicInformation NtQueryVirtualMemoryBasic(SafeNtProcessHandle processHandle, System.IntPtr address)
		{
			MemoryBasicInformation mbi;
			uint resultLength;
			NtStatus result = Native.NtQueryVirtualMemory(NTInternal.CreateHandleRef(processHandle), address, MemoryInformationClass.MemoryBasicInformation, out mbi, (uint)Marshal.SizeOf(typeof(MemoryBasicInformation)), out resultLength);
			NtException.CheckAndThrowException(result);
			return mbi;
		}

		public static uint NtReadVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address, System.IO.UnmanagedMemoryStream ums, uint bytesToRead)
		{
			if (!ums.CanWrite)
			{
				throw new System.NotSupportedException("Stream cannot be written to.");
			}
			uint bytesRead;
			unsafe
			{
				NtStatus result = Native.NtReadVirtualMemory(NTInternal.CreateHandleRef(processHandle), address, (IntPtr)ums.PositionPointer, bytesToRead, out bytesRead);
				NtException.CheckAndThrowException(result);
				return bytesRead;
			}
		}

		public static uint NtReadVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address, byte[] array, int byteOffset, uint bytesToRead)
		{
			uint bytesRead;
			System.Runtime.InteropServices.ArrayWithOffset arr = new System.Runtime.InteropServices.ArrayWithOffset(array, byteOffset);
			NtStatus result = Native.NtReadVirtualMemory(NTInternal.CreateHandleRef(processHandle), address, arr, bytesToRead, out bytesRead);
			NtException.CheckAndThrowException(result);
			return bytesRead;
		}

		public static byte[] NtReadVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address, uint bytesToRead)
		{
			uint bytesRead;
			using (System.Runtime.InteropServices.HGlobal p = new System.Runtime.InteropServices.HGlobal(bytesToRead))
			{
				NtStatus result = Native.NtReadVirtualMemory(NTInternal.CreateHandleRef(processHandle), address, p.Address, bytesToRead, out bytesRead);
				NtException.CheckAndThrowException(result);
				byte[] output = new byte[bytesRead];
				Marshal.Copy(p.Address, output, 0, (int)bytesRead);
				return output;
			}
		}

		public static uint NtWriteVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address, System.IO.UnmanagedMemoryStream ums, uint bytesToWrite)
		{
			if (!ums.CanRead)
			{
				throw new System.NotSupportedException("Stream cannot be read.");
			}
			uint bytesWritten;
			unsafe
			{
				NtStatus result = Native.NtWriteVirtualMemory(NTInternal.CreateHandleRef(processHandle), address, (IntPtr)ums.PositionPointer, bytesToWrite, out bytesWritten);
				NtException.CheckAndThrowException(result);
				return bytesWritten;
			}
		}

		public static uint NtWriteVirtualMemory(SafeNtProcessHandle processHandle, System.IntPtr address, byte[] array, int byteOffset, uint bytesToWrite)
		{
			uint bytesWritten;
			System.Runtime.InteropServices.ArrayWithOffset arr = new System.Runtime.InteropServices.ArrayWithOffset(array, byteOffset);
			NtStatus result = Native.NtWriteVirtualMemory(NTInternal.CreateHandleRef(processHandle), address, arr, bytesToWrite, out bytesWritten);
			NtException.CheckAndThrowException(result);
			return bytesWritten;
		}


		public static void NtMapViewOfSection(SafeNtSectionHandle sectionHandle, SafeNtProcessHandle processHandle, IntPtr? baseAddress, byte zeroBits, IntPtr commitSize, ref long? sectionOffset, IntPtr? viewSize, SectionInherit inherit, MemoryAllocationType allocationType, PageProtection protection, out System.IntPtr actualBaseAddress, out System.IntPtr actualViewSize)
		{
			unsafe
			{
				IntPtr @base = baseAddress ?? IntPtr.Zero;
				IntPtr* pBase = baseAddress != null ? &@base : null;
				long offset = sectionOffset ?? 0;
				long* pSectionOffset = sectionOffset != null ? &offset : null;
				IntPtr size = viewSize ?? IntPtr.Zero;
				IntPtr* pSize = viewSize != null ? &size : null;
				NtStatus result = Native.NtMapViewOfSection(NTInternal.CreateHandleRef(sectionHandle), NTInternal.CreateHandleRef(processHandle), pBase, (IntPtr)zeroBits, commitSize, pSectionOffset, pSize, inherit, allocationType, protection);
				NtException.CheckAndThrowException(result);
				actualBaseAddress = *pBase;
				if (pSectionOffset != null)
				{
					sectionOffset = *pSectionOffset;
				}
				actualViewSize = *pSize;
			}
		}


		public static SectionBasicInformation NtQuerySectionBasic(SafeNtSectionHandle sectionHandle)
		{
			SectionBasicInformation output = new SectionBasicInformation();
			uint resultLength;
			NtStatus result = Native.NtQuerySection(NTInternal.CreateHandleRef(sectionHandle), SectionInformationClass.SectionBasicInformation, out output, (uint)Marshal.SizeOf(output), out resultLength);
			NtException.CheckAndThrowException(result);
			return output;
		}

		public static SectionImageInformation NtQuerySectionImage(SafeNtSectionHandle sectionHandle)
		{
			SectionImageInformation output = new SectionImageInformation();
			uint resultLength;
			NtStatus result = Native.NtQuerySection(NTInternal.CreateHandleRef(sectionHandle), SectionInformationClass.SectionImageInformation, out output, (uint)Marshal.SizeOf(output), out resultLength);
			NtException.CheckAndThrowException(result);
			return output;
		}
	}
}