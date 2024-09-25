namespace HangmanGame;

public class State
{
    private readonly string[] Words;

    private readonly string _currentWord;
    private List<char> _guessedLetters;
    private int _lives;

    public State(string[] words)
    {
        Words = words;
        _currentWord = Words[Random.Shared.Next(0, Words.Length)];
        _guessedLetters = [];

        _lives = 5;
    }

    public void Draw()
    {
        string[] hangmenIterations =
        [

            @"

             --------

             |      |   

             |      O   

             |     /|\  

             |     / \  

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |     /|  

             |     / \  

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |      |  

             |     / \  

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |      |  

             |     /   

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |      |  

             |         

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |       

             |        

             |__________          

 

             ",

                @"

             --------

             |      |   

             |         

             |       

             |      

             |__________          

 

             "

        ];





        string hangman = @"

             --------

             |      |   

             |      O   

             |     /|\  

             |     / \  

             |__________          

 

             ";

        Console.Clear();

        var picture = hangmenIterations[_lives];
        Console.WriteLine(picture);

        foreach (char letter in _currentWord)
        {
            if (!_guessedLetters.Contains(letter))
            {
                Console.Write("_");

            }
            else
            {
                Console.Write(letter);
            }
        }

        Console.WriteLine();
    }
    public void welcome_screen()
    {
        string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
        Console.WriteLine($"{GREEN}Welcome to hangman.");
        Console.ResetColor();
    }
    public void Process()
    {
        Console.WriteLine("Enter your guess: ");
        var playerInput = Console.ReadLine()!.ToLower();
        if (playerInput.Length != 1)
        {
            Console.WriteLine("Please only write 1 letter");
            Process();
            return;
        }

        var input = playerInput[0];

        if (!"qwertyuiopasdfghjklzxcvbnm".Contains(input))
        {
            Console.WriteLine("Please only write letters");
            Process();
            return;
        }

        bool valid = false;

        for (int i = 0; i < _currentWord.Length; i++)
        {
            if (input == _currentWord[i])
            {
                valid = true;
                break;
            }
        }

        if (!valid)
        {
            _lives--;

        }

        _guessedLetters.Add(input);
        Draw();

        if (!HasLives())
        {
            Console.WriteLine("Game Over");
            Console.WriteLine($"The word was");
            Console.WriteLine(_currentWord);
        }
        else if (HasWon())
        {
            Console.WriteLine("You won!");
        }
        else
        {

            Process();
        }
    }

    public bool HasLives()
    {
        return _lives > 0;
    }

    public bool HasWon()
    {
        return _currentWord.All(c => _guessedLetters.Contains(c));
    }
}