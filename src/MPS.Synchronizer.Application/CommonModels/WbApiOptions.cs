﻿using System.ComponentModel.DataAnnotations;

namespace MPS.Synchronizer.Application.CommonModels;

public class WbApiOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Statistics { get; set; }
}