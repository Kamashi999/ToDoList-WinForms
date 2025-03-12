using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox____2;

namespace Sandbox____2
{
    public abstract class Animal
    {
        public string Type { get; set; }
        
        public string Name { get; set; }

        public int Age { get; set; }

        public string OwnerName { get; set; }

        public string OwnerNumber { get; set; }

        public string Issue { get; set; }



        public Animal(string type, string name, int age, string ownername, string ownernumber, string issue)
        {
            Name = name;
            Age = age;
            OwnerName = ownername;
            OwnerNumber = ownernumber;
            Issue = issue;
        }
        public string ShowAnimalInfo()
        {
            return $"{Type} nazywa się {Name}, wiek: {Age}. Imie właściciela {OwnerName}, numer telefonu {OwnerNumber}. Problem {Issue}";
        }
    }
}
public class Dog : Animal
{
    public Dog(string name, int age, string ownername, string ownernumber, string issue) : base("Pies", name, age, ownername, ownernumber, issue)
    {
    }
}

public class Cat : Animal
{
    public Cat(string name, string race, int age) : base("Kot", race, name, age)
    {
    }
}

