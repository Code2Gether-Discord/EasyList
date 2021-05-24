using System;

public class TaskItem
{
    public enum TaskItemStatus : Byte
    {
		INPROGRESS,
        DONE,
    }
	public static int TaskCount = 0;
    private int id;
    private string label;
    private string description;
    private int priority;
    private DateTimeOffset createdDate;
    private DateTimeOffset dueDate;
    private TaskItemStatus status;

    public int Id { get => id; set => id = value; }
    public string Label { get => label; set => label = value; }
    public string Description { get => description; set => description = value; }
    public int Priority { get => priority; set => priority = value; }
    public DateTimeOffset CreatedDate { get => createdDate; set => createdDate = value; }
    public DateTimeOffset DueDate { get => dueDate; set => dueDate = value; }
    public TaskItemStatus Status { get => status; set => status = value; }

    public TaskItem(string Label, DateTimeOffset? DueDate = null , string Description = "", int priority = 0)
	{
		this.Id = ++TaskCount;
		this.Label = Label;
		this.Description = Description;
		this.CreatedDate = DateTimeOffset.UtcNow;
		if(DueDate == null)
        {
			this.DueDate = DateTimeOffset.MinValue;
        }
		this.Status = TaskItemStatus.INPROGRESS;
	}

	public void Display(int Id)
	{
		Console.WriteLine($"ID : {this.Id}");
		Console.WriteLine($"Todo : {this.Label}");
		Console.WriteLine($"DueDate : {this.DueDate}");
		Console.WriteLine($"Priority : {this.Priority}");
		Console.WriteLine($"Stattus : {this.Status}");
	}
}
