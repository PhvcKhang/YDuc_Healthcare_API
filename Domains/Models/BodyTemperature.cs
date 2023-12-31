﻿namespace HealthCareApplication.Domains.Models;

public class BodyTemperature 
{
    public string BodyTemperatureId { get; private set; }
    public decimal Value { get; private set; }
    public string? ImageLink { get; private set; } = null;
    public DateTime Timestamp { get; private set; }
    public Person Person { get; private set; }
    public List<Notification>? Notifications { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private BodyTemperature() { }

    public BodyTemperature(decimal value, string imageLink, DateTime timestamp, Person person)
    {
        Value = value;
        ImageLink = imageLink;
        Timestamp = timestamp;
        Person = person;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
