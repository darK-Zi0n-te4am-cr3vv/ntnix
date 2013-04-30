using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
namespace Microsoft.WinNT.SafeHandles
{
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true), SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class SafeHandleZeroIsInvalid : System.Runtime.InteropServices.SafeHandle
	{
		// Methods
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandleZeroIsInvalid(bool ownsHandle)
			: base(System.IntPtr.Zero, ownsHandle)
		{
		}

		public override bool IsInvalid
		{ get { return this.handle == System.IntPtr.Zero; } }
	}
}