using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rules
{
    public static class PostRules
    {
        public const int TitleMinimumLength = 3;

        public const int TitleMaximumLength = 260;

        public const int BodyMaximumLength = 42000;

        public const int DefaultReputation = 1;
    }
}
