using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace finproja
{

    class Dictionary
    {
        private static Dictionary _instance;
        private AVLTree _tree;

        private Dictionary() {
         _tree = new AVLTree();
        }    

        public static Dictionary Instance
        {
            get
            {
                if (_instance == null) _instance = new Dictionary();
                return _instance;
            }
        }

        public AVLNode searchWord(String word)
        {
            return _tree.Search(word);
        }

        public IEnumerable<string> autoComplete()
        {
            return _tree.GetAllWords();
        }

        public void Insert600()
        {
            try
            {
                // Replace "your_file_path.txt" with the actual path to your text file
                string filePath = "C:\\Users\\Ahmad Mustabassir\\Desktop\\bt.txt";

                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                string currentWord = null;
                string currentMeaning = null;
                List<string> currentSynonyms = null;
                List<string> currentAntonyms = null;

                foreach (string line in lines)
                {
                    // Remove leading and trailing whitespaces
                    string trimmedLine = line.Trim();

                    // Check if the line contains any relevant information
                    if (trimmedLine.Length > 0)
                    {
                        // Identify the type of information based on the first word in the line
                        string[] parts = trimmedLine.Split(':');
                        string label = parts[0].Trim();

                        // Process based on the label
                        switch (label)
                        {
                            case "Meaning":
                                // Extract the meaning
                                currentMeaning = trimmedLine.Substring("Meaning:".Length).Trim();
                                break;

                            case "Synonyms":
                                // Extract synonyms
                                currentSynonyms.AddRange(trimmedLine.Substring("Synonyms:".Length).Split(',').Select(s => s.Trim()));
                                break;

                            case "Antonyms":
                                // Extract antonyms
                                currentAntonyms.AddRange(trimmedLine.Substring("Antonyms:".Length).Split(',').Select(s => s.Trim()));
                                break;

                            default:
                                // Assume it is the word
                                if (currentWord != null)
                                {
                                    // Insert the previous entry into the AVL tree
                                    _tree.Insert(currentWord, currentMeaning, currentSynonyms, currentAntonyms);
                                }

                                // Reset variables for the new entry
                                currentWord = trimmedLine.Trim();
                                currentMeaning = null;
                                currentSynonyms = new List<string>();
                                currentAntonyms = new List<string>();
                                break;
                        }
                    }
                }

                // Insert the last entry into the AVL tree
                if (currentWord != null)
                {
                    _tree.Insert(currentWord, currentMeaning, currentSynonyms, currentAntonyms);
                }

                MessageBox.Show("Inserted successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting data: {ex.Message}");
            }
        }

        public AVLNode feelingLucky() {
            return _tree.FeelingLucky();
        }





    }


    public class AVLNode
    {
        public int height;
        public AVLNode left, right;
        public string word;
        public string meanings;
        public List<string> synonyms { get; set; }
        public List<string> antonyms { get; set; }

        public AVLNode(string word, string meanings, List<string> synonyms , List<string> antonyms)
        {
            height = 1;
            this.word = word;
            this.meanings = meanings;
            this.synonyms = synonyms ?? new List<string>();
            this.antonyms = antonyms ?? new List<string>();

        }
    }

    public class AVLTree
    {
        AVLNode root;
        public AVLTree()
        {
            root = null;
        }

        int height(AVLNode node)
        {
            if (node == null)
                return 0;
            return node.height;
        }

        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        AVLNode rightRotate(AVLNode y)
        {
            AVLNode x = y.left;
            AVLNode T2 = x.right;

            x.right = y;
            y.left = T2;

            y.height = max(height(y.left), height(y.right)) + 1;
            x.height = max(height(x.left), height(x.right)) + 1;

            return x;
        }

        AVLNode leftRotate(AVLNode x)
        {
            AVLNode y = x.right;
            AVLNode T2 = y.left;

            y.left = x;
            x.right = T2;

            x.height = max(height(x.left), height(x.right)) + 1;
            y.height = max(height(y.left), height(y.right)) + 1;

            return y;
        }

        int getBalance(AVLNode node)
        {
            if (node == null)
                return 0;
            return height(node.left) - height(node.right);
        }

        AVLNode Insert(AVLNode node, string word, string meanings, List<string> synonyms, List<string> antonyms)
        {
            if (node == null)
                return new AVLNode(word, meanings, synonyms, antonyms);

            int comparisonResult = string.Compare(word, node.word, StringComparison.OrdinalIgnoreCase);

            if (comparisonResult < 0)
                node.left = Insert(node.left, word, meanings, synonyms, antonyms);
            else if (comparisonResult > 0)
                node.right = Insert(node.right, word, meanings, synonyms, antonyms);
            else
            {
                MessageBox.Show("Already exists");
                return node;
            }

            node.height = max(height(node.left), height(node.right)) + 1;

            int balance = getBalance(node);

            if (balance > 1)
            {
                if (string.Compare(word, node.left.word, StringComparison.OrdinalIgnoreCase) < 0)
                    return rightRotate(node);
                else if (string.Compare(word, node.left.word, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    node.left = leftRotate(node.left);
                    return rightRotate(node);
                }
            }

            if (balance < -1)
            {
                if (string.Compare(word, node.right.word, StringComparison.OrdinalIgnoreCase) > 0)
                    return leftRotate(node);
                else if (string.Compare(word, node.right.word, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    node.right = rightRotate(node.right);
                    return leftRotate(node);
                }
            }

            return node;
        }

        public void Insert(string word, string meanings, List<string> synonyms, List<string> antonyms)
        {
            root = Insert(root, word, meanings, synonyms, antonyms);
        }

        public void PrintInOrder(AVLNode node)
        {
            if (node != null)
            {
                PrintInOrder(node.left);
                Console.WriteLine($"{node.word}: {string.Join(", ", node.meanings)}");
                PrintInOrder(node.right);
            }
        }

        public void PrintInOrder()
        {
            PrintInOrder(root);
        }

        public AVLNode Search(string word)
        {
            return Search(root, word);
        }

        private AVLNode Search(AVLNode node, string word)
        {
            if (node == null || string.Compare(word, node.word, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return node;
            }

            if (string.Compare(word, node.word, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return Search(node.left, word);
            }
            else
            {
                return Search(node.right, word);
            }
        }

        public static IEnumerable<string> GetAllWords(AVLNode node)
        {
            List<string> words = new List<string>();

            if (node != null)
            {
                words.Add(node.word);
                words.AddRange(GetAllWords(node.left));
                words.AddRange(GetAllWords(node.right));
            }

            return words;
        }

        public IEnumerable<string> GetAllWords()
        {
            return GetAllWords(root);
        }

       

        // Additional methods like delete, edit, feeling lucky, etc. can be added here.

        // ... (unchanged code)
        public void Delete(string word)
        {
            root = Delete(root, word);
        }

        private AVLNode Delete(AVLNode root, string word)
        {
            if (root == null)
                return root;

            if (string.Compare(word, root.word, StringComparison.OrdinalIgnoreCase) < 0)
                root.left = Delete(root.left, word);
            else if (string.Compare(word, root.word, StringComparison.OrdinalIgnoreCase) > 0)
                root.right = Delete(root.right, word);
            else
            {
                // Node with only one child or no child
                if (root.left == null)
                    return root.right;
                else if (root.right == null)
                    return root.left;

                // Node with two children
                root.word = MinValueNode(root.right).word;
                root.right = Delete(root.right, root.word);
            }

            // Update height and balance factor
            root.height = max(height(root.left), height(root.right)) + 1;
            int balance = getBalance(root);

            // Perform rotations to maintain AVL property
            if (balance > 1)
            {
                if (getBalance(root.left) >= 0)
                    return rightRotate(root);
                else
                {
                    root.left = leftRotate(root.left);
                    return rightRotate(root);
                }
            }

            if (balance < -1)
            {
                if (getBalance(root.right) <= 0)
                    return leftRotate(root);
                else
                {
                    root.right = rightRotate(root.right);
                    return leftRotate(root);
                }
            }

            return root;
        }

        private AVLNode MinValueNode(AVLNode node)
        {
            AVLNode current = node;

            while (current.left != null)
                current = current.left;

            return current;
        }
        //public void Edit(string word, string newMeaning)
        //{
        //    root = Edit(root, word, newMeaning);
        //}

        //private AVLNode Edit(AVLNode root, string word, string newMeaning)
        //{
        //    if (root == null)
        //        return root;

        //    int comparisonResult = string.Compare(word, root.word, StringComparison.OrdinalIgnoreCase);

        //    if (comparisonResult < 0)
        //        root.left = Edit(root.left, word, newMeaning);
        //    else if (comparisonResult > 0)
        //        root.right = Edit(root.right, word, newMeaning);
        //    else
        //        root.meaning = newMeaning; // Update the meaning

        //    // Update height and balance factor
        //    root.height = max(height(root.left), height(root.right)) + 1;
        //    int balance = getBalance(root);

        //    // Perform rotations to maintain AVL property
        //    if (balance > 1)
        //    {
        //        if (getBalance(root.left) >= 0)
        //            return rightRotate(root);
        //        else
        //        {
        //            root.left = leftRotate(root.left);
        //            return rightRotate(root);
        //        }
        //    }

        //    if (balance < -1)
        //    {
        //        if (getBalance(root.right) <= 0)
        //            return leftRotate(root);
        //        else
        //        {
        //            root.right = rightRotate(root.right);
        //            return leftRotate(root);
        //        }
        //    }

        //    return root;
        //}
        public AVLNode FeelingLucky()
        {
            AVLNode node = root;
            if (node == null)
            {
                return null;
            }
            Random rd = new Random();
            List<int> rndm = new List<int>();
            int count = rd.Next(0, root.height);
            
            for (int i = 0; i < count; i++)
            {
                rndm.Add(rd.Next(1, 3));
            }
            if (count == 0)
            {
                return node;
            }
            for (int i = 0; i < count; i++)
            {
                if (node == null)
                {
                    break;
                }

                if (rndm[i] == 1 && node.left != null)
                {
                    node = node.left;
                }
                else if (rndm[i] == 2 && node.right != null)
                {
                    node = node.right;
                }
            }
            if (node != null)
            {
                return node;
            }
            else
            {
                return null;
            }
        }

    }

    public class Levenshtein
    {
        public static int CalculateDistance(string word1, string word2)
        {
            int[,] distanceMatrix = new int[word1.Length + 1, word2.Length + 1];

            for (int i = 0; i <= word1.Length; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (int j = 0; j <= word2.Length; j++)
            {
                distanceMatrix[0, j] = j;
            }

            for (int i = 1; i <= word1.Length; i++)
            {
                for (int j = 1; j <= word2.Length; j++)
                {
                    int cost = (word1[i - 1] == word2[j - 1]) ? 0 : 1;

                    distanceMatrix[i, j] = Math.Min(
                        Math.Min(distanceMatrix[i - 1, j] + 1,       // Deletion
                                 distanceMatrix[i, j - 1] + 1),      // Insertion
                        distanceMatrix[i - 1, j - 1] + cost);     // Substitution
                }
            }

            return distanceMatrix[word1.Length, word2.Length];
        }

        public static string GetClosestWord(string targetWord, IEnumerable<string> candidateWords)
        {
            int minDistance = int.MaxValue;
            string closestWord = null;

            foreach (string candidate in candidateWords)
            {
                int distance = CalculateDistance(targetWord, candidate);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestWord = candidate;
                }
            }

            return closestWord;
        }
    }
    

}
