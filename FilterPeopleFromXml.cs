using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData))
            return "{}";

        var document = XDocument.Parse(xmlData);

        var filteredPeople = document.Descendants("Person")
            .Select(p => new
            {
                Name = (string)p.Element("Name"),
                Age = (int?)p.Element("Age") ?? 0,
                Department = (string)p.Element("Department"),
                Salary = (int?)p.Element("Salary") ?? 0,
                HireDate = (DateTime?)p.Element("HireDate") ?? DateTime.MinValue
            })
            .Where(p =>
                p.Age > 30 &&
                p.Salary < 5000 &&
                p.HireDate < new DateTime(2010, 1, 1)
            )
            .ToList();

        if (!filteredPeople.Any())
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        var result = new
        {
            Names = filteredPeople.Select(p => p.Name).ToList(),
            TotalSalary = filteredPeople.Sum(p => p.Salary),
            AverageSalary = (int)filteredPeople.Average(p => p.Salary),
            MaxSalary = filteredPeople.Max(p => p.Salary),
            Count = filteredPeople.Count
        };

        return JsonSerializer.Serialize(result);
    }

