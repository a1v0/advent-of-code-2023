# advent-of-code-2023

My code for calculating the answers for Advent of Code 2023, this time in C#.

The tasks can be found **[here](https://adventofcode.com/2023)**. Each day has its own class which contains classes related to that day's task. Daily classes are stored in the `challenges` folder.

Inputs are store as plaintext in the `inputs` folder, e.g. `01.txt`, `15.txt` and so on. Test data files are marked as `01.test.txt`.

This repo uses xUnit for testing, though my tests aren't particularly extensive.

| Day | Task 1 | Task 2 | Notes |
| --- | ------ | ------ | ----- |
| 01  |        |        |       |
<!-- | 02  |        |        |       | -->
<!-- | 03  |        |        |       | -->
<!-- | 04  |        |        |       | -->
<!-- | 05  |        |        |       | -->
<!-- | 06  |        |        |       | -->
<!-- | 07  |        |        |       | -->
<!-- | 08  |        |        |       | -->
<!-- | 09  |        |        |       | -->
<!-- | 10  |        |        |       | -->
<!-- | 11  |        |        |       | -->
<!-- | 12  |        |        |       | -->
<!-- | 13  |        |        |       | -->
<!-- | 14  |        |        |       | -->
<!-- | 15  |        |        |       | -->
<!-- | 16  |        |        |       | -->
<!-- | 17  |        |        |       | -->
<!-- | 18  |        |        |       | -->
<!-- | 19  |        |        |       | -->
<!-- | 20  |        |        |       | -->
<!-- | 21  |        |        |       | -->
<!-- | 22  |        |        |       | -->
<!-- | 23  |        |        |       | -->
<!-- | 24  |        |        |       | -->
<!-- | 25  |        |        |       | -->

<!-- ❌⭐ emojis to copy/paste -->

## Environment variables

### `INPUT_PATH`

This is probably just my inexperience in C#, but I can't seem to find a simple way to access the solution's base folder at runtime.

This means that my `AOCUtils.GetRawInput` method can't work with a relative path to the input file. xUnit is looking for a folder called `advent-of-code-2023/AdventOfCode2023.Tests/bin/Debug/net7.0/AdventOfCode2023/inputs/`. Instead of jamming the path full of `../../../`, I thought I would set an environment variable of `INPUT_PATH`. This stores the full path to the `inputs` folder.

### `RUN_MODE`

The `BaseTest` class, from which every day's test class inherits, has a constructor that sets the environment variable `RUN_MODE` to `TEST`. The `AOCUtils.GetRawInput` method chooses which input file to take based on this variable.

I don't know if this is the accepted way of doing a Jest-style `beforeAll`, but it certainly works!

## How to run each challenge

# [PLACEHOLDER: THINK ABOUT HOW TO DO THIS. WE COULD HAVE AN ENORMOUS SWITCH STATEMENT THAT TAKES AN ARG FROM THE CONSOLE AND RUNS THE CHALLENGE ACCORDINGLY. IF NO DAY IS GIVEN, DISPLAY A MESSAGE. THAT'S MY PREFERRED OPTION]

## Unit testing with xUnit

Each day has its own test file and each test within that day has a trait of `dp` (day/part) of `x,y`, where `x` is the day and `y` is the part.

To run any specific test, use this command:

```sh
dotnet test --filter dp=1,2
                      # ^^^ replace this value as necessary
```

The majority of tests will be simple `Assert.Equals` tests that ensure each `Solve` method returns what the test data requires.
