﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.Dto
{
   public class UseForLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
