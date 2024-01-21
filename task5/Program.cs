using task5;
using Range = task5.Range;

public static partial class Program
{
    private static Dictionary<long, long> seedToSoil = [];
    private static Dictionary<long, long> soilToFertilizer = [];
    private static Dictionary<long, long> fertilizerToWater = [];
    private static Dictionary<long, long> waterToLight = [];
    private static Dictionary<long, long> lightToTemperature = [];
    private static Dictionary<long, long> temperatureToHumidity = [];
    private static Dictionary<long, long> humidityToLocation = [];

    static void Part1()
    {

        string almanac =
            """
        seeds: 79 14 55 13

        seed-to-soil map:
        50 98 2
        52 50 48

        soil-to-fertilizer map:
        0 15 37
        37 52 2
        39 0 15

        fertilizer-to-water map:
        49 53 8
        0 11 42
        42 0 7
        57 7 4

        water-to-light map:
        88 18 7
        18 25 70

        light-to-temperature map:
        45 77 23
        81 45 19
        68 64 13

        temperature-to-humidity map:
        0 69 1
        1 0 69

        humidity-to-location map:
        60 56 37
        56 93 4
        """;

        //string[] almanacLines = almanac.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        string[] almanacLines = File.ReadAllText("input.txt").Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        string seedsLine = almanacLines[0];
        List<long> seeds = seedsLine.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        //List<long> seeds = [1950497840]; // use seed from part 2 here to find the location

        string currentSource, currentDestination;
        Dictionary<long, long> currentMap = null!;
        Dictionary<long, long>? previousMap = null;
        foreach (string almanacLine in almanacLines.Skip(1))
        {
            if (almanacLine.Contains("map"))
            {
                var mapDesc = almanacLine.Split(' ')[0].Split('-');
                currentSource = mapDesc[0];
                currentDestination = mapDesc[2];
                currentMap = GetCurrentMap(currentSource, currentDestination);
                previousMap = GetPreviousMap(currentSource, currentDestination);
            }
            else
            {
                long[] numbers = almanacLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                long source = numbers[1];
                long destination = numbers[0];
                long length = numbers[2];

                if (previousMap == null)
                {
                    foreach (long seed in seeds)
                    {
                        if (source <= seed && seed < source + length)
                        {
                            long seedPosition = seed - source;
                            long correspondingSoil = destination + seedPosition;
                            seedToSoil[seed] = correspondingSoil;
                        }
                        else
                        {
                            seedToSoil.TryAdd(seed, seed);
                        }
                    }
                }
                else
                {
                    foreach (long src in previousMap.Values)
                    {
                        if (source <= src && src < source + length)
                        {
                            long seedPosition = src - source;
                            long correspondingSoil = destination + seedPosition;
                            currentMap[src] = correspondingSoil;
                        }
                        else
                        {
                            currentMap.TryAdd(src, src);
                        }
                    }
                }
            }
        }

        long lowestLocation = long.MaxValue;
        long seedForLowestLocation = -1;
        foreach (long seed in seeds)
        {
            long soil = seedToSoil[seed];
            long fertilizer = soilToFertilizer[soil];
            long water = fertilizerToWater[fertilizer];
            long light = waterToLight[water];
            long temperature = lightToTemperature[light];
            long humidity = temperatureToHumidity[temperature];
            long location = humidityToLocation[humidity];

            if (location < lowestLocation)
            {
                lowestLocation = location;
                seedForLowestLocation = seed;
            }
        }

        Console.WriteLine(lowestLocation);
        Console.WriteLine(seedForLowestLocation);
    }

    private static Dictionary<long, long>? GetPreviousMap(string currentSource, string currentDestination)
    {
        if (currentSource == "seed" && currentDestination == "soil")
            return null;
        else if (currentSource == "soil" && currentDestination == "fertilizer")
            return seedToSoil;
        else if (currentSource == "fertilizer" && currentDestination == "water")
            return soilToFertilizer;
        else if (currentSource == "water" && currentDestination == "light")
            return fertilizerToWater;
        else if (currentSource == "light" && currentDestination == "temperature")
            return waterToLight;
        else if (currentSource == "temperature" && currentDestination == "humidity")
            return lightToTemperature;
        else
            return temperatureToHumidity;
    }

    static Dictionary<long, long> GetCurrentMap(string currentSource, string currentDestination)
    {
        if (currentSource == "seed" && currentDestination == "soil")
            return seedToSoil;
        else if (currentSource == "soil" && currentDestination == "fertilizer")
            return soilToFertilizer;
        else if (currentSource == "fertilizer" && currentDestination == "water")
            return fertilizerToWater;
        else if (currentSource == "water" && currentDestination == "light")
            return waterToLight;
        else if (currentSource == "light" && currentDestination == "temperature")
            return lightToTemperature;
        else if (currentSource == "temperature" && currentDestination == "humidity")
            return temperatureToHumidity;
        else
            return humidityToLocation;
    }

    private static SortedDictionary<Range, Range> locationToHumidityRanges = new(new RangeComparer());
    private static SortedDictionary<Range, Range> humidityToTemperatureRanges = new(new RangeComparer());
    private static SortedDictionary<Range, Range> temperatureToLightRanges = new(new RangeComparer());
    private static SortedDictionary<Range, Range> lightToWaterRanges = new(new RangeComparer());
    private static SortedDictionary<Range, Range> waterToFertilizerRanges = new(new RangeComparer());
    private static SortedDictionary<Range, Range> fertilizerToSoilRanges = new(new RangeComparer());
    private static SortedDictionary<Range, Range> soilToSeedRanges = new(new RangeComparer());
    private static SortedSet<Range> seedRanges = new(new RangeComparer());

    static void Part2()
    {
        string almanac =
            """
        seeds: 79 14 55 13

        seed-to-soil map:
        50 98 2
        52 50 48

        soil-to-fertilizer map:
        0 15 37
        37 52 2
        39 0 15

        fertilizer-to-water map:
        49 53 8
        0 11 42
        42 0 7
        57 7 4

        water-to-light map:
        88 18 7
        18 25 70

        light-to-temperature map:
        45 77 23
        81 45 19
        68 64 13

        temperature-to-humidity map:
        0 69 1
        1 0 69

        humidity-to-location map:
        60 56 37
        56 93 4
        """;

        //string[] almanacLines = almanac.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        string[] almanacLines = File.ReadAllText("input.txt").Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        PreprocessMaps(almanacLines);
        long seed = -1;
        foreach (KeyValuePair<Range, Range> locToHumidity in locationToHumidityRanges)
        {
            var nextMap = humidityToTemperatureRanges;
            seed = FindSeed(locToHumidity.Value, nextMap, nameof(humidityToTemperatureRanges));
            if (seed != -1)
                break;
        }

        Console.WriteLine($"Use this seed in part1 to get the lowest location: {seed}");
    }

    private static void PreprocessMaps(string[] almanacLines)
    {
        var currentMap = locationToHumidityRanges;
        var currentMapName = nameof(locationToHumidityRanges);
        for (int i = almanacLines.Length - 1; i >= 0; i--)
        {
            if (almanacLines[i].Contains("seeds:"))
            {
                var seedInfos = almanacLines[i].Split(':')[1].Trim().Split(' ');
                long seedStart = -1;
                for (int j = 0; j < seedInfos.Length; j++)
                {
                    if (j %  2 == 0)
                    {
                        seedStart = long.Parse(seedInfos[j]);
                    }
                    else
                    {
                        long seedLength = long.Parse(seedInfos[j]);
                        Range seedRange = new(seedStart, seedStart + seedLength - 1);
                        seedRanges.Add(seedRange);
                    }
                }
            }
            else if (almanacLines[i].Contains("map:"))
            {
                (currentMap, currentMapName) = GetNextMapByCurrentMapName(currentMapName);
            }
            else
            {
                long[] numbers = almanacLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                long source = numbers[0];
                long destination = numbers[1];
                long length = numbers[2];

                Range srcRange = new(source, source + length - 1);
                Range destRange = new(destination, destination + length - 1);
                currentMap.Add(srcRange, destRange);
            }
        }

        List<Range> inbetweenRanges = [];
        for (int i = 0; i < locationToHumidityRanges.Keys.Count - 1; i++)
        {
            Range inbetweenRange;
            var currentRange = locationToHumidityRanges.ElementAt(i).Key;
            var nextRange = locationToHumidityRanges.ElementAt(i + 1).Key;

            if (i == 0)
            {
                var zeroStart = new Range(0, currentRange.Start - 1);
                if (zeroStart.IsValid())
                {
                    inbetweenRanges.Add(zeroStart);
                }
            }
            else if (i == locationToHumidityRanges.Count - 2)
            {
                var infinityEnd = new Range(nextRange.End + 1, long.MaxValue);
                if (infinityEnd.IsValid())
                {
                    inbetweenRanges.Add(infinityEnd);
                }
            }

            inbetweenRange = new(currentRange.End + 1, nextRange.Start - 1);
            
            if (inbetweenRange.IsValid())
            {
                inbetweenRanges.Add(inbetweenRange);
            }
        }

        foreach (var range in inbetweenRanges)
        {
            locationToHumidityRanges.Add(range, range);
        }
    }

    static (SortedDictionary<Range, Range>, string) GetNextMapByCurrentMapName(string currentMapName)
    {
        if (currentMapName == nameof(locationToHumidityRanges))
            return (humidityToTemperatureRanges, nameof(humidityToTemperatureRanges));
        else if (currentMapName == nameof(humidityToTemperatureRanges))
            return (temperatureToLightRanges, nameof(temperatureToLightRanges));
        else if (currentMapName == nameof(temperatureToLightRanges))
            return (lightToWaterRanges, nameof(lightToWaterRanges));
        else if (currentMapName == nameof(lightToWaterRanges))
            return (waterToFertilizerRanges, nameof(waterToFertilizerRanges));
        else if (currentMapName == nameof(waterToFertilizerRanges))
            return (fertilizerToSoilRanges, nameof(fertilizerToSoilRanges));
        else if (currentMapName == nameof(fertilizerToSoilRanges))
            return (soilToSeedRanges, nameof(soilToSeedRanges));
        else
            return (null, null)!;
    }


    static long FindSeed(Range range, SortedDictionary<Range, Range> map, string mapName)
    {
        if (mapName == null)
        {
            foreach (Range seedRange in seedRanges)
            {
                long minContainedNum = seedRange.MinimalContainedNumber(range);
                if (minContainedNum > -1)
                {
                    return minContainedNum;
                }
            }
            return -1;
        }
        else
        {
            long seed = -1;
            var nextMap = GetNextMapByCurrentMapName(mapName);
            bool fullRangeCovered = false;

            foreach (var srcDestRange in map)
            {
                if (seed != -1 || fullRangeCovered)
                {
                    break;
                }

                var srcRange = srcDestRange.Key;
                var destRange = srcDestRange.Value;

                if (range.End < srcRange.Start)
                {
                    seed = FindSeed(range, nextMap.Item1, nextMap.Item2);
                    fullRangeCovered = true;
                }
                else if (range.Start < srcRange.Start && range.End <= srcRange.End)
                {
                    Range leftRange = new(range.Start, srcRange.Start - 1);
                    Range rightRange = new(srcRange.Start, range.End);

                    long leftSeed = FindSeed(leftRange, nextMap.Item1, nextMap.Item2);

                    Range rightDestRange = new(destRange.Start, destRange.Start + rightRange.Length);
                    long rightSeed = FindSeed(rightDestRange, nextMap.Item1, nextMap.Item2);

                    if (leftSeed != -1)
                    {
                        seed = leftSeed;
                    }
                    else
                    {
                        seed = rightSeed;
                    }
                    fullRangeCovered = true;

                }
                else if (srcRange.Start <= range.Start && range.End <= srcRange.End)
                {
                    long startDistance = range.Start - srcRange.Start;
                    long endDistance = srcRange.End - range.End;

                    Range realDestRange = new(destRange.Start + startDistance, destRange.End - endDistance);
                    seed = FindSeed(realDestRange, nextMap.Item1, nextMap.Item2);
                    fullRangeCovered = true;
                }
                else if (range.Start <= srcRange.End && srcRange.End < range.End)
                {
                    Range leftRange = new(range.Start, srcRange.End);

                    Range leftDestRange = new(destRange.End - leftRange.Length, destRange.End);
                    seed = FindSeed(leftDestRange, nextMap.Item1, nextMap.Item2);
                    range = new(srcRange.End + 1, range.End);
                }

            }

            if (seed == -1 && !fullRangeCovered)
            {
                seed = FindSeed(range, nextMap.Item1, nextMap.Item2);
            }

            return seed;
        }
    }

    static void Main()
    {
        //Part1();
        Part2();
    }
}

