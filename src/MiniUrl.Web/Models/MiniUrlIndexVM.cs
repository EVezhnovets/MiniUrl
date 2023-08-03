using MiniUrl.ApplicationCore.Entities;

namespace MiniUrl.Web.Models
{
    public class MiniUrlIndexVM
    {
        public MiniUrlItem? MiniUrlItem { get; set; }
        public List<MiniUrlItem>? MiniUrls { get; set; }
    }
}