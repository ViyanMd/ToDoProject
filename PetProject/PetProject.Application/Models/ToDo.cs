namespace PetProject.Models
{
    public class ToDo
    {
        public Guid Id { get; private set; }
        public string ToDoContent { get; set; }

        public ToDo(string content)
        {
            Id = Guid.NewGuid();
            ToDoContent = content;
        }
    }
}