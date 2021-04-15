using System;
namespace MongoRest.Models
{
    public class Account
    {
        public Account(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}
