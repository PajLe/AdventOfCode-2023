static void Part1()
{
    string cardsString =
        """
        Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
        Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
        Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
        """;

    //string[] cardsSplit = cardsString.Split("\r\n");
    string[] cardsSplit = File.ReadAllLines("input.txt");

    int allCardsPointSum = 0;
    foreach (string cardLine in cardsSplit)
    {
        string[] cardParts = cardLine.Split(':');

        int cardIndex = int.Parse(cardParts[0].Substring("Card".Length + 1));

        string[] numbersLists = cardParts[1].Split("|");

        string winningList = numbersLists[0];
        string ourList = numbersLists[1];

        HashSet<int> winningNumbers = winningList.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        int numberOfMatches = 0;
        foreach (int num in ourList.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
        {
            if (winningNumbers.Contains(num))
                numberOfMatches++;
        }

        allCardsPointSum += (int)Math.Pow(2, (numberOfMatches - 1));
    }

    Console.WriteLine(allCardsPointSum);
}

static void Part2()
{
    string cardsString =
       """
        Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
        Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
        Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
        """;

    //string[] cardsSplit = cardsString.Split("\r\n");
    string[] cardsSplit = File.ReadAllLines("input.txt");

    int[] numberOfCopies = new int[cardsSplit.Length];
    foreach (string cardLine in cardsSplit)
    {
        string[] cardParts = cardLine.Split(':');

        int cardNumber = int.Parse(cardParts[0].Substring("Card".Length + 1));
        int cardIndex = cardNumber - 1;

        string[] numbersLists = cardParts[1].Split("|");

        string winningList = numbersLists[0];
        string ourList = numbersLists[1];

        HashSet<int> winningNumbers = winningList.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        int numberOfMatches = 0;
        foreach (int num in ourList.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
        {
            if (winningNumbers.Contains(num))
                numberOfMatches++;
        }

        for (int i = cardIndex + 1, j = 0; j < numberOfMatches && i < numberOfCopies.Length; i++, j++)
        {
            numberOfCopies[i] += numberOfCopies[cardIndex] + 1;
        }
    }

    Console.WriteLine(numberOfCopies.Sum(x => x + 1));
}



//Part1();
Part2();
