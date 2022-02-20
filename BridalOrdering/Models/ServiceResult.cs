namespace BridalOrdering.Models
{
    public class ServiceResult<T>
    {
        public ServiceResult() { IsError = false; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public T Result { get; set; }
    }
}