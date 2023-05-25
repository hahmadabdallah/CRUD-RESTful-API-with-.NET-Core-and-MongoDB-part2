using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace MongoExample.Services;

public class MongoDBService {

    private readonly IMongoCollection<Employees> _employeesCollection;

     public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _employeesCollection = database.GetCollection<Employees>(mongoDBSettings.Value.CollectionName);
    }
    
        public List<Employees> Get() => _employeesCollection.Find(employee => true).ToList();

        public Employees Get(string id) => _employeesCollection.Find(employee => employee.Id == id).FirstOrDefault();

        public Employees Create(Employees employee)
        {
            _employeesCollection.InsertOne(employee);
            return employee;
        }

        public void Update(string id, Employees updatedEmployee) => _employeesCollection.ReplaceOne(employee => employee.Id == id, updatedEmployee);

        public void Delete(Employees employeeForDeletion) => _employeesCollection.DeleteOne(employee => employee.Id == employeeForDeletion.Id);

        public void Delete(string id) => _employeesCollection.DeleteOne(employee => employee.Id == id);

}