using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace toDoListOPP
{
	internal class Manage
	{
		private List<TaskItem> todoList = new List<TaskItem>();

		public void NumberTask(List<TaskItem> List)
		{
			int i = 1;
			foreach (var task in List)
			{
				task.ItemID = i;
				i++;
			}
		}

		public string CheckDelete(string input, List<TaskItem> list)
		{
			foreach (var task in list)
			{
				if (task.Description == input)
				{
					list.Remove(task);
					return "Task has been deleted";
				}
			}
			return "Can not find the name";
		}

		public void CheckUpdate(string input, List<TaskItem> list)
		{
			foreach (var task in list)
			{
				if (task.Description == input)
				{
					Console.WriteLine("Input Description of Task: ");
					task.Description = Console.ReadLine();
					while (task.Description.Trim() == "")
					{
						Console.WriteLine("Description cannot be empty, please re-enter");
						task.Description = Console.ReadLine();
					}
					Console.WriteLine("Task \"completed\" or \"incomplete\"(blank will be incomplete): ");
					task.IsCompleted = Console.ReadLine();
					if (task.IsCompleted == "") task.IsCompleted = "incomplete";
					while (task.IsCompleted != "completed" && task.IsCompleted != "incomplete")
					{
						Console.WriteLine("Please define \"completed\" or \"incomplete\"(blank will be incomplete): ");
						task.IsCompleted = Console.ReadLine();
						if (task.IsCompleted == "") task.IsCompleted = "incomplete";
					}
					break;
				}
			}
			Console.WriteLine("Can not find the name");
		}

		public void Input(List<TaskItem> list, int ID)
		{
			bool flag = false;
			if (ID == 0)
			{
				ID = list.Count() + 1;
				flag = true;
			}
			Console.WriteLine("Input Description of Task: ");
			string desc = Console.ReadLine();
			while (desc.Trim() == "")
			{
				Console.WriteLine("Description cannot be empty, please re-enter");
				desc = Console.ReadLine();
			}
			Console.WriteLine("Task \"completed\" or \"incomplete\"(blank will be incomplete): ");
			string iscomplete = Console.ReadLine();
			if (iscomplete == "") iscomplete = "incomplete";
			while (iscomplete != "completed" && iscomplete != "incomplete")
			{
				Console.WriteLine("Please define \"completed\" or \"incomplete\"(blank will be incomplete): ");
				iscomplete = Console.ReadLine();
				if (iscomplete == "") iscomplete = "incomplete";
			}
			var Task = new TaskItem(ID, desc, iscomplete);
			if (flag)
			{
				list.Add(Task);
			}
		}

		public void Layout()
		{
			string decision;
			do
			{
				Console.WriteLine("\t ===============================");
				Console.WriteLine("\t|\t1.Manage To-do List\t|");
				Console.WriteLine("\t|\t2.Manage My-day List\t|");
				Console.WriteLine("\t|\t3.Exit\t\t\t|");
				Console.WriteLine("\t ===============================");
				Console.Write("\t\tPlease choose: ");

				decision = Console.ReadLine();

				if (decision != "1" && decision != "2" && decision != "3")
				{
					Console.WriteLine("\nWrong input. Please choose again.\n");
				}
			} while (decision != "1" && decision != "2" && decision != "3");

			switch (decision)
			{
				case "1":
					menutodoList();
					break;

				case "2":
					//menumyDayList();
					break;

				case "3":
					break;
			}

			void menutodoList()
			{
				string dir = @"c:\temp";
				string serializationFile = Path.Combine(dir, "salesmen.bin");
				while (true)
				{
					string menuTaskDecision = "";

					//Menu Student
					while (menuTaskDecision != "1" && menuTaskDecision != "2" && menuTaskDecision != "3" && menuTaskDecision != "4" && menuTaskDecision != "5" && menuTaskDecision != "6")
					{
						Console.WriteLine("\t*****************************************");
						Console.WriteLine("\t*\t\tTask in List: \t\t*");
						foreach (var it in todoList)
						{
							Console.Write("\t*\t");
							it.DisplayList();
						}
						Console.WriteLine("\t*****************************************");
						Console.WriteLine("");
						Console.WriteLine("\t ===============================");
						Console.WriteLine("\t|\t1.Add new item\t\t|");
						Console.WriteLine("\t|\t2.Delete item\t\t|");
						Console.WriteLine("\t|\t3.Update item\t\t|");
						Console.WriteLine("\t|\t4.Back to main menu\t|");
						Console.WriteLine("\t|\t5.Save List\t\t|");
						Console.WriteLine("\t|\t6.Load List\t\t|");
						Console.WriteLine("\t ===============================");
						Console.Write("\t\tPlease choose: ");
						menuTaskDecision = Console.ReadLine();
						if (menuTaskDecision != "1" && menuTaskDecision != "2" && menuTaskDecision != "3" && menuTaskDecision != "4" && menuTaskDecision != "5" && menuTaskDecision != "6") Console.WriteLine("Wrong Input! Please try again");
					}

					switch (Convert.ToInt32(menuTaskDecision))
					{
						case 1:
							Input(todoList, 0);
							break;

						case 2:
							Console.WriteLine("Please enter task name that you want to delete: ");
							String TaskDeleteValue = Console.ReadLine();
							Console.WriteLine(CheckDelete(TaskDeleteValue, todoList));
							NumberTask(todoList);
							menutodoList();

							break;

						case 3:
							Console.WriteLine("Please enter task Name that you want to update: ");
							String studentUpdateValue = Console.ReadLine();
							CheckUpdate(studentUpdateValue, todoList);
							menutodoList();
							break;

						case 4:
							Layout();
							break;

						case 5:
							//serialize
							using (Stream stream = File.Open(serializationFile, FileMode.Create))
							{
								var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

								bformatter.Serialize(stream, todoList);
							}

							/*var fileName = @"E:\Desktop\wordss.txt";
							if(!File.Exists(fileName))
							{
								FileStream fsCreate = new FileStream(fileName, FileMode.Create);
								fsCreate.Close();
								Console.Write($"File has been created and the Path is {fileName}");
							}
							FileStream fs = new FileStream(fileName, FileMode.Append);
							byte[] bdata = Encoding.Default.GetBytes("Hello File Handling!");
							fs.Write(bdata, 0, bdata.Length);
							fs.Close();*/
							break;

						case 6:
							try
							{
								using (Stream stream = File.Open(serializationFile, FileMode.Open))
								{
									BinaryFormatter bin = new BinaryFormatter();

									var tasklist = (List<TaskItem>)bin.Deserialize(stream);
									foreach (var taskload in tasklist)
									{
										todoList.Add(taskload);
									}
								}
							}
							catch (IOException)
							{
							}

							break;
					}
				}
			}
		}
	}
}