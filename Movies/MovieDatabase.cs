﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies;

        public static List<Movie> All 
        { 
            get 
            {
                if (movies is null)
                {
                    using (StreamReader file = File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }

                return movies; 
            } 
        }

        public static List<Movie> Search(List<Movie> movies, string term)
        {
            List<Movie> results = new List<Movie>();

            foreach (Movie movie in movies)
            {
                if (movie.Title.Contains(term, StringComparison.CurrentCultureIgnoreCase) || 
                    (movie.Director != null && movie.Director.Contains(term, StringComparison.CurrentCultureIgnoreCase)))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movieList, List<string> mpaa)
        {
            List<Movie> results = new List<Movie>();

            foreach (Movie movie in movieList)
            {
                if (mpaa.Contains(movie.MPAA_Rating))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public static List<Movie> FilterByMinIMDB(List<Movie> movieList, float min)
        {
            List<Movie> results = new List<Movie>();

            foreach (Movie movie in movieList)
            {
                if (movie?.IMDB_Rating >= min)
                {
                    results.Add(movie);
                }
            }

            return results;
        }
    }
}
