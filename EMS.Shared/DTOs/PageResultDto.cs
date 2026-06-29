namespace EMS.Shared.DTOs
{
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new();

        public int TotalRecords { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}