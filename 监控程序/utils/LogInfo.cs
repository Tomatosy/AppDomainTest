using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Tomato.FinanceMonitor.StratWinConsole
{
    public class LogInfo
    {
        public enum enumLogType { INFO = 1, ERROR };

        private Guid _ID;
        private DateTime _LogTime;
        private enumLogType _LogType1;
        private string _CompanyID;
        private string _LogMessage;

        public string LogMessage
        {
            get { return _LogMessage; }
            set { _LogMessage = value; }
        }

        public string CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        public enumLogType LogType
        {
            get { return _LogType1; }
            set { _LogType1 = value; }
        }

        public DateTime LogTime
        {
            get { return _LogTime; }
            set { _LogTime = value; }
        }

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public void AddLog(SqlConnection conn)
        {
            using (conn)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand(string.Format("insert into tb_ExecDataset_Logs(ID,LogTime,LogType,LogMessage,CompanyID) values('{0}','{1}','{2}','{3}','{4}')", Guid.NewGuid(), this.LogTime.ToString("yyyy-MM-dd HH:mm:ss"), (int)this.LogType, this.LogMessage, this.CompanyID), conn);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        //Enum.Parse(typeof(Man), Name2)

        /// <summary>
        /// 客户端日志
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Types"></param>
        /// <param name="Message"></param>
        public void InsertLog(SqlConnection conn, int Types, string Message)
        {
            Guid id = Guid.NewGuid();
            DateTime NTime = DateTime.Now;

            string sql = string.Format("INSERT INTO [dbo].[tb_ExecDataset_Logs](ID,LogTime,LogType,LogMessage) VALUES ('{0}','{1}','{2}','{3}')", id, NTime, Types, Message);

            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
