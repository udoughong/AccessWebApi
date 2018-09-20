using RestSharp;
using System.Collections.Generic;

namespace WebApiInterface_AspNetWebForm
{
    public interface IMyCRUD<T>
    {
        RestClient ThisClient();

        List<T> GetAll<T>(T entry, string className);

        T GetOne(int id, string className);

        void Create(T entry, string className);

        void Update(int id, T entry, string className);

        void Delete(int id, string className);
    }

    public interface IMyType
    {
        T ParseMyType<T>(object import, T export);
    }
}