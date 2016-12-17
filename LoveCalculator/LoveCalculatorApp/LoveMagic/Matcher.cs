using System;
using System.Collections.Generic;
using System.Linq;
using LoveCalculatorApp.ServerInteraction;

namespace LoveCalculatorApp.LoveMagic
{
    /// <summary>
    /// Class to process data from user
    /// </summary>
    public class Matcher
    {
        // to make queries to server
        private Querier querier = new Querier();

        /// <summary>
        /// Gets distance between two dates in days
        /// </summary>
        /// <param name="first">First date</param>
        /// <param name="second">Second date</param>
        /// <returns>days between dates</returns>
        private int GetDistanceBetweenDates(DateTime first, DateTime second)
        {
            // it possible that chortest distance between dates lies across year change (i.e. 31 Dec and 01 Jan)
            // so we need to check all cases and find shortest TimeSpan
            List<TimeSpan> possibleResults = new List<TimeSpan>()
            {
                new DateTime(1996, first.Month, first.Day) - new DateTime(1996, second.Month, second.Day),
                new DateTime(1996, first.Month, first.Day) - new DateTime(1995, second.Month, (second.Month == 2 && second.Day == 29) ? 27 : second.Day),
                new DateTime(1996, second.Month, second.Day) - new DateTime(1995, first.Month, first.Day)
            };

            // convert to days, take absolute value and pick smallest
            return (int) possibleResults.ConvertAll(ts => Math.Abs(ts.TotalDays)).Min();
        }

        /// <summary>
        /// Calculates compatibility percentage for two people
        /// </summary>
        /// <param name="person">First person's date of birth</param>
        /// <param name="beloved">Second person's date of birth</param>
        /// <returns>Percentage of compatibility </returns>
        public double GetCompatibilityPercentage(DateTime person, DateTime beloved)
        {
            // aquire perfect match for first person
            DateTime personsMatch = querier.GetPerfectMatch(person);

            // aquire perfect match for second person
            DateTime belovedsMatch = querier.GetPerfectMatch(beloved);

            // total differential between perfect matches and actual ones
            int sumDayDifferential =
                GetDistanceBetweenDates(personsMatch, beloved) +
                GetDistanceBetweenDates(person, belovedsMatch);
            // its maximum is 365 because max possible distance between each pair of dates is half of year

            // lets give them chance and consider less than 30 days as small differential 
            if (sumDayDifferential <= maxPerfectDayDifferential)
                return maxPercentage; // perfect match

            // calculate daily decreace of percentage
            double percentPerDay = ((double) (maxPercentage - lowestPercentage))/
                                   (daysInYear - maxPerfectDayDifferential);

            // calculate answer
            return maxPercentage - (sumDayDifferential - maxPerfectDayDifferential)*percentPerDay;
        }

        private const int maxPerfectDayDifferential = 30;
        private const int lowestPercentage = 0;
        private const int maxPercentage = 100;
        private const int daysInYear = 365;

        /// <summary>
        /// Calculates perfect match for lonely person
        /// </summary>
        /// <param name="person">Person's date of birth</param>
        /// <returns>Date of bitrh of perfect match for person</returns>
        public DateTime GetPerfectMatch(DateTime person)
        {
            // simply make query to server
            return querier.GetPerfectMatch(person);
        }
    }
}