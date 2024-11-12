using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserManager.Models.Enum;

namespace UserManager.DTOs
{
    public class StartupProgramDto
    {
        public string ClientName { get; set; }
        public string ProgramName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StartupProgramType ProgramType { get; set; }
        public string? Description { get; set; }
    }
}
