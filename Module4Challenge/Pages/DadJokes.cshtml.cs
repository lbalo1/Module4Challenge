using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Module4Challenge.Pages
{
    public class DadJokesModel : PageModel
    {
        // Array of 12 Dad Jokes
        private readonly string[] _jokesArray = new string[]
        {
            "I had a quiet game of tennis today. There was no racket.",
            "Why do melons have weddings? They cantelope.",
            "I went to the aquarium this weekend, but I didn’t stay long. There’s something fishy about that place.",
            "Why can't dinosaurs clap their hands? Because they're extinct.",
            "Who won the neck decorating contest? It was a tie.",
            "Dogs can't operate MRI machines. But catscan.",
            "How is my wallet like an onion? Every time I open it, I cry.",
            "Which vegetable has the best kung fu? Broc-lee.",
            "Why did the egg have a day off? Because it was Fryday.",
            "What word can you make shorter by adding two letters? Short.",
            "What happened when two slices of bread went on a date? It was loaf at first sight.",
            "Why do crabs never volunteer? Because they're shell-fish."
        };

        // Property to store current jokes to display
        public List<string> CurrentJokes { get; set; } = new List<string>();

        // Property to store how many jokes to show at once
        public int JokesToShow { get; set; } = 2;

        // Property to track previously displayed jokes to avoid repetition
        private List<string> _previousJokes = new List<string>();

        public void OnGet()
        {
            // Select random jokes on initial page load
            SelectRandomJokes();
        }

        public void OnPost()
        {
            // Handle button click to get more jokes
            SelectRandomJokes();
        }

        private void SelectRandomJokes()
        {
            Random random = new Random();
            CurrentJokes.Clear();

            // Select random jokes without showing the same ones twice in a row
            int jokesSelected = 0;
            int attempts = 0;
            int maxAttempts = 50; // Prevent infinite loop

            while (jokesSelected < JokesToShow && attempts < maxAttempts)
            {
                // Get a random index from the jokes array
                int randomIndex = random.Next(_jokesArray.Length);
                string selectedJoke = _jokesArray[randomIndex];

                // Check if this joke is not already in current selection
                // and wasn't in the previous selection
                if (!CurrentJokes.Contains(selectedJoke) && !_previousJokes.Contains(selectedJoke))
                {
                    CurrentJokes.Add(selectedJoke);
                    jokesSelected++;
                }

                attempts++;
            }

            // If we couldn't find enough unique jokes, just add any unique jokes to current selection
            if (CurrentJokes.Count < JokesToShow)
            {
                for (int i = 0; i < _jokesArray.Length && CurrentJokes.Count < JokesToShow; i++)
                {
                    if (!CurrentJokes.Contains(_jokesArray[i]))
                    {
                        CurrentJokes.Add(_jokesArray[i]);
                    }
                }
            }

            // Store current jokes as previous jokes for next request
            _previousJokes = new List<string>(CurrentJokes);
        }
    }
}
