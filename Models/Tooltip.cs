using System.ComponentModel.DataAnnotations;

namespace IAR.Models
{
    public class Tooltip
    {
        [Key]
        public string FieldName { get; set; } = default!;

        public string TooltipText { get; set; } = default!;
    }
}
