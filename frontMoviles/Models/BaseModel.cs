using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using frontMoviles.Models;

namespace frontMoviles.Models
{
    public class BaseModel : NotificationObject
    {
        #region Properties
        [JsonIgnore]
        public long ID { get; set; }
        #endregion Properties
    }
}
