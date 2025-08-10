namespace api.MiniCatalogo.Model
{
    public class ExcepitioResponse
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
        public string TraceId { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; }
        public string? AdditionalProp1 { get; set; }
        public string? AdditionalProp2 { get; set; }
        public string? AdditionalProp3 { get; set; }
    }
}