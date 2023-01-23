using MongoDB.Driver;
using PlacementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        public UserService(IPlacementDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>("User");
        }

        public async Task<List<User>> GetAsync() =>
             await _usersCollection.Find(usr => true).ToListAsync();

        public IEnumerable<User> GetUsers() =>
             _usersCollection.Find(usr => true).ToList();

        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public List<User> Get()
        {
            List<User> users;
            users = _usersCollection.Find(emp => true).ToList();
            return users;
        }

        public User GetUserMailDetails(string email)
        {
            User users;
            users = _usersCollection.Find(u => u.Email == email).FirstOrDefault();
            return users;
        }

        public List<Companies> GetCompanies()
        {
            List<User> users;
            List<Companies> companies = new List<Companies>();
            users = _usersCollection.Find(u => u.RegistrationType == 2).ToList();
            foreach (var item in users)
            {
                Companies company = new Companies();
                company.label = item.Name;
                company.value = item.Name;
                companies.Add(company);
            }
            return companies;
        }

        public User Get(string id) =>
            _usersCollection.Find<User>(u => u.Id == id).FirstOrDefault();

        public async Task CreateAsync(User user) =>
              await _usersCollection.InsertOneAsync(user);

        public string CreateUser(User user)
        {
            string msg = "";
            bool isUserExists = _usersCollection.Find(usr => usr.Email == user.Email).Any() ? true : false;
            bool isNameExists = _usersCollection.Find(usr => usr.Name == user.Name).Any() ? true : false;
            if (isUserExists)
                msg = "User email already exists.";
            else if(isNameExists)
            {
                if(user.RegistrationType == 2)
                    msg = "Company name already exists.";
            }
            else
            {
                _usersCollection.InsertOne(user);
                msg = "Added Successfully";
            }
            return msg;
        }

        public async Task UpdateAsync(string id, User user) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, user);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);

    }
}
