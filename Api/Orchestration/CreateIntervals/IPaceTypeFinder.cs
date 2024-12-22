﻿using Api.DataModels;

namespace Api.Orchestration.CreateIntervals;

internal interface IPaceTypeFinder
{
    public PaceType GetPaceType(string intervalName);

    public IntervalType GetIntervalType(string intervalName);
}
