# EloElo

.NET implementation of the Elo Rating system

## Code Example

Below is a code example of how to use the Nuget Package

```
using EloElo;
using EloElo.Enums;

// Create Elo rating system defining the K rating
var eloRatingSystem = EloRatingFactory.GetRatingSystem(RatingSystemVariation.EloWithMasterKRating);

// Get a tuple represening the expected score as a decimal
Console.WriteLine(eloRatingSystem.GetExpectedScore(2000, 1800));

// Get a rating result object returned from entering a result for two participants
var ratingResult = eloRatingSystem.GetResultRating(2000, 1800, ResultType.ParticipantTwoWins);

Console.WriteLine($"P1 had an old rating of {ratingResult.ParticipantOne.OldRating}");
Console.WriteLine($"P1 now has a new rating of {ratingResult.ParticipantOne.NewRating}");
Console.WriteLine($"P2 difference between old and new rating is {ratingResult.ParticipantOne.RatingChange}");
```

## TODO

- [x] Implement K Variant for Elo Rating systems
  - [x] Novice
  - [x] Intermediate
  - [x] Expert
- [x] Implement xUnit tests to verify correctness
  - [x] EloBase
  - [x] Elo K variant Expert
  - [x] Elo K variant Intermediate
  - [x] Elo K variant Novice
- [ ] Setup test and build pipelines
- [x] Create Nuget Package
- [x] Add documentation and code examples
- [ ] Sort permissions issue with using Nuget Package
