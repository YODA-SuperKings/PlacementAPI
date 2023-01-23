using MongoDB.Driver;
using PlacementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Services
{
    public class StudentDetailService
    {
        private readonly IMongoCollection<StudentDetails> _studentDetailCollection;
        private readonly IMongoCollection<User> _usersCollection;
        public StudentDetailService(IPlacementDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);
            _studentDetailCollection = mongoDatabase.GetCollection<StudentDetails>("StudentDetails");
            _usersCollection = mongoDatabase.GetCollection<User>("User");
        }

        public async Task<List<StudentDetails>> GetAsync() =>
             await _studentDetailCollection.Find(_ => true).ToListAsync();

        public async Task<StudentDetails?> GetAsync(string id) =>
            await _studentDetailCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public List<StudentDetails> Get()
        {
            List<StudentDetails> students;
            students = _studentDetailCollection.Find(s => true).ToList();
            return students;
        }

        public List<StudentDetails> GetStudentsByCompany(string emailid)
        {
            string loggedInCompanyName = "";
            List<StudentDetails> students;
            loggedInCompanyName = _usersCollection.Find(u => u.Email == emailid).FirstOrDefault().Name;
            students = _studentDetailCollection.Find(s => s.CompanyName == loggedInCompanyName).ToList();
            return students;
        }

        public StudentDetails Get(string id) =>
            _studentDetailCollection.Find<StudentDetails>(u => u.Id == id).FirstOrDefault();

        public string CreateStudent(StudentDetails student)
        {
            string msg = "";
            bool isStudentEmailExists = _studentDetailCollection.Find(sd => sd.Email == student.Email).Any() ? true : false;
            bool isStudentRegNoExists = _studentDetailCollection.Find(sd => sd.RegistrationNumber == student.RegistrationNumber).Any() ? true : false;
            if (isStudentEmailExists)
                msg = "Student email already exists.";
            else if(isStudentRegNoExists)
                msg = "Student registration number already exists.";
            else
            {
                _studentDetailCollection.InsertOne(student);
                msg = "Student details added Successfully";
            }
            return msg;
        }

        public async Task UpdateAsync(string id, StudentDetails user) =>
            await _studentDetailCollection.ReplaceOneAsync(x => x.Id == id, user);

        public async Task RemoveAsync(string id) =>
            await _studentDetailCollection.DeleteOneAsync(x => x.Id == id);

    }
}
