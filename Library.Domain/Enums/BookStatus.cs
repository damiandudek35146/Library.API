﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Enums
{
    public enum BookStatus
    {
        OnShelf = 1,
        Borrowed,
        Returned,
        Damaged
    }
}
