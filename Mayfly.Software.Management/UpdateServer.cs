﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Mayfly.Software.Management
{
    public class UpdateServer
    {
        public bool IsConnected = false;
        public Scheme SchemeData = new Scheme();

        MySqlConnection connection;
        MySqlDataAdapter adapter;
        string[] tablesOrder = new string[] { "Product", "File", "Feature", "Satellite", "FileType", "Version" };

        public UpdateServer()
        {
            SchemeData = Software.Service.GetScheme(tablesOrder);

            //Login loginForm = new Login();

            //while (!IsConnected)
            //{
            //    if (loginForm.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            connection = new MySqlConnection(loginForm.DataBaseConnectionString);
            //            connection.Open();
            //            IsConnected = true;

            //            adapter = new MySqlDataAdapter();

            //            GetData();
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show(e.Message);
            //            //goto loginEntry;
            //        }
            //    }
            //}
        }

        //public void GetData()
        //{
        //    foreach (string tableName in tablesOrder)
        //    {
        //        adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + tableName, connection);
        //        adapter.Fill(SchemeData, tableName);
        //        //SchemeData.GetChanges();
        //    }
        //}

        public void UpdateDatabase()
        {
            Login loginForm = new Login();

            while (!IsConnected)
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    connection = new MySqlConnection(loginForm.DataBaseConnectionString);
                    //connection.Open();
                    IsConnected = true;

                    adapter = new MySqlDataAdapter();

                    foreach (string tableName in tablesOrder)
                    {
                        DataSet changes = SchemeData.GetChanges().Copy();

                        if (changes.Tables[tableName].Rows.Count > 0)
                        {
                            adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + tableName, connection);
                            MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
                            adapter.InsertCommand = cb.GetInsertCommand();
                            if (SchemeData.Tables[tableName].Columns["ID"] == null)
                            {
                                adapter.UpdateCommand = null;
                                adapter.DeleteCommand = null;
                            }
                            else
                            {
                                adapter.UpdateCommand = cb.GetUpdateCommand();
                                adapter.DeleteCommand = cb.GetDeleteCommand();
                            }
                            adapter.Update(changes, tableName);
                        }
                    }
                }
            }

        }

        //public void UpdateDatabaseTable(string tableName)
        //{
        //    adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + tableName, connection);
        //    MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
        //    adapter.InsertCommand = cb.GetInsertCommand();
        //    if (SchemeData.Tables[tableName].Columns["ID"] == null)
        //    {
        //        adapter.UpdateCommand = null;
        //        adapter.DeleteCommand = null;
        //    }
        //    else
        //    {
        //        adapter.UpdateCommand = cb.GetUpdateCommand();
        //        adapter.DeleteCommand = cb.GetDeleteCommand();
        //    }
        //    adapter.Update(SchemeData.Tables[tableName]);
        //}

        //public void GetData()
        //{
        //    foreach (System.Data.DataTable dt in SchemeData.Tables)
        //    {
        //        adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + dt.TableName, connection);
        //        adapter.Fill(SchemeData, dt.TableName);
        //    }
        //}

        //public void Update()
        //{
        //    foreach (System.Data.DataTable dt in SchemeData.Tables)
        //    {
        //        adapter.Update(SchemeData, dt.TableName);
        //    }
        //}

        public void Close()
        {
            //if (connection != null) { connection.Close(); IsConnected = false; }
        }
    }
}