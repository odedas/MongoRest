using System;
using System.Collections.Generic;

namespace MongoRest.Models
{
    public interface IAccountCrud
    {
        public bool Create(Account acc);
        public bool Create(int id, string name);
        public Account Get(int id);
        public List<Account> GetAll();
        public List<Account> GetTop(int top);
        public bool Delete(int id);
        public bool Delete(int[] id);
        // public bool Delete(Account acc);
        public bool Delete();
        public Account Update(int id);
        public Account UpdateAll();

    }
}
