
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
        return $"{Type} its name is {Name}, age: {Age}, owners name {OwnerName}, contact {OwnerNumber}. Issue: {Issue}";
    }
}

public class Dog : Animal
{
    public Dog(string name, int age, string ownername, string ownernumber, string issue) : base("Dog", name, age, ownername, ownernumber, issue)
    {
    }
}

public class Cat : Animal
{
    public Cat(string name, int age, string ownername, string ownernumber, string issue) : base("Cat", name, age, ownername, ownernumber, issue)
    {
    }
}

