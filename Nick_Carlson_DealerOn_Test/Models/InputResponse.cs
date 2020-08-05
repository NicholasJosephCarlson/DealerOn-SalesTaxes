using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Carlson_DealerOn_Test.Models
{
    /// <summary>
    /// Used to return results when attempting to parse user input.
    /// </summary>
    public class InputResponse
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Success { get; set; }
    }
}
