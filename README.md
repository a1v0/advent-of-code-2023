# advent-of-code-2023

My code for calculating the answers for Advent of Code 2023, this time in C#.

The tasks can be found **[here](https://adventofcode.com/2023)**. Each day has its own folder which contains classes related to that day's task.

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

## Unit testing with xUnit

Each day has its own test file and each test within that day has a trait of `dp` (day/part) of `x,y`, where `x` is the day and `y` is the part.

To run any specific test, use this command:

```sh
dotnet test --filter dp=1,2
                      # ^^^ replace this value as necessary
```

The majority of tests will be simple `Assert.Equals` tests that ensure each `Solve` method returns what the test data requires.
