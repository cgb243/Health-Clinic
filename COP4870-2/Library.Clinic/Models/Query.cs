using System;

namespace Library.Clinic.Models;

public class Query
{
    public string? Content { get; set; }
    public Query(string content)
    {
        this.Content = content;
    }
}
