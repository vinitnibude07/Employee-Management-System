namespace EMS.Shared.DTOs
{
    public class EmployeeQueryDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "id";
        public string SearchText { get; set; } = string.Empty;

    }
}