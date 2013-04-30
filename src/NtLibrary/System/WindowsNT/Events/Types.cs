using In = System.Runtime.InteropServices.InAttribute;
namespace System.WindowsNT.Events
{
	[System.Flags()]
	public enum EventAccessMask : uint
	{
		QueryState = 0x0001,
		ModifyState = 0x0002,  // winnt
		AllAccess = (AccessMask.StandardRightsRequired | AccessMask.Synchronize | 0x3) // winnt
	}

	public enum EventType
	{
		/// <summary>
		/// <para>Notification events can be used to notify one or more threads of execution that an event has occurred.</para>
		/// <para>A notification event is not auto-resetting. Once a notification event is in the Signaled state, it remains in that state until it is explicitly reset.</para>
		/// </summary>
		NotificationEvent,
		/// <summary>
		/// <para>Synchonization events can be used in the serialization of access to hardware between two otherwise unrelated drivers.</para>
		/// <para>A synchonization event is auto-resetting. When a synchronization event is set to the Signaled state, a single thread of execution that was waiting for the event to be signaled is released, and the event is automatically reset to the Not-Signaled state.</para>
		/// </summary>
		SynchronizationEvent
	}

	internal enum EventInformationClass
	{
		EventBasicInformation
	}

	internal struct EventBasicInformation
	{
		public EventType EventType;
		public int EventState;
	}

	internal delegate void IoApcRoutine([In] System.IntPtr ApcContext, [In] ref IoStatusBlock IoStatusBlock, [In] uint Reserved);
}