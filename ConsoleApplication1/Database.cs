using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApplication1
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Database : DbContext
    {
        // Контекст настроен для использования строки подключения "Database" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "ConsoleApplication1.Database" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "Database" 
        // в файле конфигурации приложения.
        public Database()
            : base("name=Database")
        {
        }

        static Database()
        {
            System.Data.Entity.Database.SetInitializer<ConsoleApplication1.Database>(new CreateDatabaseIfNotExists<Database>());
        }

        public DbSet<Users> Users {get; set;}
        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    [Table("USERS")]
    public class Users
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public int AGE { get; set; }
    }
}