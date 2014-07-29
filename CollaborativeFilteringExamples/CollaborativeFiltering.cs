using System;
using System.Collections.Generic;
using System.Linq;

namespace CollaborativeFilteringExamples
{
	public class CollaborativeFiltering
	{
		public CollaborativeFiltering ()
		{
		}

		// Rewrite this crap in LINQ!!!
		public IEnumerable<KeyValuePair<int, double>> recommend (
			IEnumerable<UserPreference> preferences,
			Func<double> get_similarity, 
			UserPreference currentUserPref) {
			Dictionary<int, double> totals = new Dictionary<int, double> ();
			foreach (UserPreference pref in preferences) {
				int otherUserId = pref.UserId;
				if (otherUserId == currentUserPref.UserId) {
					continue; // skip the user
				}
				double score = get_similarity ();
				//only score items that the user has no preferences
				IEnumerable<int> noPrefItems = pref.ItemIds.Except (currentUserPref.ItemIds);
				foreach (int itemId in noPrefItems) {
					totals [itemId] += score;
				}
			}
			return totals.OrderBy (pair => pair.Value);
		}

		public IEnumerable<KeyValuePair<int, double>> recommendUsingLinq (
			IEnumerable<UserPreference> preferences,
			ISimilarity similarity, 
			UserPreference currentUserPref) {
			var scores = from pref in preferences
			            where pref.UserId != currentUserPref.UserId
						let score = similarity.Calculate(currentUserPref.ItemIds, pref.ItemIds)
						from itemId in pref.ItemIds.Except(currentUserPref.ItemIds) 
			            select new {
							Score = score,
							NoPrefItemId = itemId
						};
			return (from item in scores
				group item by item.NoPrefItemId into grp
				select new KeyValuePair<int, double>(grp.Key, grp.Sum(i => i.Score)) )
				.OrderByDescending (pair => pair.Value);
		}

	}
}

