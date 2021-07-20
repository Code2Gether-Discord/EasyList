using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList
{
    class Factory
    {
        public static ITodoLiteDBRepository CreateDBRepository()
        {
            return new TodoLiteDbRepository();
        }
        public static ITodoRepository CreateInMemoryRepository()
        {
            return new TodoRepository();
        }
    }
}
