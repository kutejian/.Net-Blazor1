using LearnBlazorDto.Models;
using LearnBlazorEntity.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBlazorRepository.Repository
{
    public class MongoDBHelper
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;

        public MongoDBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = MongoDB();
        }

        public string ConnectionString => _configuration.GetSection("MongoDb").GetSection("DefaultConnection").Value;

        public MongoClient MongoDB()
        {
            MongoClient mongoClient = new MongoClient(ConnectionString);

            return mongoClient;
        }

        //数据初始化  T类型是 数据对象类型  AssembleList 是在数据库中叫什么名字的集合
        public IMongoCollection<T> DateBaseCreation<T>(string AssembleList)
        {
            var database = _mongoClient.GetDatabase("LearnBlazor");
            //创建数据库
            var collection = database.GetCollection<T>(AssembleList);

            return collection;
        }

    }
}
