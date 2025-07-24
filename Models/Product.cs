using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginAndRegisterEFCORE.Models;

public partial class Product
{
    public int Id { get; set; }


    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }
}
