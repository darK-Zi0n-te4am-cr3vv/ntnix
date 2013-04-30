using T = System.Int64;

[System.Diagnostics.DebuggerDisplay("{Start}-{End}")]
public struct Range : System.IComparable<Range>, System.IEquatable<Range>
{
	private T _length;

	public Range(T start, T length)
		: this()
	{
		this.Start = start;
		this.Length = length;
	}

	public T Start { get; set; }

	public T Length
	{
		get { return this._length; }
		set
		{
			if (value < 0)
			{ throw new System.ArgumentOutOfRangeException("value", value, "Length must be nonnegative."); }
			this._length = value;
		}
	}

	public T End
	{
		get { return this.Start + this.Length; }
		set { this.Length = value - this.Start; }
	}

	public override bool Equals(object obj) { return obj is Range && this.Equals((Range)obj); }

	public bool Equals(Range other) { return this.Start == other.Start & this.Length == other.Length; }

	public override int GetHashCode() { return this.Start.GetHashCode() ^ this.Length.GetHashCode(); }

	public override string ToString() { return string.Format("0x{0:X}-0x{1:X}", this.Start, this.End); }

	public int CompareTo(Range other)
	{
		int result = this.Start.CompareTo(other.Start);
		return result; //== 0 ? this.Length.CompareTo(other.Length) : result;
	}

	public bool IsValid<T>(T[] array) { return array != null && (this.Start >= array.GetLowerBound(0) & this.End <= array.GetUpperBound(0) + 1); }

	public bool Includes(T value) { return value >= this.Start & value < this.End; }

	public bool Overlaps(Range other)
	{
		return (
			(this.Start < other.End & this.End > other.Start) && (this.Length > 0 & other.Length > 0)
			|
			(other.Start < this.End & other.End > this.Start) && (other.Length > 0 & this.Length > 0)
			);
	}
}

internal static class NTInternal
{
	public static readonly System.Runtime.InteropServices.HandleRef NullHandleRef = new System.Runtime.InteropServices.HandleRef(null, System.IntPtr.Zero);

	public static System.Runtime.InteropServices.HandleRef CreateHandleRef(System.Runtime.InteropServices.SafeHandle safeHandle)
	{ return safeHandle != null ? new System.Runtime.InteropServices.HandleRef(safeHandle, safeHandle.DangerousGetHandle()) : NullHandleRef; }

	public static System.Runtime.InteropServices.HandleRef CreateHandleRef(System.IntPtr unsafeHandle) { return new System.Runtime.InteropServices.HandleRef(null, unsafeHandle); }

	public static System.IntPtr GetHandleOrZero<T>(T handle) where T : System.Runtime.InteropServices.SafeHandle { return handle != null ? handle.DangerousGetHandle() : System.IntPtr.Zero; }
}