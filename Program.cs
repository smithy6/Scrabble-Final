using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scrabble_Remix
{
    class Program
    {
        private API dictionary;
        private string[] scrabbleLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" }; //possible letters in scrabble
        private string[] vowels = { "a", "e", "i", "o", "u" }; //vowels to check if included in random generated letters
        private int[] scrabblePoints = { 1, 3, 3, 2, 1, 4, 2, 4, 1, 8, 5, 1, 3, 1, 1, 3, 10, 1, 1, 1, 1, 4, 4, 8, 4, 10 }; //scrabble points that indexes match to the corrisponding letters of the scrabbleLetters array alphabet
        private string[] playerWords = new string[100]; //player inputted words
        private int playerPoints; //player points for current word
        private int totalPlayerPoints; //total player points all time
        private Random rnd = new Random();
        private string[] randomLetters = new string[7]; //random letters generated
        private bool generateLetters = true; //boolean that determines if letters are generated or not, can be set to true if letters need regenerating
        private bool loopInput = true; //whether the player input section (player is still inputting words) should loop, should it still ask the user for words
        public bool loopGame = true; //
        private bool stopGame = false; //if the games needs to end (player has inputted "!end") then this is set to true and the game ends
        private int playerWordCount; //how many words the player has inputted
        private bool alreadyUsed = false; //boolean for if the word has already been used

        public Program()
        {
            dictionary = new API(); //links the API class to the dictionary variable

        }
        
        public void Run()
        {
            Console.WriteLine("Welcome to Scrabble!");
            Console.WriteLine("Type !end to finish the game");
            generate();
            Console.WriteLine("Your letters are: ");
            for (int i = 0; i < randomLetters.Length; i++) //Output randomLetters as upper case
            {
                string letters = randomLetters[i];
                Console.Write(letters.ToUpper());
            }
            Console.WriteLine();

            while (loopInput) //Player Input
            {
                Console.WriteLine("Enter a word: ");
                string word = Console.ReadLine();
                if (playerWords.Contains(word)) //check if word has been used before
                {
                    Console.WriteLine("You have already used this word");
                    continue;
                }
                playerWords[playerWordCount] = word; //add word to playerWords array
                if (word.ToLower() == "!end") //if !end is entered then the game ends
                {
                    Console.WriteLine("Thanks for playing!");
                    Console.WriteLine("Your final score is: " + playerPoints + " with " + playerWordCount + " words");
                    Console.WriteLine("Press any key to exit...");
                    loopInput = false;
                    loopGame = false;
                    stopGame = true;
                }
                if (stopGame == false ) //Checks if game should continue
                {
                    if (alreadyUsed == false) //Checks if the word has already been used
                    {
                        if (dictionary.CheckWord(word) == true) //Checks if the word is in the dictionary used the API program
                        {
                            if (checkLetters(word.ToLower()) == true) //Checks whether a word can be formed using the letters in the randomLetters array
                            {
                                Console.WriteLine("Word is valid");
                                calculatePoints(word);
                            }
                            else
                            {
                                Console.WriteLine("Word is invalid CL, try again");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Word is invalid CW, try again");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Word is invalid AU, try again");
                    }
                }
            }
        }

        public void calculatePoints(string word) //Calculates the points for the players entered words
        {
            playerPoints = 0;
            for (int i = 0; i < word.Length; i++) // Iterate through each letter of the word
            {
                int index = Array.IndexOf(scrabbleLetters, word[i].ToString()); // Get the index of the current letter in the scrabbleLetters array
                playerPoints += scrabblePoints[index]; // Add the corresponding points to the playerPoints total
            }
            totalPlayerPoints += playerPoints;
            playerWordCount++;
            Console.WriteLine("Your word is worth " + playerPoints + " points!");
            Console.WriteLine("Player Total Points: " + totalPlayerPoints);
        }

        public void generate() // Generate random letters for the player
        {
            generateLetters = true;
            while (generateLetters) //Loops through if the random letters generated do not contain vowels
            {
                for (int j = 0; j < 7; j++) // Generate random letters
                {
                    randomLetters[j] = scrabbleLetters[rnd.Next(scrabbleLetters.Length)];
                }
                if (randomLetters.Contains("a") || randomLetters.Contains("e") || randomLetters.Contains("i") || randomLetters.Contains("o") || randomLetters.Contains("u")) //checks whether the random letters contain a vowel
                {
                    generateLetters = false;
                    break;
                }
                else
                {
                    generateLetters = true;
                }
            }
        }

        public bool checkLetters(string word) //Checks whether a word can be formed using the letters in the randomLetters array
        {
            int[] letterCount = new int[26];
            for (int i = 0; i < randomLetters.Length; i++) // Store the number of occurrences of each letter in the randomLetters array
            {
                int index = Array.IndexOf(scrabbleLetters, randomLetters[i]);
                letterCount[index]++;
            }
            for (int i = 0; i < word.Length; i++) // Iterate through each letter in the inputted word
            {
                int index = Array.IndexOf(scrabbleLetters, word[i].ToString());
                if (letterCount[index] <= 0)
                {
                    return false;
                }
                letterCount[index]--;
            }
            return true;
        }
    }
}
