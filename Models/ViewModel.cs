public class PersonEntryViewModel
{
    public string PersonType { get; set; } = "Visitor"; // Student / Employee / Visitor

    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string? Patronymic { get; set; }

    public string? Group { get; set; }         // для студентов
    public string? Specialty { get; set; }     // для студентов
    public string? Position { get; set; }      // для работников
    public string? Purpose { get; set; }       // для посетителей
}
