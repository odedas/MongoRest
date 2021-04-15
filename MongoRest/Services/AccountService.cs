using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoRest.Models;

namespace MongoRest.Services
{
    public class AccountService  // : IAccountCrud
    {
        private readonly IMongoCollection<Account> _account;

        public AccountService(IConfiguration config)
        {
            var client = new MongoClient(config["AccountsDatabaseSettings:ConnectionString"]);

            var db = client.GetDatabase(config["AccountsDatabaseSettings:DatabaseName"]);
            _account = db.GetCollection<Account>(config["AccountsDatabaseSettings:AccountsCollectionName"]);
        }

        public bool Create(Account acc)
        {
            _account.InsertOne(acc);
            return true;
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Create(int id, string name)
        {
            Account acc = new Account(id, name);
            _account.InsertOne(acc);
            return true;
            // throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public Account Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Account> Get()
        {
            // return _account.Find({ });
            throw new NotImplementedException();
        }

        public Account Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
