using Bogus;
using ITeam.DotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITeam.DotnetCore.Faker
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.UserName, f => f.Person.UserName);
            RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth);
            // john.smith@iteam.pl
            RuleFor(p => p.Email, (f, c)  => $"{c.FirstName}.{c.LastName}@iteam.pl");
            RuleFor(p => p.HashedPassword, "1234");

        }
    }
}
