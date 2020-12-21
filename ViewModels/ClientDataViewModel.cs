using Preveld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Preveld.ViewModels
{
    public class ClientDataViewModel
    {
        public List<Valve> Valves { get; set; }

        public List<Wrap> Wraps { get; set; }
    }
}