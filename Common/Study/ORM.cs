using System;
using System.IO;
using System.Linq;
using System.Text;
using Common.AttributeExpand;
using System.Collections.Generic;
using Common.AttributeExpand.Validates;

namespace Common.Study {
    /// <summary>
    /// 
    /// Date：2018-05-21 19:31:43
    /// </summary>
    public class ORM {
        private static List<DataBaseTableColumn> GetColumnInfo(string tableName) {
            string sqlStr = @"
                SELECT
                    ColumnNo   = a.colorder,
                    ColumnName     = a.name,
                    [TypeName]       = b.name,
                    [Bit] = a.length,
                    [Length]       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
                    [IsNull]     = case when a.isnullable=1 then 1 else 0 end,
                    [Default]     = isnull(e.text,''),
                    ColumnDescribe   = isnull(g.[value],'')
                FROM syscolumns a
                    left join systypes b on  a.xusertype=b.xusertype
                    inner join sysobjects d on  a.id=d.id and d.xtype='U' and d.name<>'dtproperties'
                    left join syscomments e on    a.cdefault=e.id
                    left join sys.extended_properties g on   a.id=g.major_id and a.colid=g.minor_id
                    left join sys.extended_properties f on  d.id=f.major_id and f.minor_id=0
                where 
                    d.name='" + tableName + @"'
                order by
                    a.id,a.colorder";

            return default;//BaseDBHelper.Find<DataBaseTableColumn>(sqlStr);
        }

        /// <summary>
        /// 数据库中与C#中的数据类型对照
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string ChangeToCSharpType(string type) {
            string reval = string.Empty;
            switch (type.ToLower()) {
                case "int":
                case "float":
                    reval = type.ToLower(); break;

                case "bigint":
                    reval = "long"; break;

                case "text":
                case "char":
                case "nchar":
                case "ntext":
                case "varchar":
                case "sysname":
                case "nvarchar":
                    reval = "string"; break;

                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    reval = "decimal"; break;

                case "datetime":
                case "timestamp":
                case "smalldatetime":
                    reval = "DateTime"; break;

                case "binary":
                case "image":
                case "varbinary":
                    reval = "Byte[]"; break;

                case "bit":
                    reval = "bool"; break;
                case "real":
                    reval = "Single"; break;
                case "tinyint":
                    reval = "Byte"; break;
                case "uniqueidentifier":
                    reval = "Guid"; break;
                case "Variant":
                    reval = "Object"; break;

                default:
                    reval = "String"; break;
            }
            return reval;
        }

        /// <summary>
        /// 拼接Model内容
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetModelStr(string tableName, Type type) {

            // 获取父类所有属性
            List<string> proList = new List<string>();

            foreach (var item in type.GetProperties())
                proList.Add(item.Name.ToUpper());

            // 拼接Model内容
            StringBuilder builder = new StringBuilder();

            builder.Append("using System;\n");
            builder.Append("using System.Linq;\n");
            builder.Append("using System.Text;\n");
            builder.Append("using System.Threading.Tasks;\n");
            builder.Append("using Common.AttributeExpand;\n");
            builder.Append("using Common.AttributeExpand.Validates;\n");

            builder.Append("\n");

            builder.Append("namespace " + type.Namespace + " {\n");
            builder.Append("    public class " + tableName + " : " + type.Name + " {\n\n");

            // 查询并拼接数据表字段
            foreach (var item in GetColumnInfo(tableName)) {

                if (!proList.Contains(item.ColumnName.ToUpper())) {

                    item.TypeName = ChangeToCSharpType(item.TypeName);

                    // 添加注释
                    builder.Append("        /// <summary>\n");
                    builder.Append("        /// " + item.ColumnDescribe + "\n");
                    builder.Append("        /// </summary>\n");


                    // 添加非空
                    if (item.IsNull == 0)
                        builder.Append($"        [{nameof(RequirdVailDateAttribute)}]\n");

                    // 添加长度校验
                    if (item.TypeName == "string")
                        builder.Append($"        [{nameof(LengthVailDateAttribute)}(0, {item.Length })]\n");
                    else if (item.IsNull == 1)
                        item.TypeName += "?";

                    // 添加时间校验
                    if (item.TypeName.ToLower().Contains("datetime"))
                        builder.Append($"        [{nameof(DateTimeValidateAttribute)}]\n");

                    // 添加字段描述
                    builder.Append($"        [{nameof(TableAttribute) }(\"{item.ColumnName}\", Remark = \"{item.ColumnDescribe }\")]\n");

                    builder.Append("        public " + item.TypeName + " " + item.ColumnName + " { get; set; }\n\n");
                }

            }

            builder.Append("    }\n");
            builder.Append("}\n");

            return builder.ToString();
        }

        /// <summary>
        /// 创建实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void CreateModel<T>(string path) {

            string sqlStr = "SELECT Name,Crdate FROM SYSOBJECTS WHERE TYPE = 'U'";
            sqlStr += "";
            Type type = typeof(T);

            // 补齐斜杠
            if (path.LastIndexOf("\\") != path.Length - 1)
                path += "\\";

            // 根据命名空间，补齐路径
            if (type.Namespace.Contains('.'))
                path += string.Join("\\", type.Namespace.Split('.'));

            // 获得绝对路径
            path = Path.GetFullPath(path);

            // 根据绝对路径创建文件夹
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // 查询所有数据表
            foreach (var item in new List<DataBaseTable>()) {
                //BaseDBHelper.Find<DataBaseTable>(sqlStr)) {

                StreamWriter file = new StreamWriter($"{path}\\{item.Name}.cs", false);

                file.Write(GetModelStr(item.Name, type));

                file.Close();
                file.Dispose();
            }
        }
    }

    /// <summary>
    /// 数据表信息类
    /// Date：2018-05-21 19:34:03
    /// </summary>
    public class DataBaseTableColumn {
        /// <summary>
        /// 字段编号
        /// </summary>
        public int ColumnNo { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 占用空间
        /// </summary>
        public int Bit { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 是否可为空
        /// </summary>
        public int IsNull { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string ColumnDescribe { get; set; }
    }

    /// <summary>
    /// 数据库表类型
    /// </summary>
    public class DataBaseTable {

        /// <summary>
        /// 数据表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据表创建时间
        /// </summary>
        public DateTime Crdate { get; set; }

    }
}