// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    public enum EGridSize : long
    {
        min1 = 1 * TimeSpan.TicksPerMinute,
        min2 = 2 * min1,
        min5 = 5 * min1,
        min10 = 10 * min1,
        min15 = 15 * min1,
        min20 = 20 * min1,
        min30 = 30 * min1,
        hour1 = TimeSpan.TicksPerHour,
        hour2 = 2 * hour1,
        hour3 = 3 * hour1,
        hour4 = 4 * hour1,
        hour6 = 6 * hour1,
        hour12 = 12 * hour1,
        day1 = 1 * TimeSpan.TicksPerDay,
        day2 = 2 * day1,
        day3 = 3 * day1,
        day5 = 5 * day1,
        week1 = 7 * day1,
        week2 = 14 * day1,
        month1 = 30 * day1,
        month2 = 60 * day1,
        month3 = 90 * day1,
        month4 = 120 * day1,
        month6 = 180 * day1,
        year1 = 365 * 3 * day1,
        year2 = 2 * year1,
        year3 = 3 * year1,
        year4 = 4 * year1,
        year5 = 5 * year1,
        year10 = 10 * year1,
        year20 = 20 * year1
    }
}
