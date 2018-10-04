using System;
using System.Collections.Generic;
using System.Linq;

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
        public static MatchResult Result(List<ScoreInfo> score, int teamId)
        {
            return (MatchResult)Math.Sign(Diff(score, teamId));
        }

        public static MatchResult Result(int my, int opp)
        {
            return (MatchResult)Math.Sign(my - opp);
        }

        public static int Diff(List<ScoreInfo> score, int teamId)
        {
            int diff = score.First(x => x.TeamId == teamId).FullTimeScore 
                     - score.First(x => x.TeamId != teamId).FullTimeScore;
            return diff;
        }
    }
}
