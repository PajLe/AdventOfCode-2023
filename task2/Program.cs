using System.Text;

static void Part1()
{
    string allGames =
        """
        Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
        Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
        Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
        Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
        """;

    //string[] games = allGames.Split("\r\n");

    string[] games = File.ReadAllLines("input.txt");

    int idSum = 0;
    foreach (var gameLine in games) 
    {
        var gameInfo = gameLine.Split(':');

        int gameIndex = int.Parse(gameInfo[0].Substring(gameInfo[0].IndexOf(' ')));

        var allBallsPicked = gameInfo[1];

        string[] rounds = allBallsPicked.Split(';');

        bool gameIsPossible = true;
        foreach (var round in rounds)
        {
            int indexRed = round.IndexOf("red");
            int indexGreen = round.IndexOf("green");
            int indexBlue = round.IndexOf("blue");

            int numRed = GetNumber(round, indexRed);
            int numGreen = GetNumber(round, indexGreen);
            int numBlue = GetNumber(round, indexBlue);

            if (numRed > 12 || numGreen > 13 || numBlue > 14)
            {
                gameIsPossible = false;
                break;
            }
        }

        if (gameIsPossible)
        {
            idSum += gameIndex;
        }
    }

    Console.WriteLine(idSum);

}

static void Part2()
{
    string allGames =
        """
        Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
        Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
        Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
        Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
        """;

    //string[] games = allGames.Split("\r\n");

    string[] games = File.ReadAllLines("input.txt");

    int sumPower = 0;
    foreach (var gameLine in games)
    {
        var gameInfo = gameLine.Split(':');

        int gameIndex = int.Parse(gameInfo[0].Substring(gameInfo[0].IndexOf(' ')));

        var allBallsPicked = gameInfo[1];

        string[] rounds = allBallsPicked.Split(';');

        int minPossibleRed = 0;
        int minPossibleGreen = 0;
        int minPossibleBlue = 0;
        foreach (var round in rounds)
        {
            int indexRed = round.IndexOf("red");
            int indexGreen = round.IndexOf("green");
            int indexBlue = round.IndexOf("blue");

            int numRed = GetNumber(round, indexRed);
            int numGreen = GetNumber(round, indexGreen);
            int numBlue = GetNumber(round, indexBlue);

            if (numRed > minPossibleRed)
            {
                minPossibleRed = numRed;
            }
            if (numGreen > minPossibleGreen)
            {
                minPossibleGreen = numGreen;
            }
            if (numBlue > minPossibleBlue)
            {
                minPossibleBlue = numBlue;
            }
        }

        int power = minPossibleRed * minPossibleGreen * minPossibleBlue;
        sumPower += power;
    }

    Console.WriteLine(sumPower);

}

static int GetNumber(string round, int colorIndex)
{
    if (colorIndex == -1)
        return -1;

    StringBuilder number = new StringBuilder();
    for (int i = colorIndex - 2; i >= 0; i--)
    {
        if (!char.IsDigit(round[i]))
            break;

        number.Insert(0, round[i]);
    }

    return int.Parse(number.ToString());
}

//Part1();
Part2();