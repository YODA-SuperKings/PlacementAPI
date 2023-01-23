using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Models
{
    public class StudentDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int RegistrationNumber { get; set; }
        public string StudentName { get; set; }
        public byte[] Image  { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int SSLC { get; set; }
        public int HSC { get; set; }
        public string Course { get; set; }
        public string InstituteName { get; set; }
        public string University { get; set; }
        public int YearOfPassing { get; set; }
        public int Percentage { get; set; }
        public string RoundOne { get; set; }
        public string RoundTwo { get; set; }
        public string IsGotOffer { get; set; }
        public string CompanyName { get; set; }
        public string PlacementDate { get; set; }
    }
}
