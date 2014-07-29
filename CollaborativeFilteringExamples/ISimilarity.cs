using System;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	public interface ISimilarity
	{
		double Calculate(ISet<int> itemIds, ISet<int> otherItemIds);
	}
}

