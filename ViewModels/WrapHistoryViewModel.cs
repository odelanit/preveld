using System;
using Preveld.Models;
using System.Collections.Generic;

namespace Preveld.ViewModels
{
    public class WrapHistoryViewModel
    {
        public Wrap Wrap { get; set; }

        public List<Wrap> Wraps { get; set; }

        public Byte[] QrCode { get; set; }
    }
}