using System;

namespace EasyList
{
    public interface ITaskRepository
    {
        public void Add(string Label);
        public void Delete(int Id);
        public void Read(int Id);
        public void Done(int Id);
        public void ListAllTask();
    }
}
