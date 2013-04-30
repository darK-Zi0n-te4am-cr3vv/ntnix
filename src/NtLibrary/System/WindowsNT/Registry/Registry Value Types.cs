using System.Runtime.InteropServices;
namespace System.WindowsNT.Registry
{
	public struct RegValueInformation : System.IEquatable<RegValueInformation>, System.ICloneable
	{
		/// <summary>Not meant to be used by clients.</summary>
		public RegValueInformation(string name, RegistryValueData data) : this(name, name.Length * sizeof(char), data, data.GetMarshaledSize(), 0) { }
		/// <summary>Not meant to be used by clients.</summary>
		public RegValueInformation(string name, int nameSize, RegistryValueData data, int dataSize, int titleIndex)
			: this()
		{
			if (name == null)
			{ throw new System.ArgumentNullException("name"); }
			if (data == null)
			{ throw new System.ArgumentNullException("data"); }
			this.Name = name;
			this.NameSize = nameSize;
			this.Data = data;
			this.DataSize = dataSize;
			this.TitleIndex = titleIndex;
		}
		public string Name { get; set; }
		public int NameSize { get; set; }
		public RegistryValueData Data { get; set; }
		public int DataSize { get; set; }
		public int TitleIndex { get; set; }
		public bool Equals(RegValueInformation other) { return this.Name.Equals(other.Name, WindowsNT.PrivateImplementationDetails.Wrapper.DefaultStringComparison) && this.Data.Equals(other.Data); }
		public override bool Equals(object obj) { return obj is RegValueInformation && this.Equals((RegValueInformation)obj); }
		public override int GetHashCode() { return this.Name.GetHashCode() ^ this.Data.GetHashCode(); }
		public object Clone()
		{
			RegValueInformation result = (RegValueInformation)this.Clone();
			result.Name = (string)this.Name.Clone();
			result.Data = (RegistryValueData)this.Data.Clone();
			return result;
		}
	}


	/*
		internal interface IMarshalable
		{
			System.IntPtr ToHGlobal();
			int GetMarshaledSize();
		}
		public interface IRegistryValueData : IMarshalable
		{
			RegValueType Type { get; }
			object Value { get; }
			string ToString();
		}
	*/
	public abstract class RegistryValueData : System.ICloneable, System.IComparable
	{
		internal RegistryValueData(RegValueType type) : base() { this.Type = type; }
		public abstract object ObjectData { get; set;}
		public RegValueType Type { get; private set; }
		public abstract override string ToString();
		internal abstract System.IntPtr ToHGlobal();
		public abstract int GetMarshaledSize();
		public virtual object Clone() { return this.MemberwiseClone(); }
		public abstract override bool Equals(object obj);
		public abstract override int GetHashCode();
		internal static RegistryValueData ToActualType(RegValueType valueType, byte[] data)
		{ unsafe { fixed (byte* pData = data) { return ToActualType(valueType, new System.IntPtr(pData), (uint)data.Length); } } }
		internal static RegistryValueData ToActualType(RegValueType valueType, System.IntPtr data, uint byteLength)
		{
			RegistryValueData result;
			switch (valueType)
			{
				case RegValueType.None:
					goto case RegValueType.Binary;
				case RegValueType.SZ:
					if (data == System.IntPtr.Zero)
					{ result = (SZ)string.Empty; }
					else
					{ result = (SZ)Marshal.PtrToStringUni(data, (int)byteLength / sizeof(char)); }
					break;
				case RegValueType.ExpandSZ:
					if (data == System.IntPtr.Zero)
					{ result = (ExpandSZ)string.Empty; }
					else
					{ result = (ExpandSZ)Marshal.PtrToStringUni(data, (int)byteLength / sizeof(char)); }
					break;
				case RegValueType.Binary:
					if (data == System.IntPtr.Zero)
					{ result = (Binary)new byte[0]; }
					else
					{
						byte[] bytes = new byte[byteLength];
						Marshal.Copy(data, bytes, 0, bytes.Length);
						result = (Binary)bytes;
					}
					break;
				case RegValueType.DWordLittleEndian:
					if (data == System.IntPtr.Zero)
					{ result = new DWordLittleEndian(); }
					else
					{ result = new DWordLittleEndian(Marshal.ReadInt32(data)); }
					break;
				case RegValueType.DWordBigEndian:
					if (data == System.IntPtr.Zero)
					{ result = new DWordBigEndian(); }
					else
					{ result = new DWordBigEndian((BigEndianInt)Marshal.PtrToStructure(data, typeof(BigEndianInt))); }
					break;
				case RegValueType.Link:
					if (data == System.IntPtr.Zero)
					{ result = (Link)new byte[0]; }
					else
					{
						byte[] bytes = new byte[byteLength];
						Marshal.Copy(data, bytes, 0, bytes.Length);
						result = (Link)bytes;
					}
					break;
				case RegValueType.MultiSZ:
					if (data == System.IntPtr.Zero)
					{ result = (MultiSZ)string.Empty; }
					else
					{ result = (MultiSZ)Marshal.PtrToStringUni(data, (int)byteLength / sizeof(char)); }
					break;
				case RegValueType.ResourceList:
					if (data == System.IntPtr.Zero)
					{ result = (ResourceList)new byte[0]; }
					else
					{
						byte[] bytes = new byte[byteLength];
						Marshal.Copy(data, bytes, 0, bytes.Length);
						result = (ResourceList)bytes;
					}
					break;
				case RegValueType.FullResourceDescriptor:
					if (data == System.IntPtr.Zero)
					{ result = (FullResourceDescriptor)new byte[0]; }
					else
					{
						byte[] bytes = new byte[byteLength];
						Marshal.Copy(data, bytes, 0, bytes.Length);
						result = (FullResourceDescriptor)bytes;
					}
					break;
				case RegValueType.ResourceRequirementList:
					if (data == System.IntPtr.Zero)
					{ result = (ResourceRequirementList)new byte[0]; }
					else
					{
						byte[] bytes = new byte[byteLength];
						Marshal.Copy(data, bytes, 0, bytes.Length);
						result = (ResourceRequirementList)bytes;
					}
					break;
				case RegValueType.QWordLittleEndian:
					if (data == System.IntPtr.Zero)
					{ result = new QWordLittleEndian(); }
					else
					{ result = new QWordLittleEndian(Marshal.ReadInt64(data)); }
					break;
				default:
					throw new System.ArgumentException(string.Format("This type is not supported: {0}", valueType));
			}
			return result;
		}
		public abstract int CompareTo(object other);
	}

	public class QWordLittleEndian : RegistryValueData, System.IComparable<QWordLittleEndian>
	{
		public QWordLittleEndian() : this(new long()) { }
		public QWordLittleEndian(long value) : base(RegValueType.QWordLittleEndian) { this.Value = value; }
		public override string ToString() { return this.Value.ToString(); }
		public override bool Equals(object obj) { return obj is QWordLittleEndian && this.Equals((QWordLittleEndian)obj); }
		public override int GetHashCode() { return this.Value.GetHashCode(); }
		public long Value { get; private set; }
		public bool Equals(QWordLittleEndian other) { return /*other != null &&*/ this.Value.Equals(other.Value); }
		public override object Clone() { return this.MemberwiseClone(); }
		public static implicit operator QWordLittleEndian(long value) { return new QWordLittleEndian(value); }
		public static implicit operator long(QWordLittleEndian value) { return value.Value; }
		internal override System.IntPtr ToHGlobal()
		{
			System.IntPtr result = Marshal.AllocHGlobal(this.GetMarshaledSize());
			Marshal.StructureToPtr(this.Value, result, false);
			return result;
		}
		public override int GetMarshaledSize() { return Marshal.SizeOf(this.Value); }
		public override object ObjectData
		{
			get { return this.Value; }
			set { this.Value = (long)value; }
		}
		public override int CompareTo(object other) { return this.CompareTo((QWordLittleEndian)other); }
		public int CompareTo(QWordLittleEndian other) { return this.Value.CompareTo(other.Value); }
	}

	public class DWordLittleEndian : RegistryValueData
	{
		public DWordLittleEndian() : this(new int()) { }
		public DWordLittleEndian(int value) : base(RegValueType.DWordLittleEndian) { this.Value = value; }
		public override string ToString() { return this.Value.ToString(); }
		public override bool Equals(object obj) { return obj is DWordLittleEndian && this.Equals((DWordLittleEndian)obj); }
		public override int GetHashCode() { return this.Value.GetHashCode(); }
		public int Value { get; private set; }
		public bool Equals(DWordLittleEndian other) { return /*other != null &&*/ this.Value.Equals(other.Value); }
		public override object Clone() { return this.MemberwiseClone(); }
		public static implicit operator DWordLittleEndian(int value) { return new DWordLittleEndian(value); }
		public static implicit operator int(DWordLittleEndian value) { return value.Value; }
		internal override System.IntPtr ToHGlobal()
		{
			System.IntPtr result = Marshal.AllocHGlobal(this.GetMarshaledSize());
			Marshal.StructureToPtr(this.Value, result, false);
			return result;
		}
		public override int GetMarshaledSize() { return Marshal.SizeOf(this.Value); }
		public override object ObjectData
		{
			get { return this.Value; }
			set { this.Value = (DWordLittleEndian)value; }
		}
		public override int CompareTo(object other) { return this.CompareTo((DWordLittleEndian)other); }
		public int CompareTo(DWordLittleEndian other) { return this.Value.CompareTo(other.Value); }
	}

	public class DWordBigEndian : RegistryValueData
	{
		public DWordBigEndian() : this(new BigEndianInt()) { }
		public DWordBigEndian(BigEndianInt value) : base(RegValueType.DWordBigEndian) { this.Value = value; }
		public override string ToString() { return this.Value.ToString(); }
		public override bool Equals(object obj) { return obj is DWordBigEndian && this.Equals((DWordBigEndian)obj); }
		public override int GetHashCode() { return this.Value.GetHashCode(); }
		public BigEndianInt Value { get; private set; }
		public bool Equals(DWordBigEndian other) { return /*other != null &&*/ this.Value.Equals(other.Value); }
		public override object Clone() { return this.MemberwiseClone(); }
		public static implicit operator DWordBigEndian(BigEndianInt value) { return new DWordBigEndian(value); }
		public static implicit operator BigEndianInt(DWordBigEndian value) { return value.Value; }
		internal override System.IntPtr ToHGlobal()
		{
			System.IntPtr result = Marshal.AllocHGlobal(this.GetMarshaledSize());
			Marshal.StructureToPtr(this.Value, result, false);
			return result;
		}
		public override int GetMarshaledSize() { return Marshal.SizeOf(this.Value); }
		public override object ObjectData
		{
			get { return this.Value; }
			set { this.Value = (DWordBigEndian)value; }
		}
		public override int CompareTo(object other) { return this.CompareTo((DWordBigEndian)other); }
		public int CompareTo(DWordBigEndian other) { return this.Value.CompareTo(other.Value); }
	}

	public class SZ : RegistryValueData
	{
		public SZ(string value) : base(RegValueType.SZ) { this.Value = value; }
		public override string ToString() { return this.Value.ToString(); }
		public override bool Equals(object obj) { return this.Equals(obj as SZ); }
		public override int GetHashCode() { return this.Value.GetHashCode(); }
		public string Value { get; private set; }
		public bool Equals(SZ other) { return other != null && this.Value.Equals(other.Value); }
		public override object Clone() { return this.MemberwiseClone(); }
		public static explicit operator SZ(string value) { return new SZ(value); }
		public static implicit operator string(SZ value) { return value.Value; }
		internal override System.IntPtr ToHGlobal() { return Marshal.StringToHGlobalUni(this.Value); }
		public override int GetMarshaledSize() { return this.Value.Length * sizeof(char); }
		public override object ObjectData
		{
			get { return this.Value; }
			set { this.Value = (SZ)value; }
		}
		public override int CompareTo(object other) { return this.CompareTo((SZ)other); }
		public int CompareTo(SZ other) { return this.Value.CompareTo(other.Value); }
	}

	public class ExpandSZ : RegistryValueData
	{
		public ExpandSZ(string value) : base(RegValueType.ExpandSZ) { this.Value = value; }
		public override string ToString() { return this.Value.ToString(); }
		public override bool Equals(object obj) { return this.Equals(obj as ExpandSZ); }
		public override int GetHashCode() { return this.Value.GetHashCode(); }
		public string Value { get; private set; }
		public bool Equals(ExpandSZ other) { return other != null && this.Value.Equals(other.Value); }
		public override object Clone() { return this.MemberwiseClone(); }
		public static explicit operator ExpandSZ(string value) { return new ExpandSZ(value); }
		public static implicit operator string(ExpandSZ value) { return value.Value; }
		internal override System.IntPtr ToHGlobal() { return Marshal.StringToHGlobalUni(this.Value); }
		public override int GetMarshaledSize() { return this.Value.Length * sizeof(char); }
		public override object ObjectData
		{
			get { return this.Value; }
			set { this.Value = (ExpandSZ)value; }
		}
		public override int CompareTo(object other) { return this.CompareTo((ExpandSZ)other); }
		public int CompareTo(ExpandSZ other) { return this.Value.CompareTo(other.Value); }
	}

	public class MultiSZ : RegistryValueData
	{
		public MultiSZ(string value) : base(RegValueType.MultiSZ) { this.Value = value; }
		public override string ToString() { return this.Value; }
		public override bool Equals(object obj) { return this.Equals(obj as MultiSZ); }
		public override int GetHashCode() { return this.Value.GetHashCode(); }
		public string Value { get; private set; }
		public bool Equals(MultiSZ other) { return other != null && this.Value.Equals(other.Value); }
		public override object Clone() { return this.MemberwiseClone(); }
		public static explicit operator MultiSZ(string value) { return new MultiSZ(value); }
		public static implicit operator string(MultiSZ value) { return value.Value; }
		internal override System.IntPtr ToHGlobal() { return Marshal.StringToHGlobalUni(this.Value); }
		public override int GetMarshaledSize() { return this.Value.Length * sizeof(char); }
		public override object ObjectData
		{
			get { return this.Value; }
			set { this.Value = (MultiSZ)value; }
		}
		public override int CompareTo(object other) { return this.CompareTo((MultiSZ)other); }
		public int CompareTo(MultiSZ other) { return this.Value.CompareTo(other.Value); }
	}

	public class Binary : RegistryValueData
	{
		public static readonly System.Text.Encoding BinaryTextEncoding = System.Text.Encoding.Unicode;
		
		private byte[] _value;
		public Binary(byte[] value) : this(value, RegValueType.Binary) { }
		internal Binary(byte[] value, RegValueType type) : base(type) { this.Data = value; }
		public override string ToString()
		{
			/*
			System.Text.StringBuilder sb = new System.Text.StringBuilder(2 * this._value.Length);
			sb.Append("0x");
			for (int i = 0; i < this._value.Length; ++i)
			{ sb.Append(this._value[i].ToString("X").PadRight(2, '0')); }
			return sb.ToString();
			*/
			return BinaryTextEncoding.GetString(this._value);
		}
		public override bool Equals(object obj) { return this.Equals(obj as Binary); }
		public override int GetHashCode()
		{
			int hashCode = 0;
			for (int i = 0; i < this._value.Length; ++i)
			{ hashCode ^= this._value[i] << (i % sizeof(int)); }
			return hashCode;
		}
		public byte[] Data
		{
			get
			{
				return (byte[])this._value.Clone();
			}
			set
			{
				if (value == null) { throw new System.ArgumentNullException("value"); }
				this._value = (byte[])value.Clone();
			}
		}
		public bool Equals(Binary other)
		{
			if (other == null) { return false; }
			if (this._value.Length != other._value.Length)
			{ return false; }
			else
			{
				for (int i = 0; i < this._value.Length; ++i)
				{ if (this._value[i] != other._value[i]) { return false; } }
				return true;
			}
		}
		public override object Clone()
		{
			Binary result = (Binary)this.MemberwiseClone();
			result._value = (byte[])this._value.Clone();
			return result;
		}
		public static explicit operator Binary(byte[] value) { return new Binary(value); }
		public static implicit operator byte[](Binary value) { return value.Data; }
		internal override System.IntPtr ToHGlobal()
		{
			System.IntPtr result = Marshal.AllocHGlobal(this.GetMarshaledSize());
			Marshal.Copy(this.Data, 0, result, this.Data.Length);
			return result;
		}
		public override int GetMarshaledSize() { return this.Data.Length; }
		public override object ObjectData
		{
			get { return this.Data; }
			set { this.Data = (byte[])value; }
		}
		public override int CompareTo(object other) { return this.CompareTo((Binary)other); }
		public int CompareTo(Binary other)
		{
			for (int i = 0; i < Math.Min(this._value.Length, other._value.Length); ++i)
			{
				int compare = this._value[i].CompareTo(other._value[i]);
				if (compare != 0)
				{
					return compare;
				}
			}
			return this._value.Length > other._value.Length ? 1 : (this._value.Length < other._value.Length ? -1 : 0);
		}
	}

	public class ResourceList : Binary
	{
		public ResourceList(byte[] value) : base(value, RegValueType.ResourceList) { }

		public static explicit operator ResourceList(byte[] value)
		{ return new ResourceList(value); }
	}

	public class ResourceRequirementList : Binary
	{
		public ResourceRequirementList(byte[] value) : base(value, RegValueType.ResourceRequirementList) { }

		public static explicit operator ResourceRequirementList(byte[] value)
		{ return new ResourceRequirementList(value); }
	}

	public class FullResourceDescriptor : Binary
	{
		public FullResourceDescriptor(byte[] value) : base(value, RegValueType.FullResourceDescriptor) { }

		public static explicit operator FullResourceDescriptor(byte[] value)
		{ return new FullResourceDescriptor(value); }
	}

	public class Link : Binary
	{
		public Link(byte[] value) : base(value, RegValueType.Link) { }
		public Link(string value) : base(BinaryTextEncoding.GetBytes(value), RegValueType.Link) { }

		public static explicit operator Link(byte[] value)
		{ return new Link(value); }
		public static explicit operator Link(string value) { return new Link(value); }
	}
}