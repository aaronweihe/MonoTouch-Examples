using System;

namespace todolist
{
	public class Task
	{
		public Task ()
		{
		}
		
		public string Name{get; set;}
		public string Description { get; set;}
		public DateTime DueDate { get; set; }
	}
}

