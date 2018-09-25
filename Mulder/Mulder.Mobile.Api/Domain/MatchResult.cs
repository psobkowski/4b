using System;

namespace Mulder.Mobile.Api.Domain
{
    public enum MatchResult
    {
        Loss = -1,
        Draw = 0,
        Win = 1
    }

    public static class MatchResultHelper
    {
        public static MatchResult Result(int my, int opp)
        {
            return (MatchResult)Math.Sign(my - opp);
        }
    }
}
