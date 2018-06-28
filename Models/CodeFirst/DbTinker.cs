namespace Models.CodeFirst {
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    public class DbTinker : DbContext {

        public DbTinker()
            : base("name=CodeFirst") {

            // 所有Model Class的修改会同步更新到数据表，并且数据会迁移
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbTinker, DbEditConfig>());
            // 当数据库不存在时，创建对应数据库
            Database.CreateIfNotExists();
        }

        /// <summary>
        /// 数据库更新配置
        /// </summary>
        internal sealed class DbEditConfig : DbMigrationsConfiguration<DbTinker> {
            public DbEditConfig() {
                // 任何Model Class的修改将会直接更新数据库
                AutomaticMigrationsEnabled = true;
                // 更新时允许数据丢失
                AutomaticMigrationDataLossAllowed = true;
            }
        }

        #region 数据表

        public DbSet<DT_User> DT_User { get; set; }

        #endregion
    }

}