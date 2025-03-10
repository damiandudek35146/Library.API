using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects
{
    public class ISBN
    {
        public string Value { get; }

        public ISBN(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsValidISBN(value))
            {
                throw new ArgumentException("Invalid ISBN format.", nameof(value));
            }
            Value = value;
        }

        private bool IsValidISBN(string isbn)
        {
            return isbn.Length == 13 && isbn.All(char.IsDigit);
        }

        public override bool Equals(object obj)
        {
            if (obj is ISBN otherISBN)
            {
                return this.Value == otherISBN.Value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
