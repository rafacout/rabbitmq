public class Person
{
    public Person(Guid id, string name, DateTime birthDate)
    {
        Id = id;
        Name = name;
        BirthDate = birthDate;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
}