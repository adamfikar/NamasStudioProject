namespace NamasStudio.API.Models
{
    public class JsonResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public T Payload { get; set; }
        public string? Message { get; set; } = string.Empty;
    }
}
