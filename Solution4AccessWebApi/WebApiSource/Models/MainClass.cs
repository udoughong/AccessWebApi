using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSource.Models
{
    public class MainData
    {
        int mainId;
        string mainName;

        public int Id { get => mainId; set => mainId = value; }
        public string MainName { get => mainName; set => mainName = value; }
    }
    public class BranchData
    {
        int branchId;
        string branchName;
        int mainId;

        public int Id { get => branchId; set => branchId = value; }
        public string BranchName { get => branchName; set => branchName = value; }
        public int MainId { get => mainId; set => mainId = value; }
    }
}