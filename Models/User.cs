using System;

public class User
{
    public Guid Id { get; set; }
    // public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public User(Guid id, string name, int age)
    {
        // this.Id = Guid.NewGuid();
        this.Id = id;
        this.Name = name;
        this.Age = age;
    }
}