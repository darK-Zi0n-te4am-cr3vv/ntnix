using System.Runtime.InteropServices;
using System.WindowsNT.PrivateImplementationDetails;
namespace System.WindowsNT.Registry.PrivateImplementationDetails
{
	internal static class Native
	{
		//==================================== REGISTRY ========================================

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCompactKeys([In] uint Count, [In] IntPtr[] KeyArray);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCompressKey([In] HandleRef Key);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtCreateKey([Out] out IntPtr KeyHandle, [In] KeyAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes, [In] uint TitleIndex, [In, Optional] ref UnicodeString Class, [In] RegCreateOptions CreateOptions, [Out, Optional] out RegCreationDisposition Disposition);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtDeleteKey([In] HandleRef KeyHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtDeleteValueKey([In] HandleRef KeyHandle, ref UnicodeString ValueName);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtEnumerateKey([In] HandleRef KeyHandle, [In] uint Index, [In] KeyInformationClass KeyInformationClass, [Out] IntPtr KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtEnumerateKey([In] HandleRef KeyHandle, [In] uint Index, [In] KeyInformationClass KeyInformationClass, [Out] out KeyBasicInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtEnumerateKey([In] HandleRef KeyHandle, [In] uint Index, [In] KeyInformationClass KeyInformationClass, [Out] out KeyNodeInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtEnumerateKey([In] HandleRef KeyHandle, [In] uint Index, [In] KeyInformationClass KeyInformationClass, [Out] out KeyFullInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtEnumerateValueKey([In] HandleRef KeyHandle, [In] uint Index, [In] KeyValueInformationClass KeyValueInformationClass, [Out] IntPtr KeyValueInformation, [In] uint Length, [Out] out uint ResultLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtFlushKey([In] HandleRef KeyHandle);


		/// <summary>Makes avaiable registry keys and values stored in Hive File.</summary>
		/// <param name="DestinationKeyName">Pointer to <see cref="ObjectAttributes"/> structure contains destination key name and HANDLE to root key. Root can be \REGISTRY\MACHINE or \REGISTRY\USER. All other keys are invalid.</param>
		/// <param name="HiveFileName">Pointer to <see cref="ObjectAttributes"/> structure contains Hive file path and name.</param>
		/// <remarks>Hive file can be created by calling <see cref="NtSaveKey"/>. If loaded Hive is no longer needed (for example when user logout for HKCU Hive), it can be udloaded by call <see cref="NtUnloadKey"/>.</remarks>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtLoadKey([In] ref ObjectAttributes DestinationKeyName, [In] ref ObjectAttributes HiveFileName);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtLoadKey2([In] ref ObjectAttributes DestinationKeyName, [In] ref ObjectAttributes HiveFileName, KeyRestoreOptions Flags);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtLockRegistryKey([In] HandleRef KeyHandle);


		/// <param name="Buffer"><see cref="RegNotifyInformation"/></param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtNotifyChangeKey([In] HandleRef KeyHandle, [In, Optional] IntPtr Event, [In, Optional, MarshalAs(UnmanagedType.FunctionPtr)] Events.IoApcRoutine ApcRoutine, [In, Optional] IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [In] RegNotifyFilter CompletionFilter, [In] bool WatchTree, [Out] IntPtr Buffer, [In] uint BufferSize, [In] bool Asynchronous);


		/// <param name="Buffer"><see cref="RegNotifyInformation"/></param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtNotifyChangeMultipleKeys([In] HandleRef MasterKeyHandle, [In] uint Count, ref ObjectAttributes SlaveObjects, [In] IntPtr Event, [In, Optional] Events.IoApcRoutine ApcRoutine, [In, Optional] IntPtr ApcContext, [Out] out IoStatusBlock IoStatusBlock, [In] uint CompletionFilter, [In] bool WatchTree, [Out] IntPtr Buffer, [In] uint Length, [In] bool Asynchronous);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenKey([Out] out IntPtr KeyHandle, [In] KeyAccessMask DesiredAccess, [In] ref ObjectAttributes ObjectAttributes);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtOpenKey([Out] out IntPtr KeyHandle, [In] KeyAccessMask DesiredAccess, [In] PObjectAttributes ObjectAttributes);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtQueryKey([In] HandleRef KeyHandle, [In] KeyInformationClass KeyInformationClass, [Out] IntPtr KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryKey([In] HandleRef KeyHandle, [In] KeyInformationClass KeyInformationClass, [Out] out KeyBasicInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryKey([In] HandleRef KeyHandle, [In] KeyInformationClass KeyInformationClass, [Out] out KeyNodeInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryKey([In] HandleRef KeyHandle, [In] KeyInformationClass KeyInformationClass, [Out] out KeyFullInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryKey([In] HandleRef KeyHandle, [In] KeyInformationClass KeyInformationClass, [Out] out KeyNameInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryKey([In] HandleRef KeyHandle, [In] KeyInformationClass KeyInformationClass, [Out] out KeyCachedInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryKey([In] HandleRef KeyHandle, [In] KeyInformationClass KeyInformationClass, [Out] out KeyFlagsInformation KeyInformation, [In] uint Length, [Out] out uint ResultLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtQueryMultipleValueKey([In] HandleRef KeyHandle, [In, Out] KeyValueEntry* ValueEntries, [In] uint EntryCount, [Out] IntPtr ValueBuffer, [In, Out] ref uint BufferLength, [Out, Optional] out uint RequiredBufferLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtQueryOpenSubKeysEx(ref ObjectAttributes TargetKey, [In] uint BufferLength, [In] void* Buffer, [In] ref uint RequiredSize);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryOpenSubKeys([In] ref ObjectAttributes TargetKey, [Out] out uint HandleCount);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryValueKey([In] HandleRef KeyHandle, [In] ref UnicodeString ValueName, [In] KeyValueInformationClass KeyValueInformationClass, [Out] IntPtr KeyValueInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryValueKey([In] HandleRef KeyHandle, [In] ref UnicodeString ValueName, [In] KeyValueInformationClass KeyValueInformationClass, [Out] out KeyValueBasicInformation KeyValueInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryValueKey([In] HandleRef KeyHandle, [In] ref UnicodeString ValueName, [In] KeyValueInformationClass KeyValueInformationClass, [Out] out KeyValuePartialInformation KeyValueInformation, [In] uint Length, [Out] out uint ResultLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtQueryValueKey([In] HandleRef KeyHandle, [In] ref UnicodeString ValueName, [In] KeyValueInformationClass KeyValueInformationClass, [Out] out KeyValueFullInformation KeyValueInformation, [In] uint Length, [Out] out uint ResultLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtRenameKey([In] HandleRef KeyHandle, [In] ref UnicodeString NewName);


		/// <summary>Saves specified hive key to file, and starts use this file instread of original hive file. Original hive file contents is replaced with contents of third hive file, specified below.</summary>
		/// <param name="NewHiveFileName">Pointer to <see cref="ObjectAttributes"/> structure containing name of third file (file with new contents).</param>
		/// <param name="KeyHandle">HANDLE to Key Object. Backuped and replaced are all keys from hive which contains key specified by <paramref name="KeyHandle"/> parameter.</param>
		/// <param name="BackupHiveFileName">Pointer to <see cref="ObjectAttributes"/> structure containing name of first file (new hive file).</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtReplaceKey([In] ref ObjectAttributes NewHiveFileName, [In] HandleRef KeyHandle, [In] ref ObjectAttributes BackupHiveFileName);


		/// <param name="KeyHandle">All keys and values stored in file represented by <paramref name="FileHandle"/> will be children of <paramref name="KeyHandle"/>.</param>
		/// <param name="FileHandle">See the FileHandle parameter of <see cref="NtSaveKey"/>.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus NtRestoreKey([In] HandleRef KeyHandle, [In] HandleRef FileHandle, [In] KeyRestoreOptions RestoreOption);


		/// <param name="FileHandle">HANDLE to any file created with write access. Before using <paramref name="FileHandle"/> in other registry function without closing it, call <see cref="NtFlushKey"/> with <param name="KeyHandle"/> as param.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSaveKey([In] HandleRef KeyHandle, [In] HandleRef FileHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSaveKeyEx([In] HandleRef KeyHandle, [In] HandleRef FileHandle, [In] RegHiveFormat HiveFormat);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSaveMergedKeys([In] HandleRef HighPrecedenceKeyHandle, [In] IntPtr LowPrecedenceKeyHandle, [In] IntPtr FileHandle);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationKey([In] HandleRef KeyHandle, [In] KeySetInformationClass InformationClass, [In] ref KeyWriteTimeInformation KeyInformationData, [In] uint DataLength);

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetInformationKey([In] HandleRef KeyHandle, [In] KeySetInformationClass InformationClass, [In] ref KeyUserFlagsInformation KeyInformationData, [In] uint DataLength);


		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtSetValueKey([In] HandleRef KeyHandle, [In] ref UnicodeString ValueName, [In, Optional] uint TitleIndex, [In] RegValueType Type, [In] IntPtr pData, [In] uint DataSize);


		/// <summary>Unloads a previously loaded Hive file from the registry structure. All changes made to keys and values under this Hive are stored.</summary>
		/// <param name="DestinationKeyName">Pointer to <see cref="ObjectAttributes"/> structure contains path and name of Hive root key.</param>
		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern NtStatus NtUnloadKey([In] ref ObjectAttributes DestinationKeyName);


		//RTL

		[DllImport("ntdll.dll", ThrowOnUnmappableChar = true, BestFitMapping = false, SetLastError = false)]
		public static extern unsafe NtStatus RtlFormatCurrentUserKeyPath([Out] out UnicodeString RegistryPath);
	}
}