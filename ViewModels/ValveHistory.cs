﻿using Preveld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Preveld.ViewModels
{
    public class ValveHistory
    {
        public Valve Valve { get; set; }

        public List<Valve> Valves { get; set; }
    }
}