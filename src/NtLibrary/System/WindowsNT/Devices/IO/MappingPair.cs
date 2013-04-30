namespace System.WindowsNT.Devices.IO
{
	[System.Diagnostics.DebuggerDisplay("Current LCN = {CurrentLCN}, Next VCN = {NextVCN}")]
	public struct MappingPair : System.IEquatable<MappingPair>, System.ICloneable
	{
		public MappingPair(long nextVCN, long currentLCN)
			: this()
		{
			this.CurrentLCN = currentLCN;
			this.NextVCN = nextVCN;
		}

		//DO NOT ADD OR REMOVE ANY FIELDS
		public long NextVCN;
		//MODIFYING ANY FIELDS WILL INVALIDATE OTHER TYPES!!
		public long CurrentLCN;

		public override bool Equals(object obj) { return obj != null && this.Equals((MappingPair)obj); }

		public bool Equals(MappingPair other) { return this.CurrentLCN == other.CurrentLCN & this.NextVCN == other.NextVCN; }

		public override int GetHashCode() { return this.NextVCN.GetHashCode() ^ this.CurrentLCN.GetHashCode(); }

		internal static Collections.Generic.List<MappingPair> Decompress(long lowestVCN, System.IO.Stream stream)
		{
			Collections.Generic.List<MappingPair> pairs = new Collections.Generic.List<MappingPair>();

			long nextVCN = lowestVCN,
				currentLCN = 0;
			byte nextByte = (byte)stream.ReadByte(); ;
			while (nextByte != 0)
			{
				long currentVCN = nextVCN;

				unsafe
				{
					byte bytesVCN = (byte)(nextByte & 0x0F);
					byte* incrementVCN = stackalloc byte[sizeof(long)];
					int i;
					for (i = 0; i < bytesVCN; i++)
					{
						incrementVCN[i] = (byte)stream.ReadByte();
					}
					byte signExtension = unchecked((byte)((incrementVCN[i - 1] & (1 << 7)) == 0 ? 0 : ~0));
					if (signExtension != default(byte))
					{
						for (; i < sizeof(long); ++i)
						{
							incrementVCN[i] = signExtension;
						}
					}
					nextVCN += *(long*)incrementVCN;
					System.Diagnostics.Debug.Assert(nextVCN >= 0, "nextVCN < 0");


					byte bytesLCN = (byte)(nextByte >> 4);
					byte* incrementLCN = stackalloc byte[sizeof(long)];
					for (i = 0; i < bytesLCN; i++)
					{
						incrementLCN[i] = (byte)stream.ReadByte();
					}
					signExtension = unchecked((byte)((incrementLCN[i - 1] & (1 << 7)) == 0 ? 0 : ~0));
					if (signExtension != default(byte))
					{
						for (; i < sizeof(long); ++i)
						{
							incrementLCN[i] = signExtension;
						}
					}
					currentLCN += *(long*)incrementLCN;
					System.Diagnostics.Debug.Assert(currentLCN >= -1, "currentLCN < -1");

					MappingPair pair = new MappingPair(/*currentVCN,*/ nextVCN, currentLCN);

					if (*(long*)incrementLCN == 0)
					{
						pair.CurrentLCN = -1;
					}

					pairs.Add(pair);
					nextByte = (byte)stream.ReadByte();
				}
			}

			return pairs;
		}

		private static unsafe long RunLCN(byte* run)
		{
			byte n1 = (byte)(*run & 0x0F);
			byte n2 = (byte)((*run >> 4) & 0x0F);
			long lcn = (n2 == 0) ? 0 : (sbyte)(run[n1 + n2]);
			for (int i = n1 + n2 - 1; i > n1; i--)
				lcn = (lcn << 8) + run[i];
			return lcn;
		}

		private static unsafe ulong RunCount(byte* run)
		{
			byte n = (byte)(*run & 0xF);
			ulong count = 0;
			for (uint i = n; i > 0; i--)
				count = (count << 8) + run[i];
			return count;
		}

		private static unsafe uint RunLength(byte* run)
		{
			return (*run & 0x0FU) + (unchecked((uint)(*run >> 4)) & 0x0FU) + 1U;
		}

		internal static unsafe bool FindRun(byte* runArray, ulong vcn, ulong startVCN, ulong endVCN, out ulong lcn, out ulong count)
		{
			byte* run;
			ulong @base = (ulong)startVCN;
			lcn = 0;
			count = 0;
			if (vcn < startVCN | vcn > endVCN)
			{
				return false;
			}
			for (run = runArray; *run != 0; run += RunLength(run))
			{
				lcn += unchecked((ulong)RunLCN(run));
				count = RunCount(run);

				if (@base <= vcn && vcn < @base + count)
				{
					lcn = (RunLCN(run) == 0) ? 0 : lcn + vcn - @base;
					count -= vcn - @base;

					return true;
				}
				else
				{
					@base += count;
				}
			}

			return false;
		}

		public override string ToString()
		{
			return string.Format("Next VCN = {1}, Current LCN = {2}", /*this.CurrentVCN,*/ this.NextVCN, this.CurrentLCN);
		}

		public object Clone() { return this.MemberwiseClone(); }
	}
}