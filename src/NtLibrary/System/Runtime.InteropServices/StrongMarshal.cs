using BindingFlags = System.Reflection.BindingFlags;

namespace System.Runtime.InteropServices
{
	public static class StrongMarshal
	{
		private static readonly Type SafePointerType = Type.GetType("System.Runtime.InteropServices.SafePointer", true);

		private static class GenericDelegates<T>
			where T : struct
		{
			private static GenericStructureToPtrDelegate<T> __structureToPointerDelegate;
			private static GenericPtrToStructureDelegate<T> __pointerToStructureDelegate;
			private static SizeOfDelegate<T> __sizeOfDelegate;

			public static GenericStructureToPtrDelegate<T> StructureToPointerDelegate
			{
				get
				{
					if (__structureToPointerDelegate == null)
					{ __structureToPointerDelegate = (GenericStructureToPtrDelegate<T>)System.Delegate.CreateDelegate(typeof(GenericStructureToPtrDelegate<T>), SafePointerType.GetMethod("GenericStructureToPtr", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeof(T))); }
					return __structureToPointerDelegate;
				}
			}

			public static GenericPtrToStructureDelegate<T> PointerToStructureDelegate
			{
				get
				{
					if (__pointerToStructureDelegate == null)
					{ __pointerToStructureDelegate = (GenericPtrToStructureDelegate<T>)System.Delegate.CreateDelegate(typeof(GenericPtrToStructureDelegate<T>), SafePointerType.GetMethod("GenericPtrToStructure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeof(T))); }
					return __pointerToStructureDelegate;
				}
			}

			public static SizeOfDelegate<T> SizeOfDelegate
			{
				get
				{
					if (__sizeOfDelegate == null)
					{ __sizeOfDelegate = (SizeOfDelegate<T>)System.Delegate.CreateDelegate(typeof(SizeOfDelegate<T>), SafePointerType.GetMethod("SizeOf", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeof(T))); }
					return __sizeOfDelegate;
				}
			}
		}

		#region DelegateFields
		private static readonly PointerToArrayCopyDelegate CopyPointerToArray =
			(PointerToArrayCopyDelegate)System.Delegate.CreateDelegate(typeof(PointerToArrayCopyDelegate), typeof(System.Buffer), "memcpy");
		private static readonly ArrayToPointerCopyDelegate CopyArrayToPointer =
			(ArrayToPointerCopyDelegate)System.Delegate.CreateDelegate(typeof(ArrayToPointerCopyDelegate), typeof(System.Buffer), "memcpy");
		private static readonly PointerToPointerCopyImplementationDelegate CopyPointerToPointer =
			(PointerToPointerCopyImplementationDelegate)System.Delegate.CreateDelegate(typeof(PointerToPointerCopyImplementationDelegate), typeof(System.Buffer), "memcpyimpl");
		#endregion

		public static uint SizeOf<T>() where T : struct { return GenericDelegates<T>.SizeOfDelegate(); }

		public static void StructureToPtr<T>(ref T value, IntPtr ptr) where T : struct
		{ unsafe { GenericDelegates<T>.StructureToPointerDelegate(ref value, (byte*)ptr, SizeOf<T>()); } }

		public static void StructureToPtr<T>(T value, IntPtr ptr) where T : struct { StructureToPtr<T>(ref value, ptr); }

		public static void PtrToStructure<T>(IntPtr ptr, out T value) where T : struct
		{ unsafe { GenericDelegates<T>.PointerToStructureDelegate((byte*)ptr, out value, SizeOf<T>()); } }

		public static T PtrToStructure<T>(IntPtr ptr) where T : struct { T value; PtrToStructure<T>(ptr, out value); return value; }

		public static int OffsetOf<T>(string fieldName)
		{ return (int)Marshal.OffsetOf(typeof(T), fieldName); }

#if CUSTOM

		public static void Copy(IntPtr address, Array array, int byteCount)
		{ Copy(address, 0, array, 0, byteCount); }

		public static void Copy(IntPtr address, int pointerOffset, Array array, int arrayOffset, int byteCount)
		{
			GCHandle hArr = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				unsafe
				{
					IntPtr* pArrayCurrent = (IntPtr*)hArr.AddrOfPinnedObject();
					IntPtr* pUnmanagedCurrent = (IntPtr*)((byte*)address + pointerOffset);
					int bytesLeft = byteCount;
					for (; bytesLeft >= sizeof(IntPtr); pArrayCurrent++, pUnmanagedCurrent++, bytesLeft -= sizeof(IntPtr))
					{
						*pArrayCurrent = *pUnmanagedCurrent;
					}

					byte* pArrayByte = (byte*)pArrayCurrent;
					byte* pUnmanagedByte = (byte*)pUnmanagedCurrent;
					for (; bytesLeft > 0; ++pArrayByte, ++pUnmanagedByte, bytesLeft--)
					{
						*pArrayByte = *pUnmanagedByte;
					}
				}
			}
			finally
			{
				hArr.Free();
			}
		}

		public static void Copy(Array array, int arrayOffset, IntPtr address, int pointerOffset, int byteCount)
		{
			GCHandle hArr = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				unsafe
				{
					IntPtr* pArrayCurrent = (IntPtr*)hArr.AddrOfPinnedObject();
					IntPtr* pUnmanagedCurrent = (IntPtr*)((byte*)address + pointerOffset);
					int bytesLeft = byteCount;
					for (; bytesLeft >= sizeof(IntPtr); pArrayCurrent++, pUnmanagedCurrent++, bytesLeft -= sizeof(IntPtr))
					{
						*pUnmanagedCurrent = *pArrayCurrent;
					}

					byte* pArrayByte = (byte*)pArrayCurrent;
					byte* pUnmanagedByte = (byte*)pUnmanagedCurrent;
					for (; bytesLeft > 0; ++pArrayByte, ++pUnmanagedByte, bytesLeft--)
					{
						*pUnmanagedByte = *pArrayByte;
					}
				}
			}
			finally
			{
				hArr.Free();
			}
		}

		public static void Copy(Array array, IntPtr address, int byteCount)
		{ Copy(array, 0, address, 0, byteCount); }

		public static void CopyArray(Array array, IntPtr address, int elementCount)
		{ Copy(array, address, elementCount * Marshal.SizeOf(array.GetType().GetElementType())); }

		public static void CopyArray(IntPtr address, Array array, int elementCount)
		{ Copy(address, array, elementCount * Marshal.SizeOf(array.GetType())); }

#else

		public static void Copy(IntPtr source, int sourceByteOffset, byte[] destination, int destinationOffset, int byteCount)
		{ unsafe { CopyPointerToArray((byte*)source, sourceByteOffset, destination, destinationOffset, byteCount); } }

		public static void Copy(byte[] source, int sourceOffset, IntPtr destination, int destinationByteOffset, int byteCount)
		{ unsafe { CopyArrayToPointer(source, sourceOffset, (byte*)destination, destinationByteOffset, byteCount); } }

		public static void Copy(IntPtr source, IntPtr destination, int bytes)
		{ unsafe { CopyPointerToPointer((byte*)source, (byte*)destination, bytes); } }

#endif


		#region Delegates
		private unsafe delegate void PointerToArrayCopyDelegate(byte* src, int srcIndex, byte[] dest, int destIndex, int len);

		private unsafe delegate void ArrayToPointerCopyDelegate(byte[] src, int srcIndex, byte* dest, int destIndex, int len);

		private unsafe delegate void PointerToPointerCopyImplementationDelegate(byte* src, byte* dest, int len);


		private unsafe delegate void GenericStructureToPtrDelegate<T>(ref T structure, byte* ptr, uint sizeofT) where T : struct;

		private unsafe delegate void GenericPtrToStructureDelegate<T>(byte* ptr, out T structure, uint sizeofT) where T : struct;

		private delegate uint SizeOfDelegate<T>() where T : struct;
		#endregion
	}
}