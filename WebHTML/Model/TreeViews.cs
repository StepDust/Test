using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace WebHTML.Modle {
    public class TreeViews {

        public static List<Menu> List { get; set; }

        /// <summary>
        /// 第一种存储结构，Pid=id
        /// </summary>
        public static void GetSoreDate() {
            DemoEntities DB = new DemoEntities();
            List<Menu01> Data = DB.Menu01.ToList();

            List = new List<Menu>();
            SoreMenu01(Data, 0, 1);
        }

        private static void SoreMenu01(List<Menu01> Data, int Pid, int layer) {

            List<Menu01> temp = Data.Where(c => c.id == Pid).ToList();

            foreach (Menu01 item in temp) {
                Menu menu = new Menu();
                menu.ID = item.id;
                menu.Pid = item.pid;
                menu.Title = item.title;
                menu.layer = layer;

                List.Add(menu);
                SoreMenu01(Data, item.id, layer + 1);
            }
        }

        /// <summary>
        /// 第二种存储结构，Pid=id+pid
        /// </summary>
        /// <param name="Data"></param>
        public static void GetSoreDate(List<Menu02> Data) { }


        /// <summary>
        /// 递归循环菜单
        /// </summary>
        /// <param name="Pid"></param>
        /// <param name="PNode"></param>
        /// <param name="UrlName"></param>
        /// <param name="t">treeView</param>
        public void AddTree(int Pid, TreeNode PNode, string UrlName, TreeView t, string id = "") {

            //查询所有Type数据
            DataTable dt = BBL.Type_B.selectByTable();
            if (dt.Rows.Count > 0) {
                //;1;2;
                foreach (DataRow Row in dt.Rows) {
                    //获取tid的数据截取成string数组
                    string[] str = Row["TId"].ToString().Split(';');
                    //过滤非顶级节点
                    if (str.Length != 3)
                        continue;

                    PNode = new TreeNode();
                    //绑定超级链接
                    PNode.NavigateUrl = string.Format(UrlName + ".aspx?id={0}", Row["Id"]);

                    //判断当前节点id是否等于选中的id
                    bool bl = Row["Id"].ToString() == id;
                    //bool bl = Row["Id"].ToString() == id;
                    PNode.Expanded = bl;
                    PNode.Selected = bl;
                    PNode.Text = Row["TypeName"].ToString();
                    PNode.Value = Row["TypePrompt"].ToString();
                    //父级ID，顶级节点
                    AddTreeNode(str[1], ref PNode, UrlName, t, id, 1);

                    FindOpen(ref PNode);

                    t.Nodes.Add(PNode);

                }
            }
        }

        public void AddTreeNode(string Pid, ref TreeNode PNode, string UrlName, TreeView t, string id, int layer) {
            if (id == null)
                id = "";
            //查询父级下的所有子级节点的数据
            DataTable dt = BBL.Type_B.selectByTable(" TId like '%;" + Pid + ";%'");

            if (dt.Rows.Count > 0) {
                //循环递归
                foreach (DataRow Row in dt.Rows) {
                    string[] str = Row["TId"].ToString().Split(';');
                    //layer是当前层数，layer+3是当层数对应的tid分割后的数量
                    //列如第一层次级layer=1，对应的tid是；1；12；，分割后长度是4(layer+3)
                    //过滤掉不是当前层数子级
                    if (str.Length != layer + 3)
                        continue;

                    //声明节点
                    TreeNode Node = new TreeNode();
                    //绑定超级链接
                    Node.NavigateUrl = string.Format(UrlName + ".aspx?id={0}", Row["Id"]);

                    Node.Value = Row["TypePrompt"].ToString();
                    //添加当前节点的子节点
                    Node.Text = Row["TypeName"].ToString();

                    //判断当前节点id是否等于选中的id
                    bool bl = Row["Id"].ToString() == id;
                    //bool bl = Row["Id"].ToString() == id;
                    if (bl) {
                        //根据id找到对应的选中节点
                        Node.Selected = bl;
                        //判断父节点是否为空（不为空展开父节点）
                        if (PNode != null)
                            PNode.Expanded = bl;
                    }
                    else {
                        int con = 0;
                        //判断父节点是否为空
                        if (PNode != null) {
                            //循环父节点的子节点
                            foreach (TreeNode item in PNode.ChildNodes) {
                                if (item.Selected == true)
                                    break;
                                con++;
                            }
                            if (con == PNode.ChildNodes.Count)
                                PNode.Expanded = false;
                        }
                    }

                    AddTreeNode(Row["Id"].ToString(), ref Node, UrlName, t, id, layer + 1);
                    PNode.ChildNodes.Add(Node);//子节点

                }
            }
        }
        public void FindOpen(ref TreeNode node, int layer = 0) {
            if (node.Expanded ?? false)
                return;
            //寻找所有子级节点
            for (int i = 0; i < node.ChildNodes.Count; i++) {//依次取出找到的所有的节点
                TreeNode temp = node.ChildNodes[i];
                // num[layer] = i+"";

                FindOpen(ref temp, layer + 1);
                //若找到的子节点是打开状态父级节点也必须为打开状态
                node.Expanded = temp.Expanded ?? false;
                if (temp.Expanded ?? false) {//打开顶级节点
                    if (layer == 0)
                        node.Expanded = true;
                    break;
                }
            }
        }

    }

    public class Menu {
        public int ID;
        public string Pid;
        public string Title;
        public int layer;
    }

}