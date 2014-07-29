using System;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	public class UserPreference
	{
		public int UserId { get; private set; }
		public HashSet<int> ItemIds { get; private set; }

		public UserPreference (int userId, int[] itemIds)
		{
			UserId = userId;
			ItemIds = new HashSet<int>(itemIds);
		}
	}
}

