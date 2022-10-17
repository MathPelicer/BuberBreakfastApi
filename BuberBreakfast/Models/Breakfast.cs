using BuberBreakfast.ServiceErrors;
using ErrorOr;
using System.ComponentModel.DataAnnotations;

namespace BuberBreakfast.Models;
public class Breakfast
{
    public const int MinNameLength = 5;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public List<string> Savory { get; set; }
    public List<string> Sweet { get; set; }

    private Breakfast(Guid id, string name, string description, DateTime startDateTime, DateTime endDateTime, DateTime lastModifiedDateTime, List<string> savory, List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(
        string name,
        string description,
        DateTime startDatetime,
        DateTime endDatetime,
        List<string> savory,
        List<string> sweet,
        Guid? id = null)
    {

        List<Error> errors = new();

        if(name.Length < MinNameLength || name.Length > MaxNameLength)
        {
            errors.Add(Errors.Breakfast.InvalidName);
        }
        if (description.Length < MinDescriptionLength || description.Length > MaxDescriptionLength)
        {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }
        if (errors.Count > 0)
        {
            return errors;
        }
        
        return new Breakfast(
            id ?? Guid.NewGuid(),
            name,
            description,
            startDatetime,
            endDatetime,
            DateTime.UtcNow,
            savory,
            sweet
        );
    }
}

