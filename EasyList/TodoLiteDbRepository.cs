using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace EasyList
{
    class TodoLiteDbRepository : ITodoRepository
    {
        private readonly LiteDatabase _liteDB;
        private readonly ILiteCollection<Todo> _todoCollection;
        public TodoLiteDbRepository()
        {
            BsonMapper.Global.RegisterType<DateTimeOffset>(
                serialize: value => value.ToString("o", CultureInfo.InvariantCulture),
                deserialize: bson => DateTimeOffset.ParseExact(bson, "o", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString;
            _liteDB = new LiteDatabase(connectionString);
            _todoCollection = _liteDB.GetCollection<Todo>(nameof(Todo));
        }
        public void Add(Todo todo)
        {
            _todoCollection.Insert(todo);
        }
        public Todo? GetTodo(int Id)
        {
            return _todoCollection.FindById(Id);
        }
        public IEnumerable<Todo> GetAllTodo(TodoOrder orderOfList = TodoOrder.CreateDate)
        {
            var todoQuery = _todoCollection.Query();
            todoQuery = orderOfList switch
            {
                TodoOrder.DueDate => todoQuery.Where(todo => todo.Status == TodoStatus.InProgress)
                                                  .OrderByDescending(_todo => _todo.DueDate),
                TodoOrder.Priority => todoQuery.Where(todo => todo.Status == TodoStatus.InProgress)
                                                  .OrderByDescending(_todo => _todo.Priority),
                _ => todoQuery.Where(todo => todo.Status == TodoStatus.InProgress)
                                                  .OrderByDescending(_todo => _todo.CreatedDate)
            };
            return todoQuery.ToEnumerable();
        }
        public void Update(Todo todo)
        {
            _todoCollection.Update(todo);
        }
        public void Delete(Todo todo)
        {
            _todoCollection.Delete(todo.Id);
        }
    }
}
