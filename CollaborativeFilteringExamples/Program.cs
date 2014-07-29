using System;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var profiles = GetAllUserProfiles(); 		
			var simiarity = new JaccardSimilarity();
			var engine = new CollaborativeFiltering();
			foreach (var profile in profiles) {
				var results = engine.recommend(profiles, simiarity, profile);
				PrintResults(profile.UserId, results);
			}
		}

		private static void PrintResults(int userId, IEnumerable<KeyValuePair<int, double>> results) {
			Console.Write("user{0}: [ ", userId);
			foreach (var result in results) {
				Console.Write (String.Format ("(item{0}, {1}) ", result.Key, result.Value));
			}
			Console.WriteLine ("]");
		}

		private static IEnumerable<UserProfile> GetAllUserProfiles() {
			var profiles = new List<UserProfile> ();

			profiles.Add(new UserProfile(1, new int[] { 1, 2, 3 }));
			profiles.Add(new UserProfile(2, new int[] { 2, 3, 4 }));
			profiles.Add(new UserProfile(3, new int[] { 1, 5 }));

			return profiles;
		}
	}
}
