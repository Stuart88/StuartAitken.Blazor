using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuartAitken.Blazor.Shared.Models
{
    public class SelectListItem<T>
    {
        public T Value { get; set; }
        public string Label { get; set; }
        public string GroupName { get; set; }
    }
}
