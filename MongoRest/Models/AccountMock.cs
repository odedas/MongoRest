using System;
using System.Collections.Generic;

namespace MongoRest.Models
{
    public class AccountMock : IAccountCrud
    {
        private List<Account> accounts;

        public AccountMock()
        {
            accounts = new List<Account>
            {
                new Account(1,"Oded"),
                new Account(2,"Yael"),
                new Account(3,"Noam"),
                new Account(4,"Galya"),
                new Account(4,"Ayala"),
            };
        }

        public bool Create(Account acc)
        {
            accounts.Add(new Account(acc.Id, acc.Name));
            return true;
        }

        public bool Create(int id, string name)
        {
            accounts.Add(new Account(id, name));
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int[] ids)
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public Account Get(int id)
        {
            return accounts.Find(acc => acc.Id == id);
            // throw new NotImplementedException();
        }

        public List<Account> GetAll()
        {
            return accounts;
        }

        public List<Account> GetTop(int top)
        {
            if(top > accounts.Count)
            {
                top = accounts.Count;
            }

            return accounts.GetRange(0,top);
        }

        public Account Update(int id)
        {
            throw new NotImplementedException();
        }

        public Account UpdateAll()
        {
            throw new NotImplementedException();
        }
    }
}
