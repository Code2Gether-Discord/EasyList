using LiteDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList
{
    class TodoDBRepository : ITodoRepository
    {

        readonly LiteDatabase db;
        readonly ILiteCollection<Todo> todoCollection;
        public TodoDBRepository()
        {
            BsonMapper.Global.RegisterType<DateTimeOffset>(
                serialize: value => value.ToString("o", CultureInfo.InvariantCulture),
                deserialize: bson => DateTimeOffset.ParseExact(bson, "o", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString;
            db = new LiteDatabase(connectionString);
            todoCollection = db.GetCollection<Todo>("todoList");

            Todo.TodoCount = todoCollection.Count();
            
        }
        public void Add(Todo todo)
        {
            todoCollection.Insert(todo);
        }

        public Todo? Get(int Id)
        {
            return todoCollection.FindById(Id);
        }

        public IEnumerable<Todo> GetAllTodo(TodoOrder orderOfList = TodoOrder.CreateDate)
        {
            ILiteQueryable<Todo> orderedList;
            switch(orderOfList)
            {
                case TodoOrder.DueDate:
                    {
                        orderedList = todoCollection.Query().Where(_todo => _todo.Status == TodoStatus.InProgress)
                                              .OrderByDescending(_todo => _todo.DueDate);
                        break;
                    }
                case TodoOrder.Priority:
                    {
                        orderedList = todoCollection.Query().Where(_todo => _todo.Status == TodoStatus.InProgress)
                                              .OrderByDescending(_todo => _todo.Priority);
                        break;
                    }
                default:
                    {
                        orderedList = todoCollection.Query().Where(_todo => _todo.Status == TodoStatus.InProgress)
                                              .OrderByDescending(_todo => _todo.CreatedDate);
                        break;
                    }
            }
            return orderedList.ToEnumerable();
        }

        public void Update(Todo todo)
        {
            todoCollection.Update(todo);
        }
        public void Delete(Todo todo)
        {
            todoCollection.Delete(todo.Id);
        }
    }
}
