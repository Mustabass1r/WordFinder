# WordFinder

WordFinder is a Windows Forms application designed to provide users with a variety of tools, including word search, anagram generation, and bookmark management. The application is built using C# and .NET Framework, with a focus on user-friendly interfaces and functionality.

---

## Tech Stack:  
C#, .NET Framework, Windows Forms, AVL Tree, BCrypt, Levenshtein Distance
## Features

---

### 1. **Search**
- Allows users to search for words and view their meanings, synonyms, and antonyms.
- Recent searches are saved and displayed for quick access.

### 2. **Anagram**
- Generates anagrams for a given word.
- Provides a clean and intuitive interface for exploring word rearrangements.

### 3. **Bookmarks**
- Users can bookmark words for future reference.
- Bookmarked words are displayed in a dedicated section.
- Users can add or remove bookmarks with a single click.

### 4. **User Management**
- Supports user registration and login.
- Each user has their own set of bookmarks and recent searches.

### 5. **Custom UI Components**
- Uses images and icons for a visually appealing interface.

---


### Key Files
- **`Search.cs`**: Implements the search functionality.
- **`Searched.cs`**: Displays search results, including synonyms and antonyms.
- **`Anagram.cs`**: Handles anagram generation.
- **`Form4.cs`**: Displays bookmarks.
- **`User.cs`**: Manages user data, including bookmarks and recent searches.
- **`UserManager.cs`**: Handles user registration and login.
- **`CustomPanelButton.cs`**: Implements custom UI buttons.

---

## Dependencies

- **.NET Framework 4.7.2**
- **BCrypt.Net-Next**: Used for password hashing and verification.

---

## Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/mustabass1r/WordFinder.git
2. Open the solution file (finproja.sln) in Visual Studio.  
3. Restore NuGet packages:
   ```sh
   nuget restore

