using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using ToDo.Model;

namespace ToDo.services
{
    public class ToDoService : ToDoInterface
    {

        private MyDataContext _db;
        public ToDoService(MyDataContext db)
        {
            _db = db;
        }
        public Tasks createtask(string Title, string Description, int IsCompleted, string Category)
        {
            Tasks tasks = new Tasks();
            tasks.Title = Title;
            tasks.Description = Description;
            tasks.IsCompleted = IsCompleted;
            tasks.Category = Category;
            _db.Tasks.Add(tasks);
            _db.SaveChanges();
            return tasks;
        }
       
        public void deletetask(int id)
        {
            var tasks = _db.Tasks.First(x => x.ID ==id);
            _db.Tasks.Remove(tasks);
            _db.SaveChanges();
        }

        public Tasks updatetask(string Title, string Description, int IsCompleted, string Category,int id)
        {
            var check = _db.Tasks.FirstOrDefault(x => x.ID == id);
            if (check == null)
            {
                return check;
            }
            else
            {
                check.Title = Title;
                check.Description = Description;
                check.IsCompleted = IsCompleted;
                check.Category = Category;
                _db.Tasks.Update(check);
                _db.SaveChanges();
                return check;
            }
        }

        public List<Tasks> viewAlltaskes()
        {
            List<Tasks> TasksList;
            try
            {
                TasksList = _db.Tasks.OrderBy(task => task.IsCompleted).OrderBy(task => task.Category).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return TasksList;
        }

        public Tasks viewtask(int id)
        {
            var check = _db.Tasks.First(x => x.ID == id);
                return check;
        }
    }
}
