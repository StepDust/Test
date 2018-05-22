using Models.CodeFirst;
using Interface.DataBase.DAL;
using Interface.DataBase.BLL;

namespace BLL.CodeFirst {
    public class DT_UserService : AbstractService<IDT_UserBase>, IDT_UserService {
        
        public DT_User AddEntity() {
            return Service.AddEntity();
        }
        
    }
}