using naif14207App.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace naif14207App.n14207
{
    public partial class Manit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatddlCityId();
                PopulatddlDepartmentId();
                PopulatddlHospitalId();
                PopulateGrid();
            }
        }

        protected void PopulatddlCityId()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT CityId, City FROM City";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlCity.DataValueField = "CityId";
            ddlCity.DataTextField = "City";
            ddlCity.DataSource = dr;
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("-- اختر المدينة --", "0"));
        }

        protected void PopulatddlDepartmentId()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT DepartmentId, DepartmentName FROM Departments";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataSource = dr;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-- اختر القسم الطبي --", "0"));
        }

        protected void PopulatddlHospitalId()
        {
            if (!IsPostBack)
            {
                {
                    CRUD myCrud = new CRUD();
                    string mySql = @"SELECT HospitalId, Hospitals FROM Hospitals";
                    SqlDataReader dr = myCrud.getDrPassSql(mySql);

                    ddlHospital.DataValueField = "HospitalId";
                    ddlHospital.DataTextField = "Hospitals";
                    ddlHospital.DataSource = dr;
                    ddlHospital.DataBind();

                    ddlHospital.Items.Insert(0, new ListItem("-- اختر المستشفى --", "0"));
                }
            }
        }



        protected void PopulateGrid()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT MaintenanceId, DeviceName, DeviceStatus, EmployeeName, Cell, RequestDate 
                             FROM MaintenanceRequests 
                             ORDER BY MaintenanceId DESC";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            GridView1.DataSource = dr;
            GridView1.DataBind();
        }



        protected void InternIntern()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"INSERT INTO MaintenanceRequests (DeviceName, DeviceStatus, EmployeeId, EmployeeName, Cell, Email, CityId, DepartmentId, Notes, RequestDate, HospitalID)
                             VALUES (@DeviceName, @DeviceStatus, @EmployeeId, @EmployeeName, @Cell, @Email, @CityId, @DepartmentId, @Notes, @RequestDate, @HospitalID)";

            Dictionary<string, object> myPara = new Dictionary<string, object>
            {
                { "@DeviceName", txtDeviceName.Text.Trim() },
                { "@DeviceStatus", txtDeviceStatus.Text.Trim() },
                { "@EmployeeId", txtEmployeeId.Text.Trim() },
                { "@EmployeeName", txtEmployeeName.Text.Trim() },
                { "@Cell", txtCell.Text.Trim() },
                { "@Email", txtEmail.Text.Trim() },
                { "@CityId", ddlCity.SelectedValue },
                { "@DepartmentId", ddlDepartment.SelectedValue },
                { "@Notes", txtNotes.Text.Trim() },
                { "@RequestDate", DateTime.Now },
                { "@HospitalID", ddlHospital.SelectedValue }
            };

            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

            if (rtn >= 1)
            {
                ShowMessage("تمت إضافة الطلب بنجاح!", System.Drawing.Color.Green);
                ClearFields();
                PopulateGrid();
            }
            else
            {
                ShowMessage("حدث خطأ: لم يتم حفظ البيانات.", System.Drawing.Color.Red);
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (ddlHospital.SelectedValue == "0" || string.IsNullOrEmpty(ddlHospital.SelectedValue))
            {
                ShowMessage("يرجى اضافةالبيانات.", System.Drawing.Color.Red);
                return; 
            }

            InternIntern();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaintenanceId.Text.Trim()))
            {
                ShowMessage("يرجى إدخال أو اختيار رقم الطلب (MaintenanceID) المراد تحديثه.", System.Drawing.Color.Red);
                return;
            }

            CRUD myCrud = new CRUD();
            string mySql = @"UPDATE MaintenanceRequests 
                             SET DeviceName = @DeviceName, 
                                 DeviceStatus = @DeviceStatus, 
                                 EmployeeId = @EmployeeId, 
                                 EmployeeName = @EmployeeName, 
                                 Cell = @Cell, 
                                 Email = @Email, 
                                 CityId = @CityId, 
                                 DepartmentId = @DepartmentId, 
                                 Notes = @Notes, 
                                 HospitalID = @HospitalID 
                             WHERE MaintenanceId = @MaintenanceId";

            Dictionary<string, object> myPara = new Dictionary<string, object>
            {
                { "@MaintenanceId", txtMaintenanceId.Text.Trim() },
                { "@DeviceName", txtDeviceName.Text.Trim() },
                { "@DeviceStatus", txtDeviceStatus.Text.Trim() },
                { "@EmployeeId", txtEmployeeId.Text.Trim() },
                { "@EmployeeName", txtEmployeeName.Text.Trim() },
                { "@Cell", txtCell.Text.Trim() },
                { "@Email", txtEmail.Text.Trim() },
                { "@CityId", ddlCity.SelectedValue },
                { "@DepartmentId", ddlDepartment.SelectedValue },
                { "@Notes", txtNotes.Text.Trim() },
                { "@HospitalID", ddlHospital.SelectedValue }
            };

            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

            if (rtn >= 1)
            {
                ShowMessage("تم تحديث بيانات الطلب بنجاح!", System.Drawing.Color.Green);
                ClearFields();
                PopulateGrid();
            }
            else
            {
                ShowMessage("فشل التحديث: لم يتم العثور على رقم الطلب المحدد.", System.Drawing.Color.Red);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaintenanceId.Text.Trim()))
            {
                ShowMessage("يرجى إدخال أو اختيار رقم الطلب (MaintenanceID) المراد حذفه.", System.Drawing.Color.Red);
                return;
            }

            CRUD myCrud = new CRUD();
            string mySql = @"DELETE FROM MaintenanceRequests WHERE MaintenanceId = @MaintenanceId";

            Dictionary<string, object> myPara = new Dictionary<string, object>
            {
                { "@MaintenanceId", txtMaintenanceId.Text.Trim() }
            };

            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

            if (rtn >= 1)
            {
                ShowMessage("تم حذف الطلب بنجاح!", System.Drawing.Color.Green);
                ClearFields();
                PopulateGrid();
            }
            else
            {
                ShowMessage("فشل الحذف: رقم الطلب غير موجود.", System.Drawing.Color.Red);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaintenanceId.Text.Trim()))
            {
                ShowMessage("يرجى إدخال رقم الطلب للبحث.", System.Drawing.Color.OrangeRed);
                return;
            }

            LoadRequestData(txtMaintenanceId.Text.Trim());
        }
        private void LoadRequestData(string maintenanceId)
        {
            string conStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["naif14207ConStr"].ConnectionString;
            string mySql = @"SELECT * FROM MaintenanceRequests WHERE MaintenanceId = @MaintenanceId";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(mySql, con))
                {
                    cmd.Parameters.AddWithValue("@MaintenanceId", maintenanceId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtMaintenanceId.Text = dr["MaintenanceId"].ToString();
                        txtDeviceName.Text = dr["DeviceName"].ToString();
                        txtDeviceStatus.Text = dr["DeviceStatus"].ToString();
                        txtEmployeeId.Text = dr["EmployeeId"].ToString();
                        txtEmployeeName.Text = dr["EmployeeName"].ToString();
                        txtCell.Text = dr["Cell"].ToString();
                        txtEmail.Text = dr["Email"].ToString();

                        if (ddlCity.Items.FindByValue(dr["CityId"].ToString()) != null)
                            ddlCity.SelectedValue = dr["CityId"].ToString();

                        if (ddlDepartment.Items.FindByValue(dr["DepartmentId"].ToString()) != null)
                            ddlDepartment.SelectedValue = dr["DepartmentId"].ToString();

                        if (ddlHospital.Items.FindByValue(dr["HospitalID"].ToString()) != null)
                            ddlHospital.SelectedValue = dr["HospitalID"].ToString();

                        txtNotes.Text = dr["Notes"].ToString();

                        ShowMessage("تم جلب بيانات الطلب بنجاح.", System.Drawing.Color.Blue);
                    }
                    else
                    {
                        ShowMessage("لم يتم العثور على طلب بهذا الرقم.", System.Drawing.Color.Red);
                    }
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedId = GridView1.SelectedDataKey.Value.ToString();
            LoadRequestData(selectedId);
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowMessage("تم مسح كافة الحقول.", System.Drawing.Color.Black);
        }

        protected void ClearFields()
        {
            txtMaintenanceId.Text = "";
            txtDeviceName.Text = "";
            txtDeviceStatus.Text = "";
            txtEmployeeId.Text = "";
            txtEmployeeName.Text = "";
            txtCell.Text = "";
            txtEmail.Text = "";
            txtNotes.Text = "";
            if (ddlCity.Items.Count > 0) ddlCity.SelectedIndex = 0;
            if (ddlDepartment.Items.Count > 0) ddlDepartment.SelectedIndex = 0;
            if (ddlHospital.Items.Count > 0) ddlHospital.SelectedIndex = 0;
        }

        private void ShowMessage(string message, System.Drawing.Color color)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = color;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // عشان مايصير خطا Form
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=MaintenanceData.xls");
            Response.Charset = "UTF-8";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView1.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btnExportWord_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=MaintenanceData.doc");
            Response.Charset = "UTF-8";
            Response.ContentType = "application/msword";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView1.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        

        
    }
    }//ضع اي كود فوق القوس ذا
}//ضع الكود اعلى القوس برقمين