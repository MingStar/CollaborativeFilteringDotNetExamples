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
									IEnumerable<UserProfile> profiles,
									ISimilarityScorer similarity, 
									UserProfile current) {

			var totals = new Dictionary<int, double> ();

			foreach (UserProfile other in profiles) {
				if (other == current) {
					continue; // skip the current user profile
				}

				double score = similarity.Calculate(current, other);

				//only score items NOT in current user's history
				foreach (int itemId in 
					     other.ItemHistory.Except(current.ItemHistory)) {
					if (!totals.ContainsKey(itemId)) {
						totals[itemId] = 0.0;
					}
					totals[itemId] += score;
				}
			}

			return totals.OrderByDescending(pair => pair.Value);
		}

		public IEnumerable<KeyValuePair<int, double>> recommendUsingLinq (
			IEnumerable<UserProfile> preferences,
			ISimilarityScorer similarity, 
			UserProfile currentUserPref) {
			var scores = from pref in preferences
						 where pref != currentUserPref // skip current user
						 let score = similarity.Calculate(currentUserPref, pref)
						 //only score items that the user has no preferences
						 from itemId in pref.ItemHistory.Except(currentUserPref.ItemHistory) 
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

