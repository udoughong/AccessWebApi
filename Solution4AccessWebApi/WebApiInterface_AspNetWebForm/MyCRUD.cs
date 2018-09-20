using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace WebApiInterface_AspNetWebForm
{
    /// <summary>
    /// 我的資料操作程序(CRUD=>新增、讀取、更新、刪除)
    /// </summary>
    /// <typeparam name="TEntry">我的資料類別模組</typeparam>
    public class MyCRUD<TEntry> : IMyCRUD<TEntry> where TEntry : class, new()
    {

        public RestClient ThisClient()
        {
            string apiURL = "http://localhost:64578/" + "api/";
            return new RestClient(apiURL);
        }

        public void Create(TEntry entry, string className)
        {
            var request = new RestRequest(className, Method.POST);
            request.AddJsonBody(entry);
            ThisClient().Execute(request);
        }

        public void Delete(int id, string className)
        {
            var request = new RestRequest(className+"/"+id,Method.DELETE);
            ThisClient().Execute(request);
        }

        public List<T> GetAll<T>(T entry, string className)
        {
            RestRequest request = new RestRequest(className,Method.GET);
            IRestResponse<List<T>> response = ThisClient().Execute<List<T>>(request);
            return response.Data;
        }

        public TEntry GetOne(int id, string className)
        {
            RestRequest request = new RestRequest(className + "/" + id, Method.GET);
            IRestResponse<TEntry> response = ThisClient().Execute<TEntry>(request);
            return response.Data;
        }

        public void Update(int id, TEntry entry, string className)
        {
            var request = new RestRequest(className + "/" + id, Method.PUT);
            request.AddJsonBody(entry);
            ThisClient().Execute(request);
        }
    }

    /// <summary>
    /// 我的自定義型別
    /// </summary>
    public class MyType : IMyType
    {
        /// <summary>
        /// 轉換任一物件為希望對應的型別
        /// </summary>
        /// <typeparam name="T">希望對應的型別</typeparam>
        /// <param name="import">原始物件</param>
        /// <param name="export">準備輸出的物件</param>
        /// <returns>實際輸出到希望對應型別的物件</returns>
        public T ParseMyType<T>(object import, T export)
        {
            try
            {
                string typeName = export.GetType().Name;
                switch (typeName)
                {
                    case "DateTime":
                        import = Convert.ToDateTime(import).ToLocalTime();
                        break;
                    default:
                        break;
                }
                return (T)import;
            }
            catch (Exception)
            {
                //當準備輸出的是string時，因為初始狀態為空物件型態之例外，就從這裡輸出。
                return (T)import;
            }
        }
    }

}