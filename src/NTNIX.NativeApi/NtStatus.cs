using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTNIX.NativeApi
{
    internal class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description)
        {
        }
    }

    public struct NtStatus
    {
        [Description("Success")]
        public const uint SUCCESS = 0x00000000;
        [Description("WAIT_0")]
        public const uint WAIT_0 = (0x00000000);
        [Description("WAIT_1")]
        public const uint WAIT_1 = (0x00000001);
        [Description("WAIT_2")]
        public const uint WAIT_2 = (0x00000002);
        [Description("WAIT_3")]
        public const uint WAIT_3 = (0x00000003);
        [Description("WAIT_63")]
        public const uint WAIT_63 = (0x0000003F);
        public const uint ABANDONED = (0x00000080);
        [Description("ABANDONED_WAIT_0")]
        public const uint ABANDONED_WAIT_0 = (0x00000080);
        [Description("ABANDONED_WAIT_63")]
        public const uint ABANDONED_WAIT_63 = (0x000000BF);
        [Description("USER_APC")]
        public const uint USER_APC = (0x000000C0);
        [Description("KERNEL_APC")]
        public const uint KERNEL_APC = (0x00000100);
        [Description("ALERTED")]
        public const uint ALERTED = (0x00000101);
        [Description("TIMEOUT")]
        public const uint TIMEOUT = (0x00000102);
        [Description("The operation that was requested is pending completion.")]
        public const uint PENDING = (0x00000103);
        [Description("A reparse should be performed by the Object Manager since the name of the file resulted in a symbolic link.")]
        public const uint REPARSE = (0x00000104);
        [Description("Returned by enumeration APIs to indicate more information is available to successive calls.")]
        public const uint MORE_ENtRIES = (0x00000105);
        [Description("Indicates not all privileges referenced are assigned to the caller. This allows; for example; all privileges to be disabled without having to know exactly which privileges are assigned.")]
        public const uint NOT_ALL_ASSIGNED = (0x00000106);
        [Description("Some of the information to be translated has not been translated.")]
        public const uint SOME_NOT_MAPPED = (0x00000107);
        [Description("An open/create operation completed while an oplock break is underway.")]
        public const uint OPLOCK_BREAK_IN_PROGRESS = (0x00000108);
        [Description("A new volume has been mounted by a file system.")]
        public const uint VOLUME_MOUNtED = (0x00000109);
        [Description("This success level status indicates that the transaction state already exists for the registry sub-tree; but that a transaction commit was previously aborted. The commit has now been completed.")]
        public const uint RXACT_COMMITTED = (0x0000010A);
        [Description("This indicates that a notify change request has been completed due to closing the handle which made the notify change request.")]
        public const uint NOTIFY_CLEANUP = (0x0000010B);
        [Description("This indicates that a notify change request is being completed and that the information is not being returned in the caller's buffer. The caller now needs to enumerate the files to find the changes.")]
        public const uint NOTIFY_ENUM_DIR = (0x0000010C);
        [Description("{No Quotas} No system quota limits are specifically set for this account.")]
        public const uint NO_QUOTAS_FOR_ACCOUNt = (0x0000010D);
        [Description("{Connect Failure on Primary Transport} An attempt was made to connect to the remote server %hs on the primary transport; but the connection failed.The computer WAS able to connect on a secondary transport.")]
        public const uint PRIMARY_TRANSPORT_CONNECT_FAILED = (0x0000010E);
        [Description("Page fault was a transition fault.")]
        public const uint PAGE_FAULT_TRANSITION = (0x00000110);
        [Description("Page fault was a demand zero fault.")]
        public const uint PAGE_FAULT_DEMAND_ZERO = (0x00000111);
        [Description("Page fault was a demand zero fault.")]
        public const uint PAGE_FAULT_COPY_ON_WRITE = (0x00000112);
        [Description("Page fault was a demand zero fault.")]
        public const uint PAGE_FAULT_GUARD_PAGE = (0x00000113);
        [Description("Page fault was satisfied by reading from a secondary storage device.")]
        public const uint PAGE_FAULT_PAGING_FILE = (0x00000114);
        [Description("Cached page was locked during operation.")]
        public const uint CACHE_PAGE_LOCKED = (0x00000115);
        [Description("Crash dump exists in paging file.")]
        public const uint CRASH_DUMP = (0x00000116);
        [Description("Specified buffer contains all zeros.")]
        public const uint BUFFER_ALL_ZEROS = (0x00000117);
        [Description("A reparse should be performed by the Object Manager since the name of the file resulted in a symbolic link.")]
        public const uint REPARSE_OBJECT = (0x00000118);
        [Description("The device has succeeded a query-stop and its resource requirements have changed.")]
        public const uint RESOURCE_REQUIREMENtS_CHANGED = (0x00000119);
        [Description("The translator has translated these resources into the global space and no further translations should be performed.")]
        public const uint TRANSLATION_COMPLETE = (0x00000120);
        [Description("The directory service evaluated group memberships locally; as it was unable to contact a global catalog server.")]
        public const uint DS_MEMBERSHIP_EVALUATED_LOCALLY = (0x00000121);
        [Description("A process being terminated has no threads to terminate.")]
        public const uint NOTHING_TO_TERMINATE = (0x00000122);
        [Description("The specified process is not part of a job.")]
        public const uint PROCESS_NOT_IN_JOB = (0x00000123);
        [Description("The specified process is part of a job.")]
        public const uint PROCESS_IN_JOB = (0x00000124);
        [Description("A file system or file system filter driver has successfully completed an FsFilter operation.")]
        public const uint FSFILTER_OP_COMPLETED_SUCCESSFULLY = (0x00000126);
        [Description("Debugger handled exception")]
        public const uint DBG_EXCEPTION_HANDLED = (0x00010001);
        [Description("Debugger continued")]
        public const uint DBG_CONtINUE = (0x00010002);
        [Description("{Object Exists} An attempt was made to create an object and the object name already existed.")]
        public const uint OBJECT_NAME_EXISTS = (0x40000000);
        [Description("{Thread Suspended} A thread termination occurred while the thread was suspended. The thread was resumed; and termination proceeded.")]
        public const uint THREAD_WAS_SUSPENDED = (0x40000001);
        [Description("{Working Set Range Error} An attempt was made to set the working set minimum or maximum to values which are outside of the allowable range.")]
        public const uint WORKING_SET_LIMIT_RANGE = (0x40000002);
        [Description("{Image Relocated} An image file could not be mapped at the address specified in the image file. Local fixups must be performed on this image.")]
        public const uint IMAGE_NOT_AT_BASE = (0x40000003);
        [Description("This informational level status indicates that a specified registry sub-tree transaction state did not yet exist and had to be created.")]
        public const uint RXACT_STATE_CREATED = (0x40000004);
        [Description("{Segment Load} A virtual DOS machine (VDM) is loading; unloading; or moving an MS-DOS or Win16 program segment image.An exception is raised so a debugger can load; unload or track symbols and breakpoints within these 16-bit segments.")]
        public const uint SEGMENt_NOTIFICATION = (0x40000005);
        [Description("{Local Session Key} A user session key was requested for a local RPC connection. The session key returned is a constant value and not unique to this connection.")]
        public const uint LOCAL_USER_SESSION_KEY = (0x40000006);
        [Description("{Invalid Current Directory} The process cannot switch to the startup current directory %hs.Select OK to set current directory to %hs; or select CANCEL to exit.")]
        public const uint BAD_CURRENt_DIRECTORY = (0x40000007);
        [Description("{Serial IOCTL Complete} A serial I/O operation was completed by another write to a serial port.(The IOCTL_SERIAL_XOFF_COUNtER reached zero.)")]
        public const uint SERIAL_MORE_WRITES = (0x40000008);
        [Description("{Registry Recovery} One of the files containing the system's Registry data had to be recovered by use of a log or alternate copy.The recovery was successful.")]
        public const uint REGISTRY_RECOVERED = (0x40000009);
        [Description("{Redundant Read} To satisfy a read request; the Nt fault-tolerant file system successfully read the requested data from a redundant copy.This was done because the file system encountered a failure on a member of the fault-tolerant volume; but was unable to reassign the failing area of the device.")]
        public const uint FT_READ_RECOVERY_FROM_BACKUP = (0x4000000A);
        [Description("{Redundant Write} To satisfy a write request; the Nt fault-tolerant file system successfully wrote a redundant copy of the information.This was done because the file system encountered a failure on a member of the fault-tolerant volume; but was not able to reassign the failing area of the device.")]
        public const uint FT_WRITE_RECOVERY = (0x4000000B);
        [Description("{Serial IOCTL Timeout} A serial I/O operation completed because the time-out period expired.(The IOCTL_SERIAL_XOFF_COUNtER had not reached zero.)")]
        public const uint SERIAL_COUNtER_TIMEOUT = (0x4000000C);
        [Description("{Password Too Complex} The Windows password is too complex to be converted to a LAN Manager password.The LAN Manager password returned is a NULL string.")]
        public const uint NULL_LM_PASSWORD = (0x4000000D);
        [Description("{Machine Type Mismatch} The image file %hs is valid; but is for a machine type other than the current machine. Select OK to continue; or CANCEL to fail the DLL load.")]
        public const uint IMAGE_MACHINE_TYPE_MISMATCH = (0x4000000E);
        [Description("{Partial Data Received} The network transport returned partial data to its client. The remaining data will be sent later.")]
        public const uint RECEIVE_PARTIAL = (0x4000000F);
        [Description("{Expedited Data Received} The network transport returned data to its client that was marked as expedited by the remote system.")]
        public const uint RECEIVE_EXPEDITED = (0x40000010);
        [Description("{Partial Expedited Data Received} The network transport returned partial data to its client and this data was marked as expedited by the remote system. The remaining data will be sent later.")]
        public const uint RECEIVE_PARTIAL_EXPEDITED = (0x40000011);
        [Description("{TDI Event Done} The TDI indication has completed successfully.")]
        public const uint EVENt_DONE = (0x40000012);
        [Description("{TDI Event Pending} The TDI indication has entered the pending state.")]
        public const uint EVENt_PENDING = (0x40000013);
        [Description("Checking file system on %wZ")]
        public const uint CHECKING_FILE_SYSTEM = (0x40000014);
        [Description("{Fatal Application Exit} %hs")]
        public const uint FATAL_APP_EXIT = (0x40000015);
        [Description("The specified registry key is referenced by a predefined handle.")]
        public const uint PREDEFINED_HANDLE = (0x40000016);
        [Description("{Page Unlocked} The page protection of a locked page was changed to 'No Access' and the page was unlocked from memory and from the process.")]
        public const uint WAS_UNLOCKED = (0x40000017);
        [Description("%hs")]
        public const uint SERVICE_NOTIFICATION = (0x40000018);
        [Description("{Page Locked} One of the pages to lock was already locked.")]
        public const uint WAS_LOCKED = (0x40000019);
        [Description("Application popup: %1 : %2")]
        public const uint LOG_HARD_ERROR = (0x4000001A);
        [Description("ALREADY_WIN32")]
        public const uint ALREADY_WIN32 = (0x4000001B);
        [Description("Exception status code used by Win32 x86 emulation subsystem.")]
        public const uint WX86_UNSIMULATE = (0x4000001C);
        [Description("Exception status code used by Win32 x86 emulation subsystem.")]
        public const uint WX86_CONtINUE = (0x4000001D);
        [Description("Exception status code used by Win32 x86 emulation subsystem.")]
        public const uint WX86_SINGLE_STEP = (0x4000001E);
        [Description("Exception status code used by Win32 x86 emulation subsystem.")]
        public const uint WX86_BREAKPOINt = (0x4000001F);
        [Description("Exception status code used by Win32 x86 emulation subsystem.")]
        public const uint WX86_EXCEPTION_CONtINUE = (0x40000020);
        [Description("Exception status code used by Win32 x86 emulation subsystem.")]
        public const uint WX86_EXCEPTION_LASTCHANCE = (0x40000021);
        [Description("Exception status code used by Win32 x86 emulation subsystem.")]
        public const uint WX86_EXCEPTION_CHAIN = (0x40000022);
        [Description("{Machine Type Mismatch} The image file %hs is valid; but is for a machine type other than the current machine.")]
        public const uint IMAGE_MACHINE_TYPE_MISMATCH_EXE = (0x40000023);
        [Description("A yield execution was performed and no thread was available to run.")]
        public const uint NO_YIELD_PERFORMED = (0x40000024);
        [Description("The resumable flag to a timer API was ignored.")]
        public const uint TIMER_RESUME_IGNORED = (0x40000025);
        [Description("The arbiter has deferred arbitration of these resources to its parent")]
        public const uint ARBITRATION_UNHANDLED = (0x40000026);
        [Description("The device \"%hs\" has detected a CardBus card in its slot; but the firmware on this system is not configured to allow the CardBus controller to be run in CardBus mode. The operating system will currently accept only 16-bit (R2) pc-cards on this controller.")]
        public const uint CARDBUS_NOT_SUPPORTED = (0x40000027);
        [Description("Exception status code used by Win32 x86 emulation subsystem.")]
        public const uint WX86_CREATEWX86TIB = (0x40000028);
        [Description("The CPUs in this multiprocessor system are not all the same revision level.To use all processors the operating system restricts itself to the features of the least capable processor in the system.Should problems occur with this system; contact the CPU manufacturer to see if this mix of processors is supported.")]
        public const uint MP_PROCESSOR_MISMATCH = (0x40000029);
        [Description("The system was put into hibernation.")]
        public const uint HIBERNATED = (0x4000002A);
        [Description("The system was resumed from hibernation.")]
        public const uint RESUME_HIBERNATION = (0x4000002B);
        [Description("A device driver is leaking locked I/O pages causing system degradation.The system has automatically enabled tracking code in order to try and catch the culprit.")]
        public const uint DRIVERS_LEAKING_LOCKED_PAGES = (0x4000002D);
        [Description("Debugger will reply later.")]
        public const uint DBG_REPLY_LATER = (0x40010001);
        [Description("Debugger can not provide handle.")]
        public const uint DBG_UNABLE_TO_PROVIDE_HANDLE = (0x40010002);
        [Description("Debugger terminated thread.")]
        public const uint DBG_TERMINATE_THREAD = (0x40010003);
        [Description("Debugger terminated process.")]
        public const uint DBG_TERMINATE_PROCESS = (0x40010004);
        [Description("Debugger got control C.")]
        public const uint DBG_CONtROL_C = (0x40010005);
        [Description("Debugger printerd exception on control C.")]
        public const uint DBG_PRINtEXCEPTION_C = (0x40010006);
        [Description("Debugger recevice RIP exception.")]
        public const uint DBG_RIPEXCEPTION = (0x40010007);
        [Description("Debugger received control break.")]
        public const uint DBG_CONtROL_BREAK = (0x40010008);
        [Description("{EXCEPTION} Guard Page ExceptionA page of memory that marks the end of a data structure; such as a stack or an array; has been accessed.")]
        public const uint GUARD_PAGE_VIOLATION = (0x80000001);
        [Description("{EXCEPTION} Alignment FaultA datatype misalignment was detected in a load or store instruction.")]
        public const uint DATATYPE_MISALIGNMENt = (0x80000002);
        [Description("{EXCEPTION} BreakpointA breakpoint has been reached.")]
        public const uint BREAKPOINt = (0x80000003);
        [Description("{EXCEPTION} Single StepA single step or trace operation has just been completed.")]
        public const uint SINGLE_STEP = (0x80000004);
        [Description("{Buffer Overflow} The data was too large to fit into the specified buffer.")]
        public const uint BUFFER_OVERFLOW = (0x80000005);
        [Description("{No More Files} No more files were found which match the file specification.")]
        public const uint NO_MORE_FILES = (0x80000006);
        [Description("{Kernel Debugger Awakened} the system debugger was awakened by an interrupt.")]
        public const uint WAKE_SYSTEM_DEBUGGER = (0x80000007);
        [Description("{Handles Closed} Handles to objects have been automatically closed as a result of the requested operation.")]
        public const uint HANDLES_CLOSED = (0x8000000A);
        [Description("{Non-Inheritable ACL} An access control list (ACL) contains no components that can be inherited.")]
        public const uint NO_INHERITANCE = (0x8000000B);
        [Description("{GUID Substitution} During the translation of a global identifier (GUID) to a Windows security ID (SID); no administratively-defined GUID prefix was found.A substitute prefix was used; which will not compromise system security. However; this may provide a more restrictive access than intended.")]
        public const uint GUID_SUBSTITUTION_MADE = (0x8000000C);
        [Description("{Partial Copy} Due to protection conflicts not all the requested bytes could be copied.")]
        public const uint PARTIAL_COPY = (0x8000000D);
        [Description("{Out of Paper} The printer is out of paper.")]
        public const uint DEVICE_PAPER_EMPTY = (0x8000000E);
        [Description("{Device Power Is Off} The printer power has been turned off.")]
        public const uint DEVICE_POWERED_OFF = (0x8000000F);
        [Description("{Device Offline} The printer has been taken offline.")]
        public const uint DEVICE_OFF_LINE = (0x80000010);
        [Description("{Device Busy} The device is currently busy.")]
        public const uint DEVICE_BUSY = (0x80000011);
        [Description("{No More EAs} No more extended attributes (EAs) were found for the file.")]
        public const uint NO_MORE_EAS = (0x80000012);
        [Description("{Illegal EA} The specified extended attribute (EA) name contains at least one illegal character.")]
        public const uint INVALID_EA_NAME = (0x80000013);
        [Description("{Inconsistent EA List} The extended attribute (EA) list is inconsistent.")]
        public const uint EA_LIST_INCONSISTENt = (0x80000014);
        [Description("{Invalid EA Flag} An invalid extended attribute (EA) flag was set.")]
        public const uint INVALID_EA_FLAG = (0x80000015);
        [Description("{Verifying Disk} The media has changed and a verify operation is in progress so no reads or writes may be performed to the device; except those used in the verify operation.")]
        public const uint VERIFY_REQUIRED = (0x80000016);
        [Description("{Too Much Information} The specified access control list (ACL) contained more information than was expected.")]
        public const uint EXTRANEOUS_INFORMATION = (0x80000017);
        [Description("This warning level status indicates that the transaction state already exists for the registry sub-tree; but that a transaction commit was previously aborted. The commit has NOT been completed; but has not been rolled back either (so it may still be committed if desired).")]
        public const uint RXACT_COMMIT_NECESSARY = (0x80000018);
        [Description("{No More Entries} No more entries are available from an enumeration operation.")]
        public const uint NO_MORE_ENTRIES = (0x8000001A);
        [Description("{Filemark Found} A filemark was detected.")]
        public const uint FILEMARK_DETECTED = (0x8000001B);
        [Description("{Media Changed} The media may have changed.")]
        public const uint MEDIA_CHANGED = (0x8000001C);
        [Description("{I/O Bus Reset} An I/O bus reset was detected.")]
        public const uint BUS_RESET = (0x8000001D);
        [Description("{End of Media} The end of the media was encountered.")]
        public const uint END_OF_MEDIA = (0x8000001E);
        [Description("Beginning of tape or partition has been detected.")]
        public const uint BEGINNING_OF_MEDIA = (0x8000001F);
        [Description("{Media Changed} The media may have changed.")]
        public const uint MEDIA_CHECK = (0x80000020);
        [Description("A tape access reached a setmark.")]
        public const uint SETMARK_DETECTED = (0x80000021);
        [Description("During a tape access; the end of the data written is reached.")]
        public const uint NO_DATA_DETECTED = (0x80000022);
        [Description("The redirector is in use and cannot be unloaded.")]
        public const uint REDIRECTOR_HAS_OPEN_HANDLES = (0x80000023);
        [Description("The server is in use and cannot be unloaded.")]
        public const uint SERVER_HAS_OPEN_HANDLES = (0x80000024);
        [Description("The specified connection has already been disconnected.")]
        public const uint ALREADY_DISCONNECTED = (0x80000025);
        [Description("A long jump has been executed.")]
        public const uint LONGJUMP = (0x80000026);
        [Description("A cleaner cartridge is present in the tape library.")]
        public const uint CLEANER_CARTRIDGE_INSTALLED = (0x80000027);
        [Description("The Plug and Play query operation was not successful.")]
        public const uint PLUGPLAY_QUERY_VETOED = (0x80000028);
        [Description("A frame consolidation has been executed.")]
        public const uint UNWIND_CONSOLIDATE = (0x80000029);
        [Description("Debugger did not handle the exception.")]
        public const uint DBG_EXCEPTION_NOT_HANDLED = (0x80010001);
        [Description("The cluster node is already up.")]
        public const uint CLUSTER_NODE_ALREADY_UP = (0x80130001);
        [Description("The cluster node is already down.")]
        public const uint CLUSTER_NODE_ALREADY_DOWN = (0x80130002);
        [Description("The cluster network is already online.")]
        public const uint CLUSTER_NETWORK_ALREADY_ONLINE = (0x80130003);
        [Description("The cluster network is already offline.")]
        public const uint CLUSTER_NETWORK_ALREADY_OFFLINE = (0x80130004);
        [Description("The cluster node is already a member of the cluster.")]
        public const uint CLUSTER_NODE_ALREADY_MEMBER = (0x80130005);
        [Description("{Operation Failed} The requested operation was unsuccessful.")]
        public const uint UNSUCCESSFUL = (0xC0000001);
        [Description("{Not Implemented} The requested operation is not implemented.")]
        public const uint NOT_IMPLEMENtED = (0xC0000002);
        [Description("{Invalid Parameter} The specified information class is not a valid information class for the specified object.")]
        public const uint INVALID_INFO_CLASS = (0xC0000003);
        [Description("The specified information record length does not match the length required for the specified information class.")]
        public const uint INFO_LENGTH_MISMATCH = (0xC0000004);
        [Description("The instruction at \"0x%08lx\" referenced memory at \"0x%08lx\". The memory could not be \"%s\".")]
        public const uint ACCESS_VIOLATION = (0xC0000005);
        [Description("The instruction at \"0x%08lx\" referenced memory at \"0x%08lx\". The required data was not placed into memory because of an I/O error status of \"0x%08lx\".")]
        public const uint IN_PAGE_ERROR = (0xC0000006);
        [Description("The pagefile quota for the process has been exhausted.")]
        public const uint PAGEFILE_QUOTA = (0xC0000007);
        [Description("An invalid HANDLE was specified.")]
        public const uint INVALID_HANDLE = (0xC0000008);
        [Description("An invalid initial stack was specified in a call to NtCreateThread.")]
        public const uint BAD_INITIAL_STACK = (0xC0000009);
        [Description("An invalid initial start address was specified in a call to NtCreateThread.")]
        public const uint BAD_INITIAL_PC = (0xC000000A);
        [Description("An invalid Client ID was specified.")]
        public const uint INVALID_CID = (0xC000000B);
        [Description("An attempt was made to cancel or set a timer that has an associated APC and the subject thread is not the thread that originally set the timer with an associated APC routine.")]
        public const uint TIMER_NOT_CANCELED = (0xC000000C);
        [Description("An invalid parameter was passed to a service or function.")]
        public const uint INVALID_PARAMETER = (0xC000000D);
        [Description("A device which does not exist was specified.")]
        public const uint NO_SUCH_DEVICE = (0xC000000E);
        [Description("{File Not Found} The file %hs does not exist.")]
        public const uint NO_SUCH_FILE = (0xC000000F);
        [Description("The specified request is not a valid operation for the target device.")]
        public const uint INVALID_DEVICE_REQUEST = (0xC0000010);
        [Description("The end-of-file marker has been reached. There is no valid data in the file beyond this marker.")]
        public const uint END_OF_FILE = (0xC0000011);
        [Description("{Wrong Volume} The wrong volume is in the drive.Please insert volume %hs into drive %hs.")]
        public const uint WRONG_VOLUME = (0xC0000012);
        [Description("{No Disk} There is no disk in the drive.Please insert a disk into drive %hs.")]
        public const uint NO_MEDIA_IN_DEVICE = (0xC0000013);
        [Description("{Unknown Disk Format} The disk in drive %hs is not formatted properly.Please check the disk; and reformat if necessary.")]
        public const uint UNRECOGNIZED_MEDIA = (0xC0000014);
        [Description("{Sector Not Found} The specified sector does not exist.")]
        public const uint NONEXISTENt_SECTOR = (0xC0000015);
        [Description("{Still Busy} The specified I/O request packet (IRP) cannot be disposed of because the I/O operation is not complete.")]
        public const uint MORE_PROCESSING_REQUIRED = (0xC0000016);
        [Description("{Not Enough Quota} Not enough virtual memory or paging file quota is available to complete the specified operation.")]
        public const uint NO_MEMORY = (0xC0000017);
        [Description("{Conflicting Address Range} The specified address range conflicts with the address space.")]
        public const uint CONFLICTING_ADDRESSES = (0xC0000018);
        [Description("Address range to unmap is not a mapped view.")]
        public const uint NOT_MAPPED_VIEW = (0xC0000019);
        [Description("Virtual memory cannot be freed.")]
        public const uint UNABLE_TO_FREE_VM = (0xC000001A);
        [Description("Specified section cannot be deleted.")]
        public const uint UNABLE_TO_DELETE_SECTION = (0xC000001B);
        [Description("An invalid system service was specified in a system service call.")]
        public const uint INVALID_SYSTEM_SERVICE = (0xC000001C);
        [Description("{EXCEPTION} Illegal InstructionAn attempt was made to execute an illegal instruction.")]
        public const uint ILLEGAL_INSTRUCTION = (0xC000001D);
        [Description("{Invalid Lock Sequence} An attempt was made to execute an invalid lock sequence.")]
        public const uint INVALID_LOCK_SEQUENCE = (0xC000001E);
        [Description("{Invalid Mapping} An attempt was made to create a view for a section which is bigger than the section.")]
        public const uint INVALID_VIEW_SIZE = (0xC000001F);
        [Description("{Bad File} The attributes of the specified mapping file for a section of memory cannot be read.")]
        public const uint INVALID_FILE_FOR_SECTION = (0xC0000020);
        [Description("{Already Committed} The specified address range is already committed.")]
        public const uint ALREADY_COMMITTED = (0xC0000021);
        [Description("{Access Denied} A process has requested access to an object; but has not been granted those access rights.")]
        public const uint ACCESS_DENIED = (0xC0000022);
        [Description("{Buffer Too Small} The buffer is too small to contain the entry. No information has been written to the buffer.")]
        public const uint BUFFER_TOO_SMALL = (0xC0000023);
        [Description("{Wrong Type} There is a mismatch between the type of object required by the requested operation and the type of object that is specified in the request.")]
        public const uint OBJECT_TYPE_MISMATCH = (0xC0000024);
        [Description("{EXCEPTION} Cannot ContinueWindows cannot continue from this exception.")]
        public const uint NONCONtINUABLE_EXCEPTION = (0xC0000025);
        [Description("An invalid exception disposition was returned by an exception handler.")]
        public const uint INVALID_DISPOSITION = (0xC0000026);
        [Description("Unwind exception code.")]
        public const uint UNWIND = (0xC0000027);
        [Description("An invalid or unaligned stack was encountered during an unwind operation.")]
        public const uint BAD_STACK = (0xC0000028);
        [Description("An invalid unwind target was encountered during an unwind operation.")]
        public const uint INVALID_UNWIND_TARGET = (0xC0000029);
        [Description("An attempt was made to unlock a page of memory which was not locked.")]
        public const uint NOT_LOCKED = (0xC000002A);
        [Description("Device parity error on I/O operation.")]
        public const uint PARITY_ERROR = (0xC000002B);
        [Description("An attempt was made to decommit uncommitted virtual memory.")]
        public const uint UNABLE_TO_DECOMMIT_VM = (0xC000002C);
        [Description("An attempt was made to change the attributes on memory that has not been committed.")]
        public const uint NOT_COMMITTED = (0xC000002D);
        [Description("Invalid Object Attributes specified to NtCreatePort or invalid Port Attributes specified to NtConnectPort")]
        public const uint INVALID_PORT_ATTRIBUTES = (0xC000002E);
        [Description("Length of message passed to NtRequestPort or NtRequestWaitReplyPort was longer than the maximum message allowed by the port.")]
        public const uint PORT_MESSAGE_TOO_LONG = (0xC000002F);
        [Description("An invalid combination of parameters was specified.")]
        public const uint INVALID_PARAMETER_MIX = (0xC0000030);
        [Description("An attempt was made to lower a quota limit below the current usage.")]
        public const uint INVALID_QUOTA_LOWER = (0xC0000031);
        [Description("{Corrupt Disk} The file system structure on the disk is corrupt and unusable.Please run the Chkdsk utility on the volume %hs.")]
        public const uint DISK_CORRUPT_ERROR = (0xC0000032);
        [Description("Object Name invalid.")]
        public const uint OBJECT_NAME_INVALID = (0xC0000033);
        [Description("Object Name not found.")]
        public const uint OBJECT_NAME_NOT_FOUND = (0xC0000034);
        [Description("Object Name already exists.")]
        public const uint OBJECT_NAME_COLLISION = (0xC0000035);
        [Description("Attempt to send a message to a disconnected communication port.")]
        public const uint PORT_DISCONNECTED = (0xC0000037);
        [Description("An attempt was made to attach to a device that was already attached to another device.")]
        public const uint DEVICE_ALREADY_ATTACHED = (0xC0000038);
        [Description("Object Path Component was not a directory object.")]
        public const uint OBJECT_PATH_INVALID = (0xC0000039);
        [Description("{Path Not Found} The path %hs does not exist.")]
        public const uint OBJECT_PATH_NOT_FOUND = (0xC000003A);
        [Description("Object Path Component was not a directory object.")]
        public const uint OBJECT_PATH_SYNTAX_BAD = (0xC000003B);
        [Description("{Data Overrun} A data overrun error occurred.")]
        public const uint DATA_OVERRUN = (0xC000003C);
        [Description("{Data Late} A data late error occurred.")]
        public const uint DATA_LATE_ERROR = (0xC000003D);
        [Description("{Data Error} An error in reading or writing data occurred.")]
        public const uint DATA_ERROR = (0xC000003E);
        [Description("{Bad CRC} A cyclic redundancy check (CRC) checksum error occurred.")]
        public const uint CRC_ERROR = (0xC000003F);
        [Description("{Section Too Large} The specified section is too big to map the file.")]
        public const uint SECTION_TOO_BIG = (0xC0000040);
        [Description("The NtConnectPort request is refused.")]
        public const uint PORT_CONNECTION_REFUSED = (0xC0000041);
        [Description("The type of port handle is invalid for the operation requested.")]
        public const uint INVALID_PORT_HANDLE = (0xC0000042);
        [Description("A file cannot be opened because the share access flags are incompatible.")]
        public const uint SHARING_VIOLATION = (0xC0000043);
        [Description("Insufficient quota exists to complete the operation")]
        public const uint QUOTA_EXCEEDED = (0xC0000044);
        [Description("The specified page protection was not valid.")]
        public const uint INVALID_PAGE_PROTECTION = (0xC0000045);
        [Description("An attempt to release a mutant object was made by a thread that was not the owner of the mutant object.")]
        public const uint MUTANT_NOT_OWNED = (0xC0000046);
        [Description("An attempt was made to release a semaphore such that its maximum count would have been exceeded.")]
        public const uint SEMAPHORE_LIMIT_EXCEEDED = (0xC0000047);
        [Description("An attempt to set a processes DebugPort or ExceptionPort was made; but a port already exists in the process.")]
        public const uint PORT_ALREADY_SET = (0xC0000048);
        [Description("An attempt was made to query image information on a section which does not map an image.")]
        public const uint SECTION_NOT_IMAGE = (0xC0000049);
        [Description("An attempt was made to suspend a thread whose suspend count was at its maximum.")]
        public const uint SUSPEND_COUNT_EXCEEDED = (0xC000004A);
        [Description("An attempt was made to suspend a thread that has begun termination.")]
        public const uint THREAD_IS_TERMINATING = (0xC000004B);
        [Description("An attempt was made to set the working set limit to an invalid value (minimum greater than maximum; etc).")]
        public const uint BAD_WORKING_SET_LIMIT = (0xC000004C);
        [Description("A section was created to map a file which is not compatible to an already existing section which maps the same file.")]
        public const uint INCOMPATIBLE_FILE_MAP = (0xC000004D);
        [Description("A view to a section specifies a protection which is incompatible with the initial view's protection.")]
        public const uint SECTION_PROTECTION = (0xC000004E);
        [Description("An operation involving EAs failed because the file system does not support EAs.")]
        public const uint EAS_NOT_SUPPORTED = (0xC000004F);
        [Description("An EA operation failed because EA set is too large.")]
        public const uint EA_TOO_LARGE = (0xC0000050);
        [Description("An EA operation failed because the name or EA index is invalid.")]
        public const uint NONEXISTENT_EA_ENTRY = (0xC0000051);
        [Description("The file for which EAs were requested has no EAs.")]
        public const uint NO_EAS_ON_FILE = (0xC0000052);
        [Description("The EA is corrupt and non-readable.")]
        public const uint EA_CORRUPT_ERROR = (0xC0000053);
        [Description("A requested read/write cannot be granted due to a conflicting file lock.")]
        public const uint FILE_LOCK_CONFLICT = (0xC0000054);
        [Description("A requested file lock cannot be granted due to other existing locks.")]
        public const uint LOCK_NOT_GRANTED = (0xC0000055);
        [Description("A non close operation has been requested of a file object with a delete pending.")]
        public const uint DELETE_PENDING = (0xC0000056);
        [Description("An attempt was made to set the control attribute on a file. This attribute is not supported in the target file system.")]
        public const uint CTL_FILE_NOT_SUPPORTED = (0xC0000057);
        [Description("Indicates a revision number encountered or specified is not one known by the service. It may be a more recent revision than the service is aware of.")]
        public const uint UNKNOWN_REVISION = (0xC0000058);
        [Description("Indicates two revision levels are incompatible.")]
        public const uint REVISION_MISMATCH = (0xC0000059);
        [Description("Indicates a particular Security ID may not be assigned as the owner of an object.")]
        public const uint INVALID_OWNER = (0xC000005A);
        [Description("Indicates a particular Security ID may not be assigned as the primary group of an object.")]
        public const uint INVALID_PRIMARY_GROUP = (0xC000005B);
        [Description("An attempt has been made to operate on an impersonation token by a thread that is not currently impersonating a client.")]
        public const uint NO_IMPERSONATION_TOKEN = (0xC000005C);
        [Description("A mandatory group may not be disabled.")]
        public const uint CANT_DISABLE_MANDATORY = (0xC000005D);
        [Description("There are currently no logon servers available to service the logon request.")]
        public const uint NO_LOGON_SERVERS = (0xC000005E);
        [Description("A specified logon session does not exist. It may already have been terminated.")]
        public const uint NO_SUCH_LOGON_SESSION = (0xC000005F);
        [Description("A specified privilege does not exist.")]
        public const uint NO_SUCH_PRIVILEGE = (0xC0000060);
        [Description("A required privilege is not held by the client.")]
        public const uint PRIVILEGE_NOT_HELD = (0xC0000061);
        [Description("The name provided is not a properly formed account name.")]
        public const uint INVALID_ACCOUNT_NAME = (0xC0000062);
        [Description("The specified user already exists.")]
        public const uint USER_EXISTS = (0xC0000063);
        [Description("The specified user does not exist.")]
        public const uint NO_SUCH_USER = (0xC0000064);
        [Description("The specified group already exists.")]
        public const uint GROUP_EXISTS = (0xC0000065);
        [Description("The specified group does not exist.")]
        public const uint NO_SUCH_GROUP = (0xC0000066);
        [Description("The specified user account is already in the specified group account. Also used to indicate a group cannot be deleted because it contains a member.")]
        public const uint MEMBER_IN_GROUP = (0xC0000067);
        [Description("The specified user account is not a member of the specified group account.")]
        public const uint MEMBER_NOT_IN_GROUP = (0xC0000068);
        [Description("Indicates the requested operation would disable or delete the last remaining administration account. This is not allowed to prevent creating a situation in which the system cannot be administrated.")]
        public const uint LAST_ADMIN = (0xC0000069);
        [Description("When trying to update a password; this return status indicates that the value provided as the current password is not correct.")]
        public const uint WRONG_PASSWORD = (0xC000006A);
        [Description("When trying to update a password; this return status indicates that the value provided for the new password contains values that are not allowed in passwords.")]
        public const uint ILL_FORMED_PASSWORD = (0xC000006B);
        [Description("When trying to update a password; this status indicates that some password update rule has been violated. For example; the password may not meet length criteria.")]
        public const uint PASSWORD_RESTRICTION = (0xC000006C);
        [Description("The attempted logon is invalid. This is either due to a bad username or authentication information.")]
        public const uint LOGON_FAILURE = (0xC000006D);
        [Description("Indicates a referenced user name and authentication information are valid; but some user account restriction has prevented successful authentication (such as time-of-day restrictions).")]
        public const uint ACCOUNT_RESTRICTION = (0xC000006E);
        [Description("The user account has time restrictions and may not be logged onto at this time.")]
        public const uint INVALID_LOGON_HOURS = (0xC000006F);
        [Description("The user account is restricted such that it may not be used to log on from the source workstation.")]
        public const uint INVALID_WORKSTATION = (0xC0000070);
        [Description("The user account's password has expired.")]
        public const uint PASSWORD_EXPIRED = (0xC0000071);
        [Description("The referenced account is currently disabled and may not be logged on to.")]
        public const uint ACCOUNT_DISABLED = (0xC0000072);
        [Description("None of the information to be translated has been translated.")]
        public const uint NONE_MAPPED = (0xC0000073);
        [Description("The number of LUIDs requested may not be allocated with a single allocation.")]
        public const uint TOO_MANY_LUIDS_REQUESTED = (0xC0000074);
        [Description("Indicates there are no more LUIDs to allocate.")]
        public const uint LUIDS_EXHAUSTED = (0xC0000075);
        [Description("Indicates the sub-authority value is invalid for the particular use.")]
        public const uint INVALID_SUB_AUTHORITY = (0xC0000076);
        [Description("Indicates the ACL structure is not valid.")]
        public const uint INVALID_ACL = (0xC0000077);
        [Description("Indicates the SID structure is not valid.")]
        public const uint INVALID_SID = (0xC0000078);
        [Description("Indicates the SECURITY_DESCRIPTOR structure is not valid.")]
        public const uint INVALID_SECURITY_DESCR = (0xC0000079);
        [Description("Indicates the specified procedure address cannot be found in the DLL.")]
        public const uint PROCEDURE_NOT_FOUND = (0xC000007A);
        [Description("{Bad Image} The application or DLL %hs is not a valid Windows image. Please check this against your installation diskette.")]
        public const uint INVALID_IMAGE_FORMAT = (0xC000007B);
        [Description("An attempt was made to reference a token that doesn't exist. This is typically done by referencing the token associated with a thread when the thread is not impersonating a client.")]
        public const uint NO_TOKEN = (0xC000007C);
        [Description("Indicates that an attempt to build either an inherited ACL or ACE was not successful. This can be caused by a number of things. One of the more probable causes is the replacement of a CreatorId with an SID that didn't fit into the ACE or ACL.")]
        public const uint BAD_INHERITANCE_ACL = (0xC000007D);
        [Description("The range specified in NtUnlockFile was not locked.")]
        public const uint RANGE_NOT_LOCKED = (0xC000007E);
        [Description("An operation failed because the disk was full.")]
        public const uint DISK_FULL = (0xC000007F);
        [Description("The GUID allocation server is [already] disabled at the moment.")]
        public const uint SERVER_DISABLED = (0xC0000080);
        [Description("The GUID allocation server is [already] enabled at the moment.")]
        public const uint SERVER_NOT_DISABLED = (0xC0000081);
        [Description("Too many GUIDs were requested from the allocation server at once.")]
        public const uint TOO_MANY_GUIDS_REQUESTED = (0xC0000082);
        [Description("The GUIDs could not be allocated because the Authority Agent was exhausted.")]
        public const uint GUIDS_EXHAUSTED = (0xC0000083);
        [Description("The value provided was an invalid value for an identifier authority.")]
        public const uint INVALID_ID_AUTHORITY = (0xC0000084);
        [Description("There are no more authority agent values available for the given identifier authority value.")]
        public const uint AGENTS_EXHAUSTED = (0xC0000085);
        [Description("An invalid volume label has been specified.")]
        public const uint INVALID_VOLUME_LABEL = (0xC0000086);
        [Description("A mapped section could not be extended.")]
        public const uint SECTION_NOT_EXTENDED = (0xC0000087);
        [Description("Specified section to flush does not map a data file.")]
        public const uint NOT_MAPPED_DATA = (0xC0000088);
        [Description("Indicates the specified image file did not contain a resource section.")]
        public const uint RESOURCE_DATA_NOT_FOUND = (0xC0000089);
        [Description("Indicates the specified resource type cannot be found in the image file.")]
        public const uint RESOURCE_TYPE_NOT_FOUND = (0xC000008A);
        [Description("Indicates the specified resource name cannot be found in the image file.")]
        public const uint RESOURCE_NAME_NOT_FOUND = (0xC000008B);
        [Description("{EXCEPTION} Array bounds exceeded.")]
        public const uint ARRAY_BOUNDS_EXCEEDED = (0xC000008C);
        [Description("{EXCEPTION} Floating-point denormal operand.")]
        public const uint FLOAT_DENORMAL_OPERAND = (0xC000008D);
        [Description("{EXCEPTION} Floating-point division by zero.")]
        public const uint FLOAT_DIVIDE_BY_ZERO = (0xC000008E);
        [Description("{EXCEPTION} Floating-point inexact result.")]
        public const uint FLOAT_INEXACT_RESULT = (0xC000008F);
        [Description("{EXCEPTION} Floating-point invalid operation.")]
        public const uint FLOAT_INVALID_OPERATION = (0xC0000090);
        [Description("{EXCEPTION} Floating-point overflow.")]
        public const uint FLOAT_OVERFLOW = (0xC0000091);
        [Description("{EXCEPTION} Floating-point stack check.")]
        public const uint FLOAT_STACK_CHECK = (0xC0000092);
        [Description("{EXCEPTION} Floating-point underflow.")]
        public const uint FLOAT_UNDERFLOW = (0xC0000093);
        [Description("{EXCEPTION} Integer division by zero.")]
        public const uint INTEGER_DIVIDE_BY_ZERO = (0xC0000094);
        [Description("{EXCEPTION} Integer overflow.")]
        public const uint INTEGER_OVERFLOW = (0xC0000095);
        [Description("{EXCEPTION} Privileged instruction.")]
        public const uint PRIVILEGED_INSTRUCTION = (0xC0000096);
        [Description("An attempt was made to install more paging files than the system supports.")]
        public const uint TOO_MANY_PAGING_FILES = (0xC0000097);
        [Description("The volume for a file has been externally altered such that the opened file is no longer valid.")]
        public const uint FILE_INVALID = (0xC0000098);
        [Description("When a block of memory is allotted for future updates; such as the memory allocated to hold discretionary access control and primary group information; successive updates may exceed the amount of memory originally allotted. Since quota may already have been charged to several processes which have handles to the object; it is not reasonable to alter the size of the allocated memory.Instead; a request that requires more memory than has been allotted must fail and the ALLOTED_SPACE_EXCEEDED error returned.")]
        public const uint ALLOTTED_SPACE_EXCEEDED = (0xC0000099);
        [Description("Insufficient system resources exist to complete the API.")]
        public const uint INSUFFICIENT_RESOURCES = (0xC000009A);
        [Description("An attempt has been made to open a DFS exit path control file.")]
        public const uint DFS_EXIT_PATH_FOUND = (0xC000009B);
        [Description("DEVICE_DATA_ERROR")]
        public const uint DEVICE_DATA_ERROR = (0xC000009C);
        [Description("DEVICE_NOT_CONNECTED")]
        public const uint DEVICE_NOT_CONNECTED = (0xC000009D);
        [Description("DEVICE_POWER_FAILURE")]
        public const uint DEVICE_POWER_FAILURE = (0xC000009E);
        [Description("Virtual memory cannot be freed as base address is not the base of the region and a region size of zero was specified.")]
        public const uint FREE_VM_NOT_AT_BASE = (0xC000009F);
        [Description("An attempt was made to free virtual memory which is not allocated.")]
        public const uint MEMORY_NOT_ALLOCATED = (0xC00000A0);
        [Description("The working set is not big enough to allow the requested pages to be locked.")]
        public const uint WORKING_SET_QUOTA = (0xC00000A1);
        [Description("{Write Protect Error} The disk cannot be written to because it is write protected.Please remove the write protection from the volume %hs in drive %hs.")]
        public const uint MEDIA_WRITE_PROTECTED = (0xC00000A2);
        [Description("{Drive Not Ready} The drive is not ready for use; its door may be open.Please check drive %hs and make sure that a disk is inserted and that the drive door is closed.")]
        public const uint DEVICE_NOT_READY = (0xC00000A3);
        [Description("The specified attributes are invalid; or incompatible with the attributes for the group as a whole.")]
        public const uint INVALID_GROUP_ATTRIBUTES = (0xC00000A4);
        [Description("A specified impersonation level is invalid. Also used to indicate a required impersonation level was not provided.")]
        public const uint BAD_IMPERSONATION_LEVEL = (0xC00000A5);
        [Description("An attempt was made to open an Anonymous level token. Anonymous tokens may not be opened.")]
        public const uint CANT_OPEN_ANONYMOUS = (0xC00000A6);
        [Description("The validation information class requested was invalid.")]
        public const uint BAD_VALIDATION_CLASS = (0xC00000A7);
        [Description("The type of a token object is inappropriate for its attempted use.")]
        public const uint BAD_TOKEN_TYPE = (0xC00000A8);
        [Description("The type of a token object is inappropriate for its attempted use.")]
        public const uint BAD_MASTER_BOOT_RECORD = (0xC00000A9);
        [Description("An attempt was made to execute an instruction at an unaligned address and the host system does not support unaligned instruction references.")]
        public const uint INSTRUCTION_MISALIGNMENT = (0xC00000AA);
        [Description("The maximum named pipe instance count has been reached.")]
        public const uint INSTANCE_NOT_AVAILABLE = (0xC00000AB);
        [Description("An instance of a named pipe cannot be found in the listening state.")]
        public const uint PIPE_NOT_AVAILABLE = (0xC00000AC);
        [Description("The named pipe is not in the connected or closing state.")]
        public const uint INVALID_PIPE_STATE = (0xC00000AD);
        [Description("The specified pipe is set to complete operations and there are current I/O operations queued so it cannot be changed to queue operations.")]
        public const uint PIPE_BUSY = (0xC00000AE);
        [Description("The specified handle is not open to the server end of the named pipe.")]
        public const uint ILLEGAL_FUNCTION = (0xC00000AF);
        [Description("The specified named pipe is in the disconnected state.")]
        public const uint PIPE_DISCONNECTED = (0xC00000B0);
        [Description("The specified named pipe is in the closing state.")]
        public const uint PIPE_CLOSING = (0xC00000B1);
        [Description("The specified named pipe is in the connected state.")]
        public const uint PIPE_CONNECTED = (0xC00000B2);
        [Description("The specified named pipe is in the listening state.")]
        public const uint PIPE_LISTENING = (0xC00000B3);
        [Description("The specified named pipe is not in message mode.")]
        public const uint INVALID_READ_MODE = (0xC00000B4);
        [Description("{Device Timeout} The specified I/O operation on %hs was not completed before the time-out period expired.")]
        public const uint IO_TIMEOUT = (0xC00000B5);
        [Description("The specified file has been closed by another process.")]
        public const uint FILE_FORCED_CLOSED = (0xC00000B6);
        [Description("Profiling not started.")]
        public const uint PROFILING_NOT_STARTED = (0xC00000B7);
        [Description("Profiling not stopped.")]
        public const uint PROFILING_NOT_STOPPED = (0xC00000B8);
        [Description("The passed ACL did not contain the minimum required information.")]
        public const uint COULD_NOT_INTERPRET = (0xC00000B9);
        [Description("The file that was specified as a target is a directory and the caller specified that it could be anything but a directory.")]
        public const uint FILE_IS_A_DIRECTORY = (0xC00000BA);
        [Description("The request is not supported.")]
        public const uint NOT_SUPPORTED = (0xC00000BB);
        [Description("This remote computer is not listening.")]
        public const uint REMOTE_NOT_LISTENING = (0xC00000BC);
        [Description("A duplicate name exists on the network.")]
        public const uint DUPLICATE_NAME = (0xC00000BD);
        [Description("The network path cannot be located.")]
        public const uint BAD_NETWORK_PATH = (0xC00000BE);
        [Description("The network is busy.")]
        public const uint NETWORK_BUSY = (0xC00000BF);
        [Description("This device does not exist.")]
        public const uint DEVICE_DOES_NOT_EXIST = (0xC00000C0);
        [Description("The network BIOS command limit has been reached.")]
        public const uint TOO_MANY_COMMANDS = (0xC00000C1);
        [Description("An I/O adapter hardware error has occurred.")]
        public const uint ADAPTER_HARDWARE_ERROR = (0xC00000C2);
        [Description("The network responded incorrectly.")]
        public const uint INVALID_NETWORK_RESPONSE = (0xC00000C3);
        [Description("An unexpected network error occurred.")]
        public const uint UNEXPECTED_NETWORK_ERROR = (0xC00000C4);
        [Description("The remote adapter is not compatible.")]
        public const uint BAD_REMOTE_ADAPTER = (0xC00000C5);
        [Description("The printer queue is full.")]
        public const uint PRINT_QUEUE_FULL = (0xC00000C6);
        [Description("Space to store the file waiting to be printed is not available on the server.")]
        public const uint NO_SPOOL_SPACE = (0xC00000C7);
        [Description("The requested print file has been canceled.")]
        public const uint PRINT_CANCELLED = (0xC00000C8);
        [Description("The network name was deleted.")]
        public const uint NETWORK_NAME_DELETED = (0xC00000C9);
        [Description("Network access is denied.")]
        public const uint NETWORK_ACCESS_DENIED = (0xC00000CA);
        [Description("{Incorrect Network Resource Type} The specified device type (LPT; for example) conflicts with the actual device type on the remote resource.")]
        public const uint BAD_DEVICE_TYPE = (0xC00000CB);
        [Description("{Network Name Not Found} The specified share name cannot be found on the remote server.")]
        public const uint BAD_NETWORK_NAME = (0xC00000CC);
        [Description("The name limit for the local computer network adapter card was exceeded.")]
        public const uint TOO_MANY_NAMES = (0xC00000CD);
        [Description("The network BIOS session limit was exceeded.")]
        public const uint TOO_MANY_SESSIONS = (0xC00000CE);
        [Description("File sharing has been temporarily paused.")]
        public const uint SHARING_PAUSED = (0xC00000CF);
        [Description("No more connections can be made to this remote computer at this time because there are already as many connections as the computer can accept.")]
        public const uint REQUEST_NOT_ACCEPTED = (0xC00000D0);
        [Description("Print or disk redirection is temporarily paused.")]
        public const uint REDIRECTOR_PAUSED = (0xC00000D1);
        [Description("A network data fault occurred.")]
        public const uint NET_WRITE_FAULT = (0xC00000D2);
        [Description("The number of active profiling objects is at the maximum and no more may be started.")]
        public const uint PROFILING_AT_LIMIT = (0xC00000D3);
        [Description("{Incorrect Volume} The target file of a rename request is located on a different device than the source of the rename request.")]
        public const uint NOT_SAME_DEVICE = (0xC00000D4);
        [Description("The file specified has been renamed and thus cannot be modified.")]
        public const uint FILE_RENAMED = (0xC00000D5);
        [Description("{Network Request Timeout} The session with a remote server has been disconnected because the time-out interval for a request has expired.")]
        public const uint VIRTUAL_CIRCUIT_CLOSED = (0xC00000D6);
        [Description("Indicates an attempt was made to operate on the security of an object that does not have security associated with it.")]
        public const uint NO_SECURITY_ON_OBJECT = (0xC00000D7);
        [Description("Used to indicate that an operation cannot continue without blocking for I/O.")]
        public const uint CANT_WAIT = (0xC00000D8);
        [Description("Used to indicate that a read operation was done on an empty pipe.")]
        public const uint PIPE_EMPTY = (0xC00000D9);
        [Description("Configuration information could not be read from the domain controller; either because the machine is unavailable; or access has been denied.")]
        public const uint CANT_ACCESS_DOMAIN_INFO = (0xC00000DA);
        [Description("Indicates that a thread attempted to terminate itself by default (called NtTerminateThread with NULL) and it was the last thread in the current process.")]
        public const uint CANT_TERMINATE_SELF = (0xC00000DB);
        [Description("Indicates the Sam Server was in the wrong state to perform the desired operation.")]
        public const uint INVALID_SERVER_STATE = (0xC00000DC);
        [Description("Indicates the Domain was in the wrong state to perform the desired operation.")]
        public const uint INVALID_DOMAIN_STATE = (0xC00000DD);
        [Description("This operation is only allowed for the Primary Domain Controller of the domain.")]
        public const uint INVALID_DOMAIN_ROLE = (0xC00000DE);
        [Description("The specified Domain did not exist.")]
        public const uint NO_SUCH_DOMAIN = (0xC00000DF);
        [Description("The specified Domain already exists.")]
        public const uint DOMAIN_EXISTS = (0xC00000E0);
        [Description("An attempt was made to exceed the limit on the number of domains per server for this release.")]
        public const uint DOMAIN_LIMIT_EXCEEDED = (0xC00000E1);
        [Description("Error status returned when oplock request is denied.")]
        public const uint OPLOCK_NOT_GRANTED = (0xC00000E2);
        [Description("Error status returned when an invalid oplock acknowledgment is received by a file system.")]
        public const uint INVALID_OPLOCK_PROTOCOL = (0xC00000E3);
        [Description("This error indicates that the requested operation cannot be completed due to a catastrophic media failure or on-disk data structure corruption.")]
        public const uint INTERNAL_DB_CORRUPTION = (0xC00000E4);
        [Description("An internal error occurred.")]
        public const uint INTERNAL_ERROR = (0xC00000E5);
        [Description("Indicates generic access types were contained in an access mask which should already be mapped to non-generic access types.")]
        public const uint GENERIC_NOT_MAPPED = (0xC00000E6);
        [Description("Indicates a security descriptor is not in the necessary format (absolute or self-relative).")]
        public const uint BAD_DESCRIPTOR_FORMAT = (0xC00000E7);
        [Description("An access to a user buffer failed at an \"expected\" point in time. This code is defined since the caller does not want to accept ACCESS_VIOLATION in its filter.")]
        public const uint INVALID_USER_BUFFER = (0xC00000E8);
        [Description("If an I/O error is returned which is not defined in the standard FsRtl filter; it is converted to the following error which is guaranteed to be in the filter. In this case information is lost; however; the filter correctly handles the exception.")]
        public const uint UNEXPECTED_IO_ERROR = (0xC00000E9);
        [Description("If an MM error is returned which is not defined in the standard FsRtl filter; it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost; however; the filter correctly handles the exception.")]
        public const uint UNEXPECTED_MM_CREATE_ERR = (0xC00000EA);
        [Description("If an MM error is returned which is not defined in the standard FsRtl filter; it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost; however; the filter correctly handles the exception.")]
        public const uint UNEXPECTED_MM_MAP_ERROR = (0xC00000EB);
        [Description("If an MM error is returned which is not defined in the standard FsRtl filter; it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost; however; the filter correctly handles the exception.")]
        public const uint UNEXPECTED_MM_EXTEND_ERR = (0xC00000EC);
        [Description("The requested action is restricted for use by logon processes only. The calling process has not registered as a logon process.")]
        public const uint NOT_LOGON_PROCESS = (0xC00000ED);
        [Description("An attempt has been made to start a new session manager or LSA logon session with an ID that is already in use.")]
        public const uint LOGON_SESSION_EXISTS = (0xC00000EE);
        [Description("An invalid parameter was passed to a service or function as the first argument.")]
        public const uint INVALID_PARAMETER_1 = (0xC00000EF);
        [Description("An invalid parameter was passed to a service or function as the second argument.")]
        public const uint INVALID_PARAMETER_2 = (0xC00000F0);
        [Description("An invalid parameter was passed to a service or function as the third argument.")]
        public const uint INVALID_PARAMETER_3 = (0xC00000F1);
        [Description("An invalid parameter was passed to a service or function as the fourth argument.")]
        public const uint INVALID_PARAMETER_4 = (0xC00000F2);
        [Description("An invalid parameter was passed to a service or function as the fifth argument.")]
        public const uint INVALID_PARAMETER_5 = (0xC00000F3);
        [Description("An invalid parameter was passed to a service or function as the sixth argument.")]
        public const uint INVALID_PARAMETER_6 = (0xC00000F4);
        [Description("An invalid parameter was passed to a service or function as the seventh argument.")]
        public const uint INVALID_PARAMETER_7 = (0xC00000F5);
        [Description("An invalid parameter was passed to a service or function as the eighth argument.")]
        public const uint INVALID_PARAMETER_8 = (0xC00000F6);
        [Description("An invalid parameter was passed to a service or function as the ninth argument.")]
        public const uint INVALID_PARAMETER_9 = (0xC00000F7);
        [Description("An invalid parameter was passed to a service or function as the tenth argument.")]
        public const uint INVALID_PARAMETER_10 = (0xC00000F8);
        [Description("An invalid parameter was passed to a service or function as the eleventh argument.")]
        public const uint INVALID_PARAMETER_11 = (0xC00000F9);
        [Description("An invalid parameter was passed to a service or function as the twelfth argument.")]
        public const uint INVALID_PARAMETER_12 = (0xC00000FA);
        [Description("An attempt was made to access a network file; but the network software was not yet started.")]
        public const uint REDIRECTOR_NOT_STARTED = (0xC00000FB);
        [Description("An attempt was made to start the redirector; but the redirector has already been started.")]
        public const uint REDIRECTOR_STARTED = (0xC00000FC);
        [Description("A new guard page for the stack cannot be created.")]
        public const uint STACK_OVERFLOW = (0xC00000FD);
        [Description("A specified authentication package is unknown.")]
        public const uint NO_SUCH_PACKAGE = (0xC00000FE);
        [Description("A malformed function table was encountered during an unwind operation.")]
        public const uint BAD_FUNCTION_TABLE = (0xC00000FF);
        [Description("Indicates the specified environment variable name was not found in the specified environment block.")]
        public const uint VARIABLE_NOT_FOUND = (0xC0000100);
        [Description("Indicates that the directory trying to be deleted is not empty.")]
        public const uint DIRECTORY_NOT_EMPTY = (0xC0000101);
        [Description("{Corrupt File} The file or directory %hs is corrupt and unreadable.Please run the Chkdsk utility.")]
        public const uint FILE_CORRUPT_ERROR = (0xC0000102);
        [Description("A requested opened file is not a directory.")]
        public const uint NOT_A_DIRECTORY = (0xC0000103);
        [Description("The logon session is not in a state that is consistent with the requested operation.")]
        public const uint BAD_LOGON_SESSION_STATE = (0xC0000104);
        [Description("An internal LSA error has occurred. An authentication package has requested the creation of a Logon Session but the ID of an already existing Logon Session has been specified.")]
        public const uint LOGON_SESSION_COLLISION = (0xC0000105);
        [Description("A specified name string is too long for its intended use.")]
        public const uint NAME_TOO_LONG = (0xC0000106);
        [Description("The user attempted to force close the files on a redirected drive; but there were opened files on the drive; and the user did not specify a sufficient level of force.")]
        public const uint FILES_OPEN = (0xC0000107);
        [Description("The user attempted to force close the files on a redirected drive; but there were opened directories on the drive; and the user did not specify a sufficient level of force.")]
        public const uint CONNECTION_IN_USE = (0xC0000108);
        [Description("RtlFindMessage could not locate the requested message ID in the message table resource.")]
        public const uint MESSAGE_NOT_FOUND = (0xC0000109);
        [Description("An attempt was made to duplicate an object handle into or out of an exiting process.")]
        public const uint PROCESS_IS_TERMINATING = (0xC000010A);
        [Description("Indicates an invalid value has been provided for the LogonType requested.")]
        public const uint INVALID_LOGON_TYPE = (0xC000010B);
        [Description("Indicates that an attempt was made to assign protection to a file system file or directory and one of the SIDs in the security descriptor could not be translated into a GUID that could be stored by the file system. This causes the protection attempt to fail; which may cause a file creation attempt to fail.")]
        public const uint NO_GUID_TRANSLATION = (0xC000010C);
        [Description("Indicates that an attempt has been made to impersonate via a named pipe that has not yet been read from.")]
        public const uint CANNOT_IMPERSONATE = (0xC000010D);
        [Description("Indicates that the specified image is already loaded.")]
        public const uint IMAGE_ALREADY_LOADED = (0xC000010E);
        [Description("ABIOS_NOT_PRESENT")]
        public const uint ABIOS_NOT_PRESENT = (0xC000010F);
        [Description("ABIOS_LID_NOT_EXIST")]
        public const uint ABIOS_LID_NOT_EXIST = (0xC0000110);
        [Description("ABIOS_LID_ALREADY_OWNED")]
        public const uint ABIOS_LID_ALREADY_OWNED = (0xC0000111);
        [Description("ABIOS_NOT_LID_OWNER")]
        public const uint ABIOS_NOT_LID_OWNER = (0xC0000112);
        [Description("ABIOS_INVALID_COMMAND")]
        public const uint ABIOS_INVALID_COMMAND = (0xC0000113);
        [Description("ABIOS_INVALID_LID")]
        public const uint ABIOS_INVALID_LID = (0xC0000114);
        [Description("ABIOS_SELECTOR_NOT_AVAILABLE")]
        public const uint ABIOS_SELECTOR_NOT_AVAILABLE = (0xC0000115);
        [Description("ABIOS_INVALID_SELECTOR")]
        public const uint ABIOS_INVALID_SELECTOR = (0xC0000116);
        [Description("Indicates that an attempt was made to change the size of the LDT for a process that has no LDT.")]
        public const uint NO_LDT = (0xC0000117);
        [Description("Indicates that an attempt was made to grow an LDT by setting its size; or that the size was not an even number of selectors.")]
        public const uint INVALID_LDT_SIZE = (0xC0000118);
        [Description("Indicates that the starting value for the LDT information was not an integral multiple of the selector size.")]
        public const uint INVALID_LDT_OFFSET = (0xC0000119);
        [Description("Indicates that the user supplied an invalid descriptor when trying to set up Ldt descriptors.")]
        public const uint INVALID_LDT_DESCRIPTOR = (0xC000011A);
        [Description("The specified image file did not have the correct format. It appears to be NE format.")]
        public const uint INVALID_IMAGE_NE_FORMAT = (0xC000011B);
        [Description("Indicates that the transaction state of a registry sub-tree is incompatible with the requested operation. For example; a request has been made to start a new transaction with one already in progress;or a request has been made to apply a transaction when one is not currently in progress.")]
        public const uint RXACT_INVALID_STATE = (0xC000011C);
        [Description("Indicates an error has occurred during a registry transaction commit. The database has been left in an unknown; but probably inconsistent; state.The state of the registry transaction is left as COMMITTING.")]
        public const uint RXACT_COMMIT_FAILURE = (0xC000011D);
        [Description("An attempt was made to map a file of size zero with the maximum size specified as zero.")]
        public const uint MAPPED_FILE_SIZE_ZERO = (0xC000011E);
        [Description("Too many files are opened on a remote server. This error should only be returned by the Windows redirector on a remote drive.")]
        public const uint TOO_MANY_OPENED_FILES = (0xC000011F);
        [Description("The I/O request was canceled.")]
        public const uint CANCELLED = (0xC0000120);
        [Description("An attempt has been made to remove a file or directory that cannot be deleted.")]
        public const uint CANNOT_DELETE = (0xC0000121);
        [Description("Indicates a name specified as a remote computer name is syntactically invalid.")]
        public const uint INVALID_COMPUTER_NAME = (0xC0000122);
        [Description("An I/O request other than close was performed on a file after it has been deleted; which can only happen to a request which did not complete before the last handle was closed via NtClose.")]
        public const uint FILE_DELETED = (0xC0000123);
        [Description("Indicates an operation has been attempted on a built-in (special) SAM account which is incompatible with built-in accounts. For example; built-in accounts cannot be deleted.")]
        public const uint SPECIAL_ACCOUNT = (0xC0000124);
        [Description("The operation requested may not be performed on the specified group because it is a built-in special group.")]
        public const uint SPECIAL_GROUP = (0xC0000125);
        [Description("The operation requested may not be performed on the specified user because it is a built-in special user.")]
        public const uint SPECIAL_USER = (0xC0000126);
        [Description("Indicates a member cannot be removed from a group because the group is currently the member's primary group.")]
        public const uint MEMBERS_PRIMARY_GROUP = (0xC0000127);
        [Description("An I/O request other than close and several other special case operations was attempted using a file object that had already been closed.")]
        public const uint FILE_CLOSED = (0xC0000128);
        [Description("Indicates a process has too many threads to perform the requested action. For example; assignment of a primary token may only be performed when a process has zero or one threads.")]
        public const uint TOO_MANY_THREADS = (0xC0000129);
        [Description("An attempt was made to operate on a thread within a specific process; but the thread specified is not in the process specified.")]
        public const uint THREAD_NOT_IN_PROCESS = (0xC000012A);
        [Description("An attempt was made to establish a token for use as a primary token but the token is already in use. A token can only be the primary token of one process at a time.")]
        public const uint TOKEN_ALREADY_IN_USE = (0xC000012B);
        [Description("Page file quota was exceeded.")]
        public const uint PAGEFILE_QUOTA_EXCEEDED = (0xC000012C);
        [Description("{Out of Virtual Memory} Your system is low on virtual memory. To ensure that Windows runs properly; increase the size of your virtual memory paging file. For more information; see Help.")]
        public const uint COMMITMENT_LIMIT = (0xC000012D);
        [Description("The specified image file did not have the correct format; it appears to be LE format.")]
        public const uint INVALID_IMAGE_LE_FORMAT = (0xC000012E);
        [Description("The specified image file did not have the correct format; it did not have an initial MZ.")]
        public const uint INVALID_IMAGE_NOT_MZ = (0xC000012F);
        [Description("The specified image file did not have the correct format; it did not have a proper e_lfarlc in the MZ header.")]
        public const uint INVALID_IMAGE_PROTECT = (0xC0000130);
        [Description("The specified image file did not have the correct format; it appears to be a 16-bit Windows image.")]
        public const uint INVALID_IMAGE_WIN_16 = (0xC0000131);
        [Description("The Netlogon service cannot start because another Netlogon service running in the domain conflicts with the specified role.")]
        public const uint LOGON_SERVER_CONFLICT = (0xC0000132);
        [Description("The time at the Primary Domain Controller is different than the time at the Backup Domain Controller or member server by too large an amount.")]
        public const uint TIME_DIFFERENCE_AT_DC = (0xC0000133);
        [Description("The SAM database on a Windows Server is significantly out of synchronization with the copy on the Domain Controller. A complete synchronization is required.")]
        public const uint SYNCHRONIZATION_REQUIRED = (0xC0000134);
        [Description("{Unable To Locate Component} This application has failed to start because %hs was not found. Re-installing the application may fix this problem.")]
        public const uint DLL_NOT_FOUND = (0xC0000135);
        [Description("The NtCreateFile API failed. This error should never be returned to an application; it is a place holder for the Windows Lan Manager Redirector to use in its internal error mapping routines.")]
        public const uint OPEN_FAILED = (0xC0000136);
        [Description("{Privilege Failed} The I/O permissions for the process could not be changed.")]
        public const uint IO_PRIVILEGE_FAILED = (0xC0000137);
        [Description("{Ordinal Not Found} The ordinal %ld could not be located in the dynamic link library %hs.")]
        public const uint ORDINAL_NOT_FOUND = (0xC0000138);
        [Description("{Entry Point Not Found} The procedure entry point %hs could not be located in the dynamic link library %hs.")]
        public const uint ENTRYPOINT_NOT_FOUND = (0xC0000139);
        [Description("{Application Exit by CTRL+C} The application terminated as a result of a CTRL+C.")]
        public const uint CONTROL_C_EXIT = (0xC000013A);
        [Description("{Virtual Circuit Closed} The network transport on your computer has closed a network connection. There may or may not be I/O requests outstanding.")]
        public const uint LOCAL_DISCONNECT = (0xC000013B);
        [Description("{Virtual Circuit Closed} The network transport on a remote computer has closed a network connection. There may or may not be I/O requests outstanding.")]
        public const uint REMOTE_DISCONNECT = (0xC000013C);
        [Description("{Insufficient Resources on Remote Computer} The remote computer has insufficient resources to complete the network request. For instance; there may not be enough memory available on the remote computer to carry out the request at this time.")]
        public const uint REMOTE_RESOURCES = (0xC000013D);
        [Description("{Virtual Circuit Closed} An existing connection (virtual circuit) has been broken at the remote computer. There is probably something wrong with the network software protocol or the network hardware on the remote computer.")]
        public const uint LINK_FAILED = (0xC000013E);
        [Description("{Virtual Circuit Closed} The network transport on your computer has closed a network connection because it had to wait too long for a response from the remote computer.")]
        public const uint LINK_TIMEOUT = (0xC000013F);
        [Description("The connection handle given to the transport was invalid.")]
        public const uint INVALID_CONNECTION = (0xC0000140);
        [Description("The address handle given to the transport was invalid.")]
        public const uint INVALID_ADDRESS = (0xC0000141);
        [Description("{DLL Initialization Failed} Initialization of the dynamic link library %hs failed. The process is terminating abnormally.")]
        public const uint DLL_INIT_FAILED = (0xC0000142);
        [Description("{Missing System File} The required system file %hs is bad or missing.")]
        public const uint MISSING_SYSTEMFILE = (0xC0000143);
        [Description("{Application Error} The exception %s (0x%08lx) occurred in the application at location 0x%08lx.")]
        public const uint UNHANDLED_EXCEPTION = (0xC0000144);
        [Description("{Application Error} The application failed to initialize properly (0x%lx). Click on OK to terminate the application.")]
        public const uint APP_INIT_FAILURE = (0xC0000145);
        [Description("{Unable to Create Paging File} The creation of the paging file %hs failed (%lx). The requested size was %ld.")]
        public const uint PAGEFILE_CREATE_FAILED = (0xC0000146);
        [Description("{No Paging File Specified} No paging file was specified in the system configuration.")]
        public const uint NO_PAGEFILE = (0xC0000147);
        [Description("{Incorrect System Call Level} An invalid level was passed into the specified system call.")]
        public const uint INVALID_LEVEL = (0xC0000148);
        [Description("{Incorrect Password to LAN Manager Server} You specified an incorrect password to a LAN Manager 2.x or MS-NET server.")]
        public const uint WRONG_PASSWORD_CORE = (0xC0000149);
        [Description("{EXCEPTION} A real-mode application issued a floating-point instruction and floating-point hardware is not present.")]
        public const uint ILLEGAL_FLOAT_CONTEXT = (0xC000014A);
        [Description("The pipe operation has failed because the other end of the pipe has been closed.")]
        public const uint PIPE_BROKEN = (0xC000014B);
        [Description("{The Registry Is Corrupt} The structure of one of the files that contains Registry data is corrupt; or the image of the file in memory is corrupt; or the file could not be recovered because the alternate copy or log was absent or corrupt.")]
        public const uint REGISTRY_CORRUPT = (0xC000014C);
        [Description("An I/O operation initiated by the Registry failed unrecoverably. The Registry could not read in; or write out; or flush; one of the files that contain the system's image of the Registry.")]
        public const uint REGISTRY_IO_FAILED = (0xC000014D);
        [Description("An event pair synchronization operation was performed using the thread specific client/server event pair object; but no event pair object was associated with the thread.")]
        public const uint NO_EVENT_PAIR = (0xC000014E);
        [Description("The volume does not contain a recognized file system. Please make sure that all required file system drivers are loaded and that the volume is not corrupt.")]
        public const uint UNRECOGNIZED_VOLUME = (0xC000014F);
        [Description("No serial device was successfully initialized. The serial driver will unload.")]
        public const uint SERIAL_NO_DEVICE_INITED = (0xC0000150);
        [Description("The specified local group does not exist.")]
        public const uint NO_SUCH_ALIAS = (0xC0000151);
        [Description("The specified account name is not a member of the local group.")]
        public const uint MEMBER_NOT_IN_ALIAS = (0xC0000152);
        [Description("The specified account name is already a member of the local group.")]
        public const uint MEMBER_IN_ALIAS = (0xC0000153);
        [Description("The specified local group already exists.")]
        public const uint ALIAS_EXISTS = (0xC0000154);
        [Description("A requested type of logon (e.g.; Interactive; Network; Service) is not granted by the target system's local security policy. Please ask the system administrator to grant the necessary form of logon.")]
        public const uint LOGON_NOT_GRANTED = (0xC0000155);
        [Description("The maximum number of secrets that may be stored in a single system has been exceeded. The length and number of secrets is limited to satisfy United States State Department export restrictions.")]
        public const uint TOO_MANY_SECRETS = (0xC0000156);
        [Description("The length of a secret exceeds the maximum length allowed. The length and number of secrets is limited to satisfy United States State Department export restrictions.")]
        public const uint SECRET_TOO_LONG = (0xC0000157);
        [Description("The Local Security Authority (LSA) database contains an internal inconsistency.")]
        public const uint INTERNAL_DB_ERROR = (0xC0000158);
        [Description("The requested operation cannot be performed in fullscreen mode.")]
        public const uint FULLSCREEN_MODE = (0xC0000159);
        [Description("During a logon attempt; the user's security context accumulated too many security IDs. This is a very unusual situation. Remove the user from some global or local groups to reduce the number of security ids to incorporate into the security context.")]
        public const uint TOO_MANY_CONTEXT_IDS = (0xC000015A);
        [Description("A user has requested a type of logon (e.g.; interactive or network) that has not been granted. An administrator has control over who may logon interactively and through the network.")]
        public const uint LOGON_TYPE_NOT_GRANTED = (0xC000015B);
        [Description("The system has attempted to load or restore a file into the registry; and the specified file is not in the format of a registry file.")]
        public const uint NOT_REGISTRY_FILE = (0xC000015C);
        [Description("An attempt was made to change a user password in the security account manager without providing the necessary Windows cross-encrypted password.")]
        public const uint NT_CROSS_ENCRYPTION_REQUIRED = (0xC000015D);
        [Description("A Windows Server has an incorrect configuration.")]
        public const uint DOMAIN_CTRLR_CONFIG_ERROR = (0xC000015E);
        [Description("An attempt was made to explicitly access the secondary copy of information via a device control to the Fault Tolerance driver and the secondary copy is not present in the system.")]
        public const uint FT_MISSING_MEMBER = (0xC000015F);
        [Description("A configuration registry node representing a driver service entry was ill-formed and did not contain required value entries.")]
        public const uint ILL_FORMED_SERVICE_ENTRY = (0xC0000160);
        [Description("An illegal character was encountered. For a multi-byte character set this includes a lead byte without a succeeding trail byte. For the Unicode character set this includes the characters 0xFFFF and 0xFFFE.")]
        public const uint ILLEGAL_CHARACTER = (0xC0000161);
        [Description("No mapping for the Unicode character exists in the target multi-byte code page.")]
        public const uint UNMAPPABLE_CHARACTER = (0xC0000162);
        [Description("The Unicode character is not defined in the Unicode character set installed on the system.")]
        public const uint UNDEFINED_CHARACTER = (0xC0000163);
        [Description("The paging file cannot be created on a floppy diskette.")]
        public const uint FLOPPY_VOLUME = (0xC0000164);
        [Description("{Floppy Disk Error} While accessing a floppy disk; an ID address mark was not found.")]
        public const uint FLOPPY_ID_MARK_NOT_FOUND = (0xC0000165);
        [Description("{Floppy Disk Error} While accessing a floppy disk; the track address from the sector ID field was found to be different than the track address maintained by the controller.")]
        public const uint FLOPPY_WRONG_CYLINDER = (0xC0000166);
        [Description("{Floppy Disk Error} The floppy disk controller reported an error that is not recognized by the floppy disk driver.")]
        public const uint FLOPPY_UNKNOWN_ERROR = (0xC0000167);
        [Description("{Floppy Disk Error} While accessing a floppy-disk; the controller returned inconsistent results via its registers.")]
        public const uint FLOPPY_BAD_REGISTERS = (0xC0000168);
        [Description("{Hard Disk Error} While accessing the hard disk; a recalibrate operation failed; even after retries.")]
        public const uint DISK_RECALIBRATE_FAILED = (0xC0000169);
        [Description("{Hard Disk Error} While accessing the hard disk; a disk operation failed even after retries.")]
        public const uint DISK_OPERATION_FAILED = (0xC000016A);
        [Description("{Hard Disk Error} While accessing the hard disk; a disk controller reset was needed; but even that failed.")]
        public const uint DISK_RESET_FAILED = (0xC000016B);
        [Description("An attempt was made to open a device that was sharing an IRQ with other devices. At least one other device that uses that IRQ was already opened.Two concurrent opens of devices that share an IRQ and only work via interrupts is not supported for the particular bus type that the devices use.")]
        public const uint SHARED_IRQ_BUSY = (0xC000016C);
        [Description("{FT Orphaning} A disk that is part of a fault-tolerant volume can no longer be accessed.")]
        public const uint FT_ORPHANING = (0xC000016D);
        [Description("The system bios failed to connect a system interrupt to the device or bus for which the device is connected.")]
        public const uint BIOS_FAILED_TO_CONNECT_INTERRUPT = (0xC000016E);
        [Description("Tape could not be partitioned.")]
        public const uint PARTITION_FAILURE = (0xC0000172);
        [Description("When accessing a new tape of a multivolume partition; the current blocksize is incorrect.")]
        public const uint INVALID_BLOCK_LENGTH = (0xC0000173);
        [Description("Tape partition information could not be found when loading a tape.")]
        public const uint DEVICE_NOT_PARTITIONED = (0xC0000174);
        [Description("Attempt to lock the eject media mechanism fails.")]
        public const uint UNABLE_TO_LOCK_MEDIA = (0xC0000175);
        [Description("Unload media fails.")]
        public const uint UNABLE_TO_UNLOAD_MEDIA = (0xC0000176);
        [Description("Physical end of tape was detected.")]
        public const uint EOM_OVERFLOW = (0xC0000177);
        [Description("{No Media} There is no media in the drive.Please insert media into drive %hs.")]
        public const uint NO_MEDIA = (0xC0000178);
        [Description("A member could not be added to or removed from the local group because the member does not exist.")]
        public const uint NO_SUCH_MEMBER = (0xC000017A);
        [Description("A new member could not be added to a local group because the member has the wrong account type.")]
        public const uint INVALID_MEMBER = (0xC000017B);
        [Description("Illegal operation attempted on a registry key which has been marked for deletion.")]
        public const uint KEY_DELETED = (0xC000017C);
        [Description("System could not allocate required space in a registry log.")]
        public const uint NO_LOG_SPACE = (0xC000017D);
        [Description("Too many Sids have been specified.")]
        public const uint TOO_MANY_SIDS = (0xC000017E);
        [Description("An attempt was made to change a user password in the security account manager without providing the necessary LM cross-encrypted password.")]
        public const uint LM_CROSS_ENCRYPTION_REQUIRED = (0xC000017F);
        [Description("An attempt was made to create a symbolic link in a registry key that already has subkeys or values.")]
        public const uint KEY_HAS_CHILDREN = (0xC0000180);
        [Description("An attempt was made to create a Stable subkey under a Volatile parent key.")]
        public const uint CHILD_MUST_BE_VOLATILE = (0xC0000181);
        [Description("The I/O device is configured incorrectly or the configuration parameters to the driver are incorrect.")]
        public const uint DEVICE_CONFIGURATION_ERROR = (0xC0000182);
        [Description("An error was detected between two drivers or within an I/O driver.")]
        public const uint DRIVER_INTERNAL_ERROR = (0xC0000183);
        [Description("The device is not in a valid state to perform this request.")]
        public const uint INVALID_DEVICE_STATE = (0xC0000184);
        [Description("The I/O device reported an I/O error.")]
        public const uint IO_DEVICE_ERROR = (0xC0000185);
        [Description("A protocol error was detected between the driver and the device.")]
        public const uint DEVICE_PROTOCOL_ERROR = (0xC0000186);
        [Description("This operation is only allowed for the Primary Domain Controller of the domain.")]
        public const uint BACKUP_CONTROLLER = (0xC0000187);
        [Description("Log file space is insufficient to support this operation.")]
        public const uint LOG_FILE_FULL = (0xC0000188);
        [Description("A write operation was attempted to a volume after it was dismounted.")]
        public const uint TOO_LATE = (0xC0000189);
        [Description("The workstation does not have a trust secret for the primary domain in the local LSA database.")]
        public const uint NO_TRUST_LSA_SECRET = (0xC000018A);
        [Description("The SAM database on the Windows Server does not have a computer account for this workstation trust relationship.")]
        public const uint NO_TRUST_SAM_ACCOUNT = (0xC000018B);
        [Description("The logon request failed because the trust relationship between the primary domain and the trusted domain failed.")]
        public const uint TRUSTED_DOMAIN_FAILURE = (0xC000018C);
        [Description("The logon request failed because the trust relationship between this workstation and the primary domain failed.")]
        public const uint TRUSTED_RELATIONSHIP_FAILURE = (0xC000018D);
        [Description("The Eventlog log file is corrupt.")]
        public const uint EVENTLOG_FILE_CORRUPT = (0xC000018E);
        [Description("No Eventlog log file could be opened. The Eventlog service did not start.")]
        public const uint EVENTLOG_CANT_START = (0xC000018F);
        [Description("The network logon failed. This may be because the validation authority can't be reached.")]
        public const uint TRUST_FAILURE = (0xC0000190);
        [Description("An attempt was made to acquire a mutant such that its maximum count would have been exceeded.")]
        public const uint MUTANT_LIMIT_EXCEEDED = (0xC0000191);
        [Description("An attempt was made to logon; but the netlogon service was not started.")]
        public const uint NETLOGON_NOT_STARTED = (0xC0000192);
        [Description("The user's account has expired.")]
        public const uint ACCOUNT_EXPIRED = (0xC0000193);
        [Description("{EXCEPTION} Possible deadlock condition.")]
        public const uint POSSIBLE_DEADLOCK = (0xC0000194);
        [Description("Multiple connections to a server or shared resource by the same user; using more than one user name; are not allowed. Disconnect all previous connections to the server or shared resource and try again..")]
        public const uint NETWORK_CREDENTIAL_CONFLICT = (0xC0000195);
        [Description("An attempt was made to establish a session to a network server; but there are already too many sessions established to that server.")]
        public const uint REMOTE_SESSION_LIMIT = (0xC0000196);
        [Description("The log file has changed between reads.")]
        public const uint EVENTLOG_FILE_CHANGED = (0xC0000197);
        [Description("The account used is an Interdomain Trust account. Use your global user account or local user account to access this server.")]
        public const uint NOLOGON_INTERDOMAIN_TRUST_ACCOUNT = (0xC0000198);
        [Description("The account used is a Computer Account. Use your global user account or local user account to access this server.")]
        public const uint NOLOGON_WORKSTATION_TRUST_ACCOUNT = (0xC0000199);
        [Description("The account used is an Server Trust account. Use your global user account or local user account to access this server.")]
        public const uint NOLOGON_SERVER_TRUST_ACCOUNT = (0xC000019A);
        [Description("The name or SID of the domain specified is inconsistent with the trust information for that domain.")]
        public const uint DOMAIN_TRUST_INCONSISTENT = (0xC000019B);
        [Description("A volume has been accessed for which a file system driver is required that has not yet been loaded.")]
        public const uint FS_DRIVER_REQUIRED = (0xC000019C);
        [Description("There is no user session key for the specified logon session.")]
        public const uint NO_USER_SESSION_KEY = (0xC0000202);
        [Description("The remote user session has been deleted.")]
        public const uint USER_SESSION_DELETED = (0xC0000203);
        [Description("Indicates the specified resource language ID cannot be found in the image file.")]
        public const uint RESOURCE_LANG_NOT_FOUND = (0xC0000204);
        [Description("Insufficient server resources exist to complete the request.")]
        public const uint INSUFF_SERVER_RESOURCES = (0xC0000205);
        [Description("The size of the buffer is invalid for the specified operation.")]
        public const uint INVALID_BUFFER_SIZE = (0xC0000206);
        [Description("The transport rejected the network address specified as invalid.")]
        public const uint INVALID_ADDRESS_COMPONENT = (0xC0000207);
        [Description("The transport rejected the network address specified due to an invalid use of a wildcard.")]
        public const uint INVALID_ADDRESS_WILDCARD = (0xC0000208);
        [Description("The transport address could not be opened because all the available addresses are in use.")]
        public const uint TOO_MANY_ADDRESSES = (0xC0000209);
        [Description("The transport address could not be opened because it already exists.")]
        public const uint ADDRESS_ALREADY_EXISTS = (0xC000020A);
        [Description("The transport address is now closed.")]
        public const uint ADDRESS_CLOSED = (0xC000020B);
        [Description("The transport connection is now disconnected.")]
        public const uint CONNECTION_DISCONNECTED = (0xC000020C);
        [Description("The transport connection has been reset.")]
        public const uint CONNECTION_RESET = (0xC000020D);
        [Description("The transport cannot dynamically acquire any more nodes.")]
        public const uint TOO_MANY_NODES = (0xC000020E);
        [Description("The transport aborted a pending transaction.")]
        public const uint TRANSACTION_ABORTED = (0xC000020F);
        [Description("The transport timed out a request waiting for a response.")]
        public const uint TRANSACTION_TIMED_OUT = (0xC0000210);
        [Description("The transport did not receive a release for a pending response.")]
        public const uint TRANSACTION_NO_RELEASE = (0xC0000211);
        [Description("The transport did not find a transaction matching the specific token.")]
        public const uint TRANSACTION_NO_MATCH = (0xC0000212);
        [Description("The transport had previously responded to a transaction request.")]
        public const uint TRANSACTION_RESPONDED = (0xC0000213);
        [Description("The transport does not recognized the transaction request identifier specified.")]
        public const uint TRANSACTION_INVALID_ID = (0xC0000214);
        [Description("The transport does not recognize the transaction request type specified.")]
        public const uint TRANSACTION_INVALID_TYPE = (0xC0000215);
        [Description("The transport can only process the specified request on the server side of a session.")]
        public const uint NOT_SERVER_SESSION = (0xC0000216);
        [Description("The transport can only process the specified request on the client side of a session.")]
        public const uint NOT_CLIENT_SESSION = (0xC0000217);
        [Description("{Registry File Failure} The registry cannot load the hive (file):%hs or its log or alternate. It is corrupt; absent; or not writable.")]
        public const uint CANNOT_LOAD_REGISTRY_FILE = (0xC0000218);
        [Description("{Unexpected Failure in DebugActiveProcess} An unexpected failure occurred while processing a DebugActiveProcess API request. You may choose OK to terminate the process; or Cancel to ignore the error.")]
        public const uint DEBUG_ATTACH_FAILED = (0xC0000219);
        [Description("{Fatal System Error} The %hs system process terminated unexpectedly with a status of 0x%08x (0x%08x 0x%08x).The system has been shut down.")]
        public const uint SYSTEM_PROCESS_TERMINATED = (0xC000021A);
        [Description("{Data Not Accepted} The TDI client could not handle the data received during an indication.")]
        public const uint DATA_NOT_ACCEPTED = (0xC000021B);
        [Description("{Unable to Retrieve Browser Server List} The list of servers for this workgroup is not currently available.")]
        public const uint NO_BROWSER_SERVERS_FOUND = (0xC000021C);
        [Description("NTVDM encountered a hard error.")]
        public const uint VDM_HARD_ERROR = (0xC000021D);
        [Description("{Cancel Timeout} The driver %hs failed to complete a cancelled I/O request in the allotted time.")]
        public const uint DRIVER_CANCEL_TIMEOUT = (0xC000021E);
        [Description("{Reply Message Mismatch} An attempt was made to reply to an LPC message; but the thread specified by the client ID in the message was not waiting on that message.")]
        public const uint REPLY_MESSAGE_MISMATCH = (0xC000021F);
        [Description("{Mapped View Alignment Incorrect} An attempt was made to map a view of a file; but either the specified base address or the offset into the file were not aligned on the proper allocation granularity.")]
        public const uint MAPPED_ALIGNMENT = (0xC0000220);
        [Description("{Bad Image Checksum} The image %hs is possibly corrupt. The header checksum does not match the computed checksum.")]
        public const uint IMAGE_CHECKSUM_MISMATCH = (0xC0000221);
        [Description("{Delayed Write Failed} Windows was unable to save all the data for the file %hs. The data has been lost.This error may be caused by a failure of your computer hardware or network connection. Please try to save this file elsewhere.")]
        public const uint LOST_WRITEBEHIND_DATA = (0xC0000222);
        [Description("The parameter(s) passed to the server in the client/server shared memory window were invalid. Too much data may have been put in the shared memory window.")]
        public const uint CLIENT_SERVER_PARAMETERS_INVALID = (0xC0000223);
        [Description("The user's password must be changed before logging on the first time.")]
        public const uint PASSWORD_MUST_CHANGE = (0xC0000224);
        [Description("The object was not found.")]
        public const uint NOT_FOUND = (0xC0000225);
        [Description("The stream is not a tiny stream.")]
        public const uint NOT_TINY_STREAM = (0xC0000226);
        [Description("A transaction recover failed.")]
        public const uint RECOVERY_FAILURE = (0xC0000227);
        [Description("The request must be handled by the stack overflow code.")]
        public const uint STACK_OVERFLOW_READ = (0xC0000228);
        [Description("A consistency check failed.")]
        public const uint FAIL_CHECK = (0xC0000229);
        [Description("The attempt to insert the ID in the index failed because the ID is already in the index.")]
        public const uint DUPLICATE_OBJECTID = (0xC000022A);
        [Description("The attempt to set the object's ID failed because the object already has an ID.")]
        public const uint OBJECTID_EXISTS = (0xC000022B);
        [Description("Internal OFS status codes indicating how an allocation operation is handled. Either it is retried after the containing onode is moved or the extent stream is converted to a large stream.")]
        public const uint CONVERT_TO_LARGE = (0xC000022C);
        [Description("The request needs to be retried.")]
        public const uint RETRY = (0xC000022D);
        [Description("The attempt to find the object found an object matching by ID on the volume but it is out of the scope of the handle used for the operation.")]
        public const uint FOUND_OUT_OF_SCOPE = (0xC000022E);
        [Description("The bucket array must be grown. Retry transaction after doing so.")]
        public const uint ALLOCATE_BUCKET = (0xC000022F);
        [Description("The property set specified does not exist on the object.")]
        public const uint PROPSET_NOT_FOUND = (0xC0000230);
        [Description("The user/kernel marshalling buffer has overflowed.")]
        public const uint MARSHALL_OVERFLOW = (0xC0000231);
        [Description("The supplied variant structure contains invalid data.")]
        public const uint INVALID_VARIANt = (0xC0000232);
        [Description("Could not find a domain controller for this domain.")]
        public const uint DOMAIN_CONtROLLER_NOT_FOUND = (0xC0000233);
        [Description("The user account has been automatically locked because too many invalid logon attempts or password change attempts have been requested.")]
        public const uint ACCOUNt_LOCKED_OUT = (0xC0000234);
        [Description("NtClose was called on a handle that was protected from close via NtSetInformationObject.")]
        public const uint HANDLE_NOT_CLOSABLE = (0xC0000235);
        [Description("The transport connection attempt was refused by the remote system.")]
        public const uint CONNECTION_REFUSED = (0xC0000236);
        [Description("The transport connection was gracefully closed.")]
        public const uint GRACEFUL_DISCONNECT = (0xC0000237);
        [Description("The transport endpoint already has an address associated with it.")]
        public const uint ADDRESS_ALREADY_ASSOCIATED = (0xC0000238);
        [Description("An address has not yet been associated with the transport endpoint.")]
        public const uint ADDRESS_NOT_ASSOCIATED = (0xC0000239);
        [Description("An operation was attempted on a nonexistent transport connection.")]
        public const uint CONNECTION_INVALID = (0xC000023A);
        [Description("An invalid operation was attempted on an active transport connection.")]
        public const uint CONNECTION_ACTIVE = (0xC000023B);
        [Description("The remote network is not reachable by the transport.")]
        public const uint NETWORK_UNREACHABLE = (0xC000023C);
        [Description("The remote system is not reachable by the transport.")]
        public const uint HOST_UNREACHABLE = (0xC000023D);
        [Description("The remote system does not support the transport protocol.")]
        public const uint PROTOCOL_UNREACHABLE = (0xC000023E);
        [Description("No service is operating at the destination port of the transport on the remote system.")]
        public const uint PORT_UNREACHABLE = (0xC000023F);
        [Description("The request was aborted.")]
        public const uint REQUEST_ABORTED = (0xC0000240);
        [Description("The transport connection was aborted by the local system.")]
        public const uint CONNECTION_ABORTED = (0xC0000241);
        [Description("The specified buffer contains ill-formed data.")]
        public const uint BAD_COMPRESSION_BUFFER = (0xC0000242);
        [Description("The requested operation cannot be performed on a file with a user mapped section open.")]
        public const uint USER_MAPPED_FILE = (0xC0000243);
        [Description("{Audit Failed} An attempt to generate a security audit failed.")]
        public const uint AUDIT_FAILED = (0xC0000244);
        [Description("The timer resolution was not previously set by the current process.")]
        public const uint TIMER_RESOLUTION_NOT_SET = (0xC0000245);
        [Description("A connection to the server could not be made because the limit on the number of concurrent connections for this account has been reached.")]
        public const uint CONNECTION_COUNt_LIMIT = (0xC0000246);
        [Description("Attempting to login during an unauthorized time of day for this account.")]
        public const uint LOGIN_TIME_RESTRICTION = (0xC0000247);
        [Description("The account is not authorized to login from this station.")]
        public const uint LOGIN_WKSTA_RESTRICTION = (0xC0000248);
        [Description("{UP/MP Image Mismatch} The image %hs has been modified for use on a uniprocessor system; but you are running it on a multiprocessor machine.Please reinstall the image file.")]
        public const uint IMAGE_MP_UP_MISMATCH = (0xC0000249);
        [Description("There is insufficient account information to log you on.")]
        public const uint INSUFFICIENt_LOGON_INFO = (0xC0000250);
        [Description("{Invalid DLL Entrypoint} The dynamic link library %hs is not written correctly. The stack pointer has been left in an inconsistent state.The entrypoint should be declared as WINAPI or STDCALL. Select YES to fail the DLL load. Select NO to continue execution. Selecting NO may cause the application to operate incorrectly.")]
        public const uint BAD_DLL_ENtRYPOINt = (0xC0000251);
        [Description("{Invalid Service Callback Entrypoint} The %hs service is not written correctly. The stack pointer has been left in an inconsistent state.The callback entrypoint should be declared as WINAPI or STDCALL. Selecting OK will cause the service to continue operation. However; the service process may operate incorrectly.")]
        public const uint BAD_SERVICE_ENtRYPOINt = (0xC0000252);
        [Description("The server received the messages but did not send a reply.")]
        public const uint LPC_REPLY_LOST = (0xC0000253);
        [Description("There is an IP address conflict with another system on the network")]
        public const uint IP_ADDRESS_CONFLICT1 = (0xC0000254);
        [Description("There is an IP address conflict with another system on the network")]
        public const uint IP_ADDRESS_CONFLICT2 = (0xC0000255);
        [Description("{Low On Registry Space} The system has reached the maximum size allowed for the system part of the registry.Additional storage requests will be ignored.")]
        public const uint REGISTRY_QUOTA_LIMIT = (0xC0000256);
        [Description("The contacted server does not support the indicated part of the DFS namespace.")]
        public const uint PATH_NOT_COVERED = (0xC0000257);
        [Description("A callback return system service cannot be executed when no callback is active.")]
        public const uint NO_CALLBACK_ACTIVE = (0xC0000258);
        [Description("The service being accessed is licensed for a particular number of connections. No more connections can be made to the service at this time because there are already as many connections as the service can accept.")]
        public const uint LICENSE_QUOTA_EXCEEDED = (0xC0000259);
        [Description("The password provided is too short to meet the policy of your user account. Please choose a longer password.")]
        public const uint PWD_TOO_SHORT = (0xC000025A);
        [Description("The policy of your user account does not allow you to change passwords too frequently. This is done to prevent users from changing back to a familiar; but potentially discovered; password. If you feel your password has been compromised then please contact your administrator immediately to have a new one assigned.")]
        public const uint PWD_TOO_RECENt = (0xC000025B);
        [Description("You have attempted to change your password to one that you have used in the past. The policy of your user account does not allow this. Please select a password that you have not previously used.")]
        public const uint PWD_HISTORY_CONFLICT = (0xC000025C);
        [Description("You have attempted to load a legacy device driver while its device instance had been disabled.")]
        public const uint PLUGPLAY_NO_DEVICE = (0xC000025E);
        [Description("The specified compression format is unsupported.")]
        public const uint UNSUPPORTED_COMPRESSION = (0xC000025F);
        [Description("The specified hardware profile configuration is invalid.")]
        public const uint INVALID_HW_PROFILE = (0xC0000260);
        [Description("The specified Plug and Play registry device path is invalid.")]
        public const uint INVALID_PLUGPLAY_DEVICE_PATH = (0xC0000261);
        [Description("{Driver Entry Point Not Found} The %hs device driver could not locate the ordinal %ld in driver %hs.")]
        public const uint DRIVER_ORDINAL_NOT_FOUND = (0xC0000262);
        [Description("{Driver Entry Point Not Found} The %hs device driver could not locate the entry point %hs in driver %hs.")]
        public const uint DRIVER_ENtRYPOINt_NOT_FOUND = (0xC0000263);
        [Description("{Application Error} The application attempted to release a resource it did not own. Click on OK to terminate the application.")]
        public const uint RESOURCE_NOT_OWNED = (0xC0000264);
        [Description("An attempt was made to create more links on a file than the file system supports.")]
        public const uint TOO_MANY_LINKS = (0xC0000265);
        [Description("The specified quota list is internally inconsistent with its descriptor.")]
        public const uint QUOTA_LIST_INCONSISTENt = (0xC0000266);
        [Description("The specified file has been relocated to offline storage.")]
        public const uint FILE_IS_OFFLINE = (0xC0000267);
        [Description("{Windows Evaluation Notification} The evaluation period for this installation of Windows has expired. This system will shutdown in 1 hour. To restore access to this installation of Windows; please upgrade this installation using a licensed distribution of this product.")]
        public const uint EVALUATION_EXPIRATION = (0xC0000268);
        [Description("{Illegal System DLL Relocation} The system DLL %hs was relocated in memory. The application will not run properly.The relocation occurred because the DLL %hs occupied an address range reserved for Windows system DLLs. The vendor supplying the DLL should be contacted for a new DLL.")]
        public const uint ILLEGAL_DLL_RELOCATION = (0xC0000269);
        [Description("{License Violation} The system has detected tampering with your registered product type. This is a violation of your software license. Tampering with product type is not permitted.")]
        public const uint LICENSE_VIOLATION = (0xC000026A);
        [Description("{DLL Initialization Failed} The application failed to initialize because the window station is shutting down.")]
        public const uint DLL_INIT_FAILED_LOGOFF = (0xC000026B);
        [Description("{Unable to Load Device Driver} %hs device driver could not be loaded.Error Status was 0x%x")]
        public const uint DRIVER_UNABLE_TO_LOAD = (0xC000026C);
        [Description("DFS is unavailable on the contacted server.")]
        public const uint DFS_UNAVAILABLE = (0xC000026D);
        [Description("An operation was attempted to a volume after it was dismounted.")]
        public const uint VOLUME_DISMOUNtED = (0xC000026E);
        [Description("An internal error occurred in the Win32 x86 emulation subsystem.")]
        public const uint WX86_INtERNAL_ERROR = (0xC000026F);
        [Description("Win32 x86 emulation subsystem Floating-point stack check.")]
        public const uint WX86_FLOAT_STACK_CHECK = (0xC0000270);
        [Description("The validation process needs to continue on to the next step.")]
        public const uint VALIDATE_CONtINUE = (0xC0000271);
        [Description("There was no match for the specified key in the index.")]
        public const uint NO_MATCH = (0xC0000272);
        [Description("There are no more matches for the current index enumeration.")]
        public const uint NO_MORE_MATCHES = (0xC0000273);
        [Description("The NtFS file or directory is not a reparse point.")]
        public const uint NOT_A_REPARSE_POINt = (0xC0000275);
        [Description("The Windows I/O reparse tag passed for the NtFS reparse point is invalid.")]
        public const uint IO_REPARSE_TAG_INVALID = (0xC0000276);
        [Description("The Windows I/O reparse tag does not match the one present in the NtFS reparse point.")]
        public const uint IO_REPARSE_TAG_MISMATCH = (0xC0000277);
        [Description("The user data passed for the NtFS reparse point is invalid.")]
        public const uint IO_REPARSE_DATA_INVALID = (0xC0000278);
        [Description("The layered file system driver for this IO tag did not handle it when needed.")]
        public const uint IO_REPARSE_TAG_NOT_HANDLED = (0xC0000279);
        [Description("The NtFS symbolic link could not be resolved even though the initial file name is valid.")]
        public const uint REPARSE_POINt_NOT_RESOLVED = (0xC0000280);
        [Description("The NtFS directory is a reparse point.")]
        public const uint DIRECTORY_IS_A_REPARSE_POINt = (0xC0000281);
        [Description("The range could not be added to the range list because of a conflict.")]
        public const uint RANGE_LIST_CONFLICT = (0xC0000282);
        [Description("The specified medium changer source element contains no media.")]
        public const uint SOURCE_ELEMENt_EMPTY = (0xC0000283);
        [Description("The specified medium changer destination element already contains media.")]
        public const uint DESTINATION_ELEMENt_FULL = (0xC0000284);
        [Description("The specified medium changer element does not exist.")]
        public const uint ILLEGAL_ELEMENt_ADDRESS = (0xC0000285);
        [Description("The specified element is contained within a magazine that is no longer present.")]
        public const uint MAGAZINE_NOT_PRESENt = (0xC0000286);
        [Description("The device requires reinitialization due to hardware errors.")]
        public const uint REINITIALIZATION_NEEDED = (0xC0000287);
        [Description("The device has indicated that cleaning is necessary.")]
        public const uint DEVICE_REQUIRES_CLEANING = (0x80000288);
        [Description("The device has indicated that it's door is open. Further operations require it closed and secured.")]
        public const uint DEVICE_DOOR_OPEN = (0x80000289);
        [Description("The file encryption attempt failed.")]
        public const uint ENCRYPTION_FAILED = (0xC000028A);
        [Description("The file decryption attempt failed.")]
        public const uint DECRYPTION_FAILED = (0xC000028B);
        [Description("The specified range could not be found in the range list.")]
        public const uint RANGE_NOT_FOUND = (0xC000028C);
        [Description("There is no encryption recovery policy configured for this system.")]
        public const uint NO_RECOVERY_POLICY = (0xC000028D);
        [Description("The required encryption driver is not loaded for this system.")]
        public const uint NO_EFS = (0xC000028E);
        [Description("The file was encrypted with a different encryption driver than is currently loaded.")]
        public const uint WRONG_EFS = (0xC000028F);
        [Description("There are no EFS keys defined for the user.")]
        public const uint NO_USER_KEYS = (0xC0000290);
        [Description("The specified file is not encrypted.")]
        public const uint FILE_NOT_ENCRYPTED = (0xC0000291);
        [Description("The specified file is not in the defined EFS export format.")]
        public const uint NOT_EXPORT_FORMAT = (0xC0000292);
        [Description("The specified file is encrypted and the user does not have the ability to decrypt it.")]
        public const uint FILE_ENCRYPTED = (0xC0000293);
        [Description("The system has awoken")]
        public const uint WAKE_SYSTEM = (0x40000294);
        [Description("The guid passed was not recognized as valid by a WMI data provider.")]
        public const uint WMI_GUID_NOT_FOUND = (0xC0000295);
        [Description("The instance name passed was not recognized as valid by a WMI data provider.")]
        public const uint WMI_INSTANCE_NOT_FOUND = (0xC0000296);
        [Description("The data item id passed was not recognized as valid by a WMI data provider.")]
        public const uint WMI_ITEMID_NOT_FOUND = (0xC0000297);
        [Description("The WMI request could not be completed and should be retried.")]
        public const uint WMI_TRY_AGAIN = (0xC0000298);
        [Description("The policy object is shared and can only be modified at the root")]
        public const uint SHARED_POLICY = (0xC0000299);
        [Description("The policy object does not exist when it should")]
        public const uint POLICY_OBJECT_NOT_FOUND = (0xC000029A);
        [Description("The requested policy information only lives in the Ds")]
        public const uint POLICY_ONLY_IN_DS = (0xC000029B);
        [Description("The volume must be upgraded to enable this feature")]
        public const uint VOLUME_NOT_UPGRADED = (0xC000029C);
        [Description("The remote storage service is not operational at this time.")]
        public const uint REMOTE_STORAGE_NOT_ACTIVE = (0xC000029D);
        [Description("The remote storage service encountered a media error.")]
        public const uint REMOTE_STORAGE_MEDIA_ERROR = (0xC000029E);
        [Description("The tracking (workstation) service is not running.")]
        public const uint NO_TRACKING_SERVICE = (0xC000029F);
        [Description("The server process is running under a SID different than that required by client.")]
        public const uint SERVER_SID_MISMATCH = (0xC00002A0);
        [Description("The specified directory service attribute or value does not exist.")]
        public const uint DS_NO_ATTRIBUTE_OR_VALUE = (0xC00002A1);
        [Description("The attribute syntax specified to the directory service is invalid.")]
        public const uint DS_INVALID_ATTRIBUTE_SYNtAX = (0xC00002A2);
        [Description("The attribute type specified to the directory service is not defined.")]
        public const uint DS_ATTRIBUTE_TYPE_UNDEFINED = (0xC00002A3);
        [Description("The specified directory service attribute or value already exists.")]
        public const uint DS_ATTRIBUTE_OR_VALUE_EXISTS = (0xC00002A4);
        [Description("The directory service is busy.")]
        public const uint DS_BUSY = (0xC00002A5);
        [Description("The directory service is not available.")]
        public const uint DS_UNAVAILABLE = (0xC00002A6);
        [Description("The directory service was unable to allocate a relative identifier.")]
        public const uint DS_NO_RIDS_ALLOCATED = (0xC00002A7);
        [Description("The directory service has exhausted the pool of relative identifiers.")]
        public const uint DS_NO_MORE_RIDS = (0xC00002A8);
        [Description("The requested operation could not be performed because the directory service is not the master for that type of operation.")]
        public const uint DS_INCORRECT_ROLE_OWNER = (0xC00002A9);
        [Description("The directory service was unable to initialize the subsystem that allocates relative identifiers.")]
        public const uint DS_RIDMGR_INIT_ERROR = (0xC00002AA);
        [Description("The requested operation did not satisfy one or more constraints associated with the class of the object.")]
        public const uint DS_OBJ_CLASS_VIOLATION = (0xC00002AB);
        [Description("The directory service can perform the requested operation only on a leaf object.")]
        public const uint DS_CANt_ON_NON_LEAF = (0xC00002AC);
        [Description("The directory service cannot perform the requested operation on the Relatively Defined Name (RDN) attribute of an object.")]
        public const uint DS_CANt_ON_RDN = (0xC00002AD);
        [Description("The directory service detected an attempt to modify the object class of an object.")]
        public const uint DS_CANt_MOD_OBJ_CLASS = (0xC00002AE);
        [Description("An error occurred while performing a cross domain move operation.")]
        public const uint DS_CROSS_DOM_MOVE_FAILED = (0xC00002AF);
        [Description("Unable to Contact the Global Catalog Server.")]
        public const uint DS_GC_NOT_AVAILABLE = (0xC00002B0);
        [Description("The requested operation requires a directory service; and none was available.")]
        public const uint DIRECTORY_SERVICE_REQUIRED = (0xC00002B1);
        [Description("The reparse attribute cannot be set as it is incompatible with an existing attribute.")]
        public const uint REPARSE_ATTRIBUTE_CONFLICT = (0xC00002B2);
        [Description("A group marked use for deny onlycan not be enabled.")]
        public const uint CANt_ENABLE_DENY_ONLY = (0xC00002B3);
        [Description("{EXCEPTION} Multiple floating point faults.")]
        public const uint FLOAT_MULTIPLE_FAULTS = (0xC00002B4);
        [Description("{EXCEPTION} Multiple floating point traps.")]
        public const uint FLOAT_MULTIPLE_TRAPS = (0xC00002B5);
        [Description("The device has been removed.")]
        public const uint DEVICE_REMOVED = (0xC00002B6);
        [Description("The volume change journal is being deleted.")]
        public const uint JOURNAL_DELETE_IN_PROGRESS = (0xC00002B7);
        [Description("The volume change journal is not active.")]
        public const uint JOURNAL_NOT_ACTIVE = (0xC00002B8);
        [Description("The requested interface is not supported.")]
        public const uint NOINtERFACE = (0xC00002B9);
        [Description("A directory service resource limit has been exceeded.")]
        public const uint DS_ADMIN_LIMIT_EXCEEDED = (0xC00002C1);
        [Description("{System Standby Failed} The driver %hs does not support standby mode. Updating this driver may allow the system to go to standby mode.")]
        public const uint DRIVER_FAILED_SLEEP = (0xC00002C2);
        [Description("Mutual Authentication failed. The server's password is out of date at the domain controller.")]
        public const uint MUTUAL_AUTHENtICATION_FAILED = (0xC00002C3);
        [Description("The system file %1 has become corrupt and has been replaced.")]
        public const uint CORRUPT_SYSTEM_FILE = (0xC00002C4);
        [Description("{EXCEPTION} Alignment ErrorA datatype misalignment error was detected in a load or store instruction.")]
        public const uint DATATYPE_MISALIGNMENt_ERROR = (0xC00002C5);
        [Description("The WMI data item or data block is read only.")]
        public const uint WMI_READ_ONLY = (0xC00002C6);
        [Description("The WMI data item or data block could not be changed.")]
        public const uint WMI_SET_FAILURE = (0xC00002C7);
        [Description("{Virtual Memory Minimum Too Low} Your system is low on virtual memory. Windows is increasing the size of your virtual memory paging file. During this process; memory requests for some applications may be denied. For more information; see Help.")]
        public const uint COMMITMENt_MINIMUM = (0xC00002C8);
        [Description("{EXCEPTION} Register NaT consumption faults. A NaT value is consumed on a non speculative instruction.")]
        public const uint REG_NAT_CONSUMPTION = (0xC00002C9);
        [Description("The medium changer's transport element contains media; which is causing the operation to fail.")]
        public const uint TRANSPORT_FULL = (0xC00002CA);
        [Description("Security Accounts Manager initialization failed because of the following error: Error Status: 0x%x. Please click OK to shutdown this system and reboot into Directory Services Restore Mode; check the event log for more detailed information. %hs")]
        public const uint DS_SAM_INIT_FAILURE = (0xC00002CB);
        [Description("This operation is supported only when you are connected to the server.")]
        public const uint ONLY_IF_CONNECTED = (0xC00002CC);
        [Description("Only an administrator can modify the membership list of an administrative group.")]
        public const uint DS_SENSITIVE_GROUP_VIOLATION = (0xC00002CD);
        [Description("A device was removed so enumeration must be restarted.")]
        public const uint PNP_RESTART_ENUMERATION = (0xC00002CE);
        [Description("The journal entry has been deleted from the journal.")]
        public const uint JOURNAL_ENtRY_DELETED = (0xC00002CF);
        [Description("Cannot change the primary group ID of a domain controller account.")]
        public const uint DS_CANt_MOD_PRIMARYGROUPID = (0xC00002D0);
        [Description("{Fatal System Error} The system image %s is not properly signed. The file has been replaced with the signed file. The system has been shut down.")]
        public const uint SYSTEM_IMAGE_BAD_SIGNATURE = (0xC00002D1);
        [Description("Device will not start without a reboot.")]
        public const uint PNP_REBOOT_REQUIRED = (0xC00002D2);
        [Description("Current device power state cannot support this request.")]
        public const uint POWER_STATE_INVALID = (0xC00002D3);
        [Description("The specified group type is invalid.")]
        public const uint DS_INVALID_GROUP_TYPE = (0xC00002D4);
        [Description("In mixed domain no nesting of global group if group is security enabled.")]
        public const uint DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN = (0xC00002D5);
        [Description("In mixed domain; cannot nest local groups with other local groups; if the group is security enabled.")]
        public const uint DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN = (0xC00002D6);
        [Description("A global group cannot have a local group as a member.")]
        public const uint DS_GLOBAL_CANt_HAVE_LOCAL_MEMBER = (0xC00002D7);
        [Description("A global group cannot have a universal group as a member.")]
        public const uint DS_GLOBAL_CANt_HAVE_UNIVERSAL_MEMBER = (0xC00002D8);
        [Description("A universal group cannot have a local group as a member.")]
        public const uint DS_UNIVERSAL_CANt_HAVE_LOCAL_MEMBER = (0xC00002D9);
        [Description("A global group cannot have a cross domain member.")]
        public const uint DS_GLOBAL_CANt_HAVE_CROSSDOMAIN_MEMBER = (0xC00002DA);
        [Description("A local group cannot have another cross domain local group as a member.")]
        public const uint DS_LOCAL_CANt_HAVE_CROSSDOMAIN_LOCAL_MEMBER = (0xC00002DB);
        [Description("Can not change to security disabled group because of having primary members in this group.")]
        public const uint DS_HAVE_PRIMARY_MEMBERS = (0xC00002DC);
        [Description("The WMI operation is not supported by the data block or method.")]
        public const uint WMI_NOT_SUPPORTED = (0xC00002DD);
        [Description("There is not enough power to complete the requested operation.")]
        public const uint INSUFFICIENt_POWER = (0xC00002DE);
        [Description("Security Account Manager needs to get the boot password.")]
        public const uint SAM_NEED_BOOTKEY_PASSWORD = (0xC00002DF);
        [Description("Security Account Manager needs to get the boot key from floppy disk.")]
        public const uint SAM_NEED_BOOTKEY_FLOPPY = (0xC00002E0);
        [Description("Directory Service can not start.")]
        public const uint DS_CANt_START = (0xC00002E1);
        [Description("Directory Services could not start because of the following error: Error Status: 0x%x. Please click OK to shutdown this system and reboot into Directory Services Restore Mode; check the event log for more detailed information. %hs")]
        public const uint DS_INIT_FAILURE = (0xC00002E2);
        [Description("Security Accounts Manager initialization failed because of the following error: Error Status: 0x%x. Please click OK to shutdown this system and reboot into Safe Mode; check the event log for more detailed information. %hs")]
        public const uint SAM_INIT_FAILURE = (0xC00002E3);
        [Description("The requested operation can be performed only on a global catalog server.")]
        public const uint DS_GC_REQUIRED = (0xC00002E4);
        [Description("A local group can only be a member of other local groups in the same domain.")]
        public const uint DS_LOCAL_MEMBER_OF_LOCAL_ONLY = (0xC00002E5);
        [Description("Foreign security principals cannot be members of universal groups.")]
        public const uint DS_NO_FPO_IN_UNIVERSAL_GROUPS = (0xC00002E6);
        [Description("Your computer could not be joined to the domain. You have exceeded the maximum number of computer accounts you are allowed to create in this domain. Contact your system administrator to have this limit reset or increased.")]
        public const uint DS_MACHINE_ACCOUNt_QUOTA_EXCEEDED = (0xC00002E7);
        [Description("MULTIPLE_FAULT_VIOLATION")]
        public const uint MULTIPLE_FAULT_VIOLATION = (0xC00002E8);
        [Description("This operation can not be performed on the current domain.")]
        public const uint CURRENt_DOMAIN_NOT_ALLOWED = (0xC00002E9);
        [Description("The directory or file cannot be created.")]
        public const uint CANNOT_MAKE = (0xC00002EA);
        [Description("The system is in the process of shutting down.")]
        public const uint SYSTEM_SHUTDOWN = (0xC00002EB);
        [Description("Directory Services could not start because of the following error: Error Status: 0x%x. Please click OK to shutdown the system. You can use the recovery console to diagnose the system further. %hs")]
        public const uint DS_INIT_FAILURE_CONSOLE = (0xC00002EC);
        [Description("Security Accounts Manager initialization failed because of the following error: Error Status: 0x%x. Please click OK to shutdown the system. You can use the recovery console to diagnose the system further. %hs")]
        public const uint DS_SAM_INIT_FAILURE_CONSOLE = (0xC00002ED);
        [Description("A security context was deleted before the context was completed.This is considered a logon failure.")]
        public const uint UNFINISHED_CONtEXT_DELETED = (0xC00002EE);
        [Description("The client is trying to negotiate a context and the server requires user-to-user but didn't send a TGT reply.")]
        public const uint NO_TGT_REPLY = (0xC00002EF);
        [Description("An object ID was not found in the file.")]
        public const uint OBJECTID_NOT_FOUND = (0xC00002F0);
        [Description("Unable to accomplish the requested task because the local machine does not have any IP addresses.")]
        public const uint NO_IP_ADDRESSES = (0xC00002F1);
        [Description("The supplied credential handle does not match the credential associated with the security context.")]
        public const uint WRONG_CREDENtIAL_HANDLE = (0xC00002F2);
        [Description("The crypto system or checksum function is invalid because a required function is unavailable.")]
        public const uint CRYPTO_SYSTEM_INVALID = (0xC00002F3);
        [Description("The number of maximum ticket referrals has been exceeded.")]
        public const uint MAX_REFERRALS_EXCEEDED = (0xC00002F4);
        [Description("The local machine must be a Kerberos KDC (domain controller) and it is not.")]
        public const uint MUST_BE_KDC = (0xC00002F5);
        [Description("The other end of the security negotiation is requires strong crypto but it is not supported on the local machine.")]
        public const uint STRONG_CRYPTO_NOT_SUPPORTED = (0xC00002F6);
        [Description("The KDC reply contained more than one principal name.")]
        public const uint TOO_MANY_PRINCIPALS = (0xC00002F7);
        [Description("Expected to find PA data for a hint of what etype to use; but it was not found.")]
        public const uint NO_PA_DATA = (0xC00002F8);
        [Description("The client cert name does not matches the user name or the KDC name is incorrect.")]
        public const uint PKINIT_NAME_MISMATCH = (0xC00002F9);
        [Description("Smartcard logon is required and was not used.")]
        public const uint SMARTCARD_LOGON_REQUIRED = (0xC00002FA);
        [Description("An invalid request was sent to the KDC.")]
        public const uint KDC_INVALID_REQUEST = (0xC00002FB);
        [Description("The KDC was unable to generate a referral for the service requested.")]
        public const uint KDC_UNABLE_TO_REFER = (0xC00002FC);
        [Description("The encryption type requested is not supported by the KDC.")]
        public const uint KDC_UNKNOWN_ETYPE = (0xC00002FD);
        [Description("A system shutdown is in progress.")]
        public const uint SHUTDOWN_IN_PROGRESS = (0xC00002FE);
        [Description("The server machine is shutting down.")]
        public const uint SERVER_SHUTDOWN_IN_PROGRESS = (0xC00002FF);
        [Description("This operation is not supported on a Microsoft Small Business Server")]
        public const uint NOT_SUPPORTED_ON_SBS = (0xC0000300);
        [Description("The WMI GUID is no longer available")]
        public const uint WMI_GUID_DISCONNECTED = (0xC0000301);
        [Description("Collection or events for the WMI GUID is already disabled.")]
        public const uint WMI_ALREADY_DISABLED = (0xC0000302);
        [Description("Collection or events for the WMI GUID is already enabled.")]
        public const uint WMI_ALREADY_ENABLED = (0xC0000303);
        [Description("The Master File Table on the volume is too fragmented to complete this operation.")]
        public const uint MFT_TOO_FRAGMENtED = (0xC0000304);
        [Description("Copy protection failure.")]
        public const uint COPY_PROTECTION_FAILURE = (0xC0000305);
        [Description("Copy protection error - DVD CSS Authentication failed.")]
        public const uint CSS_AUTHENtICATION_FAILURE = (0xC0000306);
        [Description("Copy protection error - The given sector does not contain a valid key.")]
        public const uint CSS_KEY_NOT_PRESENt = (0xC0000307);
        [Description("Copy protection error - DVD session key not established.")]
        public const uint CSS_KEY_NOT_ESTABLISHED = (0xC0000308);
        [Description("Copy protection error - The read failed because the sector is encrypted.")]
        public const uint CSS_SCRAMBLED_SECTOR = (0xC0000309);
        [Description("Copy protection error - The given DVD's region does not correspond to the region setting of the drive.")]
        public const uint CSS_REGION_MISMATCH = (0xC000030A);
        [Description("Copy protection error - The drive's region setting may be permanent.")]
        public const uint CSS_RESETS_EXHAUSTED = (0xC000030B);
        [Description("The kerberos protocol encountered an error while validating the KDC certificate during smartcard Logon")]
        public const uint PKINIT_FAILURE = (0xC0000320);
        [Description("The kerberos protocol encountered an error while attempting to utilize the smartcard subsystem.")]
        public const uint SMARTCARD_SUBSYSTEM_FAILURE = (0xC0000321);
        [Description("The target server does not have acceptable kerberos credentials.")]
        public const uint NO_KERB_KEY = (0xC0000322);
        [Description("The transport determined that the remote system is down.")]
        public const uint HOST_DOWN = (0xC0000350);
        [Description("An unsupported preauthentication mechanism was presented to the kerberos package.")]
        public const uint UNSUPPORTED_PREAUTH = (0xC0000351);
        [Description("The encryption algorithm used on the source file needs a bigger key buffer than the one used on the destination file.")]
        public const uint EFS_ALG_BLOB_TOO_BIG = (0xC0000352);
        [Description("An attempt to remove a processes DebugPort was made; but a port was not already associated with the process.")]
        public const uint PORT_NOT_SET = (0xC0000353);
        [Description("An attempt to do an operation on a debug port failed because the port is in the process of being deleted.")]
        public const uint DEBUGGER_INACTIVE = (0xC0000354);
        [Description("This version of Windows is not compatible with the behavior version of directory forest; domain or domain controller.")]
        public const uint DS_VERSION_CHECK_FAILURE = (0xC0000355);
        [Description("The specified event is currently not being audited.")]
        public const uint AUDITING_DISABLED = (0xC0000356);
        [Description("The machine account was created pre-Nt4.The account needs to be recreated.")]
        public const uint PRENt4_MACHINE_ACCOUNt = (0xC0000357);
        [Description("A account group can not have a universal group as a member.")]
        public const uint DS_AG_CANt_HAVE_UNIVERSAL_MEMBER = (0xC0000358);
        [Description("The specified image file did not have the correct format; it appears to be a 32-bit Windows image.")]
        public const uint INVALID_IMAGE_WIN_32 = (0xC0000359);
        [Description("The specified image file did not have the correct format; it appears to be a 64-bit Windows image.")]
        public const uint INVALID_IMAGE_WIN_64 = (0xC000035A);
        [Description("Client's supplied SSPI channel bindings were incorrect.")]
        public const uint BAD_BINDINGS = (0xC000035B);
        [Description("The client's session has expired; so the client must reauthenticate to continue accessing the remote resources.")]
        public const uint NETWORK_SESSION_EXPIRED = (0xC000035C);
        [Description("AppHelp dialog canceled thus preventing the application from starting.")]
        public const uint APPHELP_BLOCK = (0xC000035D);
        [Description("The SID filtering operation removed all SIDs.")]
        public const uint ALL_SIDS_FILTERED = (0xC000035E);
        [Description("The driver was not loaded because the system is booting into safe mode.")]
        public const uint NOT_SAFE_MODE_DRIVER = (0xC000035F);
        [Description("Access to %1 has been restricted by your Administrator by the default software restriction policy level.")]
        public const uint ACCESS_DISABLED_BY_POLICY_DEFAULT = (0xC0000361);
        [Description("Access to %1 has been restricted by your Administrator by location with policy rule %2 placed on path %3")]
        public const uint ACCESS_DISABLED_BY_POLICY_PATH = (0xC0000362);
        [Description("Access to %1 has been restricted by your Administrator by software publisher policy.")]
        public const uint ACCESS_DISABLED_BY_POLICY_PUBLISHER = (0xC0000363);
        [Description("Access to %1 has been restricted by your Administrator by policy rule %2.")]
        public const uint ACCESS_DISABLED_BY_POLICY_OTHER = (0xC0000364);
        [Description("The driver was not loaded because it failed it's initialization call.")]
        public const uint FAILED_DRIVER_ENtRY = (0xC0000365);
        [Description("The \"%hs\" encountered an error while applying power or reading the device configuration. This may be caused by a failure of your hardware or by a poor connection.")]
        public const uint DEVICE_ENUMERATION_ERROR = (0xC0000366);
        [Description("An operation is blocked waiting for an oplock.")]
        public const uint WAIT_FOR_OPLOCK = (0x00000367);
        [Description("The create operation failed because the name contained at least one mount point which resolves to a volume to which the specified device object is not attached.")]
        public const uint MOUNt_POINt_NOT_RESOLVED = (0xC0000368);
        [Description("The device object parameter is either not a valid device object or is not attached to the volume specified by the file name.")]
        public const uint INVALID_DEVICE_OBJECT_PARAMETER = (0xC0000369);
        [Description("A Machine Check Error has occured. Please check the system eventlog for additional information.")]
        public const uint MCA_OCCURED = (0xC000036A);
        [Description("Driver %2 has been blocked from loading.")]
        public const uint DRIVER_BLOCKED_CRITICAL = (0xC000036B);
        [Description("Driver %2 has been blocked from loading.")]
        public const uint DRIVER_BLOCKED = (0xC000036C);
        [Description("There was error [%2] processing the driver database.")]
        public const uint DRIVER_DATABASE_ERROR = (0xC000036D);
        [Description("System hive size has exceeded its limit.")]
        public const uint SYSTEM_HIVE_TOO_LARGE = (0xC000036E);
        [Description("A dynamic link library (DLL) referenced a module that was neither a DLL nor the process's executable image.")]
        public const uint INVALID_IMPORT_OF_NON_DLL = (0xC000036F);
        [Description("The Directory Service is shuting down.")]
        public const uint DS_SHUTTING_DOWN = (0x40000370);
        [Description("An incorrect PIN was presented to the smart card")]
        public const uint SMARTCARD_WRONG_PIN = (0xC0000380);
        [Description("The smart card is blocked")]
        public const uint SMARTCARD_CARD_BLOCKED = (0xC0000381);
        [Description("No PIN was presented to the smart card")]
        public const uint SMARTCARD_CARD_NOT_AUTHENtICATED = (0xC0000382);
        [Description("No smart card available")]
        public const uint SMARTCARD_NO_CARD = (0xC0000383);
        [Description("The requested key container does not exist on the smart card")]
        public const uint SMARTCARD_NO_KEY_CONtAINER = (0xC0000384);
        [Description("The requested certificate does not exist on the smart card")]
        public const uint SMARTCARD_NO_CERTIFICATE = (0xC0000385);
        [Description("The requested keyset does not exist")]
        public const uint SMARTCARD_NO_KEYSET = (0xC0000386);
        [Description("A communication error with the smart card has been detected.")]
        public const uint SMARTCARD_IO_ERROR = (0xC0000387);
        [Description("The system detected a possible attempt to compromise security. Please ensure that you can contact the server that authenticated you.")]
        public const uint DOWNGRADE_DETECTED = (0xC0000388);
        [Description("The smartcard certificate used for authentication has been revoked. Please contact your system administrator.There may be additional information in the event log.")]
        public const uint SMARTCARD_CERT_REVOKED = (0xC0000389);
        [Description("An untrusted certificate authority was detected While processing the smartcard certificate used for authentication.Please contact your systemadministrator.")]
        public const uint ISSUING_CA_UNtRUSTED = (0xC000038A);
        [Description("The revocation status of the smartcard certificate used for authentication could not be determined. Please contact your system administrator.")]
        public const uint REVOCATION_OFFLINE_C = (0xC000038B);
        [Description("The smartcard certificate used for authentication was not trusted.Please contact your system administrator.")]
        public const uint PKINIT_CLIENt_FAILURE = (0xC000038C);
        [Description("The smartcard certificate used for authentication has expired.Please contact your system administrator.")]
        public const uint SMARTCARD_CERT_EXPIRED = (0xC000038D);
        [Description("The driver could not be loaded because a previous version of the driver is still in memory.")]
        public const uint DRIVER_FAILED_PRIOR_UNLOAD = (0xC000038E);
        /* MessageId up to 0x400 is reserved for smart cards */
        /* This code is used by .Net server.reserved. */
        /* MessageId=0x408 Facility=System Severity=Error SymbolicName=USER2USER_REQUIRED */
        /* Language=English */
        /* Kerberos sub-protocol User2User is required. */
        /* . */
        [Description("The system detected an overrun of a stack-based buffer in this application.This overrun could potentially allow a malicious user to gain control of this application.")]
        public const uint STACK_BUFFER_OVERRUN = (0xC0000409);
        /* This code is used by .Net server. reserved. */
        /* MessageId=0x0415 Facility=System Severity=ERROR SymbolicName=HUNG_DISPLAY_DRIVER_THREAD */
        /* Language=English */
        /* {Display Driver Stopped Responding} */
        /* The %hs display driver has stopped working normally.	Save your work and reboot the system to restore full display functionality. */
        /* The next time you reboot the machine a dialog will be displayed giving you a chance to upload data about this failure to Microsoft. */
        /* . */
        [Description("WOW Assertion Error.")]
        public const uint WOW_ASSERTION = (0xC0009898);
        [Description("Debugger did not perform a state change.")]
        public const uint DBG_NO_STATE_CHANGE = (0xC0010001);
        [Description("Debugger has found the application is not idle.")]
        public const uint DBG_APP_NOT_IDLE = (0xC0010002);
        [Description("The string binding is invalid.")]
        public const uint RPC_Nt_INVALID_STRING_BINDING = (0xC0020001);
        [Description("The binding handle is not the correct type.")]
        public const uint RPC_Nt_WRONG_KIND_OF_BINDING = (0xC0020002);
        [Description("The binding handle is invalid.")]
        public const uint RPC_Nt_INVALID_BINDING = (0xC0020003);
        [Description("The RPC protocol sequence is not supported.")]
        public const uint RPC_Nt_PROTSEQ_NOT_SUPPORTED = (0xC0020004);
        [Description("The RPC protocol sequence is invalid.")]
        public const uint RPC_Nt_INVALID_RPC_PROTSEQ = (0xC0020005);
        [Description("The string UUID is invalid.")]
        public const uint RPC_Nt_INVALID_STRING_UUID = (0xC0020006);
        [Description("The endpoint format is invalid.")]
        public const uint RPC_Nt_INVALID_ENDPOINt_FORMAT = (0xC0020007);
        [Description("The network address is invalid.")]
        public const uint RPC_Nt_INVALID_NET_ADDR = (0xC0020008);
        [Description("No endpoint was found.")]
        public const uint RPC_Nt_NO_ENDPOINt_FOUND = (0xC0020009);
        [Description("The timeout value is invalid.")]
        public const uint RPC_Nt_INVALID_TIMEOUT = (0xC002000A);
        [Description("The object UUID was not found.")]
        public const uint RPC_Nt_OBJECT_NOT_FOUND = (0xC002000B);
        [Description("The object UUID has already been registered.")]
        public const uint RPC_Nt_ALREADY_REGISTERED = (0xC002000C);
        [Description("The type UUID has already been registered.")]
        public const uint RPC_Nt_TYPE_ALREADY_REGISTERED = (0xC002000D);
        [Description("The RPC server is already listening.")]
        public const uint RPC_Nt_ALREADY_LISTENING = (0xC002000E);
        [Description("No protocol sequences have been registered.")]
        public const uint RPC_Nt_NO_PROTSEQS_REGISTERED = (0xC002000F);
        [Description("The RPC server is not listening.")]
        public const uint RPC_Nt_NOT_LISTENING = (0xC0020010);
        [Description("The manager type is unknown.")]
        public const uint RPC_Nt_UNKNOWN_MGR_TYPE = (0xC0020011);
        [Description("The interface is unknown.")]
        public const uint RPC_Nt_UNKNOWN_IF = (0xC0020012);
        [Description("There are no bindings.")]
        public const uint RPC_Nt_NO_BINDINGS = (0xC0020013);
        [Description("There are no protocol sequences.")]
        public const uint RPC_Nt_NO_PROTSEQS = (0xC0020014);
        [Description("The endpoint cannot be created.")]
        public const uint RPC_Nt_CANt_CREATE_ENDPOINt = (0xC0020015);
        [Description("Not enough resources are available to complete this operation.")]
        public const uint RPC_Nt_OUT_OF_RESOURCES = (0xC0020016);
        [Description("The RPC server is unavailable.")]
        public const uint RPC_Nt_SERVER_UNAVAILABLE = (0xC0020017);
        [Description("The RPC server is too busy to complete this operation.")]
        public const uint RPC_Nt_SERVER_TOO_BUSY = (0xC0020018);
        [Description("The network options are invalid.")]
        public const uint RPC_Nt_INVALID_NETWORK_OPTIONS = (0xC0020019);
        [Description("There are no remote procedure calls active on this thread.")]
        public const uint RPC_Nt_NO_CALL_ACTIVE = (0xC002001A);
        [Description("The remote procedure call failed.")]
        public const uint RPC_Nt_CALL_FAILED = (0xC002001B);
        [Description("The remote procedure call failed and did not execute.")]
        public const uint RPC_Nt_CALL_FAILED_DNE = (0xC002001C);
        [Description("An RPC protocol error occurred.")]
        public const uint RPC_Nt_PROTOCOL_ERROR = (0xC002001D);
        [Description("The transfer syntax is not supported by the RPC server.")]
        public const uint RPC_Nt_UNSUPPORTED_TRANS_SYN = (0xC002001F);
        [Description("The type UUID is not supported.")]
        public const uint RPC_Nt_UNSUPPORTED_TYPE = (0xC0020021);
        [Description("The tag is invalid.")]
        public const uint RPC_Nt_INVALID_TAG = (0xC0020022);
        [Description("The array bounds are invalid.")]
        public const uint RPC_Nt_INVALID_BOUND = (0xC0020023);
        [Description("The binding does not contain an entry name.")]
        public const uint RPC_Nt_NO_ENtRY_NAME = (0xC0020024);
        [Description("The name syntax is invalid.")]
        public const uint RPC_Nt_INVALID_NAME_SYNtAX = (0xC0020025);
        [Description("The name syntax is not supported.")]
        public const uint RPC_Nt_UNSUPPORTED_NAME_SYNtAX = (0xC0020026);
        [Description("No network address is available to use to construct a UUID.")]
        public const uint RPC_Nt_UUID_NO_ADDRESS = (0xC0020028);
        [Description("The endpoint is a duplicate.")]
        public const uint RPC_Nt_DUPLICATE_ENDPOINt = (0xC0020029);
        [Description("The authentication type is unknown.")]
        public const uint RPC_Nt_UNKNOWN_AUTHN_TYPE = (0xC002002A);
        [Description("The maximum number of calls is too small.")]
        public const uint RPC_Nt_MAX_CALLS_TOO_SMALL = (0xC002002B);
        [Description("The string is too long.")]
        public const uint RPC_Nt_STRING_TOO_LONG = (0xC002002C);
        [Description("The RPC protocol sequence was not found.")]
        public const uint RPC_Nt_PROTSEQ_NOT_FOUND = (0xC002002D);
        [Description("The procedure number is out of range.")]
        public const uint RPC_Nt_PROCNUM_OUT_OF_RANGE = (0xC002002E);
        [Description("The binding does not contain any authentication information.")]
        public const uint RPC_Nt_BINDING_HAS_NO_AUTH = (0xC002002F);
        [Description("The authentication service is unknown.")]
        public const uint RPC_Nt_UNKNOWN_AUTHN_SERVICE = (0xC0020030);
        [Description("The authentication level is unknown.")]
        public const uint RPC_Nt_UNKNOWN_AUTHN_LEVEL = (0xC0020031);
        [Description("The security context is invalid.")]
        public const uint RPC_Nt_INVALID_AUTH_IDENtITY = (0xC0020032);
        [Description("The authorization service is unknown.")]
        public const uint RPC_Nt_UNKNOWN_AUTHZ_SERVICE = (0xC0020033);
        [Description("The entry is invalid.")]
        public const uint EPT_Nt_INVALID_ENtRY = (0xC0020034);
        [Description("The operation cannot be performed.")]
        public const uint EPT_Nt_CANt_PERFORM_OP = (0xC0020035);
        [Description("There are no more endpoints available from the endpoint mapper.")]
        public const uint EPT_Nt_NOT_REGISTERED = (0xC0020036);
        [Description("No interfaces have been exported.")]
        public const uint RPC_Nt_NOTHING_TO_EXPORT = (0xC0020037);
        [Description("The entry name is incomplete.")]
        public const uint RPC_Nt_INCOMPLETE_NAME = (0xC0020038);
        [Description("The version option is invalid.")]
        public const uint RPC_Nt_INVALID_VERS_OPTION = (0xC0020039);
        [Description("There are no more members.")]
        public const uint RPC_Nt_NO_MORE_MEMBERS = (0xC002003A);
        [Description("There is nothing to unexport.")]
        public const uint RPC_Nt_NOT_ALL_OBJS_UNEXPORTED = (0xC002003B);
        [Description("The interface was not found.")]
        public const uint RPC_Nt_INtERFACE_NOT_FOUND = (0xC002003C);
        [Description("The entry already exists.")]
        public const uint RPC_Nt_ENtRY_ALREADY_EXISTS = (0xC002003D);
        [Description("The entry is not found.")]
        public const uint RPC_Nt_ENtRY_NOT_FOUND = (0xC002003E);
        [Description("The name service is unavailable.")]
        public const uint RPC_Nt_NAME_SERVICE_UNAVAILABLE = (0xC002003F);
        [Description("The network address family is invalid.")]
        public const uint RPC_Nt_INVALID_NAF_ID = (0xC0020040);
        [Description("The requested operation is not supported.")]
        public const uint RPC_Nt_CANNOT_SUPPORT = (0xC0020041);
        [Description("No security context is available to allow impersonation.")]
        public const uint RPC_Nt_NO_CONtEXT_AVAILABLE = (0xC0020042);
        [Description("An internal error occurred in RPC.")]
        public const uint RPC_Nt_INtERNAL_ERROR = (0xC0020043);
        [Description("The RPC server attempted an integer divide by zero.")]
        public const uint RPC_Nt_ZERO_DIVIDE = (0xC0020044);
        [Description("An addressing error occurred in the RPC server.")]
        public const uint RPC_Nt_ADDRESS_ERROR = (0xC0020045);
        [Description("A floating point operation at the RPC server caused a divide by zero.")]
        public const uint RPC_Nt_FP_DIV_ZERO = (0xC0020046);
        [Description("A floating point underflow occurred at the RPC server.")]
        public const uint RPC_Nt_FP_UNDERFLOW = (0xC0020047);
        [Description("A floating point overflow occurred at the RPC server.")]
        public const uint RPC_Nt_FP_OVERFLOW = (0xC0020048);
        [Description("The list of RPC servers available for auto-handle binding has been exhausted.")]
        public const uint RPC_Nt_NO_MORE_ENtRIES = (0xC0030001);
        [Description("The file designated by DCERPCCHARTRANS cannot be opened.")]
        public const uint RPC_Nt_SS_CHAR_TRANS_OPEN_FAIL = (0xC0030002);
        [Description("The file containing the character translation table has fewer than 512 bytes.")]
        public const uint RPC_Nt_SS_CHAR_TRANS_SHORT_FILE = (0xC0030003);
        [Description("A null context handle is passed as an [in] parameter.")]
        public const uint RPC_Nt_SS_IN_NULL_CONtEXT = (0xC0030004);
        [Description("The context handle does not match any known context handles.")]
        public const uint RPC_Nt_SS_CONtEXT_MISMATCH = (0xC0030005);
        [Description("The context handle changed during a call.")]
        public const uint RPC_Nt_SS_CONtEXT_DAMAGED = (0xC0030006);
        [Description("The binding handles passed to a remote procedure call do not match.")]
        public const uint RPC_Nt_SS_HANDLES_MISMATCH = (0xC0030007);
        [Description("The stub is unable to get the call handle.")]
        public const uint RPC_Nt_SS_CANNOT_GET_CALL_HANDLE = (0xC0030008);
        [Description("A null reference pointer was passed to the stub.")]
        public const uint RPC_Nt_NULL_REF_POINtER = (0xC0030009);
        [Description("The enumeration value is out of range.")]
        public const uint RPC_Nt_ENUM_VALUE_OUT_OF_RANGE = (0xC003000A);
        [Description("The byte count is too small.")]
        public const uint RPC_Nt_BYTE_COUNt_TOO_SMALL = (0xC003000B);
        [Description("The stub received bad data.")]
        public const uint RPC_Nt_BAD_STUB_DATA = (0xC003000C);
        [Description("A remote procedure call is already in progress for this thread.")]
        public const uint RPC_Nt_CALL_IN_PROGRESS = (0xC0020049);
        [Description("There are no more bindings.")]
        public const uint RPC_Nt_NO_MORE_BINDINGS = (0xC002004A);
        [Description("The group member was not found.")]
        public const uint RPC_Nt_GROUP_MEMBER_NOT_FOUND = (0xC002004B);
        [Description("The endpoint mapper database entry could not be created.")]
        public const uint EPT_Nt_CANt_CREATE = (0xC002004C);
        [Description("The object UUID is the nil UUID.")]
        public const uint RPC_Nt_INVALID_OBJECT = (0xC002004D);
        [Description("No interfaces have been registered.")]
        public const uint RPC_Nt_NO_INtERFACES = (0xC002004F);
        [Description("The remote procedure call was cancelled.")]
        public const uint RPC_Nt_CALL_CANCELLED = (0xC0020050);
        [Description("The binding handle does not contain all required information.")]
        public const uint RPC_Nt_BINDING_INCOMPLETE = (0xC0020051);
        [Description("A communications failure occurred during a remote procedure call.")]
        public const uint RPC_Nt_COMM_FAILURE = (0xC0020052);
        [Description("The requested authentication level is not supported.")]
        public const uint RPC_Nt_UNSUPPORTED_AUTHN_LEVEL = (0xC0020053);
        [Description("No principal name registered.")]
        public const uint RPC_Nt_NO_PRINC_NAME = (0xC0020054);
        [Description("The error specified is not a valid Windows RPC error code.")]
        public const uint RPC_Nt_NOT_RPC_ERROR = (0xC0020055);
        [Description("A UUID that is valid only on this computer has been allocated.")]
        public const uint RPC_Nt_UUID_LOCAL_ONLY = (0x40020056);
        [Description("A security package specific error occurred.")]
        public const uint RPC_Nt_SEC_PKG_ERROR = (0xC0020057);
        [Description("Thread is not cancelled.")]
        public const uint RPC_Nt_NOT_CANCELLED = (0xC0020058);
        [Description("Invalid operation on the encoding/decoding handle.")]
        public const uint RPC_Nt_INVALID_ES_ACTION = (0xC0030059);
        [Description("Incompatible version of the serializing package.")]
        public const uint RPC_Nt_WRONG_ES_VERSION = (0xC003005A);
        [Description("Incompatible version of the RPC stub.")]
        public const uint RPC_Nt_WRONG_STUB_VERSION = (0xC003005B);
        [Description("The RPC pipe object is invalid or corrupted.")]
        public const uint RPC_Nt_INVALID_PIPE_OBJECT = (0xC003005C);
        [Description("An invalid operation was attempted on an RPC pipe object.")]
        public const uint RPC_Nt_INVALID_PIPE_OPERATION = (0xC003005D);
        [Description("Unsupported RPC pipe version.")]
        public const uint RPC_Nt_WRONG_PIPE_VERSION = (0xC003005E);
        [Description("The RPC pipe object has already been closed.")]
        public const uint RPC_Nt_PIPE_CLOSED = (0xC003005F);
        [Description("The RPC call completed before all pipes were processed.")]
        public const uint RPC_Nt_PIPE_DISCIPLINE_ERROR = (0xC0030060);
        [Description("No more data is available from the RPC pipe.")]
        public const uint RPC_Nt_PIPE_EMPTY = (0xC0030061);
        [Description("Invalid asynchronous remote procedure call handle.")]
        public const uint RPC_Nt_INVALID_ASYNC_HANDLE = (0xC0020062);
        [Description("Invalid asynchronous RPC call handle for this operation.")]
        public const uint RPC_Nt_INVALID_ASYNC_CALL = (0xC0020063);
        [Description("Some data remains to be sent in the request buffer.")]
        public const uint RPC_Nt_SEND_INCOMPLETE = (0x400200AF);
        [Description("An attempt was made to run an invalid AML opcode")]
        public const uint ACPI_INVALID_OPCODE = (0xC0140001);
        [Description("The AML Interpreter Stack has overflowed")]
        public const uint ACPI_STACK_OVERFLOW = (0xC0140002);
        [Description("An inconsistent state has occurred")]
        public const uint ACPI_ASSERT_FAILED = (0xC0140003);
        [Description("An attempt was made to access an array outside of its bounds")]
        public const uint ACPI_INVALID_INDEX = (0xC0140004);
        [Description("A required argument was not specified")]
        public const uint ACPI_INVALID_ARGUMENt = (0xC0140005);
        [Description("A fatal error has occurred")]
        public const uint ACPI_FATAL = (0xC0140006);
        [Description("An invalid SuperName was specified")]
        public const uint ACPI_INVALID_SUPERNAME = (0xC0140007);
        [Description("An argument with an incorrect type was specified")]
        public const uint ACPI_INVALID_ARGTYPE = (0xC0140008);
        [Description("An object with an incorrect type was specified")]
        public const uint ACPI_INVALID_OBJTYPE = (0xC0140009);
        [Description("A target with an incorrect type was specified")]
        public const uint ACPI_INVALID_TARGETTYPE = (0xC014000A);
        [Description("An incorrect number of arguments were specified")]
        public const uint ACPI_INCORRECT_ARGUMENt_COUNt = (0xC014000B);
        [Description("An address failed to translate")]
        public const uint ACPI_ADDRESS_NOT_MAPPED = (0xC014000C);
        [Description("An incorrect event type was specified")]
        public const uint ACPI_INVALID_EVENtTYPE = (0xC014000D);
        [Description("A handler for the target already exists")]
        public const uint ACPI_HANDLER_COLLISION = (0xC014000E);
        [Description("Invalid data for the target was specified")]
        public const uint ACPI_INVALID_DATA = (0xC014000F);
        [Description("An invalid region for the target was specified")]
        public const uint ACPI_INVALID_REGION = (0xC0140010);
        [Description("An attempt was made to access a field outside of the defined range")]
        public const uint ACPI_INVALID_ACCESS_SIZE = (0xC0140011);
        [Description("The Global system lock could not be acquired")]
        public const uint ACPI_ACQUIRE_GLOBAL_LOCK = (0xC0140012);
        [Description("An attempt was made to reinitialize the ACPI subsystem")]
        public const uint ACPI_ALREADY_INITIALIZED = (0xC0140013);
        [Description("The ACPI subsystem has not been initialized")]
        public const uint ACPI_NOT_INITIALIZED = (0xC0140014);
        [Description("An incorrect mutex was specified")]
        public const uint ACPI_INVALID_MUTEX_LEVEL = (0xC0140015);
        [Description("The mutex is not currently owned")]
        public const uint ACPI_MUTEX_NOT_OWNED = (0xC0140016);
        [Description("An attempt was made to access the mutex by a process that was not the owner")]
        public const uint ACPI_MUTEX_NOT_OWNER = (0xC0140017);
        [Description("An error occurred during an access to Region Space")]
        public const uint ACPI_RS_ACCESS = (0xC0140018);
        [Description("An attempt was made to use an incorrect table")]
        public const uint ACPI_INVALID_TABLE = (0xC0140019);
        [Description("The registration of an ACPI event failed")]
        public const uint ACPI_REG_HANDLER_FAILED = (0xC0140020);
        [Description("An ACPI Power Object failed to transition state")]
        public const uint ACPI_POWER_REQUEST_FAILED = (0xC0140021);
        [Description("Session name %1 is invalid.")]
        public const uint CTX_WINSTATION_NAME_INVALID = (0xC00A0001);
        [Description("The protocol driver %1 is invalid.")]
        public const uint CTX_INVALID_PD = (0xC00A0002);
        [Description("The protocol driver %1 was not found in the system path.")]
        public const uint CTX_PD_NOT_FOUND = (0xC00A0003);
        [Description("The Client Drive Mapping Service Has Connected on Terminal Connection.")]
        public const uint CTX_CDM_CONNECT = (0x400A0004);
        [Description("The Client Drive Mapping Service Has Disconnected on Terminal Connection.")]
        public const uint CTX_CDM_DISCONNECT = (0x400A0005);
        [Description("A close operation is pending on the Terminal Connection.")]
        public const uint CTX_CLOSE_PENDING = (0xC00A0006);
        [Description("There are no free output buffers available.")]
        public const uint CTX_NO_OUTBUF = (0xC00A0007);
        [Description("The MODEM.INF file was not found.")]
        public const uint CTX_MODEM_INF_NOT_FOUND = (0xC00A0008);
        [Description("The modem (%1) was not found in MODEM.INF.")]
        public const uint CTX_INVALID_MODEMNAME = (0xC00A0009);
        [Description("The modem did not accept the command sent to it. Verify the configured modem name matches the attached modem.")]
        public const uint CTX_RESPONSE_ERROR = (0xC00A000A);
        [Description("The modem did not respond to the command sent to it. Verify the modem is properly cabled and powered on.")]
        public const uint CTX_MODEM_RESPONSE_TIMEOUT = (0xC00A000B);
        [Description("Carrier detect has failed or carrier has been dropped due to disconnect.")]
        public const uint CTX_MODEM_RESPONSE_NO_CARRIER = (0xC00A000C);
        [Description("Dial tone not detected within required time. Verify phone cable is properly attached and functional.")]
        public const uint CTX_MODEM_RESPONSE_NO_DIALTONE = (0xC00A000D);
        [Description("Busy signal detected at remote site on callback.")]
        public const uint CTX_MODEM_RESPONSE_BUSY = (0xC00A000E);
        [Description("Voice detected at remote site on callback.")]
        public const uint CTX_MODEM_RESPONSE_VOICE = (0xC00A000F);
        [Description("Transport driver error")]
        public const uint CTX_TD_ERROR = (0xC00A0010);
        [Description("The client you are using is not licensed to use this system. Your logon request is denied.")]
        public const uint CTX_LICENSE_CLIENt_INVALID = (0xC00A0012);
        [Description("The system has reached its licensed logon limit. Please try again later.")]
        public const uint CTX_LICENSE_NOT_AVAILABLE = (0xC00A0013);
        [Description("The system license has expired. Your logon request is denied.")]
        public const uint CTX_LICENSE_EXPIRED = (0xC00A0014);
        [Description("The specified session cannot be found.")]
        public const uint CTX_WINSTATION_NOT_FOUND = (0xC00A0015);
        [Description("The specified session name is already in use.")]
        public const uint CTX_WINSTATION_NAME_COLLISION = (0xC00A0016);
        [Description("The requested operation cannot be completed because the Terminal Connection is currently busy processing a connect; disconnect; reset; or delete operation.")]
        public const uint CTX_WINSTATION_BUSY = (0xC00A0017);
        [Description("An attempt has been made to connect to a session whose video mode is not supported by the current client.")]
        public const uint CTX_BAD_VIDEO_MODE = (0xC00A0018);
        [Description("The application attempted to enable DOS graphics mode. DOS graphics mode is not supported.")]
        public const uint CTX_GRAPHICS_INVALID = (0xC00A0022);
        [Description("The requested operation can be performed only on the system console. This is most often the result of a driver or system DLL requiring direct console access.")]
        public const uint CTX_NOT_CONSOLE = (0xC00A0024);
        [Description("The client failed to respond to the server connect message.")]
        public const uint CTX_CLIENt_QUERY_TIMEOUT = (0xC00A0026);
        [Description("Disconnecting the console session is not supported.")]
        public const uint CTX_CONSOLE_DISCONNECT = (0xC00A0027);
        [Description("Reconnecting a disconnected session to the console is not supported.")]
        public const uint CTX_CONSOLE_CONNECT = (0xC00A0028);
        [Description("The request to control another session remotely was denied.")]
        public const uint CTX_SHADOW_DENIED = (0xC00A002A);
        [Description("A process has requested access to a session; but has not been granted those access rights.")]
        public const uint CTX_WINSTATION_ACCESS_DENIED = (0xC00A002B);
        [Description("The Terminal Connection driver %1 is invalid.")]
        public const uint CTX_INVALID_WD = (0xC00A002E);
        [Description("The Terminal Connection driver %1 was not found in the system path.")]
        public const uint CTX_WD_NOT_FOUND = (0xC00A002F);
        [Description("The requested session cannot be controlled remotely. You cannot control your own session; a session that is trying to control your session;a session that has no user logged on; nor control other sessions from the console.")]
        public const uint CTX_SHADOW_INVALID = (0xC00A0030);
        [Description("The requested session is not configured to allow remote control.")]
        public const uint CTX_SHADOW_DISABLED = (0xC00A0031);
        [Description("The RDP protocol component %2 detected an error in the protocol stream and has disconnected the client.")]
        public const uint RDP_PROTOCOL_ERROR = (0xC00A0032);
        [Description("Your request to connect to this Terminal server has been rejected. Your Terminal Server Client license number has not been entered for this copy of the Terminal Client.Please call your system administrator for help in entering a valid; unique license number for this Terminal Server Client.Click OK to continue.")]
        public const uint CTX_CLIENt_LICENSE_NOT_SET = (0xC00A0033);
        [Description("Your request to connect to this Terminal server has been rejected. Your Terminal Server Client license number is currently being used by another user.Please call your system administrator to obtain a new copy of the Terminal Server Client with a valid; unique license number.Click OK to continue.")]
        public const uint CTX_CLIENt_LICENSE_IN_USE = (0xC00A0034);
        [Description("The remote control of the console was terminated because the display mode was changed. Changing the display mode in a remote control session is not supported.")]
        public const uint CTX_SHADOW_ENDED_BY_MODE_CHANGE = (0xC00A0035);
        [Description("Remote control could not be terminated because the specified session is not currently being remotely controlled.")]
        public const uint CTX_SHADOW_NOT_RUNNING = (0xC00A0036);
        [Description("A device is missing in the system BIOS MPS table. This device will not be used. Please contact your system vendor for system BIOS update.")]
        public const uint PNP_BAD_MPS_TABLE = (0xC0040035);
        [Description("A translator failed to translate resources.")]
        public const uint PNP_TRANSLATION_FAILED = (0xC0040036);
        [Description("A IRQ translator failed to translate resources.")]
        public const uint PNP_IRQ_TRANSLATION_FAILED = (0xC0040037);
        [Description("The requested section is not present in the activation context.")]
        public const uint SXS_SECTION_NOT_FOUND = (0xC0150001);
        [Description("Windows was not able to process the application binding information. Please refer to your System Event Log for further information.")]
        public const uint SXS_CANt_GEN_ACTCTX = (0xC0150002);
        [Description("The application binding data format is invalid.")]
        public const uint SXS_INVALID_ACTCTXDATA_FORMAT = (0xC0150003);
        [Description("The referenced assembly is not installed on your system.")]
        public const uint SXS_ASSEMBLY_NOT_FOUND = (0xC0150004);
        [Description("The manifest file does not begin with the required tag and format information.")]
        public const uint SXS_MANIFEST_FORMAT_ERROR = (0xC0150005);
        [Description("The manifest file contains one or more syntax errors.")]
        public const uint SXS_MANIFEST_PARSE_ERROR = (0xC0150006);
        [Description("The application attempted to activate a disabled activation context.")]
        public const uint SXS_ACTIVATION_CONtEXT_DISABLED = (0xC0150007);
        [Description("The requested lookup key was not found in any active activation context.")]
        public const uint SXS_KEY_NOT_FOUND = (0xC0150008);
        [Description("A component version required by the application conflicts with another component version already active.")]
        public const uint SXS_VERSION_CONFLICT = (0xC0150009);
        [Description("The type requested activation context section does not match the query API used.")]
        public const uint SXS_WRONG_SECTION_TYPE = (0xC015000A);
        [Description("Lack of system resources has required isolated activation to be disabled for the current thread of execution.")]
        public const uint SXS_THREAD_QUERIES_DISABLED = (0xC015000B);
        [Description("The referenced assembly could not be found.")]
        public const uint SXS_ASSEMBLY_MISSING = (0xC015000C);
        [Description("A kernel mode component is releasing a reference on an activation context.")]
        public const uint SXS_RELEASE_ACTIVATION_CONtEXT = (0x4015000D);
        [Description("An attempt to set the process default activation context failed because the process default activation context was already set.")]
        public const uint SXS_PROCESS_DEFAULT_ALREADY_SET = (0xC015000E);
        [Description("The activation context being deactivated is not the most recently activated one.")]
        public const uint SXS_EARLY_DEACTIVATION = (0xC015000F);
        [Description("The activation context being deactivated is not active for the current thread of execution.")]
        public const uint SXS_INVALID_DEACTIVATION = (0xC0150010);
        [Description("The activation context being deactivated has already been deactivated.")]
        public const uint SXS_MULTIPLE_DEACTIVATION = (0xC0150011);
        [Description("The activation context of system default assembly could not be generated.")]
        public const uint SXS_SYSTEM_DEFAULT_ACTIVATION_CONtEXT_EMPTY = (0xC0150012);
        [Description("A component used by the isolation facility has requested to terminate the process.")]
        public const uint SXS_PROCESS_TERMINATION_REQUESTED = (0xC0150013);
        [Description("The cluster node is not valid.")]
        public const uint CLUSTER_INVALID_NODE = (0xC0130001);
        [Description("The cluster node already exists.")]
        public const uint CLUSTER_NODE_EXISTS = (0xC0130002);
        [Description("A node is in the process of joining the cluster.")]
        public const uint CLUSTER_JOIN_IN_PROGRESS = (0xC0130003);
        [Description("The cluster node was not found.")]
        public const uint CLUSTER_NODE_NOT_FOUND = (0xC0130004);
        [Description("The cluster local node information was not found.")]
        public const uint CLUSTER_LOCAL_NODE_NOT_FOUND = (0xC0130005);
        [Description("The cluster network already exists.")]
        public const uint CLUSTER_NETWORK_EXISTS = (0xC0130006);
        [Description("The cluster network was not found.")]
        public const uint CLUSTER_NETWORK_NOT_FOUND = (0xC0130007);
        [Description("The cluster network interface already exists.")]
        public const uint CLUSTER_NETINtERFACE_EXISTS = (0xC0130008);
        [Description("The cluster network interface was not found.")]
        public const uint CLUSTER_NETINtERFACE_NOT_FOUND = (0xC0130009);
        [Description("The cluster request is not valid for this object.")]
        public const uint CLUSTER_INVALID_REQUEST = (0xC013000A);
        [Description("The cluster network provider is not valid.")]
        public const uint CLUSTER_INVALID_NETWORK_PROVIDER = (0xC013000B);
        [Description("The cluster node is down.")]
        public const uint CLUSTER_NODE_DOWN = (0xC013000C);
        [Description("The cluster node is not reachable.")]
        public const uint CLUSTER_NODE_UNREACHABLE = (0xC013000D);
        [Description("The cluster node is not a member of the cluster.")]
        public const uint CLUSTER_NODE_NOT_MEMBER = (0xC013000E);
        [Description("A cluster join operation is not in progress.")]
        public const uint CLUSTER_JOIN_NOT_IN_PROGRESS = (0xC013000F);
        [Description("The cluster network is not valid.")]
        public const uint CLUSTER_INVALID_NETWORK = (0xC0130010);
        [Description("No network adapters are available.")]
        public const uint CLUSTER_NO_NET_ADAPTERS = (0xC0130011);
        [Description("The cluster node is up.")]
        public const uint CLUSTER_NODE_UP = (0xC0130012);
        [Description("The cluster node is paused.")]
        public const uint CLUSTER_NODE_PAUSED = (0xC0130013);
        [Description("The cluster node is not paused.")]
        public const uint CLUSTER_NODE_NOT_PAUSED = (0xC0130014);
        [Description("No cluster security context is available.")]
        public const uint CLUSTER_NO_SECURITY_CONtEXT = (0xC0130015);
        [Description("The cluster network is not configured for internal cluster communication.")]
        public const uint CLUSTER_NETWORK_NOT_INtERNAL = (0xC0130016);
        [Description("The cluster node has been poisoned.")]
        public const uint CLUSTER_POISONED = (0xC0130017);

        private uint m_Value;
    }
}
