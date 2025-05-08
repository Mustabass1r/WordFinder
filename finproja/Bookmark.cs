using System;
using System.Collections.Generic;

namespace finproja
{
    internal class Bookmark
    {
        public List<string> bookmarks;
       

        public Bookmark()
        {
            bookmarks = new List<string>();
        }

        public void RemoveBookmark(string word)
        {
            if (bookmarks != null && bookmarks.Contains(word))
            {
                bookmarks.Remove(word);
            }
        }

        public void AddBookmark(string word)
        {
            bookmarks.Add(word);
        }

        private static void quickSort(List<string> list, int start, int end)
        {
            if (start >= end) return;   //base case
            int pivot = partiton(list, start, end);
            quickSort(list, start, pivot - 1);
            quickSort(list, pivot + 1, end);
        }

        private static int partiton(List<string> list, int start, int end)
        {
            string pivot = list[end];
            int i = start - 1;
            for (int j = start; j <= end - 1; j++)
            {
                if (list[j].CompareTo(pivot) < 0)
                {
                    i++;
                    string tempp = list[j];
                    list[j] = list[i];
                    list[i] = tempp;
                }
            }
            i++;
            string temp = list[end];
            list[end] = list[i];
            list[i] = temp;

            return i;
        }
        public bool findBookmark(string word)
        {
             if (bookmarks.Contains(word))
            {
                return true;
            }
            else { return false; }
        }
    }
}
