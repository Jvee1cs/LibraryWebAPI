using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookContext.ValueObjects
{
    public record ISBN(string Value)
    {
        public override string ToString() => Value;

        public static bool IsValid(string value)
        {
            // Basic check — customize as needed
            return !string.IsNullOrWhiteSpace(value) && value.Length >= 10;
        }
    }
}
