using System;
using System.ComponentModel.DataAnnotations;

namespace MvcModalDialog.Models
{
    public class DialogModel
    {
        [Required]
        public int Value { get; set; }
    }
}