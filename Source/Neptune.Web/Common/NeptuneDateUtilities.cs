﻿/*-----------------------------------------------------------------------
<copyright file="NeptuneDateUtilities.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;

namespace Neptune.Web.Common
{
    public class NeptuneDateUtilities
    {
        // todo: these values should be set at the config file
        public const int YearsBeyondPresentForMaximumYearForUserInput = 30;
        public const int MinimumYear = 1950;

        /// <summary>
        /// Range of Years for user input, using MinimumYear and MaximumYearforUserInput
        /// </summary>
        /// <returns></returns>
        public static List<int> YearsForUserInput()
        {
            return GetRangeOfYears(MinimumYear, DateTime.Now.Year + YearsBeyondPresentForMaximumYearForUserInput);
        }


        /// <summary>
        /// Range of Years for user input, using MinimumYear and MaximumYearforUserInput
        /// </summary>
        /// <returns></returns>
        public static List<int> FutureYearsForUserInput()
        {
            return GetRangeOfYears(DateTime.Now.Year, DateTime.Now.Year + YearsBeyondPresentForMaximumYearForUserInput);
        }

        /// <summary>
        /// Range of Years for user input, using MinimumYearForReporting and DateTime.Now.Year
        /// </summary>
        /// <returns></returns>
        public static List<int> ReportingYearsForUserInput()
        {
            return GetRangeOfYears(MinimumYear, DateTime.Now.Year);
        }

        public static List<int> GetRangeOfYears(int startYear, int endYear)
        {
            startYear = Math.Min(endYear, startYear); // if the start year is greater than the end year, just use the end year
            return Enumerable.Range(startYear, (endYear - startYear) + 1).ToList();
        }

        public static DateTime LastReportingPeriodStartDate()
        {
            return new DateTime(CalculateCurrentYearToUseForReporting(), NeptuneWebConfiguration.ReportingPeriodStartMonth, NeptuneWebConfiguration.ReportingPeriodStartDay);
        }

        public static int GetMinimumYearForReportingExpenditures()
        {
            return MinimumYear;
        }

        public static int CalculateCurrentYearToUseForReporting()
        {
            var reportingPeriodStartMonth = NeptuneWebConfiguration.ReportingPeriodStartMonth;
            var reportingPeriodStartDay = NeptuneWebConfiguration.ReportingPeriodStartDay;
            return CalculateCurrentYearToUseForReportingImpl(DateTime.Today, reportingPeriodStartMonth, reportingPeriodStartDay);
        }

        //Only public for unit testing
        public static int CalculateCurrentYearToUseForReportingImpl(DateTime currentDateTime, int reportingStartMonth, int reportingStartDay)
        {
            var dateToCheckAgainst = new DateTime(currentDateTime.Year, reportingStartMonth, reportingStartDay);
            return currentDateTime.IsDateBefore(dateToCheckAgainst) ? currentDateTime.Year - 1 : currentDateTime.Year;
        }

        public static List<int> CalculateCalendarYearRangeAccountingForExistingYears(List<int> existingYears, int? startYear, int? endYear, int currentYearToUse, int? floorYear, int? ceilingYear)
        {
            Check.Require((!endYear.HasValue && !startYear.HasValue) // both not provided
                          || (endYear.HasValue && (!startYear.HasValue || endYear.Value >= startYear.Value)) // endYear provided and needs to either have start year null or start year <= end year
                          || !endYear.HasValue // only have startYear
                ,
                $"Start Year {startYear} and End Year {endYear} are out of order!");
            var currentYear = currentYearToUse;
            var defaultStartYear = currentYear;
            if (startYear.HasValue)
            {
                defaultStartYear = startYear.Value;
            }
            else if (endYear.HasValue && endYear.Value <= currentYear)
            {
                defaultStartYear = endYear.Value;
            }
            var defaultEndYear = currentYear;
            if (endYear.HasValue)
            {
                defaultEndYear = endYear.Value;
            }
            else if (startYear.HasValue && startYear.Value > currentYear)
            {
                defaultEndYear = startYear.Value;
            }

            // if provided a floor year, make the minimum be that
            if (floorYear.HasValue)
            {
                defaultStartYear = Math.Max(floorYear.Value, defaultStartYear);
                defaultEndYear = Math.Max(floorYear.Value, defaultEndYear);
            }

            // if provided a ceiling year, cap it to that
            if (ceilingYear.HasValue)
            {
                defaultStartYear = Math.Min(ceilingYear.Value, defaultStartYear);
                defaultEndYear = Math.Min(ceilingYear.Value, defaultEndYear);
            }

            var minEnteredCalendarYear = existingYears.Any() ? Math.Min(existingYears.Min(), defaultStartYear) : defaultStartYear;
            var maxEnteredCalendarYear = existingYears.Any() ? Math.Max(existingYears.Max(), defaultEndYear) : defaultEndYear;
            var calendarYearsToPopulate = GetRangeOfYears(minEnteredCalendarYear, maxEnteredCalendarYear);
            return calendarYearsToPopulate;
        }

        public static bool DateIsInReportingRange(int calendarYear)
        {
            return calendarYear > MinimumYear && calendarYear <= CalculateCurrentYearToUseForReporting();
        }

        public static List<int> GetRangeOfYearsForReportingExpenditures()
        {
            return GetRangeOfYears(GetMinimumYearForReportingExpenditures(), CalculateCurrentYearToUseForReporting());
        }

        public static List<int> GetRangeOfYearsForReporting()
        {
            return GetRangeOfYears(MinimumYear, CalculateCurrentYearToUseForReporting());
        }

        public static bool IsDayToSendDelinquentReminder(DateTime dateToCheck, int delinquentReminderIntervalInDays, int deadlineMonth, int deadlineDay, int reportingPeriodEndMonth, int reportingPeriodEndDay)
        {
            var deadlineYear = CalculateDeadlineYear();
            var deadlineDate = new DateTime(deadlineYear, deadlineMonth, deadlineDay);
            var reportingPeriodEndDate = new DateTime(deadlineYear, reportingPeriodEndMonth, reportingPeriodEndDay);
            var withinDelinquentReminderPeriod = dateToCheck > deadlineDate && dateToCheck < reportingPeriodEndDate;
            var daysFromDeadline = (dateToCheck - deadlineDate).Days;
            var isDayToSendDelinquentReminder = daysFromDeadline > 0 && daysFromDeadline % delinquentReminderIntervalInDays == 0;
            return withinDelinquentReminderPeriod && isDayToSendDelinquentReminder;
        }

        public static int CalculateDeadlineYear()
        {
            var currentDateTime = DateTime.Today;
            var dateToCheckAgainst = new DateTime(DateTime.Today.Year, NeptuneWebConfiguration.ReportingPeriodStartMonth, NeptuneWebConfiguration.ReportingPeriodStartDay);
            var reportingYear = currentDateTime.IsDateBefore(dateToCheckAgainst) ? currentDateTime.Year : currentDateTime.Year + 1;
            return reportingYear;
        }
    }
}
