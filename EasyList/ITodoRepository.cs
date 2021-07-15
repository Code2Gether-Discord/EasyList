﻿using System;
using System.Collections.Generic;

namespace EasyList
{
    interface ITodoRepository
    {
        void Add(Todo todo);
        Todo? Get(int Id);
        IEnumerable<Todo> GetAllTodo(TodoOrder orderOfList = TodoOrder.CreateDate);
        void Update(Todo todo);
        void Delete(Todo todo);
    }
}
