using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;

namespace ToDo.services
{
    public interface ToDoInterface
    {
        Tasks createtask(string Title, string Description, int IsCompleted, string Category);
        Tasks updatetask(string Title, string Description, int IsCompleted, string Category,int id);
        List<Tasks> viewAlltaskes();
        Tasks viewtask(int id);
        void deletetask(int id);
    }
}
