using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList
{
    interface ITodoLiteDBRepository : ITodoRepository
    {
        void UpdateTodo(Todo todo);
    }
}
