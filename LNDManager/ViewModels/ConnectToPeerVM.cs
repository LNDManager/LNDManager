using System.ComponentModel.DataAnnotations;

namespace LNDManager.ViewModels
{
    public class ConnectToPeerVM
    {
        [Required]
        public string Connection { get; set; }
    }
}
