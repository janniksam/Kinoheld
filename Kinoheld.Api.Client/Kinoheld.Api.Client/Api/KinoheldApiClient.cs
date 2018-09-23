﻿using System;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Kinoheld.Api.Client.Api.Queries;
using Newtonsoft.Json.Linq;

namespace Kinoheld.Api.Client.Api
{
    internal class KinoheldApiClient : IKinoheldApiClient
    {
        private const string KinoheldEndpoint = "https://graph.kinoheld.de/graphql/v1/query";

        public async Task<JObject> GetCinemas(string city, string searchTerm, int distance)
        {
            if (string.IsNullOrEmpty(city?.Trim()))
            {
                throw new ArgumentNullException(nameof(city));
            }

            if (distance <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(distance), $"{nameof(distance)} needs to be bigger than 0");
            }

            using (var client = GetClient())
            {
                var cinemaRequest = new GraphQLRequest
                {
                    Query = SearchCinemaQuery.Query(),
                    OperationName = SearchCinemaQuery.OperationName(),
                    Variables = SearchCinemaQuery.Parameters(searchTerm, city, distance)
                };

                var response = await client.PostAsync(cinemaRequest);
                return response?.Data;
            }
        }

        public async Task<JObject> GetShows(int cinemaId, DateTime? date)
        {
            if (cinemaId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cinemaId), $"{nameof(cinemaId)} needs to be bigger than 0");
            }

            using (var client = GetClient())
            {
                var cinemaRequest = new GraphQLRequest
                {
                    Query = GetShowsQuery.Query(),
                    OperationName = GetShowsQuery.OperationName(),
                    Variables = GetShowsQuery.Parameters(cinemaId, date)
                };

                var response = await client.PostAsync(cinemaRequest);
                return response?.Data;
            }
        }

        public async Task<JObject> GetCities(string searchTerm, int limit)
        {
            if (string.IsNullOrEmpty(searchTerm?.Trim()))
            {
                throw new ArgumentNullException(nameof(searchTerm));
            }

            if (limit <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(limit), $"{nameof(limit)} needs to be bigger than 0");
            }

            using (var client = GetClient())
            {
                var cinemaRequest = new GraphQLRequest
                {
                    Query = GetCitiesQuery.Query(),
                    OperationName = GetCitiesQuery.OperationName(),
                    Variables = GetCitiesQuery.Parameters(searchTerm, limit)
                };

                var response = await client.PostAsync(cinemaRequest);
                return response?.Data;
            }
        }

        private GraphQLClient GetClient()
        {
            return new GraphQLClient(KinoheldEndpoint);
        }
    }
}