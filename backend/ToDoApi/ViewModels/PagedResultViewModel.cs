namespace ToDoApi.ViewModels
{
    public class PagedResultViewModel<T> 
    {
        public PagedResultViewModel() { }
        public PagedResultViewModel(IEnumerable<T> tasks, int totalTasks, int currentPage, int pageSize)
        {
            Tasks = tasks;
            TotalItems = totalTasks;
            TotalPages = (int)Math.Ceiling(totalTasks / (double)pageSize);
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public IEnumerable<T> Tasks { get; set; } = Enumerable.Empty<T>();
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
