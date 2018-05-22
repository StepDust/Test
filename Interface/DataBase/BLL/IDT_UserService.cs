using Models.CodeFirst;

namespace Interface.DataBase.BLL {
    public interface IDT_UserService : IDataBase {

         DT_User AddEntity();
    }
}