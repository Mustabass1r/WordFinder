using System;
using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;

namespace finproja
{
    class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        private string PasswordHash { get; set; }
        public RecentSearchManager recentSearchManager { get; set; }
        public Bookmark bookmarks;

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            PasswordHash = HashPassword(password);
            recentSearchManager = new RecentSearchManager();
            bookmarks = new Bookmark();
        }

        public void AddBookmark(string word)
        {
            bookmarks.AddBookmark(word);

        }

        public void RemoveBookmark(string word)
        {
            if (bookmarks != null)
            {
                bookmarks.RemoveBookmark(word);
            }
        }

        public List<string> GetBookmarks()
        {
            return bookmarks.bookmarks;
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(8));
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
        public void addRecentSearch(string word) { 
            this.recentSearchManager.UpdateRecentSearch(word);
        }
       
    }
}
