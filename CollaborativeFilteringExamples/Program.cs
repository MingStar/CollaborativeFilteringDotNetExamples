using System;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var preferences = new List<UserPreference> ();
			preferences.Add (new UserPreference (1, new int[] { 1, 2, 3 }));
			preferences.Add (new UserPreference (2, new int[] { 2, 3, 4 }));
			preferences.Add (new UserPreference (3, new int[] { 1, 5 }));
			var engine = new CollaborativeFiltering ();
			foreach (var pref in preferences) {
				var results = engine.recommendUsingLinq (preferences, new JaccardSimilarity (), 
					pref);
				PrintResults (results);
			}
		}

		private static void PrintResults(IEnumerable<KeyValuePair<int, double>> results) {
			Console.Write ("[");
			foreach (var result in results) {
				Console.Write (String.Format ("({0},{1})", result.Key, result.Value));
			}
			Console.WriteLine ("]");
		}
	}
}
