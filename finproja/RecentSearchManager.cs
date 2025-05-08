using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finproja
{
    public class RecentSearchManager
    {
        public Queue<string> recentSearches;
        private const int MaxRecentSearches = 5;

        public RecentSearchManager()
        {
            recentSearches = new Queue<string>();
        }

        public void UpdateRecentSearch(string word)
        {
            if (recentSearches.Count()<=MaxRecentSearches)
            {
                if (!recentSearches.Contains(word))
                {
                    recentSearches.Enqueue(word);
                }
            }
            else
            {
                recentSearches.Dequeue();
                recentSearches.Enqueue(word);
            }
        }

        public IEnumerable<string> GetRecentSearches()
        {
            return recentSearches;
        }
    }
}
