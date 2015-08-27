Enum.Performance.Library ![Licence](https://img.shields.io/github/license/tonyferrell/Enum.Performance.Library.svg)
========================
Provides tools for caching enum value -> name maps, allowing performant, low-latency transitions between an enum and its string representation.

Motivation
----------
C# enums are very performant and still let you maintain readable code.  Unfortunatly, performing operations like listing all enum values, converting enum's back to their string names, and parsing strings to enum values are incredibly non-performant. In low-latency/high-performance systems, this can have a huge negative impact on your performance metrics.

The ideal solution would be to treat all Enum's as their underlying type, and only do conversions offline. However this is not always feasible, and this package will help by providing some tools to trade off that processing time for a little bit of space.

Usage
-----
You can now use the provided EnumManager<T> class to do fast string <--> enum conversion.

```c#
using Enum.Performance.Library;
using System;

enum ResultState
{
    Success = 1,
    Failure = 2,
}

public class MyOnlineTool
{
    // Depending on your program structure, a static constructor might be an OK initalization point.
    static MyOnlineTool()
    {
        EnumManager<ResultState>.Initalize();
    }

    // This could be any kind of request handler. Details aren't that important. 
    public string GetResults(string[] requestData)
    {
        // Do some processing.
        ResultState state = DoSomeWorkUntrusted();

        // Since any value can be cast to an enum, we use a Try pattern to ensure this is a valid enum.
        string resultString;
        if (EnumManager<ResultState>.TryGetString(state, out resultString))
        {
            return resultString;
        }
        else
        {
            // DoSomeWork may not be trusted to return a valid value, handle its error here.
            return String.Empty;
        }
    }

    // This could be any kind of request handler. Details aren't that important. 
    public string GetMoreResults(string[] requestData)
    {
        // Do some processing.
        ResultState state = DoSomeWorkTrusted();

        // Since any value can be cast to an enum, we use a Try pattern to ensure this is a valid enum.
        string resultString;
        EnumManager<ResultState>.TryGetString(state, out resultString);
        return resultString;
    }

    // Always returns a valid value.
    public ResultState DoSomeWorkTrusted()
    {
        return ResultState.Success;
    }

    // This may be from code outside of your control, or just something you don't monitor.
    public ResultState DoSomeWorkUntrusted()
    {
        // This is an invalid result state. .NET is OK with this, but we have to be careful when converting to string.
        return (ResultState)15;
    }
}
```

Obviously you will need to follow whatever best practices your platform/needs dicate.

Caution
-------
If you are developing an app where startup time is more critical (a mobile or desktop application, for instance) than "running performance" (query serving), you will want to wait until an acceptable time to do your loading. As always, when working with performance, *Measure, measure, measure*!

Licence
-------
The MIT License (MIT)

Copyright (c) 2015 Tony Ferrell

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
