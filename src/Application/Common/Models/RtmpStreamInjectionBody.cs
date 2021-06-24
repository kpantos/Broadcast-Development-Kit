using Domain.Enums;

namespace Application.Common.Models
{
    public class RtmpStreamInjectionBody: StartStreamInjectionBody
    {
        public bool EnableSsl { get; set; }
        public RtmpMode Mode { get; set; }
    }
}
