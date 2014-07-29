using System;
using System.Linq;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	public class JaccardSimilarity : ISimilarity
	{
		public JaccardSimilarity ()
		{
		}

		#region ISimilarity implementation

		public double Calculate (ISet<int> itemIds, ISet<int> otherItemIds)
		{
			int intersectionCount = itemIds.Intersect(otherItemIds).Count();
			int unionCount = itemIds.Count + otherItemIds.Count - intersectionCount;
			return intersectionCount / (double)unionCount;
		}

		#endregion
	}
}

