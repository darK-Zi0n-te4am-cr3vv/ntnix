//#define TEST_FRN
using System.Collections.Generic;
using System.WindowsNT.Devices.IO;
using System.WindowsNT.Devices.IO.FileTables1;
using System.WindowsNT.Directories;
using System.WindowsNT.Events;
using System.WindowsNT.IO;
using System.WindowsNT.Processes;
using System.WindowsNT.Registry;
using Algorithms;
using Microsoft.WinNT.SafeHandles;
namespace System.WindowsNT.Tester
{
	/// <summary>DO NOT USE THIS CLASS!!</summary>
	internal static class Tester
	{
		public static void Main(string[] args)
		{
			//NtProcess.Current.AdjustPrivilege(Security.Privilege.ChangeNotify, true);
			//TestEvent();
			//TestRestore();
			//TestLoadHive();
			//TestNtFileSynchronized();
			//TestNtFileAsynchronized();
			//TestNtDevice();
			//TestNtDirectoryFileSynchronized();
			//TestNtDirectoryFileAsynchronized();
			//TestNtDirectoryObject();
			//TestNtDeviceIoControlFile();
			//TestNtEnumerateProcesses();
			//TestNtRegistryKey1();
			//TestNtRegistryKey2();
			//TestLSA();
			//TestThread();
			//TestProcess();

#if TEST_FRN
			using (NtFile someFile = NtFile.Create(@"\Device\Harddisk0\Partition2\", FileAccessMask.GenericRead | FileAccessMask.Synchronize, AllowedObjectAttributes.CaseInsensitive, FileShare.ReadWriteDelete, FileCreateOptions.SynchronousIoNonAlert, null, FileAttributes.Normal, FileCreationDisposition.OpenOnly, ErrorsNotToThrow.ThrowAnyError))
			{
				using (NtFile fso = NtFile.CreateByID(someFile, 1123, FileAccessMask.MaximumAllowed, AllowedObjectAttributes.None, FileShare.ReadWrite, FileCreateOptions.NonDirectoryFile, FileCreationDisposition.OpenOnly, ErrorsNotToThrow.ThrowAnyError))
				{

				}
			}
#endif

			System.Console.Write("Press any key to continue...");
			System.Console.ReadKey(true);
		}

		public static void TestProcess()
		{
			using (NtNonDirectoryFile file = new NtNonDirectoryFile(@"\DosDevices\C:\Windows\system32\notepad.exe", FileAccessMask.MaximumAllowed | FileAccessMask.Synchronize, FileShare.ReadWriteDelete, FileCreateOptions.Default))
			{
				using (NtProcess p = NtProcess.Current.RunChild2(file, "myPipeName", false))
				{
					System.Console.WriteLine("Process created!");
					System.Console.WriteLine();
				}
			}
		}

		public static void TestNtEnumerateProcesses()
		{
			ProcessInformationSnapshot[] processes = NtProcess.GetAllProcessesInformation();
			NtProcess currentP = NtProcess.Current;
			System.Console.WriteLine(currentP.InheritedFromUniqueProcessID);
			foreach (ProcessInformationSnapshot p in processes)
			{
				System.Console.WriteLine(p);
				{
					try
					{
						NtProcess proc = Processes.NtProcess.OpenProcess(new ClientID() { UniqueProcess = new IntPtr(p.ProcessId) }, ProcessAccessMask.AllAccess, AllowedObjectAttributes.None);
						//NtProcess proc = Processes.NtProcess.OpenProcess(p.Name, AccessMask.MaximumAllowed, AllowedObjectAttributes.None);
						System.Console.WriteLine("Image name: {0}", proc.ImageName);
					}
					catch (Exception ex)
					{
						System.Console.WriteLine(ex.Message);
					}
				}
			}
		}

		public static void TestEvent()
		{
			NtEvent e = NtEvent.Create("EventName", EventAccessMask.AllAccess, EventType.NotificationEvent);
			e.Wait(false);
		}

		public static void TestNtFileAsynchronized()
		{
			string fileName = @"\DosDevices\E:\Mehrdad\ISOs and Disk Images\Windows Server 2003 DDK.iso";
			System.Console.WriteLine("Creating file {0}...", fileName);
			using (NtNonDirectoryFile file = new NtNonDirectoryFile(fileName, FileAccessMask.GenericRead, FileShare.ReadWriteDelete, FileCreateOptions.WriteThrough | FileCreateOptions.NoIntermediateBuffering))
			{
				System.Console.WriteLine("File creation successful! Handle = {0}", file.Handle);
				foreach (var p in file.GetType().GetProperties())
				{
					try
					{
						if (p.CanRead)
						{
							object value = p.GetValue(file, null);
							if (value.GetType().IsSubclassOf(typeof(System.Array)))
							{
								System.Console.WriteLine("{0}:", p.Name);
								foreach (object item in (System.Array)value)
								{
									System.Console.WriteLine("\t{0}", item);
								}
							}
							else
							{
								System.Console.WriteLine("{0} = {1}", p.Name, value);
							}
						}
					}
					catch (Exception ex)
					{
						System.Console.WriteLine("ERROR ({0}): {1}", p.Name, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
					}
				}

				byte[] buffer = new byte[0x1C2E100];
				Events.NtEvent @event = System.WindowsNT.IO.PrivateImplementationDetails.Wrapper.NtBeginReadFile(file, null, buffer, 0, null);
			}
		}

		public static void TestNtFileSynchronized()
		{
			string fileName = @"\DosDevices\E:\Temp.txt";
			System.Console.WriteLine("Creating file {0}...", fileName);
			using (NtNonDirectoryFile file = new NtNonDirectoryFile(fileName, FileAccessMask.MaximumAllowed | FileAccessMask.Synchronize, FileShare.ReadWriteDelete, FileCreateOptions.SynchronousIoNonAlert))
			{
				System.Console.WriteLine("File creation successful! Handle = {0}", file.Handle);

				foreach (FileStreamInformation stream in file.GetStreams())
				{
					System.Console.WriteLine("Stream: {0}", stream.StreamName);
				}

				foreach (var p in file.GetType().GetProperties())
				{
					try
					{
						if (p.CanRead)
						{
							object value = p.GetValue(file, null);
							if (value.GetType().IsSubclassOf(typeof(System.Array)))
							{
								System.Console.WriteLine("{0}:", p.Name);
								foreach (object item in (System.Array)value)
								{
									System.Console.WriteLine("\t{0}", item);
								}
							}
							else
							{
								System.Console.WriteLine("{0} = {1}", p.Name, value);
							}
						}
					}
					catch (Exception ex)
					{
						System.Console.WriteLine("ERROR ({0}): {1}", p.Name, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
					}
				}
			}
		}

		public static void TestNtDevice()
		{
			FileOpenInformation foi;
			using (NtSynchronizedFile volume = (NtSynchronizedFile)NtSynchronizedFile.Open(@"\DosDevices\D:", FileAccessMask.MaximumAllowed | FileAccessMask.Synchronize, AllowedObjectAttributes.CaseInsensitive, FileShare.Read, FileCreateOptions.SynchronousIoNonAlert, out foi, ErrorsNotToThrow.ThrowAnyError))
			{
				System.Collections.BitArray bitmap = Devices.IO.FileSystem.GetVolumeBitmap(volume);
				long recordNumber = Devices.IO.FileSystem.GetHighestFileReferenceNumber(volume);
				List<FileRecord> allFiles = new List<FileRecord>();
				while (recordNumber > 0)
				{
					FileRecord attributes = Devices.IO.FileSystem.GetFileRecord(volume, ref recordNumber);
					recordNumber--;
					foreach (MFTAttribute ar in attributes)
					{
						FileNameAttribute ra = ar as FileNameAttribute;
						if (ra != null && System.Text.RegularExpressions.Regex.IsMatch(ra.FileName, @"^winword\.exe$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
						{
							allFiles.Add(attributes);
							break;
						}
					}
				}
				System.Diagnostics.Debugger.Break();
				//Devices.Volumes.MoveFile(volume, file, pointers.StartingVcn, Defragmentation.FindSmallestOpenClusterRange(bitmap, (int)pointers[0]).StartIndex, (uint)pointers[0]);
			}
		}

		public static void TestNtDirectoryFileSynchronized()
		{
			string directoryName = @"\DosDevices\E:\";
			System.Console.WriteLine("Opening directory {0}...", directoryName);
			FileOpenInformation foi;
			using (NtDirectoryFile file = NtDirectoryFile.Open(directoryName, FileAccessMask.GenericRead, AllowedObjectAttributes.None, FileShare.ReadWriteDelete, FileCreateOptions.SynchronousIoNonAlert, out foi, ErrorsNotToThrow.ThrowAnyError))
			{
				System.Console.WriteLine("File creation successful!");
				foreach (var p in file.GetType().GetProperties())
				{
					try
					{
						if (p.CanRead)
						{
							object value = p.GetValue(file, null);
							if (value.GetType().IsSubclassOf(typeof(System.Array)))
							{
								System.Console.WriteLine("{0}:", p.Name);
								foreach (object item in (System.Array)value)
								{
									System.Console.WriteLine("\t{0}", item);
								}
							}
							else
							{
								System.Console.WriteLine("{0} = {1}", p.Name, p.GetValue(file, null));
							}
						}
					}
					catch (Exception ex)
					{
						System.Console.WriteLine("Could not retrieve property {0} because of error:\r\n{1}", p.Name, ex.InnerException.Message);
					}
				}

				try
				{

				}
				catch (Exception ex)
				{
					System.Console.WriteLine("Could not read file informations because of: {0}", ex);
				}
			}
		}

		public static void TestNtDirectoryFileAsynchronized()
		{
			string directoryName = @"\DosDevices\E:\";
			System.Console.WriteLine("Opening directory {0}...", directoryName);
			FileOpenInformation foi;
			using (NtDirectoryFile file = NtDirectoryFile.Open(directoryName, FileAccessMask.GenericRead, AllowedObjectAttributes.None, FileShare.ReadWriteDelete, FileCreateOptions.None, out foi, ErrorsNotToThrow.ThrowAnyError))
			{
				System.Console.WriteLine("File creation successful!");
				foreach (var p in file.GetType().GetProperties())
				{
					try
					{
						if (p.CanRead)
						{
							object value = p.GetValue(file, null);
							if (value.GetType().IsSubclassOf(typeof(System.Array)))
							{
								System.Console.WriteLine("{0}:", p.Name);
								foreach (object item in (System.Array)value)
								{
									System.Console.WriteLine("\t{0}", item);
								}
							}
							else
							{
								System.Console.WriteLine("{0} = {1}", p.Name, p.GetValue(file, null));
							}
						}
					}
					catch (Exception ex)
					{
						System.Console.WriteLine("Could not retrieve property {0} because of error:\r\n{1}", p.Name, ex.InnerException.Message);
					}
				}

				System.Console.WriteLine("Waiting for change...");

				file.Changed += (sender, ea) =>
				{
					System.Console.WriteLine("Notified!");
					System.Console.WriteLine(ea);
				};

				file.Wait(true);

				System.Console.WriteLine("Press any key to continue TWICE!...");
				System.Console.ReadKey(true);
			}
		}

		public static void TestNtDirectoryObject()
		{
			try
			{
				using (NtDirectoryObject dir = NtDirectoryObject.OpenDirectory(@"\DosDevices\Global", DirectoryAccessMask.QUERY, AllowedObjectAttributes.CaseInsensitive))
				{
					var children = dir.GetChildren<System.Collections.Generic.List<DirectoryInformation>>();
					children.Sort((di1, di2) => di1.ObjectName.CompareTo(di2.ObjectName));
					foreach (DirectoryInformation di in children)
					{
						System.Console.WriteLine(di);
					}
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}
		}

		public static void TestNtDeviceIoControlFile()
		{
			FileOpenInformation foi;
			string volumeName = @"\DosDevices\D:";
			System.Console.WriteLine("Creating VOLUME {0}...", volumeName);
			using (NtNonDirectoryFile volume = NtNonDirectoryFile.Open(volumeName, FileAccessMask.MaximumAllowed | FileAccessMask.Synchronize, AllowedObjectAttributes.None, FileShare.ReadWriteDelete, FileCreateOptions.SynchronousIoNonAlert, out foi, ErrorsNotToThrow.ThrowAnyError))
			{
				System.Console.WriteLine("Opened volume! Handle = {0}", volume.Handle);
				NTFS_VOLUME_EXTENDED_VOLUME_DATA volumeData = Devices.IO.FileSystem.GetVolumeData(volume);
				System.Collections.BitArray bitmap = Devices.IO.FileSystem.GetVolumeBitmap(volume);
				using (NtFile tempFile = NtNonDirectoryFile.Open(@"\DosDevices\D:\Windows\System32\winmine.exe", FileAccessMask.MaximumAllowed, AllowedObjectAttributes.None, FileShare.ReadWriteDelete, FileCreateOptions.None, out foi, ErrorsNotToThrow.ThrowAnyError))
				{
					System.Console.WriteLine("Opened file: {0}", tempFile.Name);
					long record = tempFile.IndexNumber;
					System.Console.WriteLine("Retrieval Pointers: {0}", Devices.IO.FileSystem.GetRetrievalPointers(tempFile, 0, ErrorsNotToThrow.ThrowAnyError));
				}
				System.Console.WriteLine("First open segment index = {0}", Defragmentation.IndexOfFirstOpenCluster(bitmap));
				System.Console.WriteLine("Largest open segment = {0}", Defragmentation.FindLargestOpenClusterRange(bitmap));
				System.Console.WriteLine("Smallest open segment = {0}", Defragmentation.FindSmallestOpenClusterRange(bitmap, 0));
			}
		}

		public static void TestNtRegistryKey1()
		{
			NtRegistryKey registry = NtRegistryKey.OpenRegistry();

			foreach (string subKeyName1 in registry.SubKeys.GetNames(true))
			{
				NtRegistryKey subKey1 = registry.SubKeys.Open(subKeyName1, KeyAccessMask.MaximumAllowed, AllowedObjectAttributes.CaseInsensitive);
				System.Console.WriteLine("SubKey: {0}", subKey1.Name);
				foreach (string subKeyName2 in subKey1.SubKeys.GetNames(true))
				{
					try
					{
						NtRegistryKey subKey2 = subKey1.SubKeys.Open(subKeyName2, KeyAccessMask.MaximumAllowed, AllowedObjectAttributes.CaseInsensitive);
						System.Console.WriteLine("\tSubKey: {0}", subKey2.Name);
						foreach (string subKeyName3 in subKey2.SubKeys.GetNames(true))
						{
							System.Console.WriteLine("\t\tSubKey: {0}", subKeyName3);
						}
					}
					catch (System.WindowsNT.Errors.NtException)
					{
					}
				}
			}

			/*
			NtRegistryKey cuSoftware;
			CreationDisposition creationDisposition;
			string keyName = "Temp1";
			cuSoftware = NtRegistryKey.OpenCurrentUserKey().CreateSubKey(string.Format(@"Software\{0}", keyName), KeyAccessMask.AllAccess, System.WindowsNT.AllowedObjectAttributes.CaseInsensitive, RegCreateOptions.Default, out creationDisposition);
			System.Console.WriteLine("Handle: {0}", cuSoftware.Handle);
			System.Console.WriteLine("Name: {0}", cuSoftware.Name);
			System.Console.WriteLine("Disposition: {0}", creationDisposition);
			cuSoftware.Name = "Temp2";
			System.Console.WriteLine("Renamed to: {0}", cuSoftware.Name);
			cuSoftware.Name = keyName;
			System.Console.WriteLine("Again renamed to: {0}", cuSoftware.Name);
			*/
		}

		public static void TestNtRegistryKey2()
		{
			using (NtRegistryKey temp = NtRegistryKey.OpenCurrentUserKey())
			{
				using (NtRegistryKey temp1 = temp.SubKeys.Open(@"Temp", KeyAccessMask.MaximumAllowed, AllowedObjectAttributes.CaseInsensitive))
				{
					temp1.Values["TempValueName1"] = new DWordLittleEndian(1234);
					temp1.Values["TempValueName2"] = new SZ("TempValue");
				}
				System.Console.WriteLine("Key Name: {0}", WindowsNT.Registry.PrivateImplementationDetails.Wrapper.NtQueryKeyName((SafeNtRegistryHandle)temp.Handle).Name);
				System.Console.WriteLine("Key Name: {0}", WindowsNT.Registry.PrivateImplementationDetails.Wrapper.NtQueryKeyCached((SafeNtRegistryHandle)temp.Handle).Name);
			}
		}

		public static void TestLoadHive()
		{
			using (NtRegistryKey machine = NtRegistryKey.OpenMachineHive())
			{
				System.Console.WriteLine("Current hive name: {0}", machine.Name);
				System.Console.WriteLine("Current hive path: {0}", machine.FullPath);
				string fileName = @"\??\D:\Windows\System32\config\SYSTEM";
				string hiveName = @"LoadedHiveName_DO_NOT_TOUCH";
				System.Console.WriteLine("Loading2 hive {0}...", hiveName);
				machine.Load2(hiveName, fileName, KeyRestoreOptions.Default, AllowedObjectAttributes.CaseInsensitive);
				System.Console.WriteLine("Finished loading hive!");
				using (NtFile saveFile = new NtNonDirectoryFile(@"\??\E:\TempHive", FileAccessMask.AllAccess, FileShare.Read, FileCreateOptions.Default))
				{
					using (NtRegistryKey loadedHive = machine.SubKeys.Open(hiveName, KeyAccessMask.AllAccess, AllowedObjectAttributes.None))
					{
						loadedHive.SaveEx(saveFile, RegHiveFormat.Latest);
					}
				}
				System.Console.WriteLine("Unloading hive...");
				machine.Unload(hiveName);
				System.Console.WriteLine("Finished unloading hive!");
			}
		}

		public static void TestRestore()
		{
			string fileName = @"\??\E:\TestHive";
			RegCreationDisposition cd;
			using (NtRegistryKey key = NtRegistryKey.OpenCurrentUserKey().SubKeys.Create("Temp", KeyAccessMask.AllAccess, AllowedObjectAttributes.CaseInsensitive, RegCreateOptions.NonVolatile, out cd))
			{
				FileOpenInformation oi;
				using (NtFile saveFile = NtNonDirectoryFile.Open(fileName, FileAccessMask.AllAccess, AllowedObjectAttributes.CaseInsensitive, FileShare.Exclusive, FileCreateOptions.Default, out oi, ErrorsNotToThrow.ThrowAnyError))
				{
					key.Restore(saveFile, KeyRestoreOptions.Default);
				}
			}
		}

		public static void TestLSA()
		{
			try
			{
				SafeLSAPolicyHandle handle = Security.PrivateImplementationDetails.Wrapper.LsaOpenPolicy(AccessMask.MaximumAllowed);
				Security.PrivateImplementationDetails.Wrapper.LsaRetrievePrivateData(handle, "$machine.acc");
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}
		}

		public static void TestThread()
		{
			System.Console.WriteLine(Threading.NtThread.Current.ObjectName);
			Threading.NtThread.Current.Suspend();
		}
	}
}