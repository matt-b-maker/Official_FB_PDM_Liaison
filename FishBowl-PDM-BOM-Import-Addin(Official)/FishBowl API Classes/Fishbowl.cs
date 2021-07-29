using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FishBowl_PDM_BOM_Import_Addin_Official_
{
    /// <summary>
    /// Primary interaction class, wrap in a <see langword="using" /> statement to automatically log out and close the connection
    /// </summary>
    public class Fishbowl
    {
        private string _key = "";

        private readonly string _user;
        private readonly string _pass;
        private readonly string _host;
        private readonly int _port;

        private ConnectionObject _connection;

        /// <summary>
        /// Constructor for specifying credentials
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public Fishbowl(string host, int port, string user, string password)
        {
            _host = host;
            _port = port;
            _user = user;

            MD5 md5 = MD5.Create();
            byte[] encoded = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            string md5Pass = Convert.ToBase64String(encoded, 0, 16);

            _pass = md5Pass;
        }

        /// <summary>
        /// Use for connecting and logging in
        /// </summary>
        public int Connect()
        {
            _connection = new ConnectionObject(_host, _port);
            return Login();
        }

        private dynamic BeginRequest(dynamic _cmd)
        {
            dynamic cmd = new
            {
                FbiJson = new
                {
                    Ticket = new { Key = _key },
                    FbiMsgsRq = _cmd
                }
            };

            return cmd;
        }

        private int Login()
        {
            dynamic cmd = new { LoginRq = new { IAID = 3399 /* Application ID, can be any number */, IAName = "Ok Jobby Bobby", IADescription = "This is a test", UserName = _user, UserPassword = _pass } };
            cmd = BeginRequest(cmd);
            string r = _connection.sendCommand(JsonConvert.SerializeObject(cmd));
            if (r == "")
            {
                throw new Exception("Empty response returned for Login request");
            }
            dynamic resp = JsonConvert.DeserializeObject(r);
            _key = resp.FbiJson.Ticket.Key;

            //if ((resp.FbiJson.FbiMsgsRs.statusCode != 1000 && resp.FbiJson.FbiMsgsRs.statusCode != 900) || String.IsNullOrWhiteSpace(_key))
            //    throw new Exception("Login failed with status code " + resp.FbiJson.FbiMsgsRs.statusCode + (resp.FbiJson.FbiMsgsRs.statusMessage != null ? ": " + resp.FbiJson.FbiMsgsRs.statusMessage : ""));
            //if (resp.FbiJson.FbiMsgsRs.LoginRs.statusCode != 1000)
            //    throw new Exception("Login Error " + resp.FbiJson.FbiMsgsRs.LoginRs.statusCode + ": " + resp.FbiJson.FbiMsgsRs.LoginRs.statusMessage.Value);

            return resp.FbiJson.FbiMsgsRs.statusCode;
        }

        /// <summary>
        /// Execute a query against the Fishbowl database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string ExecuteQuery(string query)
        {
            dynamic cmd = new { ExecuteQueryRq = new { Query = query } };
            cmd = BeginRequest(cmd);
            string r = _connection.sendCommand(JsonConvert.SerializeObject(cmd));
            if (r == "")
            {
                return r;
            }
            //File.WriteAllText(@"C:\tmp\cmd_resp.json", r);
            dynamic resp = JsonConvert.DeserializeObject(r);

            //if (resp.FbiJson.FbiMsgsRs.statusCode != 1000 && resp.FbiJson.FbiMsgsRs.statusCode != 900)
            //    throw new Exception("Execute Query failed with status code " + resp.FbiJson.FbiMsgsRs.statusCode + (resp.FbiJson.FbiMsgsRs.statusMessage != null ? ": " + resp.FbiJson.FbiMsgsRs.statusMessage : ""));
            //if (resp.FbiJson.FbiMsgsRs.ExecuteQueryRs.statusCode != 1000)
            //    throw new Exception("Execute Query Error " + resp.FbiJson.FbiMsgsRs.ExecuteQueryRs.statusCode + ": " + resp.FbiJson.FbiMsgsRs.ExecuteQueryRs.statusMessage.Value);

            if (resp.FbiJson.FbiMsgsRs.statusCode != 1000 && resp.FbiJson.FbiMsgsRs.statusCode != 900)
                resp = "Execute Query failed with status code " + resp.FbiJson.FbiMsgsRs.statusCode + (resp.FbiJson.FbiMsgsRs.statusMessage != null ? ": " + resp.FbiJson.FbiMsgsRs.statusMessage : "");
            if (resp.FbiJson.FbiMsgsRs.ExecuteQueryRs.statusCode != 1000)
                resp = "Execute Query Error " + resp.FbiJson.FbiMsgsRs.ExecuteQueryRs.statusCode + ": " + resp.FbiJson.FbiMsgsRs.ExecuteQueryRs.statusMessage.Value;

            return ConvertQueryFromJson(resp);
        }

        /// <summary>
        /// Run an import against the Fishbowl database
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        public string Import(string type, dynamic data)
        {
            dynamic cmd = new { ImportRq = new { Type = type, Rows = new { Row = data } } };
            cmd = BeginRequest(cmd);
            //File.WriteAllText(@"C:\tmp\cmd.json", JsonConvert.SerializeObject(cmd));
            string r = _connection.sendCommand(JsonConvert.SerializeObject(cmd));
            //File.WriteAllText(@"C:\tmp\cmd_resp.json", r);
            if (r == "")
                return "Something went wrong";
            dynamic resp = JsonConvert.DeserializeObject(r);

            //if (resp.FbiJson.FbiMsgsRs.statusCode != 1000 && resp.FbiJson.FbiMsgsRs.statusCode != 900)
            //    resp = "Import failed with status code " + resp.FbiJson.FbiMsgsRs.statusCode + (resp.FbiJson.FbiMsgsRs.statusMessage != null ? ": " + resp.FbiJson.FbiMsgsRs.statusMessage : "");
            //if (resp.FbiJson.FbiMsgsRs.ImportRs.statusCode != 1000)
            //    resp = "Import Error " + resp.FbiJson.FbiMsgsRs.ImportRs.statusCode + ": " + resp.FbiJson.FbiMsgsRs.ImportRs.statusMessage.Value;

            return resp.FbiJson.FbiMsgsRs.ImportRs.statusCode + " " + resp.FbiJson.FbiMsgsRs.ImportRs.statusMessage;
        }

        private string ConvertQueryFromJson(dynamic data)
        {
            //DataTable t = new DataTable();

            _key = data.FbiJson.Ticket.Key;
            var rowData = data.FbiJson.FbiMsgsRs.ExecuteQueryRs.Rows.Row;

            string row = rowData.ToString().Trim('[', ']').Trim().Trim('"').Replace("\\\"", "\"").Replace("\",\r\n", "\r\n").Replace("\"\"", "\"");
            if (!row.EndsWith("\""))
            {
                row = row.Trim('\\');
                row += "\"";
            }

            //using (var csv = new CsvReader(new StringReader(row), true, ','))
            //{
            //    int fieldCount = csv.FieldCount;

            //    foreach (string c in csv.GetFieldHeaders())
            //        t.Columns.Add(c);

            //    while (csv.ReadNextRecord())
            //    {
            //        DataRow r = t.NewRow();
            //        for (int i = 0; i < fieldCount; i++)
            //            r[i] = csv[i];
            //        t.Rows.Add(r);
            //    }
            //}

            return row;
        }

        /// <summary>
        /// Convert the DataTable object to a CSV string
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string ConvertDataTableToCsv(DataTable t)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataColumn c in t.Columns)
                sb.Append("\"" + c.ColumnName + "\",");
            sb.AppendLine();
            foreach (DataRow r in t.Rows)
            {
                foreach (DataColumn c in t.Columns)
                {
                    if (!String.IsNullOrWhiteSpace(r[c].ToString()))
                    {
                        if (r[c] is string)
                            sb.Append("\"" + r[c] + "\"");
                        else if (r[c] is int)
                            sb.Append(r[c].ToString());
                        else if (r[c] is decimal)
                            sb.AppendFormat("{0:0.####}", r[c]);
                    }
                    sb.Append(",");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Logs out and closes the connection
        /// </summary>
        public void Dispose()
        {
            dynamic cmd = new { LogoutRq = "" };
            cmd = BeginRequest(cmd);

            try
            {
                dynamic resp = JsonConvert.DeserializeObject(_connection.sendCommand(JsonConvert.SerializeObject(cmd)));

                _connection.Dispose();
            }
            catch { }
        }
    }
}
