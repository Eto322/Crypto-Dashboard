using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager.Helper
{
    public class AdditionalCoinInfoModel
    {
        public string Id { get; set; }
        public string Homepage { get; set; }
        public string Repository { get; set; }
        

        public AdditionalCoinInfoModel(string id, string homepage, string repository)
        {
            Id = id;
            Homepage = homepage;
            Repository = repository;
        }

        public AdditionalCoinInfoModel()
        {
            
        }
    }
}
