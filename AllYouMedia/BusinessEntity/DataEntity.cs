using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BusinessEntity.ConcreateEntity
{
    public class DataEntity
    {
        private string[] _ParaName;

        #region ParaNameArray()
        public void ParaNameArray(params string[] paraname)
        {
            _ParaName = paraname;
        }
        #endregion

        #region Set_SelectSP
        private void Set_SelectSP(string SPName, object[] ParaArray, SqlCommand cmd)
        {
            cmd.CommandTimeout = 4000;
            for (int i = 0; i < ParaArray.Length; i++)
            {
                cmd.Parameters.AddWithValue(_ParaName.GetValue(i).ToString(), ParaArray.GetValue(i));
            }
        }
        #endregion

        #region ExecuteDataTable
        public DataTable ExecuteDataTable(string SPName, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();
            SqlDataAdapter dapt = new SqlDataAdapter();
            DataTable DTable = new DataTable();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);
            dapt.SelectCommand = cmd;
            dapt.Fill(DTable);

            con.Close();
            return DTable;
        }
        public DataTable ExecuteDataTable(string SPName, string InMessageParameter, out object OutMessageParameter, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();
            SqlDataAdapter dapt = new SqlDataAdapter();
            DataTable DTable = new DataTable();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);

            SqlParameter parameterOut = cmd.Parameters.AddWithValue(InMessageParameter, "");
            parameterOut.Size = 2000;
            parameterOut.Direction = ParameterDirection.Output;

            dapt.SelectCommand = cmd;
            dapt.Fill(DTable);

            OutMessageParameter = parameterOut.Value;

            con.Close();
            return DTable;
        }
        public DataTable ExecuteDataTable(string SPName, string InMessageParameter, out object OutMessageParameter, string InMessage2Parameter, out object OutMessage2Parameter, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();
            SqlDataAdapter dapt = new SqlDataAdapter();
            DataTable DTable = new DataTable();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);

            SqlParameter parameterOut = cmd.Parameters.AddWithValue(InMessageParameter, "");
            parameterOut.Size = 2000;
            parameterOut.Direction = ParameterDirection.Output;

            SqlParameter parameter2Out = cmd.Parameters.AddWithValue(InMessage2Parameter, "");
            parameter2Out.Size = 2000;
            parameter2Out.Direction = ParameterDirection.Output;

            dapt.SelectCommand = cmd;
            dapt.Fill(DTable);

            OutMessageParameter = parameterOut.Value;
            OutMessage2Parameter = parameter2Out.Value;

            con.Close();
            return DTable;
        }
        #endregion

        #region ExecuteDataSet
        public DataSet ExecuteDataSet(string SPName, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();
            SqlDataAdapter dapt = new SqlDataAdapter();
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);
            dapt.SelectCommand = cmd;
            dapt.Fill(ds);

            con.Close();
            return ds;
        }
        public DataSet ExecuteDataSet(string SPName, string InMessageParameter, out object OutMessageParameter, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();
            SqlDataAdapter dapt = new SqlDataAdapter();
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);

            SqlParameter parameterOut = cmd.Parameters.AddWithValue(InMessageParameter, "");
            parameterOut.Size = 2000;
            parameterOut.Direction = ParameterDirection.Output;

            dapt.SelectCommand = cmd;
            dapt.Fill(ds);

            OutMessageParameter = parameterOut.Value;

            con.Close();
            return ds;
        }
        public DataSet ExecuteDataSet(string SPName, string InMessageParameter, out object OutMessageParameter, string InMessage2Parameter, out object OutMessage2Parameter, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();
            SqlDataAdapter dapt = new SqlDataAdapter();
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);

            SqlParameter parameterOut = cmd.Parameters.AddWithValue(InMessageParameter, "");
            parameterOut.Size = 2000;
            parameterOut.Direction = ParameterDirection.Output;

            SqlParameter parameter2Out = cmd.Parameters.AddWithValue(InMessage2Parameter, "");
            parameter2Out.Size = 2000;
            parameter2Out.Direction = ParameterDirection.Output;

            dapt.SelectCommand = cmd;
            dapt.Fill(ds);

            OutMessageParameter = parameterOut.Value;
            OutMessage2Parameter = parameter2Out.Value;

            con.Close();
            return ds;
        }
        #endregion

        #region ExecuteScalar
        public object ExecuteScalar(string SPName, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);
            object _object = cmd.ExecuteScalar();

            con.Close();
            return _object;
        }
        public object ExecuteScalar(string SPName, string InMessageParameter, out object OutMessageParameter, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);

            SqlParameter parameterOut = cmd.Parameters.AddWithValue(InMessageParameter, "");
            parameterOut.Size = 2000;
            parameterOut.Direction = ParameterDirection.Output;

            object _object = cmd.ExecuteScalar();

            OutMessageParameter = parameterOut.Value;

            con.Close();
            return _object;
        }
        #endregion

        #region ExecuteScalarFunction
        public object ExecuteScalarFunction(string SPName, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);
            object _object = cmd.ExecuteScalar();

            con.Close();
            return _object;
        }
        #endregion

        #region ExecuteReader
        public SqlDataReader ExecuteReader(string SPName, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();
            SqlDataReader dataReader;

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);
            dataReader = cmd.ExecuteReader();

            con.Close();
            return dataReader;
        }
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(string SPName, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);
            int AffectiveRow = cmd.ExecuteNonQuery();

            con.Close();
            return AffectiveRow;
        }
        public int ExecuteNonQuery(string SPName, string InMessageParameter, out object OutMessageParameter, params object[] ParaArray)
        {
            SqlCommand cmd = new SqlCommand();
            Connection conClass = new Connection();

            SqlConnection con = new SqlConnection(conClass.ConnectionString);
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            this.Set_SelectSP(SPName, ParaArray, cmd);

            SqlParameter parameterOut = cmd.Parameters.AddWithValue(InMessageParameter, "");
            parameterOut.Size = 2000;
            parameterOut.Direction = ParameterDirection.Output;

            int AffectiveRow = cmd.ExecuteNonQuery();
            OutMessageParameter = parameterOut.Value;

            con.Close();
            return AffectiveRow;
        }
        #endregion

        #region ConvertedDate
        public string ConvertedDate(string date)
        {
            date = Convert.ToDateTime(date).GetDateTimeFormats(new System.Globalization.CultureInfo("en-US"))[0].ToString();
            return date;
        }
        #endregion

    }
}
