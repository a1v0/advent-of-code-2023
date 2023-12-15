# advent-of-code-2023

My code for calculating the answers for Advent of Code 2023, this time in C#.

The tasks can be found **[here](https://adventofcode.com/2023)**. Each day has its own class which refers to other classes that contain the logic for that day's task. Daily classes are stored in the `tasks` folder.

Inputs are store as plaintext in the `inputs` folder, e.g. `01.txt`, `15.txt` and so on. Files containing test data (i.e. the example data provided in the task description) are marked as `01.test.txt`.

This repo uses xUnit for testing, though my tests aren't particularly extensive. They mostly assert that the output from the test data matches what Advent of Code specifies.

| Day | Task 1 | Task 2 | Notes |
| --- | ------ | ------ | ----- |
| 01  | ⭐    | ⭐     |       |
| 02  | ⭐    | ⭐     |       |
| 03  | ⭐    | ⭐     | I sacrificed good practice for reusability. _Mea culpa._ Could do with a major refactor. |
| 04  | ⭐    | ⭐     |       |
| 05  | ⭐    | ❌     | Need to find a shortcut to the right answer, otherwise it takes too long to run. |
| 06  | ⭐    | ⭐     |       |
| 07  | ⭐    | ⭐     |       |
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

I wanted to set the path to the input files as an environment variable in an `appsettings.json` file but simply couldn't get it to work. I decided there's not too much harm in hard-coding the path inside the `GetRawInput` method.

### `RUN_MODE`

The `BaseTest` class, from which every day's test class inherits, has a constructor that sets the environment variable `RUN_MODE` to `TEST`. The `AOCUtils.GetRawInput` method chooses which input file to take based on this variable.

I don't know if this is the accepted way of doing a Jest-style `beforeAll`, but it certainly works!

### `SECONDARY_INPUT_INTERPOLATION`

Sometimes the second task of the day uses different demo data. To automate the loading of the correct data, I've used this (awkwardly named) environment variable. It will add a string to the file path retrieved by the `GetRawInput` method.

## How to run each challenge (with live data)

Enter the `AdventOfCode2023` folder and run `dotnet run -- x y`, where `x` is the day name and `y` is the problem name (can be 1 or 2). Any additional arguments will be ignored.

```sh
$ dotnet run -- 1 2
runs the second challenge of the first day
```

## Unit testing with xUnit (with test data)

Each day has its own test file and each test within that day has a trait of `dp` (day/part) of `x,y`, where `x` is the day and `y` is the part.

To run any specific test, use this command:

```sh
$ dotnet test --filter dp=1,2
                          ^^^ replace this value as necessary
```

The majority of tests will be simple `Assert.Equals` tests that ensure each `Solve` method returns what the test data requires.

On the assumption that the answers to the Advent of Code challenges will all either be numerical or string types, the `Solve` method returns a string every time. Challenges whose answer is numerical will be converted to a string before returning. This is important mainly for assertions in the test suite; the `Solve` method otherwise prints the solution to the console, rather than returning the value anywhere.

## Auto-generating daily files

To auto-generate boilerplate files for a day's tasks, open the `AdventOfCode2023.FileCreator` directory, run `dotnet run -- DAY_NAME` where `DAY_NAME` is the date you want files made for. It will create a blank class, a blank test file, and two empty input files.

## Input files

Having seen only after Day 7 that the daily inputs are subject to copyright, I have added all subsequent inputs to the `.gitignore` file. Next year I will make sure not to upload my inputs to GitHub. My apologies to Advent of Code.
