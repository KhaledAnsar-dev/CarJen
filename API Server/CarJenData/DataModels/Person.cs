using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Person
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public byte Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public bool IsActive { get; set; }

    public string? Image { get; set; }

    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
