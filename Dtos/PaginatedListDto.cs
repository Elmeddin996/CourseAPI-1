namespace CourseApi.Dtos
{
    public class PaginatedListDto<T>
    {
        public PaginatedListDto(List<T> items, int totalPages, int pageIndex)
        {
            Items = items;
            TotalPages = totalPages;
            PageIndex = pageIndex;
        }

        public List<T> Items { get; }
        public int TotalPages { get; }
        public int PageIndex { get; }
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrev => PageIndex > 1;

    }
}
