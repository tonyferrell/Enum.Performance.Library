using Enum.Performance.Library;
using System;

namespace Examples
{
    public enum ResultState
    {
        Success = 1,
        Failure = 2,
    }

    public class EnumManagerExample
    {
        // Depending on your program structure, a static constructor might be an OK initalization point.
        static EnumManagerExample()
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
                return "Unable to parse";
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
}