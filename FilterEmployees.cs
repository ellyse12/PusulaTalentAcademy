using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;


    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null)
            return "{}";
        var minAge = 25;
        var maxAge = 40;
        var allowedDepartments = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "IT", "Finance" };
        var minSalary = 3500m;
        var maxSalary = 7000m;
        var hireDateThresholdExclusive = new DateTime(2017, 12, 31); // 2018 ve sonrası

        var filtered = employees
            .Where(e =>
                e.Age >= minAge &&
                e.Age <= maxAge &&
                allowedDepartments.Contains(e.Department ?? string.Empty) &&
                e.Salary >= minSalary &&
                e.Salary <= maxSalary &&
                e.HireDate > hireDateThresholdExclusive
            )
            .ToList();

        if (filtered.Count == 0)
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0m,
                AverageSalary = 0m,
                MinSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            });
        }

        var names = filtered
            .Select(e => e.Name ?? string.Empty)
            .OrderBy(n => n, StringComparer.Ordinal)
            .ToList();

        var total = filtered.Sum(e => e.Salary);
        var avg = filtered.Average(e => e.Salary);
        var min = filtered.Min(e => e.Salary);
        var max = filtered.Max(e => e.Salary);

        var result = new
        {
            Names = names,
            TotalSalary = total,
            AverageSalary = avg,
            MinSalary = min,
            MaxSalary = max,
            Count = filtered.Count
        };

        return JsonSerializer.Serialize(result);
    }

