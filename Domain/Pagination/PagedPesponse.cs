namespace Domain.Pagination
{
    public class PagedPesponse<T> 
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords{ get; set; }
        public T Data { get; set; }
        public PagedPesponse()
        {
        }

        public PagedPesponse(T data, int totalRecords, int pageNumber, int pageSize)
        {
            Data = data;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
