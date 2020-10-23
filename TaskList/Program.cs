using System;
using System.Collections.Generic;

namespace TaskList
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set Up List of ToDoTasks and valid categories
            List<ToDoTask> tasks = new List<ToDoTask>();
            ToDoTask sampleTask1 = new ToDoTask("Bart Martinon", "This is a sample task.", DateTime.Parse("10/26/2020"),true);
            ToDoTask sampleTask2 = new ToDoTask("Bart Martinon", "Fix your code!", DateTime.Parse("10/26/2020"));
            ToDoTask sampleTask3 = new ToDoTask("Bart Martinon", "Get Candy for Halloween", DateTime.Parse("10/31/2020"));
            tasks.Add(sampleTask1);
            tasks.Add(sampleTask2);
            tasks.Add(sampleTask3);

            // Application Loop
            bool done = false;
            while (!done)
            {
                // Display Menu
                Console.WriteLine("Welcome to the Task List Capstone Program!");
                MakeLineSpace(1);
                Console.WriteLine("==========================================");
                MakeLineSpace(1);
                Console.WriteLine("Task List Main Menu:");
                Console.WriteLine("    1. List Tasks");
                Console.WriteLine("    2. Add Task");
                Console.WriteLine("    3. Delete Task");
                Console.WriteLine("    4. Mark Task as Complete");
                Console.WriteLine("    5. Edit Task");
                Console.WriteLine("    6. Check Schedule");
                MakeLineSpace(1);
                Console.WriteLine("    0. Quit");
                MakeLineSpace(1);
                Console.WriteLine("==========================================");
                MakeLineSpace(1);

                // Main Menu Selection Loop
                bool menuInputValid = false;
                int selectInt = -1;
                while (!menuInputValid)
                {
                    // User selects a number shown in the menu above, runs that value-1 for a matching index in the menu.
                    // Valid values are non-blank and integers.
                    // Integers outside the range are considered invalid.
                    string selectStr = PromptForInput("Select an Option: ");
                    try
                    {
                        selectInt = int.Parse(selectStr);
                        if (selectInt < 0 || selectInt > 6)
                        {
                            Console.WriteLine("Error: Invalid Option number. Please enter an integer shown above.");
                        }
                        else
                        {
                            menuInputValid = true;
                            if (selectInt == 1) // Selection 1
                            {
                                ListTasks(tasks);
                            } 
                            else if (selectInt == 2) // Selection 2
                            {
                                tasks = AddTask(tasks);
                            }
                            else if (selectInt == 3) // Selection 3
                            {
                                tasks = DeleteTask(tasks);
                            }
                            else if (selectInt == 4) // Selection 4
                            {
                                tasks = MarkTaskAsComplete(tasks);
                            }
                            else if (selectInt == 5) // Selection 5
                            {
                                tasks = EditTask(tasks);
                            }
                            else if (selectInt == 6) // Selection 6
                            {
                                CheckSchedule(tasks);
                            }
                            else // Selection 0: Quit - Run AskToContinue and negate bool for "AskToQuit" effect, ends Application Loop on true
                            {
                                bool userWantsToQuit = !(AskToContinue("Are you sure you want to quit the program?"));
                                if (userWantsToQuit)
                                {
                                    Console.Write("Thank you for your time! Have a nice day!");
                                    done = true;
                                }
                                else
                                {
                                    Console.Clear();
                                }
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error: Non-Integer input. Please enter an integer shown above.");
                    }
                }
                MakeLineSpace(1);
            };
        }

        // Prints all values of the ToDoTask list out into the Console
        public static void PrintList(List<ToDoTask> list)
        {
            int taskCount = 1;
            foreach (ToDoTask task in list)
            {
                Console.WriteLine("{0}. {1}", taskCount, task.PrintTask());
                MakeLineSpace(1);
                taskCount++;
            }
        }

        // Selection 1: List Tasks
        // Prints the Task List and then pauses to let the user read through the list
        public static void ListTasks(List<ToDoTask> list)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("Displaying Current List of Tasks!");
            MakeLineSpace(1);
            PrintList(list);
            Console.WriteLine("==========================================");
            PauseByAnyKey();
        }

        // Selection 2: Add Task
        // Takes a ToDoTask list and prompts the user to add individual pieces of information for a new ToDoTask instance that will be added
        // to the given list.
        // Will any String input as long as it is not a blank input via PromptForInput calls.
        public static List<ToDoTask> AddTask(List<ToDoTask> list)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("Prepare to enter information for new Task!");
            Console.WriteLine("==========================================");
            MakeLineSpace(1);

            // Ask for a name string
            string taskName = PromptForInput("Please enter the assigned Team Member's Name: ");

            // Ask for a description string
            string taskDesc = PromptForInput("Please enter a brief Description of the Task: ");

            // Ask for a deadline string, validate for valid format
            DateTime taskDedLn;
            while (true)
            {
                string taskDedLnStr = PromptForInput("Please enter the Task's Deadline in dd/mm/yyyy format: ");
                try
                {
                    taskDedLn = DateTime.Parse(taskDedLnStr);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Incorrect format, try again!");
                }
            }

            // Add new Task to list, return modified list
            Console.WriteLine("Adding!");
            ToDoTask newTask = new ToDoTask(taskName, taskDesc, taskDedLn);
            list.Add(newTask);
            PauseByAnyKey();
            return list;
        }

        // Selection 3: Delete Task
        // Takes a ToDoTask list and removes a Task based on whichever number that the user enters.
        public static List<ToDoTask> DeleteTask(List<ToDoTask> list)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("Choose a Task to remove.");
            MakeLineSpace(1);
            PrintList(list);
            Console.WriteLine("==========================================");

            // Delete Selection Loop
            bool deleteInputValid = false;
            int deleteInt = -1;
            while (!deleteInputValid)
            {
                // User selects a number shown in the menu above, runs that value-1 for a matching index in the task list.
                // Valid values are non-blank and integers.
                // Integers outside the range are considered invalid.
                string deleteStr = PromptForInput("Please enter a number shown above: ");
                try
                {
                    deleteInt = int.Parse(deleteStr);
                    if (deleteInt < 1 || deleteInt > list.Count)
                    {
                        Console.WriteLine("Error: Integer option not available. Please enter an integer shown above.");
                    }
                    else
                    {
                        deleteInputValid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Non-Integer input. Please enter an integer shown above.");
                }
            }
            MakeLineSpace(1);

            // If match is found, prompt user to confirm their choice.
            //     If so, remove specified task and return modified list.
            //     Otherwise, return unchanged list.
            string deleteMsg = "Are you sure that you want to remove Task #" + deleteInt;
            bool confirmDelete = !(AskToContinue(deleteMsg));
            if (confirmDelete)
            {
                Console.WriteLine("Deleting!");
                ToDoTask targetTask = list[deleteInt-1];
                list.Remove(targetTask);
            }
            else
            {
                Console.WriteLine("Aborting! Returning to Main Menu!");
            }
            PauseByAnyKey();
            return list;
        }

        // Selection 4: Mark Task as Complete
        // Takes a ToDoTask list and changes the isComplete property of the Task based on whichever number that the user enters to true.
        public static List<ToDoTask> MarkTaskAsComplete(List<ToDoTask> list)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("Choose a Task to mark as Complete (True).");
            MakeLineSpace(1);
            PrintList(list);
            Console.WriteLine("==========================================");

            // Mark Selection Loop
            bool markInputValid = false;
            int markInt = -1;
            while (!markInputValid)
            {
                // User selects a number shown in the menu above, runs that value-1 for a matching index in the task list.
                // Valid values are non-blank and integers.
                // Integers outside the range are considered invalid.
                string markStr = PromptForInput("Please enter a number shown above: ");
                try
                {
                    markInt = int.Parse(markStr);
                    if (markInt < 1 || markInt > list.Count)
                    {
                        Console.WriteLine("Error: Integer option not available. Please enter an integer shown above.");
                    }
                    else
                    {
                        markInputValid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Non-Integer input. Please enter an integer shown above.");
                }
            }
            MakeLineSpace(1);

            // If match is found, prompt user to confirm their choice.
            //     If so, mark the specified task and return modified list.
            //     Otherwise, return unchanged list.
            string markMsg = "Are you sure that you want to mark Task #" + markInt + " as complete?";
            bool confirmMark = !(AskToContinue(markMsg));
            if (confirmMark)
            {
                ToDoTask targetTask = list[markInt - 1];
                if (targetTask.IsComplete)
                {
                    Console.WriteLine("Task is already complete! Returning to Main Menu!");
                }
                else
                {
                    Console.WriteLine("Marking!");
                    targetTask.ToggleComplete();
                }
            }
            else
            {
                Console.WriteLine("Aborting! Returning to Main Menu!");
            }
            PauseByAnyKey();
            return list;
        }

        // Selection 5: Edit Task
        // Takes a ToDoTask list and changes the properties of the Task based on whichever number that the user enters to whichever values the user inputs.
        public static List<ToDoTask> EditTask(List<ToDoTask> list)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("Choose a Task to Edit.");
            MakeLineSpace(1);
            PrintList(list);
            Console.WriteLine("==========================================");

            // Mark Selection Loop
            bool editInputValid = false;
            int editInt = -1;
            while (!editInputValid)
            {
                // User selects a number shown in the menu above, runs that value-1 for a matching index in the task list.
                // Valid values are non-blank and integers.
                // Integers outside the range are considered invalid.
                string editStr = PromptForInput("Please enter a number shown above: ");
                try
                {
                    editInt = int.Parse(editStr);
                    if (editInt < 1 || editInt > list.Count)
                    {
                        Console.WriteLine("Error: Integer option not available. Please enter an integer shown above.");
                    }
                    else
                    {
                        editInputValid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Non-Integer input. Please enter an integer shown above.");
                }
            }
            MakeLineSpace(1);

            // If match is found, prompt user to confirm their choice.
            //     If so, mark the specified task and return modified list.
            //     Otherwise, return unchanged list.
            string editMsg = "Are you sure that you want to edit Task #" + editInt + " with brand new values?";
            bool confirmEdit = !(AskToContinue(editMsg));
            if (confirmEdit)
            {
                // Ask for a name string
                string taskName = PromptForInput("Please enter the assigned Team Member's Name: ");

                // Ask for a description string
                string taskDesc = PromptForInput("Please enter a brief Description of the Task: ");

                // Ask for a deadline string, validate for valid format
                DateTime taskDedLn;
                while (true)
                {
                    string taskDedLnStr = PromptForInput("Please enter the Task's Deadline in dd/mm/yyyy format: ");
                    try
                    {
                        taskDedLn = DateTime.Parse(taskDedLnStr);
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error: Incorrect format, try again!");
                    }
                }

                // Ask for a isComplete string
                bool taskIsComp = !(AskToContinue("Is the task completed? "));

                // Change values of the target Task
                list[editInt - 1].MemberName = taskName;
                list[editInt - 1].Description = taskDesc;
                list[editInt - 1].Deadline = taskDedLn;
                list[editInt - 1].IsComplete = taskIsComp;
                Console.WriteLine("Editing!");
            }
            else
            {
                Console.WriteLine("Aborting! Returning to Main Menu!");
            }
            PauseByAnyKey();
            return list;
        }

        // Selection 6: Check Schedule
        // Prints the Task List and then pauses to let the user read through the list
        public static void CheckSchedule(List<ToDoTask> list)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("Upcoming Tasks");
            Console.WriteLine("==========================================");
            
            // Ask for a deadline string, validate for valid format
            DateTime taskDedLn;
            while (true)
            {
                string taskDedLnStr = PromptForInput("Please enter a date in dd/mm/yyyy format: ");
                try
                {
                    taskDedLn = DateTime.Parse(taskDedLnStr);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Incorrect format, try again!");
                }
            }

            // Create a new list of ToDoTasks with Deadline values that fall before the given date.
            List<ToDoTask> upcomingTasks = new List<ToDoTask>();
            foreach (ToDoTask task in list)
            {
                if (task.Deadline < taskDedLn)
                {
                    upcomingTasks.Add(task);
                }
            }
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("The following Tasks are due before " + taskDedLn.ToShortDateString());
            MakeLineSpace(1);
            PrintList(upcomingTasks);
            if (upcomingTasks.Count == 0)
            {
                Console.WriteLine("None.");
                MakeLineSpace(1);
            }
            Console.WriteLine("==========================================");
            PauseByAnyKey();
        }

        // ============================================================================================================================
        // Formatting Methods: Provides common functionality for most input-oriented console applications

        // Prompts user for an input, with the message parameter serving as context. Returns the string generated by the user's input.
        // Does not allow blank inputs, and will repeat until an input is given.
        public static string PromptForInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string userInput = (Console.ReadLine()).Trim();
                if (userInput.Length > 0)
                {
                    return userInput;
                }
            }
        }

        // Prompts user if they want to continue using the program or a program functionality. 
        // If yes, then let the loop iterate. Otherwise, stop the loop by setting done to true.
        public static bool AskToContinue(string message)
        {
            while (true)
            {
                string promptMsg = message + " (y/n) ";
                string inputStr = PromptForInput(promptMsg);
                inputStr = inputStr.Trim().ToLower();
                if (inputStr.Equals("y"))
                {
                    return false;
                }
                else if (inputStr.Equals("n"))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: Please input y/Y or n/N.");
                }
            }
        }

        // Stops program until user inputs a keystroke
        public static void PauseByAnyKey()
        {
            Console.WriteLine("Press any Key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        // Adds empty lines in console for formatting
        public static void MakeLineSpace(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine(" ");
            }
        }
    }
}

