static void Part1()
{
    string timeSheet =
        """
        Time:      7  15   30
        Distance:  9  40  200
        """;

    //string[] timeSheetLines = timeSheet.Split("\r\n");
    string[] timeSheetLines = File.ReadAllLines("input.txt");

    var times = timeSheetLines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
    var distances = timeSheetLines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

    int waysToBeatMultiplication = 1;
    for (int i = 0; i < times.Count; i++)
    {
        int waysToBeat = 0;
        for (int acceleration = 1; acceleration < times[i]; acceleration++)
        {
            int secondsSpentMoving = times[i] - acceleration;

            if (acceleration * secondsSpentMoving > distances[i])
            {
                waysToBeat++;
            }
        }
        waysToBeatMultiplication *= waysToBeat;
    }

    Console.WriteLine(waysToBeatMultiplication);
}


Part1();

static void Part2()
{
    string timeSheet =
        """
        Time:      7  15   30
        Distance:  9  40  200
        """;

    //string[] timeSheetLines = timeSheet.Split("\r\n");
    string[] timeSheetLines = File.ReadAllLines("input.txt");

    long time = long.Parse(timeSheetLines[0].Split(':')[1].Replace(" ", string.Empty));
    long distance = long.Parse(timeSheetLines[1].Split(':')[1].Replace(" ", string.Empty));

    int waysToBeat = 0;
    for (long acceleration = 1; acceleration < time; acceleration++)
    {
        long secondsSpentMoving = time - acceleration;

        if (acceleration * secondsSpentMoving > distance)
        {
            waysToBeat++;
        }
    }

    Console.WriteLine(waysToBeat);
}


Part2();