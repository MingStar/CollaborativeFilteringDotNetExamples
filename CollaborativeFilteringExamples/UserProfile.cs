using System;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	public class UserProfile
	{
		public int UserId { get; private set; }
		public int[] ItemHistory { get; private set; }

		public UserProfile (int userId, int[] itemIds)
		{
			UserId = userId;
			ItemHistory = itemIds;
		}
	}
}

