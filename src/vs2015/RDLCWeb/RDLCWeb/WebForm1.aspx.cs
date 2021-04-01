using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace RDLCWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string connectionString = "Data Source =192.168.0.58; Database =testdb; User ID =sa; Password =oyyl@2021; Trusted_Connection = False";
                string queryString = "select  dense_rank() over(order by name asc) as no ,Name,Type,FeeType,Fee from  TestDb";
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = queryString;
                cmd.Connection = conn;
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(queryString, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();

                //DataTable dt = new DataTable();
                //DataColumn column;
                //column = new DataColumn();
                //column.DataType = System.Type.GetType("System.Int32");
                //column.ColumnName = "No";
                //column.ReadOnly = true;
                //dt.Columns.Add(column);

                //column = new DataColumn();
                //column.DataType = System.Type.GetType("System.String");
                //column.ColumnName = "Name";
                //column.ReadOnly = true;
                //dt.Columns.Add(column);

                //column = new DataColumn();
                //column.DataType = System.Type.GetType("System.Decimal");
                //column.ColumnName = "Fee";
                //column.ReadOnly = true;
                //dt.Columns.Add(column);

                //column = new DataColumn();
                //column.DataType = System.Type.GetType("System.String");
                //column.ColumnName = "FeeType";
                //column.ReadOnly = true;
                //dt.Columns.Add(column);

                //column = new DataColumn();
                //column.DataType = System.Type.GetType("System.String");
                //column.ColumnName = "Type";
                //column.ReadOnly = true;
                //dt.Columns.Add(column);

                //DataRow row = dt.NewRow();
                //row["No"] = 1;
                //row["Name"] = "张三";
                //row["Fee"] = 30;
                //row["FeeType"] = "微";
                //row["Type"] = "咖啡";
                //dt.Rows.Add(row);

                //row = dt.NewRow();
                //row["No"] = 2;
                //row["Name"] = "李四";
                //row["Fee"] = 800;
                //row["FeeType"] = "现";
                //row["Type"] = "衣服";
                //dt.Rows.Add(row);

                //row = dt.NewRow();
                //row["No"] = 3;
                //row["Name"] = "王五";
                //row["Fee"] = 3000;
                //row["FeeType"] = "支";
                //row["Type"] = "咖啡";
                //dt.Rows.Add(row);

                //row = dt.NewRow();
                //row["No"] = 4;
                //row["Name"] = "赵六";
                //row["Fee"] = 50;
                //row["FeeType"] = "微";
                //row["Type"] = "咖啡";
                //dt.Rows.Add(row);

                //row = dt.NewRow();
                //row["No"] = 3;
                //row["Name"] = "王五";
                //row["Fee"] = 600;
                //row["FeeType"] = "微";
                //row["Type"] = "衣服";
                //dt.Rows.Add(row);

                //row = dt.NewRow();
                //row["No"] = 4;
                //row["Name"] = "赵六";
                //row["Fee"] = 600;
                //row["FeeType"] = "微";
                //row["Type"] = "衣服";
                //dt.Rows.Add(row);

                rvShow.LocalReport.ReportEmbeddedResource = "RDLCWeb.Report1.rdlc";
                //rvShow.LocalReport.SetParameters(paras);

                ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)dt);
                rvShow.LocalReport.DataSources.Clear();
                rvShow.LocalReport.DataSources.Add(datasource);
                rvShow.LocalReport.DisplayName = "123";
                rvShow.LocalReport.Refresh();

                ProcessReportShowing(rvShow);
            }
        }

        public void ProcessReportShowing(Microsoft.Reporting.WebForms.ReportViewer myReportViewer)
        {
            try
            {
                foreach (Microsoft.Reporting.WebForms.RenderingExtension re in myReportViewer.LocalReport.ListRenderingExtensions())
                {
                    ////屏蔽掉你需要取消的导出功能 Excel PDF WORD  
                    //if (re.Name == "PDF")
                    //{
                    //    FieldInfo fi = re.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                    //    fi.SetValue(re, false);
                    //}
                    //else 
                    if (re.Name == "WORDOPENXML")
                    {
                        FieldInfo fi = re.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                        fi.SetValue(re, false);
                    }
                }
                //谷歌兼容性修复
                myReportViewer.SizeToReportContent = true;
                myReportViewer.ShowPrintButton = false;
            }
            catch
            {
            }
        }
    }
}