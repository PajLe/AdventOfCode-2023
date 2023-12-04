
using System.Text;

static void Part1()
{
    string engineSchemaString =
        """
        467..114..
        ...*......
        ..35..633.
        ......#...
        617*......
        .....+.58.
        ..592.....
        ......755.
        ...$.*....
        .664.598..
        """;

    //string[] engineSchema = engineSchemaString.Split("\r\n");
    string[] engineSchema = File.ReadAllLines("input.txt");

    bool isPartNumber = false;
    StringBuilder number = new();
    int sumOfPartNumbers = 0;
    for (int i = 0; i < engineSchema.Length; i++)
    {
        for (int j = 0; j < engineSchema[i].Length; j++)
        {

            if (char.IsDigit(engineSchema[i][j]))
            {
                number.Append(engineSchema[i][j]);

                if (CheckIfPartNumber(engineSchema, i, j))
                    isPartNumber = true;
            }
            else
            {
                if (isPartNumber)
                {
                    sumOfPartNumbers += int.Parse(number.ToString());
                }
                isPartNumber = false;
                number.Clear();
            }
        }
    }

    if (isPartNumber)
    {
        sumOfPartNumbers += int.Parse(number.ToString());
    }

    Console.WriteLine(sumOfPartNumbers);
}

static bool CheckIfPartNumber(string[] engineSchema, int i, int j)
{
    int iTopLeft = i - 1, iTop = i - 1, iTopRight = i - 1, iLeft = i, iRight = i, iBottomLeft = i + 1, iBottom = i + 1, iBottomRight = i + 1;
    int jTopLeft = j - 1, jTop = j, jTopRight = j + 1, jLeft = j - 1, jRight = j + 1, jBottomLeft = j - 1, jBottom = j, jBottomRight = j + 1;

    if (iTopLeft > -1 && jTopLeft > -1 && IsEngineSymbol(engineSchema[iTopLeft][jTopLeft])) return true;
    if (iTop > -1 && IsEngineSymbol(engineSchema[iTop][jTop])) return true;
    if (iTopRight > -1 && jTopRight < engineSchema[iTopRight].Length && IsEngineSymbol(engineSchema[iTopRight][jTopRight])) return true;
    if (jLeft > -1 && IsEngineSymbol(engineSchema[iLeft][jLeft])) return true;
    if (jRight < engineSchema[iRight].Length && IsEngineSymbol(engineSchema[iRight][jRight])) return true;
    if (iBottomLeft < engineSchema.Length && jBottomLeft > -1 && IsEngineSymbol(engineSchema[iBottomLeft][jBottomLeft])) return true;
    if (iBottom < engineSchema.Length && IsEngineSymbol(engineSchema[iBottom][jBottom])) return true;
    if (iBottomRight < engineSchema.Length && jBottomRight < engineSchema[iBottomRight].Length && IsEngineSymbol(engineSchema[iBottomRight][jBottomRight])) return true;

    return false;
}

static bool IsEngineSymbol(char c)
{
    return !char.IsDigit(c) && c != '.';
}

static void Part2()
{
    string engineSchemaString =
        """
        467..114..
        ...*......
        ..35..633.
        ......#...
        617*......
        .....+.58.
        ..592.....
        ......755.
        ...$.*....
        .664.598..
        """;

    //string[] engineSchema = engineSchemaString.Split("\r\n");
    string[] engineSchema = File.ReadAllLines("input.txt");

    StringBuilder number = new();
    Dictionary<Star, List<int>> allStarsWithAdjacentNumbers = [];
    HashSet<Star> stars = [];
    for (int i = 0; i < engineSchema.Length; i++)
    {
        for (int j = 0; j < engineSchema[i].Length; j++)
        {

            if (char.IsDigit(engineSchema[i][j]))
            {
                number.Append(engineSchema[i][j]);

                GetAdjacentStars(engineSchema, i, j).ForEach(star => stars.Add(star));
            }
            else if (number.Length > 0) 
            {
                foreach (Star star in stars)
                {
                    if (allStarsWithAdjacentNumbers.TryGetValue(star, out List<int>? numbersRelatedToTheStar))
                    {
                        numbersRelatedToTheStar.Add(int.Parse(number.ToString()));
                    }
                    else
                    {
                        allStarsWithAdjacentNumbers.Add(star, [int.Parse(number.ToString())]);
                    }
                }
                stars.Clear();
                number.Clear();
            }
        }
    }

    if (number.Length > 0)
    {
        foreach (Star star in stars)
        {
            if (allStarsWithAdjacentNumbers.TryGetValue(star, out List<int>? numbersRelatedToTheStar))
            {
                numbersRelatedToTheStar.Add(int.Parse(number.ToString()));
            }
            else
            {
                allStarsWithAdjacentNumbers.Add(star, [int.Parse(number.ToString())]);
            }
        }
    }

    int gearRatioSum = 0;
    foreach (var kvp in allStarsWithAdjacentNumbers.Where(kvp => kvp.Value.Count == 2))
    {
        int gearRatio = kvp.Value[0] * kvp.Value[1];
        gearRatioSum += gearRatio;
    }

    Console.WriteLine(gearRatioSum);
}


static List<Star> GetAdjacentStars(string[] engineSchema, int i, int j)
{
    int iTopLeft = i - 1, iTop = i - 1, iTopRight = i - 1, iLeft = i, iRight = i, iBottomLeft = i + 1, iBottom = i + 1, iBottomRight = i + 1;
    int jTopLeft = j - 1, jTop = j, jTopRight = j + 1, jLeft = j - 1, jRight = j + 1, jBottomLeft = j - 1, jBottom = j, jBottomRight = j + 1;

    List<Star> stars = [];

    if (iTopLeft > -1 && jTopLeft > -1 && engineSchema[iTopLeft][jTopLeft] == '*') stars.Add(new Star(iTopLeft, jTopLeft));
    if (iTop > -1 && engineSchema[iTop][jTop] == '*') stars.Add(new Star(iTop, jTop));
    if (iTopRight > -1 && jTopRight < engineSchema[iTopRight].Length && engineSchema[iTopRight][jTopRight] == '*') stars.Add(new Star(iTopRight, jTopRight));
    if (jLeft > -1 && engineSchema[iLeft][jLeft] == '*') stars.Add(new Star(iLeft, jLeft));
    if (jRight < engineSchema[iRight].Length && engineSchema[iRight][jRight] == '*') stars.Add(new Star(iRight, jRight));
    if (iBottomLeft < engineSchema.Length && jBottomLeft > -1 && engineSchema[iBottomLeft][jBottomLeft] == '*') stars.Add(new Star(iBottomLeft, jBottomLeft));
    if (iBottom < engineSchema.Length && engineSchema[iBottom][jBottom] == '*') stars.Add(new Star(iBottom, jBottom));
    if (iBottomRight < engineSchema.Length && jBottomRight < engineSchema[iBottomRight].Length && engineSchema[iBottomRight][jBottomRight] == '*') stars.Add(new Star(iBottomRight, jBottomRight));

    return stars;
}

//Part1();
Part2();

class Star(int i, int j)
{
    public int i { get; set; } = i;
    public int j { get; set; } = j;

    public override int GetHashCode()
    {
        return HashCode.Combine(i, j);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is Star s)
        {
            return s.i == i && s.j == j;
        }
        return base.Equals(obj);
    }
}