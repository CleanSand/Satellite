using System;
using System.Collections.Generic;

namespace Satellite.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Login { get; set; } =  null!;

    public string Password { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Secondname { get; set; }

    public string Lastname { get; set; } = null!;

    public DateOnly Birthdate { get; set; }
}
