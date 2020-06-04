using System;
using System.Collections.Generic;
using System.Linq;

namespace toDoListOPP
{
	[Serializable]
	public class TaskItem
	{
		private string _description;
		private string _isCompleted;
		private int _itemID;

		public string Description { get => _description; set => _description = value; }
		public string IsCompleted { get => _isCompleted; set => _isCompleted = value; }
		public int ItemID { get => _itemID; set => _itemID = value; }

		public TaskItem()
		{

		}
		public TaskItem(int itemID, string description, string isCompleted)
		{
			this.ItemID = itemID;
			this.Description = description;
			this.IsCompleted = isCompleted;	
		}

		public virtual void DisplayList()
		{
			//Console.CLear();
			//Console.WriteLine("\t Task List");
			Console.WriteLine($"ID: {this.ItemID}\t|\tDescription: {this.Description}\t|\tStatus: {this.IsCompleted}");
		}
	}
}