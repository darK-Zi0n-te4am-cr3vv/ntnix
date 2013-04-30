namespace Algorithms
{
	public static class Defragmentation
	{
		public static int IndexOfFirstOpenCluster(System.Collections.BitArray bitmap)
		{
			for (int i = 0; i < bitmap.Length; ++i)
			{
				if (!bitmap[i])
				{
					return i;
				}
			}
			return -1;
		}

		public static Range FindSmallestOpenClusterRange(System.Collections.BitArray bitmap, int minimumClusterCount)
		{
			Range segment = new Range(-1, int.MaxValue);
			int currentSegmentLength = 0;
			int i;
			for (i = 0; i < bitmap.Length; i++)
			{
				if (bitmap[i])
				{
					if (currentSegmentLength < segment.Length && currentSegmentLength >= minimumClusterCount)
					{
						segment.Length = currentSegmentLength;
						segment.Start = i - currentSegmentLength;
					}
					currentSegmentLength = 0;
				}
				else
				{ currentSegmentLength++; }
			}
			if (currentSegmentLength < segment.Length && currentSegmentLength > minimumClusterCount)
			{
				segment.Length = currentSegmentLength;
				segment.Start = bitmap.Length - segment.Length;
			}
			if (segment.Length == int.MaxValue)
			{
				segment.Length = 0;
			}
			return segment;
		}

		public static Range FindLargestOpenClusterRange(System.Collections.BitArray bitmap)
		{
			Range segment = new Range(-1, 0);
			int currentSegmentLength = 0;
			for (int i = 0; i < bitmap.Length; i++)
			{
				if (bitmap[i])
				{
					if (segment.Length < currentSegmentLength)
					{
						segment.Length = currentSegmentLength;
						segment.Start = i - currentSegmentLength;
					}
					currentSegmentLength = 0;
				}
				else
				{ currentSegmentLength++; }
			}
			if (segment.Length < currentSegmentLength)
			{
				segment.Length = currentSegmentLength;
				segment.Start = bitmap.Length - segment.Length;
			}
			return segment;
		}
	}
}