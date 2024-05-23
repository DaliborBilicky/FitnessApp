using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.LogicLibrary
{
    public class UserProfile
    {
        public string Name { get; set; } = "user";
        public string Password { get; set; } = "1234";
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }

        public override string? ToString()
        {
            return $"User: {Name}, Height: {Height}, Weight: {Weight}, Age: {Age}";
        }
    }
}
