
namespace Lift.App.DTO.Logger
{
    public record ApiRequestLogDto
    {

        public string ApiService { get; set; } = string.Empty;
        public string LoginName { get; set; }
        public string IP { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RequestBody { get; set; }
        public string ResponseStatus { get; set; }
        public string ResponseText { get; set; }
        public string ResponseBody { get; set; }
    }
}
