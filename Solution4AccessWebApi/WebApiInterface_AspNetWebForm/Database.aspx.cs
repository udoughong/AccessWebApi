using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApiInterface_AspNetWebForm
{
    public partial class Database : System.Web.UI.Page
    {
        #region 通用宣告
        /// <summary>
        /// 目前所選擇的索引值
        /// </summary>
        private int thisSelectedIndex;

        /// <summary>
        /// 重新整理資料
        /// </summary>
        /// <typeparam name="T">標的資料類別</typeparam>
        /// <param name="gv">標的GridView控制項</param>
        /// <param name="targetClassName">資料類別名稱</param>
        /// <param name="targetClass">資料類別模組</param>
        private void ReflashData<T>(GridView gv, string targetClassName, T targetClass)
        {
            gv.DataSource = MyCRUD4Target.GetAll(targetClass, targetClassName);
            gv.DataBind();
        }

        /// <summary>
        /// 取得標的類別的項目數
        /// </summary>
        /// <param name="ClassName">標的類別名稱</param>
        /// <param name="inputControlJsonFile">標的類別項目JSON檔</param>
        /// <returns>標的類別項目數</returns>
        private int GetCountOfThisClass(string ClassName, string inputControlJsonFile)
        {
            JObject o = JObject.Parse(File.ReadAllText(Server.MapPath("~") + inputControlJsonFile));
            return o[ClassName].Count();
        }

        /// <summary>
        /// 我的自定義型別物件
        /// </summary>
        MyType myType = new MyType();

        /// <summary>
        /// 套入資料類別的資料操作程序物件
        /// </summary>
        MyCRUD<MainData> MyCRUD4Target = new MyCRUD<MainData>();

        /// <summary>
        /// 從標的GridView控制項中取得標的代碼
        /// </summary>
        /// <param name="gv">標的GridView控制項</param>
        /// <returns>標的代碼</returns>
        private int GetThisTargetId(GridView gv)
        {
            int thisTargetId = Convert.ToInt32(gv.SelectedIndex);
            int idOfSelected = Convert.ToInt32(gv.Rows[thisTargetId].Cells[1].Text);
            return idOfSelected;
        }

        /// <summary>
        /// 從指定類別模組取得資料清單
        /// </summary>
        /// <typeparam name="T">指定類別模組</typeparam>
        /// <param name="targetClass">標的類別模組</param>
        /// <param name="targetClassName">標的類別名稱</param>
        /// <returns>資料清單</returns>
        private List<T> MyAllData<T>(T targetClass, string targetClassName)
        {
            if (targetClassName == null)
            {
                throw new ArgumentNullException(nameof(targetClassName));
            }

            return MyCRUD4Target.GetAll(targetClass, targetClassName);
        }

        /// <summary>
        /// 從資料清單取得資料表
        /// LOOK>>https://stackoverflow.com/questions/18100783/how-to-convert-a-list-into-data-table
        /// </summary>
        /// <typeparam name="T">指定類別模組</typeparam>
        /// <param name="items">資料清單</param>
        /// <returns>資料表</returns>
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        #endregion

        /// <summary>
        /// 頁面預先載入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;
            RegisterUserControl();
        }

        /// <summary>
        /// 操作介面初始設定
        /// </summary>
        private void RegisterUserControl()
        {
            ClientCreateUserControl.Visible = false;
            ClientSearchUserControl.Visible = false;
            ClientUpdateUserControl.Visible = false;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;
        }

        /// <summary>
        /// 頁面正式載入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;
            if (IsPostBack)
            {
                #region 網站內執行程序
                #endregion
            }
            else
            {
                #region 進入網站執行程序
                MainData md = new MainData();
                ReflashData(GridView1, "MainData", md);
                InitCreateForm("MainData", ClientCreateUserControlArea, @"/Form.json");
                thisSelectedIndex = -1;
                #endregion
            }
        }

        #region 新增資料程序
        /// <summary>
        /// 請求新增資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAskCreate_Click(object sender, EventArgs e)
        {
            InitCreateForm("MainData", ClientCreateUserControlArea, @"/Form.json");
            ClientCreateUserControl.Visible = true;
        }

        /// <summary>
        /// 新增程序介面設定
        /// </summary>
        /// <param name="ClassName">類別名稱</param>
        /// <param name="thisPanel">作用面板</param>
        /// <param name="inputControlJsonFile">輸入欄位設定表檔案</param>
        private void InitCreateForm(string ClassName, Panel thisPanel, string inputControlJsonFile)
        {
            JObject o = JObject.Parse(File.ReadAllText(Server.MapPath("~") + inputControlJsonFile));
            thisPanel.Controls.Add(new Literal() { Text = "<br />" });
            for (int i = 1; i < o[ClassName].Count(); i++)
            {
                string strName = (string)o[ClassName][i]["Name"];
                string strCellName = (string)o[ClassName][i]["CellName"];
                Label lbName = new Label();
                thisPanel.Controls.Add(lbName);
                lbName.ID = "lb" + strCellName.ToUpper();
                lbName.Text = strName;
                string strTypeName = (string)o[ClassName][i]["TypeName"];
                TextBox tbStringCellName = new TextBox();
                TextBox tbIntCellName = new TextBox();
                TextBox tbDateTimeCellName = new TextBox();
                switch (strTypeName)
                {
                    #region example
                    //case "type":
                    //TextBox tbTypeCellName = new TextBox();
                    //thisPanel.Controls.Add(tbTypeCellName);
                    //break;
                    #endregion
                    case "string":
                        thisPanel.Controls.Add(tbStringCellName);
                        break;
                    case "int":
                        thisPanel.Controls.Add(tbIntCellName);
                        break;
                    case "DateTime":
                        thisPanel.Controls.Add(tbDateTimeCellName);
                        break;
                    default:
                        break;
                }
                if (!IsPostBack)
                {
                    switch (strTypeName)
                    {
                        #region example
                        //case "type":
                        //{
                        //tbTypeCellName.ID = "tb" + i.ToString();
                        //break;
                        //}
                        #endregion
                        case "string":
                            {
                                tbStringCellName.ID = "tb" + i.ToString();
                                break;
                            }
                        case "int":
                            {
                                tbIntCellName.ID = "tb" + i.ToString();
                                break;
                            }
                        case "DateTime":
                            {
                                tbDateTimeCellName.ID = "tb" + i.ToString();
                                break;
                            }
                        default:
                            break;
                    }
                }
                thisPanel.Controls.Add(new Literal() { Text = "<br />" });
            }
        }

        /// <summary>
        /// 完成新增資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnFinishCreate_Click(object sender, EventArgs e)
        {
            MainData md = new MainData();
            #region 以迴圈的方式將輸入結果導入物件陣列
            int countOfThisClass = GetCountOfThisClass("MainData", @"/Form.json");
            string[] thisCreateObj = new string[countOfThisClass];
            thisCreateObj[0] = "0";
            for (int i = 1; i < countOfThisClass; i++)
            {
                Panel thisPanel = GetThisPanel();
                TextBox thisTextBox = (TextBox)thisPanel.FindControl("tb" + i.ToString());
                thisCreateObj[i] = thisTextBox.Text;
            }
            #endregion
            #region[From IMyType] T ParseMyType<T>(object import, T export);
            md.Id = myType.ParseMyType(thisCreateObj[0], md.Id);
            md.MainName = myType.ParseMyType(thisCreateObj[1], md.MainName);
            #endregion
            MyCRUD4Target.Create(md, "MainData");
            //ReflashData();

            ReflashData(GridView1, "MainData", md);
        }

        public Panel GetThisPanel()
        {
            return (Panel)FindControl("ClientCreateUserControlArea");
        }
        #endregion

        #region 更新資料程序
        /// <summary>
        /// 請求更新資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAskUpdate_Click(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;

            var md = MyCRUD4Target.GetOne(GetThisTargetId(GridView1), "MainData");
            int countOfThisClass = GetCountOfThisClass("MainData", @"/Form.json");
            object[] thisUpdateObject = new object[countOfThisClass];
            thisUpdateObject[1] = md.MainName;
            InputOriginalData(thisUpdateObject, ClientUpdateUserControlArea, countOfThisClass);
            ClientUpdateUserControl.Visible = true;
        }

        /// <summary>
        /// 載入原始資料
        /// </summary>
        /// <param name="originalDataObject">原始資料物件</param>
        /// <param name="targetPanel">標的面板</param>
        /// <param name="countOfTargetClass">標的類別項目數</param>
        private void InputOriginalData(object[] originalDataObject, Panel targetPanel, int countOfTargetClass)
        {
            for (int i = 1; i < countOfTargetClass; i++)
            {
                ((TextBox)targetPanel.FindControl("tb" + i.ToString())).Text = originalDataObject[i].ToString();
            }
        }

        /// <summary>
        /// 準備更新用的輸入介面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClientUpdateUserControl_Init(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;
            if (IsPostBack)
            {
                #region 網站內執行程序
                #endregion
            }
            else
            {
                #region 進入網站執行程序
                InitUpdateForm("MainData", ClientUpdateUserControlArea, @"/Form.json");
                #endregion
            }
        }

        /// <summary>
        /// 更新程序介面設定
        /// </summary>
        /// <param name="ClassName">類別名稱</param>
        /// <param name="thisPanel">作用面板</param>
        /// <param name="inputControlJsonFile">輸入欄位設定表檔案</param>
        private void InitUpdateForm(string ClassName, Panel thisPanel, string inputControlJsonFile)
        {
            //int thisStep = stepInt;

            JObject o = JObject.Parse(File.ReadAllText(Server.MapPath("~") + inputControlJsonFile));
            thisPanel.Controls.Add(new Literal() { Text = "<br />" });
            for (int i = 1; i < o[ClassName].Count(); i++)
            {
                string strName = (string)o[ClassName][i]["Name"];
                string strCellName = (string)o[ClassName][i]["CellName"];
                Label lbName = new Label
                {
                    ID = "lb" + strCellName.ToUpper(),
                    Text = strName
                };
                thisPanel.Controls.Add(lbName);
                string strTypeName = (string)o[ClassName][i]["TypeName"];
                switch (strTypeName)
                {
                    #region example
                    //case "type":
                    //{
                    //TextBox tbTypeCellName = new TextBox { ID = "tb" + i.ToString() };
                    //thisPanel.Controls.Add(tbTypeCellName);
                    //break;
                    //}
                    #endregion
                    case "string":
                        {
                            TextBox tbStringCellName = new TextBox { ID = "tb" + i.ToString() };
                            thisPanel.Controls.Add(tbStringCellName);
                            break;
                        }
                    case "int":
                        {
                            TextBox tbIntCellName = new TextBox { ID = "tb" + i.ToString() };
                            thisPanel.Controls.Add(tbIntCellName);
                            break;
                        }
                    case "DateTime":
                        {
                            TextBox tbDateTimeCellName = new TextBox { ID = "tb" + i.ToString() };
                            thisPanel.Controls.Add(tbDateTimeCellName);
                            break;
                        }
                    default:
                        break;
                }
                thisPanel.Controls.Add(new Literal() { Text = "<br />" });
            }
        }

        /// <summary>
        /// 完成更新資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnFinishUpdate_Click(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;

            MainData md = new MainData();
            if (GridView1.SelectedIndex > (-1))
            {
                //Get original data
                //md = GetOne(TargetId);
                md = MyCRUD4Target.GetOne(GetThisTargetId(GridView1), "MainData");
                #region 以迴圈的方式將輸入結果導入物件陣列
                int countOfThisClass = GetCountOfThisClass("MainData", @"/Form.json");
                object[] thisUpdateObj = new object[countOfThisClass];
                for (int i = 1; i < countOfThisClass; i++)
                {
                    thisUpdateObj[i] = ((TextBox)ClientUpdateUserControlArea.FindControl("tb" + i.ToString())).Text;
                }
                #endregion
                //Update new data
                #region[From IMyType] T ParseMyType<T>(object import, T export);
                md.MainName = myType.ParseMyType(thisUpdateObj[1], md.MainName);
                #endregion
                //Update(md.Id, md);
                MyCRUD4Target.Update(GetThisTargetId(GridView1), md, "MainData");
                GridView1.SelectedIndex = -1;
                thisSelectedIndex = -1;
                //ReflashData();

                ReflashData(GridView1, "MainData", md);
            }
            else
            {
                //ReflashData();

                ReflashData(GridView1, "MainData", md);
            }
        }
        #endregion

        #region 刪除資料程序
        /// <summary>
        /// 完成刪除資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnFinishDelete_Click(object sender, EventArgs e)
        {
            MainData md = new MainData();
            if (GridView1.SelectedIndex > (-1))
            {
                //Get original data
                int idx = Convert.ToInt32(GridView1.SelectedIndex);
                int idOfSelected = Convert.ToInt32(GridView1.Rows[idx].Cells[1].Text);
                //md = GetOne(idOfSelected);

                md = MyCRUD4Target.GetOne(idOfSelected, "MainData");

                //Delete data
                //Delete(md.Id);
                MyCRUD4Target.Delete(md.Id, "MainData");

                GridView1.SelectedIndex = -1;
                thisSelectedIndex = -1;
                //ReflashData();

                ReflashData(GridView1, "MainData", md);
            }
        }
        #endregion








        //int iii6 = 0;

        /// <summary>
        /// 請求搜尋資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAskSearch_Click(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;

            //int iii = ClientSearchUserControlArea.Controls.Count;

            if (IsPostBack)
            {
                #region 網站內執行程序

                #endregion
                //iii6 = iii6 + 1;
                //Response.Write("iii6=" + iii6.ToString() + "      Post Back BtnAskSearch_Click<br />");

                MainData md = new MainData();
                InitSearchForm<MainData>(md, "MainData", ClientSearchUserControlArea, @"/Form.json");
                //Response.Write("BtnAskSearch_Click<br />");

            }
            else
            {
                #region 進入網站執行程序

                #endregion
                //iii6 = iii6 - 1;
                //Response.Write("iii6=" + iii6.ToString() + "      Not Post Back BtnAskSearch_Click<br />");
            }
            ClientSearchUserControl.Visible = true;
        }

        //int stepInt = 0;
        /// <summary>
        /// 搜尋程序介面設定
        /// </summary>
        /// <typeparam name="T">指定類別模組</typeparam>
        /// <param name="targetClass">標的類別</param>
        /// <param name="targetClassName">標的類別名稱</param>
        /// <param name="thisPanel">作用面板</param>
        /// <param name="inputControlJsonFile">輸入欄位設定表檔案</param>
        private void InitSearchForm<T>(T targetClass, string targetClassName, Panel thisPanel, string inputControlJsonFile)
        {
            //int thisStep = stepInt;

            JObject o = JObject.Parse(File.ReadAllText(Server.MapPath("~") + inputControlJsonFile));
            List<T> myList = MyAllData(targetClass, targetClassName: targetClassName);
            DataTable myDataTable = ToDataTable(myList);
            thisPanel.Controls.Add(new Literal() { Text = "<br />" });
            for (int i = 1; i < o[targetClassName].Count(); i++)
            {
                bool boolSearchTarget = (bool)o[targetClassName][i]["SearchTarget"];
                if (boolSearchTarget)
                {
                    string strName = (string)o[targetClassName][i]["Name"];
                    string strCellName = (string)o[targetClassName][i]["CellName"];
                    Label lbName = new Label
                    {
                        ID = "lb" + strCellName.ToUpper(),
                        Text = strName
                    };
                    thisPanel.Controls.Add(lbName);
                    string strTypeName = (string)o[targetClassName][i]["TypeName"];
                    DropDownList ddlTypeCellName = new DropDownList { ID = "ddl" + i.ToString() };
                    #region 建立DDropDownList內含資料列
                    List<string> newList = new List<string>();
                    newList.Clear();
                    newList.Add("不拘");
                    for (int j = 0; j < myDataTable.Rows.Count; j++)
                    {
                        string originalString = myDataTable.Rows[j][strCellName].ToString();
                        #region 如果插入字串有重複的地方就跳過
                        if (!newList.Contains(originalString))
                        {
                            newList.Add(originalString);
                        }
                        #endregion
                    }
                    ddlTypeCellName.DataSource = newList;
                    ddlTypeCellName.DataBind();
                    #endregion
                    thisPanel.Controls.Add(ddlTypeCellName);
                    thisPanel.Controls.Add(new Literal() { Text = "<br />" });
                }
            }
        }

        //int iii7 = 0;

        /// <summary>
        /// 完成搜尋資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnFinishSearch_Click(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;

            int iii = ClientSearchUserControlArea.Controls.Count;

            //Response.Write("BtnFinishSearch_Click<br />");
            if (IsPostBack)
            {
                #region 網站內執行程序
                MainData md = new MainData();
                GetSearchResult<MainData>(md, "MainData", ClientSearchUserControlArea, @"/Form.json");
                #endregion
                //iii7 = iii7 + 1;
                //Response.Write("iii7=" + iii7.ToString() + "      Post Back BtnFinishSearch_Click<br />");
            }
            else
            {
                #region 進入網站執行程序
                #endregion
                //iii7 = iii7 - 1;
                //Response.Write("iii7=" + iii7.ToString() + "      Not Post Back BtnFinishSearch_Click<br />");
            }
        }
        private void GetSearchResult<T>(T targetClass, string targetClassName, Panel thisPanel, string inputControlJsonFile)
        {
            //將指定欄位名稱放入指定列表陣列List<T>
            JObject o = JObject.Parse(File.ReadAllText(Server.MapPath("~") + inputControlJsonFile));
            List<T> myList = MyAllData(targetClass, targetClassName: targetClassName);
            DataTable myDataTable = ToDataTable(myList);
            List<string> searchTargetItem = new List<string>();
            for (int i = 1; i < o[targetClassName].Count(); i++)
            {
                bool boolSearchTarget = (bool)o[targetClassName][i]["SearchTarget"];
                if (boolSearchTarget)
                {
                    string strDropDownListId = "ddl" + i.ToString();
                    searchTargetItem.Add(strDropDownListId);
                }
            }

            //從指定列表陣列List<T>的內容和GridView的內容交叉比對
            for (int i = 0; i < searchTargetItem.Count; i++)
            {
                string DropDownListId = searchTargetItem[i];
                DropDownList thisDropDownList = (DropDownList)thisPanel.FindControl(DropDownListId);
                if (thisDropDownList == null)
                {
                    Response.Write("this is null");
                }
                else
                {
                    Response.Write("this is not null");
                }
                //int intTest = thisDropDownList.SelectedIndex;
                //string strTest = thisDropDownList.SelectedValue;
            }
            //將比對結果放入另一個GridView
            //進行從系統匯出到Excel功能
            //追加從Excel匯入到系統功能        
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            #region 產生GridView內容
            GridViewCreateContent(e);
            #endregion
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewSelectedIndexChanged(GridView1);
        }

        private void GridViewSelectedIndexChanged(GridView gv)
        {
            thisSelectedIndex = gv.SelectedIndex;
            //ReflashData();
            MainData md = new MainData();
            ReflashData(GridView1, "MainData", md);
            gv.Rows[thisSelectedIndex].BorderWidth = 2;
            gv.Rows[thisSelectedIndex].BorderColor = Color.Red;
        }

        private void GridViewCreateContent(GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    {
                        e.Row.BackColor = Color.FromArgb(153, 0, 0);
                        e.Row.ForeColor = Color.White;
                        break;
                    }
                case DataControlRowType.Footer:
                    {
                        break;
                    }
                case DataControlRowType.DataRow:
                    {
                        if (Convert.ToInt32(ViewState["LineNo"]) == 0)
                        {
                            e.Row.BackColor = Color.FromArgb(255, 251, 214);
                            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFBD6';this.style.color='black'");
                            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#C0C0FF';this.style.color='#ffffff'");
                            ViewState["LineNo"] = 1;
                        }
                        else
                        {
                            e.Row.BackColor = Color.White;
                            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF';this.style.color='black'");
                            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#C0C0FF';this.style.color='#ffffff'");
                            ViewState["LineNo"] = 0;
                        }
                        break;
                    }
                case DataControlRowType.Separator:
                    {
                        break;
                    }
                case DataControlRowType.Pager:
                    {
                        break;
                    }
                case DataControlRowType.EmptyDataRow:
                    {
                        break;
                    }
            }
        }

        protected void ClientSearchUserControl_Init(object sender, EventArgs e)
        {
            //stepInt = stepInt + 1;

            MainData md = new MainData();
            InitSearchForm<MainData>(md, "MainData", ClientSearchUserControlArea, @"/Form.json");
        }

    }
}