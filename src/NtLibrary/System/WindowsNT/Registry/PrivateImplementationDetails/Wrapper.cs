using System.WindowsNT.Errors;
using System.WindowsNT.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;
using Marshal = System.Runtime.InteropServices.Marshal;
using Other = System.WindowsNT.PrivateImplementationDetails.Wrapper;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.WindowsNT.Registry.PrivateImplementationDetails
{
	//[System.Diagnostics.DebuggerStepThrough()]
	internal static partial class Wrapper
	{
		public static void NtCompactKeys(params SafeNtRegistryHandle[] handles)
		{
			System.IntPtr[] dangerousHandles = System.Array.ConvertAll<SafeNtRegistryHandle, System.IntPtr>(handles, (SafeNtRegistryHandle h) => h.DangerousGetHandle());
			NtStatus result = Native.NtCompactKeys((uint)dangerousHandles.Length, dangerousHandles);
			System.GC.KeepAlive(handles);
			CheckAndThrowRegistryException(result);
		}

		public static void NtCompressKey(SafeNtRegistryHandle keyHandle)
		{
			NtStatus result = Native.NtCompressKey(NTInternal.CreateHandleRef(keyHandle));
			CheckAndThrowRegistryException(result);
		}

		public static SafeNtRegistryHandle NtCreateKey(SafeNtRegistryHandle previousHandle, string objectName, KeyAccessMask desiredAccess, RegCreateOptions createOptions, out RegCreationDisposition creationDisposition, AllowedObjectAttributes attributes)
		{
			UnicodeString wName = (UnicodeString)objectName;
			UnicodeString @class = string.Empty;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(NTInternal.GetHandleOrZero(previousHandle), &wName, attributes, null, null);
					System.IntPtr handle;
					NtStatus result = Native.NtCreateKey(out handle, desiredAccess, ref oa, 0, ref @class, createOptions, out creationDisposition);
					System.GC.KeepAlive(previousHandle);
					if (result == NtStatus.OBJECT_NAME_NOT_FOUND | result == NtStatus.OBJECT_PATH_NOT_FOUND)
					{
						return null;
					}
					else
					{
						CheckAndThrowRegistryException(result);
						return new SafeNtRegistryHandle(handle);
					}
				}
			}
			finally
			{
				wName.Dispose();
				@class.Dispose();
			}
		}

		public static void NtDeleteKey(SafeNtRegistryHandle keyHandle)
		{
			NtStatus result = Native.NtDeleteKey(NTInternal.CreateHandleRef(keyHandle));
			CheckAndThrowRegistryException(result);
		}

		public static void NtDeleteValueKey(SafeNtRegistryHandle keyHandle, string valueName)
		{
			UnicodeString wName = (UnicodeString)valueName;
			try
			{
				NtStatus result = Native.NtDeleteValueKey(NTInternal.CreateHandleRef(keyHandle), ref wName);
				CheckAndThrowRegistryException(result);
			}
			finally
			{ wName.Dispose(); }
		}

		private static IEnumerable<T> NtEnumerateKey<T>(SafeNtRegistryHandle keyHandle, uint startIndex, KeyInformationClass keyInformationClass, System.Converter<System.IntPtr, T> marshaler)
		{
			NtStatus result;
			uint i = startIndex;
			using (HGlobal buffer = new HGlobal(Marshal.SizeOf(typeof(T))))
			{
				do
				{
					uint resultLength;
					result = Native.NtEnumerateKey(NTInternal.CreateHandleRef(keyHandle), i, keyInformationClass, buffer.Address, buffer.AllocatedSize32, out resultLength);
					if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
					{
						buffer.ReAlloc(resultLength + Other.ExtraPadding);
						result = Native.NtEnumerateKey(NTInternal.CreateHandleRef(keyHandle), i, keyInformationClass, buffer.Address, buffer.AllocatedSize32, out resultLength);
					}
					if (result == NtStatus.NO_MORE_ENTRIES)
					{
						break;
					}
					CheckAndThrowRegistryException(result);
					yield return marshaler(buffer.Address);
					++i;
				} while (true);
			}
		}

		private static void NtEnumerateKey(SafeNtRegistryHandle keyHandle, uint startIndex, int maxCount, ref System.IntPtr hGlobalBuffer, KeyInformationClass keyInformationClass, ref uint bufferSize, System.Predicate<System.IntPtr> action)
		{
			NtStatus result;
			uint i = startIndex;
			bool @continue;
			do
			{
				uint resultLength;
				result = Native.NtEnumerateKey(NTInternal.CreateHandleRef(keyHandle), i, keyInformationClass, hGlobalBuffer, bufferSize, out resultLength);
				if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
				{
					bufferSize = resultLength + Other.ExtraPadding;
					hGlobalBuffer = Marshal.ReAllocHGlobal(hGlobalBuffer, new System.IntPtr(bufferSize));
					result = Native.NtEnumerateKey(NTInternal.CreateHandleRef(keyHandle), i, keyInformationClass, hGlobalBuffer, bufferSize, out resultLength);
				}
				if (result == NtStatus.NO_MORE_ENTRIES)
				{ break; }
				CheckAndThrowRegistryException(result);
				@continue = action(hGlobalBuffer);
				++i;
			} while (@continue & (maxCount == NtRegistryKey.NoCountLimit || i - startIndex < maxCount));
		}

		public static IEnumerable<KeyBasicInformation> NtEnumerateKeyBasic(SafeNtRegistryHandle keyHandle, uint startIndex)
		{
			return NtEnumerateKey<KeyBasicInformation>(keyHandle, startIndex, KeyInformationClass.KeyBasicInformation, KeyBasicInformation.FromPtr);
		}

		public static IEnumerable<KeyFullInformation> NtEnumerateKeyFull(SafeNtRegistryHandle keyHandle, uint startIndex)
		{
			return NtEnumerateKey<KeyFullInformation>(keyHandle, startIndex, KeyInformationClass.KeyFullInformation, KeyFullInformation.FromPtr);
		}

		public static IEnumerable<KeyNodeInformation> NtEnumerateKeyNode(SafeNtRegistryHandle keyHandle, uint startIndex)
		{
			return NtEnumerateKey<KeyNodeInformation>(keyHandle, startIndex, KeyInformationClass.KeyNodeInformation, KeyNodeInformation.FromPtr);
		}

		private static IEnumerable<T> NtEnumerateValueKey<T>(SafeNtRegistryHandle keyHandle, uint startIndex, int maxCount, KeyValueInformationClass keyValueInformationClass, System.Converter<System.IntPtr, T> marshaler)
		{
			using (HGlobal pKeyValueInformation = new HGlobal(Other.SuggestedBufferSize))
			{
				uint i = startIndex;
				NtStatus result;
				do
				{
					uint resultLength;
					result = Native.NtEnumerateValueKey(NTInternal.CreateHandleRef(keyHandle), i, keyValueInformationClass, pKeyValueInformation.Address, pKeyValueInformation.AllocatedSize32, out resultLength);
					if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
					{
						pKeyValueInformation.ReAlloc(resultLength + Other.ExtraPadding);
						result = Native.NtEnumerateValueKey(NTInternal.CreateHandleRef(keyHandle), i, keyValueInformationClass, pKeyValueInformation.Address, pKeyValueInformation.AllocatedSize32, out resultLength);
					}
					if (result == NtStatus.NO_MORE_ENTRIES)
					{ break; }
					CheckAndThrowRegistryException(result);
					yield return marshaler(pKeyValueInformation.Address);
					++i;
				} while (maxCount == NtRegistryKey.NoCountLimit || i - startIndex < maxCount);
			}
		}

		public static IEnumerable<KeyValueBasicInformation> NtEnumerateValueKeyBasic(SafeNtRegistryHandle keyHandle, uint startIndex, int maxCount)
		{
			return NtEnumerateValueKey<KeyValueBasicInformation>(keyHandle, startIndex, maxCount, KeyValueInformationClass.KeyValueBasicInformation, KeyValueBasicInformation.FromPtr);
		}

		public static IEnumerable<KeyValuePartialInformation> NtEnumerateValueKeyPartial(SafeNtRegistryHandle keyHandle, uint startIndex, int maxCount)
		{
			return NtEnumerateValueKey<KeyValuePartialInformation>(keyHandle, startIndex, maxCount, KeyValueInformationClass.KeyValuePartialInformation, KeyValuePartialInformation.FromPtr);
		}

		public static IEnumerable<KeyValueFullInformation> NtEnumerateValueKeyFull(SafeNtRegistryHandle keyHandle, uint startIndex, int maxCount)
		{
			return NtEnumerateValueKey<KeyValueFullInformation>(keyHandle, startIndex, maxCount, KeyValueInformationClass.KeyValueFullInformation, KeyValueFullInformation.FromPtr);
		}

		public static void NtFlushKey(SafeNtRegistryHandle keyHandle)
		{
			NtStatus result = Native.NtFlushKey(NTInternal.CreateHandleRef(keyHandle));
			CheckAndThrowRegistryException(result);
		}

		public static bool NtKeyExists(SafeNtRegistryHandle previousHandle, string keyOrSubKeyName)
		{
			AllowedObjectAttributes attributes = AllowedObjectAttributes.OpenIf | AllowedObjectAttributes.CaseInsensitive;
			UnicodeString wName = (UnicodeString)keyOrSubKeyName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(NTInternal.GetHandleOrZero(previousHandle), &wName, attributes, null, null);
					System.IntPtr handle;
					NtStatus result = Native.NtOpenKey(out handle, KeyAccessMask.MaximumAllowed, ref oa);
					System.GC.KeepAlive(previousHandle);
#if DEBUG
					if (result != NtStatus.SUCCESS & result != NtStatus.OBJECT_NAME_NOT_FOUND & result != NtStatus.OBJECT_PATH_NOT_FOUND)
					{
						System.Diagnostics.Debugger.Break();
					}
#endif
					WindowsNT.PrivateImplementationDetails.Native.NtClose(handle);
					return NtException.IsSuccess(result);
				}
			}
			finally
			{
				wName.Dispose();
			}
		}

		public static void NtLockRegistryKey(SafeNtRegistryHandle keyHandle)
		{
			NtStatus result = Native.NtLockRegistryKey(NTInternal.CreateHandleRef(keyHandle));
			CheckAndThrowRegistryException(result);
		}

		public static RegNotifyInformation[] NtNotifyChangeKey(SafeNtRegistryHandle keyHandle, RegNotifyFilter filter, bool watchSubTree)
		{
			IoStatusBlock ioStatusBlock;
			uint inputLength = 1 * ((uint)Marshal.SizeOf(typeof(RegNotifyInformation)) + (1 << 15));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)inputLength);
			try
			{
				NtStatus result = Native.NtNotifyChangeKey(NTInternal.CreateHandleRef(keyHandle), System.IntPtr.Zero, null, System.IntPtr.Zero, out ioStatusBlock, filter, watchSubTree, pBuffer, inputLength, false);
				CheckAndThrowRegistryException(result);
				return RegNotifyInformation.FromPtr(pBuffer);
			}
			finally
			{
				Marshal.FreeHGlobal(pBuffer);
			}
		}

		public static void NtLoadKey(SafeNtRegistryHandle hiveHandle, string hiveName, string fileName, AllowedObjectAttributes attributes)
		{
			UnicodeString destKeyName = (UnicodeString)hiveName;
			UnicodeString hiveFileName = (UnicodeString)fileName;
			unsafe
			{
				try
				{
					ObjectAttributes destKey = new ObjectAttributes(hiveHandle.DangerousGetHandle(), &destKeyName, attributes, null, null);
					ObjectAttributes hiveFile = new ObjectAttributes(System.IntPtr.Zero, &hiveFileName, attributes, null, null);
					NtStatus result = Native.NtLoadKey(ref destKey, ref hiveFile);
					System.GC.KeepAlive(hiveHandle);
					CheckAndThrowRegistryException(result);
				}
				finally
				{
					destKeyName.Dispose();
					hiveFileName.Dispose();
				}
			}
		}

		public static void NtLoadKey2(SafeNtRegistryHandle hiveHandle, string hiveName, string fileName, KeyRestoreOptions loadOptions, AllowedObjectAttributes attributes)
		{

			UnicodeString destKeyName = (UnicodeString)hiveName;
			UnicodeString hiveFileName = (UnicodeString)fileName;
			unsafe
			{
				try
				{
					ObjectAttributes destKey = new ObjectAttributes(hiveHandle.DangerousGetHandle(), &destKeyName, attributes, null, null);
					ObjectAttributes hiveFile = new ObjectAttributes(System.IntPtr.Zero, &hiveFileName, attributes, null, null);
					NtStatus result = Native.NtLoadKey2(ref destKey, ref hiveFile, loadOptions);
					System.GC.KeepAlive(hiveHandle);
					CheckAndThrowRegistryException(result);
				}
				finally
				{
					destKeyName.Dispose();
					hiveFileName.Dispose();
				}
			}
		}

		public static SafeNtRegistryHandle NtOpenKey(SafeNtRegistryHandle previousHandle, string objectName, KeyAccessMask desiredAccess, AllowedObjectAttributes attributes)
		{
			UnicodeString wObjectName = (UnicodeString)objectName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(NTInternal.GetHandleOrZero(previousHandle), &wObjectName, attributes, null, null);
					System.IntPtr handle;
					NtStatus result = Native.NtOpenKey(out handle, desiredAccess, ref oa);
					System.GC.KeepAlive(previousHandle);
					if (result == NtStatus.OBJECT_NAME_NOT_FOUND | result == NtStatus.OBJECT_PATH_NOT_FOUND)
					{
						return null;
					}
					else
					{
						CheckAndThrowRegistryException(result);
						return new SafeNtRegistryHandle(handle);
					}
				}
			}
			finally
			{
				wObjectName.Dispose();
			}
		}

		private static void NtQueryKey(SafeNtRegistryHandle keyHandle, KeyInformationClass keyInformationClass, ref System.IntPtr pBuffer, ref uint bufferSize, System.Action<System.IntPtr> action)
		{
			uint resultLength;
			NtStatus result = result = Native.NtQueryKey(NTInternal.CreateHandleRef(keyHandle), keyInformationClass, pBuffer, bufferSize, out resultLength);
			if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
			{
				bufferSize = resultLength + Other.ExtraPadding;
				pBuffer = Marshal.ReAllocHGlobal(pBuffer, new System.IntPtr(bufferSize));
				result = Native.NtQueryKey(NTInternal.CreateHandleRef(keyHandle), keyInformationClass, pBuffer, bufferSize, out resultLength);
			}
			CheckAndThrowRegistryException(result);
			action(pBuffer);
		}

		public static KeyBasicInformation NtQueryKeyBasic(SafeNtRegistryHandle keyHandle)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyBasicInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyBasicInformation? keyInformation = null;
				NtQueryKey(keyHandle, KeyInformationClass.KeyBasicInformation, ref pBuffer, ref bufferSize, buffer => keyInformation = KeyBasicInformation.FromPtr(buffer));
				return keyInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static KeyCachedInformation NtQueryKeyCached(SafeNtRegistryHandle keyHandle)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyCachedInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyCachedInformation? keyInformation = null;
				NtQueryKey(keyHandle, KeyInformationClass.KeyCachedInformation, ref pBuffer, ref bufferSize, buffer => keyInformation = KeyCachedInformation.FromPtr(buffer));
				return keyInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static KeyFlagsInformation NtQueryKeyFlags(SafeNtRegistryHandle keyHandle)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyFlagsInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyFlagsInformation? keyInformation = null;
				NtQueryKey(keyHandle, KeyInformationClass.KeyFlagsInformation, ref pBuffer, ref bufferSize, buffer => keyInformation = KeyFlagsInformation.FromPtr(buffer));
				return keyInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static KeyFullInformation NtQueryKeyFull(SafeNtRegistryHandle keyHandle)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyFullInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyFullInformation? keyInformation = null;
				NtQueryKey(keyHandle, KeyInformationClass.KeyFullInformation, ref pBuffer, ref bufferSize, buffer => keyInformation = KeyFullInformation.FromPtr(buffer));
				return keyInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static KeyNameInformation NtQueryKeyName(SafeNtRegistryHandle keyHandle)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyNameInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyNameInformation? keyInformation = null;
				NtQueryKey(keyHandle, KeyInformationClass.KeyNameInformation, ref pBuffer, ref bufferSize, buffer => keyInformation = KeyNameInformation.FromPtr(buffer));
				return keyInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static KeyNodeInformation NtQueryKeyNode(SafeNtRegistryHandle keyHandle)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyNodeInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyNodeInformation? keyInformation = null;
				NtQueryKey(keyHandle, KeyInformationClass.KeyNodeInformation, ref pBuffer, ref bufferSize, buffer => keyInformation = KeyNodeInformation.FromPtr(buffer));
				return keyInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static uint NtQueryOpenSubKeys(SafeNtRegistryHandle keyHandle, string keyName)
		{
			uint count;
			UnicodeString wName = (UnicodeString)keyName;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(keyHandle.DangerousGetHandle(), &wName, AllowedObjectAttributes.None, null, null);
					NtStatus result = Native.NtQueryOpenSubKeys(ref oa, out count);
					System.GC.KeepAlive(keyHandle);
					CheckAndThrowRegistryException(result);
					return count;
				}
			}
			finally
			{
				wName.Dispose();
			}
		}

		public static KeyBasicInformation QuerySubKeyBasic(SafeNtRegistryHandle keyHandle, uint index)
		{
			foreach (KeyBasicInformation ki in NtEnumerateKeyBasic(keyHandle, index))
			{
				return ki;
			}
			CheckAndThrowRegistryException(NtStatus.NO_MORE_ENTRIES);
			throw new System.ArgumentOutOfRangeException("No more entries found.");
		}

		public static KeyNodeInformation QuerySubKeyNode(SafeNtRegistryHandle keyHandle, uint index)
		{
			foreach (KeyNodeInformation ki in NtEnumerateKeyNode(keyHandle, index))
			{
				return ki;
			}
			CheckAndThrowRegistryException(NtStatus.NO_MORE_ENTRIES);
			throw new System.ArgumentOutOfRangeException("No more entries found.");
		}

		public static KeyFullInformation QuerySubKeyFull(SafeNtRegistryHandle keyHandle, uint index)
		{
			foreach (KeyFullInformation ki in NtEnumerateKeyFull(keyHandle, index))
			{
				return ki;
			}
			CheckAndThrowRegistryException(NtStatus.NO_MORE_ENTRIES);
			throw new System.ArgumentOutOfRangeException("No more entries found.");
		}

		private static void NtQueryValueKey(SafeNtRegistryHandle keyHandle, string valueName, ref System.IntPtr pBuffer, ref uint bufferSize, KeyValueInformationClass keyValueInformationClass, System.Action<System.IntPtr> action)
		{
			UnicodeString wValueName = (UnicodeString)valueName;
			try
			{
				uint resultLength;
				NtStatus result = Native.NtQueryValueKey(NTInternal.CreateHandleRef(keyHandle), ref wValueName, keyValueInformationClass, pBuffer, bufferSize, out resultLength);
				if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
				{
					bufferSize = resultLength + Other.ExtraPadding;
					pBuffer = Marshal.ReAllocHGlobal(pBuffer, new System.IntPtr(bufferSize));
					result = Native.NtQueryValueKey(NTInternal.CreateHandleRef(keyHandle), ref wValueName, keyValueInformationClass, pBuffer, bufferSize, out resultLength);
				}
				CheckAndThrowRegistryException(result);
				action(pBuffer);
			}
			finally
			{ wValueName.Dispose(); }
		}

		public static KeyValueBasicInformation NtQueryValueKeyBasic(SafeNtRegistryHandle keyHandle, string valueName)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyValueBasicInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyValueBasicInformation? keyValueInformation = null;
				NtQueryValueKey(keyHandle, valueName, ref pBuffer, ref bufferSize, KeyValueInformationClass.KeyValueBasicInformation, buffer => keyValueInformation = KeyValueBasicInformation.FromPtr(buffer));
				return keyValueInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static KeyValueFullInformation NtQueryValueKeyFull(SafeNtRegistryHandle keyHandle, string valueName)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyValueFullInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyValueFullInformation? keyValueInformation = null;
				NtQueryValueKey(keyHandle, valueName, ref pBuffer, ref bufferSize, KeyValueInformationClass.KeyValueFullInformation, buffer => keyValueInformation = KeyValueFullInformation.FromPtr(buffer));
				return keyValueInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static KeyValuePartialInformation NtQueryValueKeyPartial(SafeNtRegistryHandle keyHandle, string valueName)
		{
			uint bufferSize = (uint)Marshal.SizeOf(typeof(KeyValuePartialInformation));
			System.IntPtr pBuffer = Marshal.AllocHGlobal((int)bufferSize);
			try
			{
				KeyValuePartialInformation? keyValueInformation = null;
				NtQueryValueKey(keyHandle, valueName, ref pBuffer, ref bufferSize, KeyValueInformationClass.KeyValuePartialInformation, buffer => keyValueInformation = KeyValuePartialInformation.FromPtr(buffer));
				return keyValueInformation.Value;
			}
			finally
			{ Marshal.FreeHGlobal(pBuffer); }
		}

		public static KeyValueBasicInformation NtQueryValueKeyBasic(SafeNtRegistryHandle keyHandle, uint index)
		{
			foreach (KeyValueBasicInformation kvi in NtEnumerateValueKeyBasic(keyHandle, index, 1))
			{
				return kvi;
			}
			CheckAndThrowRegistryException(NtStatus.NO_MORE_ENTRIES);
			throw new System.ArgumentOutOfRangeException("No more entries found.");
		}

		public static KeyValueFullInformation NtQueryValueKeyFull(SafeNtRegistryHandle keyHandle, uint index)
		{
			foreach (KeyValueFullInformation kvi in NtEnumerateValueKeyFull(keyHandle, index, 1))
			{
				return kvi;
			}
			CheckAndThrowRegistryException(NtStatus.NO_MORE_ENTRIES);
			throw new System.ArgumentOutOfRangeException("No more entries found.");
		}

		public static KeyValuePartialInformation NtQueryValueKeyPartial(SafeNtRegistryHandle keyHandle, uint index)
		{
			foreach (KeyValuePartialInformation kvi in NtEnumerateValueKeyPartial(keyHandle, index, 1))
			{
				return kvi;
			}
			CheckAndThrowRegistryException(NtStatus.NO_MORE_ENTRIES);
			throw new System.ArgumentOutOfRangeException("No more entries found.");
		}

		public static uint NtQuerySubKeyCount(SafeNtRegistryHandle keyHandle)
		{
			KeyFullInformation keyInformation = new KeyFullInformation();
			uint resultLength;
			NtStatus result = Native.NtQueryKey(NTInternal.CreateHandleRef(keyHandle), KeyInformationClass.KeyFullInformation, out keyInformation, (uint)Marshal.SizeOf(keyInformation), out resultLength);
			if (result != NtStatus.BUFFER_OVERFLOW)
			{
				CheckAndThrowRegistryException(result);
			}
			return keyInformation.SubKeys;
		}

		public static void NtRenameKey(SafeNtRegistryHandle keyHandle, string newName)
		{
			UnicodeString wNewName = (UnicodeString)newName;
			try
			{
				NtStatus result = Native.NtRenameKey(NTInternal.CreateHandleRef(keyHandle), ref wNewName);
				CheckAndThrowRegistryException(result);
			}
			finally
			{
				wNewName.Dispose();
			}
		}

		public static void NtReplaceKey(SafeNtRegistryHandle keyHandle, string newHiveFileName, AllowedObjectAttributes newHiveAttributes, string backupHiveFileName, AllowedObjectAttributes backupHiveAttributes)
		{
			UnicodeString wNewHiveFileName = (UnicodeString)newHiveFileName;
			UnicodeString wBackupHiveFileName = (UnicodeString)backupHiveFileName;
			try
			{
				unsafe
				{
					ObjectAttributes oaNew = new ObjectAttributes(System.IntPtr.Zero, &wNewHiveFileName, newHiveAttributes, null, null);
					ObjectAttributes oaBackup = new ObjectAttributes(System.IntPtr.Zero, &wBackupHiveFileName, newHiveAttributes, null, null);
					NtStatus result = Native.NtReplaceKey(ref oaNew, NTInternal.CreateHandleRef(keyHandle), ref oaBackup);
					CheckAndThrowRegistryException(result);
				}
			}
			finally
			{
				wNewHiveFileName.Dispose();
				wBackupHiveFileName.Dispose();
			}
		}

		public static void NtRestoreKey(SafeNtRegistryHandle keyHandle, SafeNtFileHandle fileHandle, KeyRestoreOptions restoreOptions)
		{
			NtStatus result = Native.NtRestoreKey(NTInternal.CreateHandleRef(keyHandle), NTInternal.CreateHandleRef(fileHandle), restoreOptions);
			CheckAndThrowRegistryException(result);
		}

		public static void NtSaveKey(SafeNtRegistryHandle keyHandle, SafeNtFileHandle fileHandle)
		{
			NtStatus result = Native.NtSaveKey(NTInternal.CreateHandleRef(keyHandle), NTInternal.CreateHandleRef(fileHandle));
			CheckAndThrowRegistryException(result);
		}

		public static void NtSaveKeyEx(SafeNtRegistryHandle keyHandle, SafeNtFileHandle fileHandle, RegHiveFormat format)
		{
			NtStatus result = Native.NtSaveKeyEx(NTInternal.CreateHandleRef(keyHandle), NTInternal.CreateHandleRef(fileHandle), format);
			CheckAndThrowRegistryException(result);
		}

		public static void NtSetInformationKey(SafeNtRegistryHandle keyHandle, KeyUserFlagsInformation keyUserFlagsInformation)
		{
			NtStatus result = Native.NtSetInformationKey(NTInternal.CreateHandleRef(keyHandle), KeySetInformationClass.KeyUserFlagsInformation, ref keyUserFlagsInformation, (uint)Marshal.SizeOf(keyUserFlagsInformation));
			CheckAndThrowRegistryException(result);
		}

		public static void NtSetInformationKey(SafeNtRegistryHandle keyHandle, KeyWriteTimeInformation keyWriteTimeInformation)
		{
			NtStatus result = Native.NtSetInformationKey(NTInternal.CreateHandleRef(keyHandle), KeySetInformationClass.KeyWriteTimeInformation, ref keyWriteTimeInformation, (uint)Marshal.SizeOf(keyWriteTimeInformation));
			CheckAndThrowRegistryException(result);
		}

		public static void NtSetValueKey(SafeNtRegistryHandle keyHandle, string valueName, RegistryValueData value, uint titleIndex)
		{
			UnicodeString wValueName = (UnicodeString)valueName;
			try
			{
				System.IntPtr pValue = value.ToHGlobal();
				try
				{
					NtStatus result = Native.NtSetValueKey(NTInternal.CreateHandleRef(keyHandle), ref wValueName, titleIndex, value.Type, pValue, (uint)value.GetMarshaledSize());
					CheckAndThrowRegistryException(result);
				}
				finally
				{ Marshal.FreeHGlobal(pValue); }
			}
			finally
			{ wValueName.Dispose(); }
		}

		public static void NtUnloadKey(string hiveNameAndPath)
		{
			UnicodeString wHiveNameAndPath = (UnicodeString)hiveNameAndPath;
			try
			{
				unsafe
				{
					ObjectAttributes oa = new ObjectAttributes(System.IntPtr.Zero, &wHiveNameAndPath, AllowedObjectAttributes.None, null, null);
					NtStatus result = Native.NtUnloadKey(ref oa);
					CheckAndThrowRegistryException(result);
				}
			}
			finally
			{
				wHiveNameAndPath.Dispose();
			}
		}

		public static bool NtValueExists(SafeNtRegistryHandle keyHandle, string valueName)
		{
			bool exists;

			uint resultLength;
			uint inputLength = (uint)Marshal.SizeOf(typeof(KeyValueBasicInformation));

			System.IntPtr pKeyValueInformation = Marshal.AllocHGlobal((int)inputLength);
			UnicodeString wValueName = (UnicodeString)valueName;
			try
			{
				NtStatus result = Native.NtQueryValueKey(NTInternal.CreateHandleRef(keyHandle), ref wValueName, KeyValueInformationClass.KeyValueBasicInformation, pKeyValueInformation, inputLength, out resultLength);
				if (result == NtStatus.BUFFER_OVERFLOW | result == NtStatus.BUFFER_TOO_SMALL)
				{
					inputLength = resultLength + Other.ExtraPadding;
					pKeyValueInformation = Marshal.ReAllocHGlobal(pKeyValueInformation, new System.IntPtr(inputLength));
					result = Native.NtQueryValueKey(NTInternal.CreateHandleRef(keyHandle), ref wValueName, KeyValueInformationClass.KeyValueBasicInformation, pKeyValueInformation, inputLength, out resultLength);
				}
				exists = NtException.IsSuccess(result);
#if DEBUG
				if (result != NtStatus.SUCCESS & result != NtStatus.OBJECT_NAME_NOT_FOUND & result != NtStatus.OBJECT_PATH_NOT_FOUND)
				{
					System.Diagnostics.Debugger.Break();
				}
#endif
				return exists;
			}
			finally
			{
				wValueName.Dispose();
				Marshal.FreeHGlobal(pKeyValueInformation);
			}
		}

		[System.Diagnostics.DebuggerStepThrough()]
		private static void CheckAndThrowRegistryException(NtStatus errorCode)
		{
			if (!NtException.IsSuccess(errorCode))
			{
				System.Exception ex  = null;
				switch (errorCode)
				{
					case NtStatus.OBJECT_NAME_NOT_FOUND:
						ex = new NameNotFoundException("The specified registry key or value was not found.");
						break;
					case NtStatus.OBJECT_PATH_NOT_FOUND:
						ex = new PathNotFoundException("The specified registry path was not found.");
						break;
					case NtStatus.OBJECT_NAME_INVALID:
						ex = new InvalidNameException("The specified registry name was invalid.");
						break;
					case NtStatus.OBJECT_PATH_INVALID:
						ex = new InvalidPathException("The specified registry path was invalid.");
						break;
					case NtStatus.ACCESS_DENIED:
						ex = new System.UnauthorizedAccessException();
						break;
				}
				if (ex != null)
				{
					System.Diagnostics.Debug.WriteLine(ex);
					throw ex;
				}
				else
				{
					NtException.CheckAndThrowException(errorCode);
				}
			}
		}
	}
}