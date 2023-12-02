
static void Part1()
{
    string allLines =
        """
        1abc2
        pqr3stu8vwx
        a1b2c3d4e5f
        treb7uchet
        """;

    //string[] lineArray = allLines.Split("\r\n");

    string[] lineArray = File.ReadAllLines("input.txt");

    int sum = 0;

    foreach (var line in lineArray)
    {
        char? firstNum = null;
        char? secondNum = null;

        foreach (char c in line)
        {
            if (int.TryParse(c + "", out int _))
            {
                if (!firstNum.HasValue)
                {
                    firstNum = c;
                }

                secondNum = c;
            }
        }

        sum += int.Parse("" + firstNum!.Value + secondNum!.Value);
    }

    Console.WriteLine(sum);
}

static void Part2()
{
    string allLines =
        """
        two1nine
        eightwothree
        abcone2threexyz
        xtwone3four
        4nineeightseven2
        zoneight234
        7pqrstsixteen
        """;

    //string[] lineArray = allLines.Split("\r\n");

    string[] lineArray = File.ReadAllLines("input.txt");

    int sum = 0;

    foreach (var line in lineArray)
    {
        char? firstNum = null;
        char? secondNum = null;
        int firstPos = -1;
        int secondPos = -1;
        
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (int.TryParse(c + "", out int _))
            {
                if (!firstNum.HasValue)
                {
                    firstNum = c;
                    firstPos = i;
                }

                secondNum = c;
                secondPos = i;
            }
        }


        int i1 = line.IndexOf("one");
        int i2 = line.IndexOf("two");
        int i3 = line.IndexOf("three");
        int i4 = line.IndexOf("four");
        int i5 = line.IndexOf("five");
        int i6 = line.IndexOf("six");
        int i7 = line.IndexOf("seven");
        int i8 = line.IndexOf("eight");
        int i9 = line.IndexOf("nine");

        if (i1 != -1 && (i1 < firstPos || firstPos == -1)) { firstPos = i1; firstNum = '1'; }
        if (i2 != -1 && (i2 < firstPos || firstPos == -1)) { firstPos = i2; firstNum = '2'; }
        if (i3 != -1 && (i3 < firstPos || firstPos == -1)) { firstPos = i3; firstNum = '3'; }
        if (i4 != -1 && (i4 < firstPos || firstPos == -1)) { firstPos = i4; firstNum = '4'; }
        if (i5 != -1 && (i5 < firstPos || firstPos == -1)) { firstPos = i5; firstNum = '5'; }
        if (i6 != -1 && (i6 < firstPos || firstPos == -1)) { firstPos = i6; firstNum = '6'; }
        if (i7 != -1 && (i7 < firstPos || firstPos == -1)) { firstPos = i7; firstNum = '7'; }
        if (i8 != -1 && (i8 < firstPos || firstPos == -1)) { firstPos = i8; firstNum = '8'; }
        if (i9 != -1 && (i9 < firstPos || firstPos == -1)) { firstPos = i9; firstNum = '9'; }


        int il1 = line.LastIndexOf("one");
        int il2 = line.LastIndexOf("two");
        int il3 = line.LastIndexOf("three");
        int il4 = line.LastIndexOf("four");
        int il5 = line.LastIndexOf("five");
        int il6 = line.LastIndexOf("six");
        int il7 = line.LastIndexOf("seven");
        int il8 = line.LastIndexOf("eight");
        int il9 = line.LastIndexOf("nine");

        if (il1 != -1 && (il1 > secondPos || secondPos == -1)) { secondPos = il1; secondNum = '1'; }
        if (il2 != -1 && (il2 > secondPos || secondPos == -1)) { secondPos = il2; secondNum = '2'; }
        if (il3 != -1 && (il3 > secondPos || secondPos == -1)) { secondPos = il3; secondNum = '3'; }
        if (il4 != -1 && (il4 > secondPos || secondPos == -1)) { secondPos = il4; secondNum = '4'; }
        if (il5 != -1 && (il5 > secondPos || secondPos == -1)) { secondPos = il5; secondNum = '5'; }
        if (il6 != -1 && (il6 > secondPos || secondPos == -1)) { secondPos = il6; secondNum = '6'; }
        if (il7 != -1 && (il7 > secondPos || secondPos == -1)) { secondPos = il7; secondNum = '7'; }
        if (il8 != -1 && (il8 > secondPos || secondPos == -1)) { secondPos = il8; secondNum = '8'; }
        if (il9 != -1 && (il9 > secondPos || secondPos == -1)) { secondPos = il9; secondNum = '9'; }

        sum += int.Parse("" + firstNum!.Value + secondNum!.Value);
    }

    Console.WriteLine(sum);
}

Part2();
