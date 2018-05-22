using Models.CodeFirst;
using Interface.DataBase.DAL;
using System;

namespace DAL.CodeFirst {
    public class DT_UserBase : BaseService<DT_User>, IDT_UserBase {
        public DT_User AddEntity() {
            DT_User user = new DT_User
            {
                Name = new Random().Next(0, 999999) + ""
            };

            return AddEntity(user);
        }
    }
}