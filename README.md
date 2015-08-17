Enum.Performance.Library ![Licence](https://img.shields.io/github/license/tonyferrell/Enum.Performance.Library.svg)
========================
Provides tools for caching enum value -> name maps, allowing performant, low-latency transitions between an enum and its string representation.

Motivation
----------
C# enums are very performant and still let you maintain readable code.  Unfortunatly, performing operations like listing all enum values, converting enum's back to their string names, and parsing strings to enum values are incredibly non-performant. In low-latency/high-performance systems, this can have a huge negative impact on your performance metrics.

The ideal solution would be to treat all Enum's as their underlying type, and only do conversions offline. However this is not always feasible, and this package will help by providing some tools to trade off that processing time for a little bit of space.

Usage
-----
To build your cache of conversion values, do something like the following:
```c#
using Enum.Performance.Library;

public static class MyEnumCache
{
  private static readonly Dictionary<MyEnum, string> enumToStringMap;
  
  static MyEnumCache()
  {
    EnumPerformanceParser.TryCreateEnumToNameMap(out enumToStringMap);
  }
  
  public static string GetString(MyEnum enumValue)
  {
    return enumToStringMap[enumValue];
  }

  public static bool TryParse(string enumText, out MyEnum enumValue)
  {
    return enumToStringMap.TryGetValue(enumText, out enumValue);
  }
}
```

Obviously you will need to follow whatever best practices your platform/needs dicate.

Caution
-------
If you are developing an app where startup time is more critical (a mobile or desktop application, for instance) than "running performance" (query serving), you will want to wait until an acceptable time to do your loading. As always, when working with performance, *Measure, measure, measure*!
