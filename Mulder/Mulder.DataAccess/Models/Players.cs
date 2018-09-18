using System;
using System.Collections.Generic;

namespace Mulder.DataAccess.Models
{
    public partial class Players
    {
        public Players()
        {
            MatchesLineUp = new HashSet<MatchesLineUp>();
        }

        public int Id { get; set; }
        public int CurrentTeamId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Number { get; set; }
        public bool? OnLoan { get; set; }

        public Teams CurrentTeam { get; set; }
        public ICollection<MatchesLineUp> MatchesLineUp { get; set; }
    }
}
