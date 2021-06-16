using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Tomato.FinanceMonitor.StratWinConsole
{
    [Serializable]
    public class VersionOutputModel
    {

        /// <summary>
        /// 本本类型  1、DLL 2、winfrom
        /// </summary>
        public int VersionType { get => versionType; set => versionType = value; }
        public Guid? VersionID { get => versionID; set => versionID = value; }

        public DateTime? RlraseDate { get => rlraseDate; set => rlraseDate = value; }

        public bool? IsNewest { get => isNewest; set => isNewest = value; }

        public string VersionNO { get => versionNO; set => versionNO = value; }

        public string DownloadUrl { get => downloadUrl; set => downloadUrl = value; }


        protected int versionType;

        protected Guid? versionID;

        protected DateTime? rlraseDate;

        protected bool? isNewest;

        protected string versionNO;

        protected string downloadUrl;


    }
}
