using Factory;
using Interface.DataBase;
using Interface.DataBase.DAL;

namespace BLL.CodeFirst {
    public abstract class AbstractService<T> where T : IDataBase {

        public T Service => DataBaseFactory.CreateBllBase<T>();

        public void BeginTrans() => Service.BeginTrans();

        public int Commit() => Service.Commit();

        public void Rollback() => Service.Rollback();

    }
}
