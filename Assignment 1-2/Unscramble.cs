// See https://aka.ms/new-console-template for more information

using System;

namespace UnscrambleGame {
    
    class Unscramble
    {
        static void Main()
        {
            Console.WriteLine("Hello There!, This is Unscramble.");
            Console.WriteLine(" ");
            Console.WriteLine(@"Instructions:
                1. You are presented with a Scrambled Word.
                2. Unscramble the word by figuring out the word it was scrambled from.
                3. Type out your answer.
                4. You lose the game if you fail so think hard. GoodLuck!.
            ");
            int count = 0;
            String shuffled = "";
            String answer = "";
            do {
                Console.WriteLine($"Level {count + 1} / {WordDatabase.wordListLength}");
                shuffled = WordDatabase.pickRandomShuffledWord();
                Console.WriteLine($"Your word is: {shuffled}");

            // re-asks user for input
            RETRY:
                Console.WriteLine("Your answer: ");
                answer = Console.ReadLine();
                if(answer == null || answer == "")
                {
                    Console.WriteLine("You have to input a value.");
                    goto RETRY;
                }
                if (!WordDatabase.verifyUnscrambledWord(answer ?? " "))
                {
                    Console.WriteLine("Wrong");
                    Console.WriteLine($"Your Score is {count}");
                    Console.WriteLine($"Your Rank is {WordDatabase.playerRank(count)}");
                    break;
                }
                else
                {
                    Console.WriteLine("Correct!");
                    Console.WriteLine(" ");
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine(" ");
                    count++;
                    continue;
                }

            } while (count <= WordDatabase.wordListLength);
        }
    }
 }

/// <summary>
/// Class that abstracts useful methods.
/// </summary>
class WordDatabase
{
    private static readonly List<String> words = new List<String> { "animal",
        "auto",
        "anecdote",
        "alphabet",
        "all",
        "awesome",
        "arise",
        "balloon",
        "basket",
        "bench",
        "best",
        "birthday",
        "book",
        "briefcase",
        "camera",
        "camping",
        "candle",
        "cat",
        "cauliflower",
        "chat",
        "children",
        "class",
        "classic",
        "classroom",
        "coffee",
        "colorful",
        "cookie",
        "creative",
        "cruise",
        "dance",
        "daytime",
        "dinosaur",
        "doorknob",
        "dine",
        "dream",
        "dusk",
        "eating",
        "elephant",
        "emerald",
        "eerie",
        "electric",
        "finish",
        "flowers",
        "follow",
        "fox",
        "frame",
        "free",
        "frequent",
        "funnel",
        "green",
        "guitar",
        "grocery",
        "glass",
        "great",
        "giggle",
        "haircut",
        "half",
        "homemade",
        "happen",
        "honey",
        "hurry",
        "hundred",
        "ice",
        "igloo",
        "invest",
        "invite",
        "icon",
        "introduce",
        "joke",
        "jovial",
        "journal",
        "jump",
        "join",
        "kangaroo",
        "keyboard",
        "kitchen",
        "koala",
        "kind",
        "kaleidoscope",
        "landscape",
        "late",
        "laugh",
        "learning",
        "lemon",
        "letter",
        "lily",
        "magazine",
        "marine",
        "marshmallow",
        "maze",
        "meditate",
        "melody",
        "minute",
        "monument",
        "moon",
        "motorcycle",
        "mountain",
        "music",
        "north",
        "nose",
        "night",
        "name",
        "never",
        "negotiate",
        "number",
        "opposite",
        "octopus",
        "oak",
        "order",
        "open",
        "polar",
        "pack",
        "painting",
        "person",
        "picnic",
        "pillow",
        "pizza",
        "podcast",
        "presentation",
        "puppy",
        "puzzle",
        "recipe",
        "release",
        "restaurant",
        "revolve",
        "rewind",
        "room",
        "run",
        "secret",
        "seed",
        "ship",
        "shirt",
        "should",
        "small",
        "spaceship",
        "stargazing",
        "skill",
        "street",
        "style",
        "sunrise",
        "taxi",
        "tidy",
        "timer",
        "together",
        "tooth",
        "tourist",
        "travel",
        "truck",
        "under",
        "useful",
        "unicorn",
        "unique",
        "uplift",
        "uniform",
        "vase",
        "violin",
        "visitor",
        "vision",
        "volume",
        "view",
        "walrus",
        "wander",
        "world",
        "winter",
        "well",
        "whirlwind",
        "x-ray",
        "xylophone",
        "yoga",
        "yogurt",
        "yoyo",
        "you",
        "year",
        "yummy",
        "zebra",
        "zigzag",
        "zoology",
        "zone",
        "zeal"
    };

    private static HashSet<String> usedWords = new HashSet<String>();
    public static String currentWord = "";
    public static int wordListLength = words.Count;

    /// <summary>
    /// Pics a random word from word list
    /// </summary>
    /// <returns> Shuffled word </returns>
    public static String pickRandomShuffledWord()
    {
        Random random = new Random();
        int index = random.Next(words.Count);
        currentWord = words[index];
        while (usedWords.Contains(currentWord))
        {
          pickRandomShuffledWord();
        }
        usedWords.Add(currentWord);
        return ShuffleCurrentWord(currentWord);
    }

    /// <summary>
    /// Takes in a string and shuffles its characters
    /// </summary>
    /// <param name="word">Selected word to be shuffled</param>
    /// <returns>Shuffled String</returns>
    private static String ShuffleCurrentWord(String word)
    {
        Random random = new Random();
        Char[] tempWord = word.ToCharArray();
        for (int i = 0; i < tempWord.Length; i++ )
        {
            int j = random.Next(i + 1);
            Char copy = tempWord[i];
            tempWord[i] = tempWord[j];
            tempWord[j] = copy;
        }
        String shuffledWord = new String(tempWord);
        return shuffledWord;
    }

    /// <summary>
    /// Verifies user inputed word against actual word
    /// </summary>
    /// <param name="word">User input to be verified</param>
    /// <returns>True if matches else false</returns>

    public static Boolean verifyUnscrambledWord(String word)
    {
        if (word == currentWord) return true;
        return false;
    }

    // For Assignment 2:  Either playerRank() or playerRank2() can be used to achieve the same result.
    /// <summary>
    /// Receives user score and grades the user's skill based on the Score.
    /// </summary>
    /// <param name="score">User's score from the game</param>
    /// <returns>User skill Level</returns>
    public static String playerRank(int score)
    {
        int playerScore = calculatePercent(score, wordListLength);
        String playerRank = "";

        switch (playerScore)
        {
            case int n when (n == 0):
                playerRank = "Loser";
                break;
            case int n when (n <= 10):
                playerRank = "Novice";
                break;
            case int n when (n <= 20):
                playerRank = "Rookie";
                break;
            case int n when (n <= 30):
                playerRank = "Amateur";
                break;
            case int n when (n <= 40):
                playerRank = "Skilled";
                break;
            case int n when (n <= 50):
                playerRank = "Experienced";
                break;
            case int n when (n <= 60):
                playerRank = "Professional";
                break;
            case int n when (n <= 70):
                playerRank = "Expert";
                break;
            case int n when (n <= 80):
                playerRank = "Veteran";
                break;
            case int n when (n <= 90):
                playerRank = "Master";
                break;
            case int n when (n <= 100):
                playerRank = "GrandMaster";
                break;
            default:
                playerRank = "Loser";
                break;
        }
        return playerRank;
    }

    // OR

    public static String playerRank2(int score)
    {
        int playerScore = calculatePercent(score, wordListLength);
        if(playerScore == 0)
        {
            return "Loser";
        }
        else if (playerScore <= 10)
        {
            return "Novice";
        }else if(playerScore <= 20)
        {
            return "Rookie";
        }else if (playerScore <= 30)
        {
            return "Amateur";
        }else if (playerScore <= 40)
        {
            return "Skilled";
        }else if (playerScore <= 50)
        {
            return "Experienced";
        }else if(playerScore <= 60)
        {
            return "Professional";
        }else if(playerScore <= 70)
        {
            return "Expert";
        }else if (playerScore <= 80)
        {
            return "Veteran";
        }else if (playerScore <= 90)
        {
            return "Master";
        }else if (playerScore <= 100)
        {
            return "GrandMaster";
        }
        else
        {
            return "Loser";
        };

    }
        private static int calculatePercent(int score, int total)
    {
        return (score / total) * 100;
    }
}