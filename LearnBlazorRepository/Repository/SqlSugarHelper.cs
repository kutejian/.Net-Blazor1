using LearnBlazorEntity.Models;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.Configuration;

namespace LearnBlazorRepository.Repository
{
    public class SqlSugarHelper
    {
        private readonly IConfiguration _configuration;

        public SqlSugarHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString => _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

        public SqlSugarClient SqlSugarDb()
        {
            ConnectionConfig connectionConfig = new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                IsAutoCloseConnection = true,
                DbType = DbType.SqlServer
            };

            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响

                    //获取原生SQL推荐 5.1.4.63  性能OK
                    //UtilMethods.GetNativeSql(sql,pars)

                    //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                    //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)
                };
                DateBaseCreation(db);
                return db;
            }
        }

        //数据初始化
        public void DateBaseCreation(SqlSugarClient Db)
        {
            //创建数据库
            Db.DbMaintenance.CreateDatabase();
            //创建表
            if (!Db.DbMaintenance.IsAnyTable("CategoryEntity"))
                Db.CodeFirst.InitTables<CategoryEntity>();
            if (!Db.DbMaintenance.IsAnyTable("ProductEntity"))
                Db.CodeFirst.InitTables<ProductEntity>();
            if (!Db.DbMaintenance.IsAnyTable("ProductImageEntity"))
                Db.CodeFirst.InitTables<ProductImageEntity>();
        }
    }
}