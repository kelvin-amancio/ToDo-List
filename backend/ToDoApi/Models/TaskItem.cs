using ToDoApi.ViewModels;

namespace ToDoApi.Models
{
    public class TaskItem
    {
        public TaskItem() { }

        public TaskItem(string title, string description, bool completed, string userId)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Description = description;
            Completed = completed;
            UserId = userId;
        }

        public TaskItem(string id, string title, string description, bool completed)
        {
            Id = id;
            Title = title;
            Description = description;
            Completed = completed;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool Completed { get; private set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public void BuilderTaskItem(string title, string description, bool completed)
        {
            Title = title;
            Description = description;
            Completed = completed;
        }
    }
}
