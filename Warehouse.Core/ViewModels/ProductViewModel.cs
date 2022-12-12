using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Warehouse.Core.CustomAttributes;
using Warehouse.Infrastructure.Enums;

namespace Warehouse.Core.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }


        public string? Image { get; set; }
    }
}
