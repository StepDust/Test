namespace Interface.DataBase {
    public interface IDataBase {
        void BeginTrans();
        int Commit();
        void Rollback();
    }
}
