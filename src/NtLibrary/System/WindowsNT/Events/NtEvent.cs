using System.WindowsNT.Events.PrivateImplementationDetails;
using Microsoft.WinNT.SafeHandles;

namespace System.WindowsNT.Events
{
	public class NtEvent : NtObject
	{
		protected NtEvent(SafeNtEventHandle handle) : base() { this.Handle = handle; }

		public EventAccessMask AccessMask { get { return (EventAccessMask)this.ObjectAccessMask; } }

		public bool Reset()
		{ return System.Convert.ToBoolean(Wrapper.NtResetEvent(this.Handle)); }

		public void Clear()
		{ Wrapper.NtClearEvent(this.Handle); }


		public bool Raise()
		{ return System.Convert.ToBoolean(Wrapper.NtPulseEvent(this.Handle)); }

		public bool Set()
		{ return System.Convert.ToBoolean(Wrapper.NtSetEvent(this.Handle)); }

		public EventType Type
		{ get { return Wrapper.NtQueryEventBasic(this.Handle).EventType; } }

		public bool State
		{ get { return System.Convert.ToBoolean(Wrapper.NtQueryEventBasic(this.Handle).EventState); } }

		public SafeNtEventHandle Handle { get; private set; }

		public override SafeNtHandle GenericHandle
		{ get { return this.Handle; } }


		public static NtEvent Create(string name, EventAccessMask desiredAccess, EventType eventType)
		{ return new NtEvent(Wrapper.NtCreateEvent(desiredAccess, name, eventType)); }

		public static NtEvent Open(string name, EventAccessMask desiredAccess)
		{ return new NtEvent(Wrapper.NtOpenEvent(desiredAccess, name)); }
	}
}