using System;
using System.Collections.Generic;
using System.Text;

namespace Week2_TaskList
{
    class TheTask
    {
        public string Member { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public bool IsComplete { get; set; }

        public TheTask(string Member, string Description, string DueDate)
        {
            this.Member = Member;
            this.Description = Description;
            this.DueDate = DueDate;
            this.IsComplete = false;
        }

        public void PrintTasks(int c)
        {
            Console.WriteLine(String.Format($"{c}) {Member,-10} {DueDate,15} {IsComplete,-5}\t {Description,-30}"));
        }
    }
}
