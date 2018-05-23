using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common.Study {
    public class Code {

        private static string ProjectPath => Regular.RepStr(Path.GetFullPath(@".\"), @"[\\]{1}\w{1,}[\\]{1}bin.*", "\\");

        private static string BllStr => "Service";

        private static string DalStr => "Base";

        private static string[] Db => new string[] { "Models", "Models.CodeFirst" };

        private static string[] Bll => new string[] { Constant.BllPath, "BLL.CodeFirst" };
        private static string[] BllInterface => new string[] { "Interface", "Interface.DataBase.BLL" };

        private static string[] Dal => new string[] { Constant.DalPath, "DAL.CodeFirst" };
        private static string[] DalInterface => new string[] { "Interface", "Interface.DataBase.DAL" };
        
        public static void Run() {

            Assembly assembly = Assembly.Load(Db[0]);
            Type type = assembly.GetType(Db[1] + ".CodeFirst");

            // 获取所有数据表
            foreach (var item in type.GetProperties()) {
                // 获取数据表
                if (item.GetMethod.IsPublic && item.GetMethod.ReturnType.Name == "DbSet`1") {
                    CreateDal(item);
                    CreateBll(item);
                }

            }
        }
        
        private static void CreateDal(PropertyInfo propertyInfo) {

            string className = propertyInfo.Name + DalStr;

            string classContext = $"" +
                $"using {Db[1]};\n" +
                $"using {DalInterface[1]};\n\n" +
                $"namespace {Dal[1]}" + " {\n" +
                $"    public class {className} : BaseService<{propertyInfo.Name}>, I{className} " + "{\n\n";

            string interContext = $"" +
                $"using {Db[1]};\n\n" +
                $"namespace {DalInterface[1]} " + "{\n" +
                $"    public interface I{className}:IDataBase " + "{\n";

            Type classType = CreateFile(className, Dal, classContext);
            Type interType = CreateFile("I" + className, DalInterface, interContext);

            UpdateFile(classType, interType, propertyInfo.Name, interContext);
        }

        private static void CreateBll(PropertyInfo propertyInfo) {

            string className = propertyInfo.Name + BllStr;

            string classContext = $"" +
                $"using {Db[1]};\n" +
                $"using {DalInterface[1]};\n" +
                $"using {BllInterface[1]};\n\n" +
                $"namespace {Bll[1]}" + " {\n" +
                $"    public class {className} : AbstractService<I{propertyInfo.Name + DalStr}>, I{className} " + "{\n\n";

            string interContext = $"" +
                $"using {Db[1]};\n\n" +
                $"namespace {BllInterface[1]} " + "{\n" +
                $"    public interface I{className} : IDataBase " + "{\n";

            Type classType = CreateFile(className, Bll, classContext);
            Type interType = CreateFile("I" + className, BllInterface, interContext);

            UpdateFile(classType, interType, propertyInfo.Name, interContext);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <param name="info">相关信息</param>
        /// <param name="context">文件内容</param>
        /// <returns></returns>
        private static Type CreateFile(string name, string[] info, string context) {
            Assembly assembly;
            if (info[0].IndexOf("\\") > 0)
                assembly = Assembly.LoadFile(info[0]);
            else
                assembly = Assembly.Load(info[0]);

            Type type = assembly.GetType($"{info[1]}.{name}");

            if (type == null) {
                using (StreamWriter writer = File.CreateText($"{ProjectPath}{string.Join("\\", info[1].Split('.'))}\\{name}.cs")) {
                    context += "    }\n}";

                    writer.Write(context);
                    writer.Close();
                }
            }
            return type;
        }

        /// <summary>
        /// 更新接口
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="interType"></param>
        private static void UpdateFile(Type classType, Type interType, string taleName, string interContext) {
            if (classType == null || interType == null)
                return;
            List<string> usList = new List<string>();
            List<string> methList = new List<string>();

            string meth = "";

            foreach (MethodInfo method in classType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)) {
                usList.Add(method.ReturnType.Namespace);
                meth = GetParameterType(method.ReturnType, taleName) + " " + method.Name + "(";

                meth += string.Join(",",
                    method.GetParameters().Select(c =>
                    {
                        usList.Add(c.ParameterType.Namespace);
                        return GetParameterType(c.ParameterType, taleName) + " " + c.Name;
                    }));

                meth += ")";

                methList.Add(meth);
            }

            usList = usList.Distinct().OrderBy(c => c.Length).ToList();

            interContext = string.Join("\n", usList.Select(c => $"using {c};")) + "\n\n" + interContext;
            interContext += "\n";

            foreach (var item in methList)
                interContext += $"         {item};\n\n";

            using (StreamWriter writer = File.CreateText($"{ProjectPath}{string.Join("\\", interType.Namespace.Split('.'))}\\{(interType.IsGenericType ? interType.Name.Substring(0, interType.Name.LastIndexOf("`")) : interType.Name)}.cs")) {
                interContext += "    }\n}";

                writer.Write(interContext);
                writer.Close();
            }
        }

        private static string GetParameterType(Type type, string tableName) {

            string res = "";

            if (!type.IsGenericType) {
                //res = type.Name == tableName ? "T" : type.Name;
                res = type.Name;
            }
            else {
                res = type.Name.Substring(0, type.Name.LastIndexOf("`")) + "<";
                res += string.Join(",", type.GenericTypeArguments.Select(c => GetParameterType(c, tableName))) + ">";
            }

            switch (res) {
                case "String": return "string";
                case "Boolean": return "bool";
                case "Int32": return "int";
                case "Void": return "void";
                default: return res;
            }
        }


    }
}