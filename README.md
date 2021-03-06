# Kinoheld API Client

[![Build status master](https://ci.appveyor.com/api/projects/status/b982ewnsagvbyd5i?svg=true&passingText=master%20-%20passing&failingText=master%20-%20failing&pendingText=master%20-%20pending)](https://ci.appveyor.com/project/janniksam/kinoheld) 
[![Build status dev](https://ci.appveyor.com/api/projects/status/b982ewnsagvbyd5i/branch/dev?svg=true&passingText=dev%20-%20passing&failingText=dev%20-%20failing&pendingText=dev%20-%20pending)](https://ci.appveyor.com/project/janniksam/kinoheld/branch/dev)
[![NuGet version](https://badge.fury.io/nu/Kinoheld.Api.Client.svg)](https://badge.fury.io/nu/Kinoheld.Api.Client)

## Description

Kinoheld API Client is a client for the GraphQL-API, which is provided by [kinoheld.de](https://www.kinoheld.de).
The client currently supports:
- Searching a cinema by giving a city, a search term and a maximum distance between the city and the cinema.
- Retrieving information for movies that are currently played / will be played at the given cinema.
- Searching for city by giving a searchterm (e.g. a postal code)

Please bear in mind [kinoheld.de](https://www.kinoheld.de) only lists cinemas that are Germany.

## Basis usage:
 
### Search for a cinema

```cs
// Get all cinemas that are near the city "Aurich"
var client = new KinoheldClient();
var cinemas = await client.GetCinemas("aurich");

// Search for cinemas near Aurich that contain the term 'autokino'
var client = new KinoheldClient();
var cinemas = await client.GetCinemas("aurich", "autokino");
 ```
### Retrieve information about upcoming movies

```cs
// Retrieve all movies, that will be played tommorow
var client = new KinoheldClient();
var upcoming = await client.GetShows(cinema.Id, DateTime.Today.AddDays(1));   
 ```

### Dynamic queries

You can use dynamic queries to only get back the information you need. That way you can save transmission overhead.

```cs
//Retrieve only ID and Name of all cinemas near the City "Aurich"
IKinoheldClient client = new KinoheldClient();
var dynamicQuery = GetCinemasDynamicQuery.Id | GetCinemasDynamicQuery.Name;
var cinemas = await client.GetCinemas("aurich", dynamicQuery: dynamicQuery);
```

### Cancellation

The client currently supports basic cancellation.

```cs
var cts = new CancellationTokenSource();
IKinoheldClient client = new KinoheldClient();
var cinemas = await client.GetCinemas("aurich", cancellationToken: cts.Token);
// ...
cts.Cancel();
```
