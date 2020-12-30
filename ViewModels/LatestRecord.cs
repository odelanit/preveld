using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Preveld.Models;

namespace Preveld.ViewModels
{
    public class LatestRecord
    {
        public Byte[] qrCode { get; set; }

        public Valve Valve { get; set; }

        public Wrap Wrap { get; set; }
    }
}