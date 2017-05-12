using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineLibraryManagementSystem
{
    public partial class frmLogin : System.Web.UI.Page
    {
        clsDatabase db = new clsDatabase();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        

        public void Login()
        {
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                lblMessage.Visible = true;
                lnkSupport.Visible = false;
                lblMessage.Text = "Please input your userName and Password";
                MessageBox.Show("Please input your userName and Password");
            }

            else
            {

                string userName = txtUserName.Text.ToString();
                string userpass = txtPassword.Text.ToString();
                //string sqlstring = "SELECT* FROM tbl_users WHERE username LIKE '" + txtUserName.Text + "' AND password LIKE '" + txtPassword.Text + "'";
                string sqlstring = "Exec userLogin '" + txtUserName + "', '" + txtPassword + "'";
                try
                {
                    db.openConnection();
                    db.comm.CommandText = sqlstring;
                    db.reader = db.comm.ExecuteReader();
                    if (db.reader.Read())
                    {
                        Session["userID"] = db.reader["userID"].ToString();
                        Session["userName"] = db.reader["first_name"].ToString();
                        Session["departmentID"] = db.reader["departmentID"].ToString();

                        if (int.Parse(Session["departmentID"].ToString()) == 1)
                        {
                            Response.Redirect("");
                        }
                        else if (int.Parse(Session["departmentID"].ToString()) == 2)
                        {
                            Response.Redirect("frmUsers.aspx");
                        }
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lnkSupport.Visible = false;
                        lblMessage.Text = "Sorry, Wrong UserName or Password used!!";
                    }
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
                finally
                {
                    db.conn.Close();
                }
            }
        }

        protected void lnkAdminLogin_Click(object sender, EventArgs e)
        {
            PanelAdminLogin.Visible = true;
            lnkAdminLogin.CssClass = "LinkSelected";

            PanelStaffLogin.Visible = false;
            LnkStaffLogin.CssClass = "";

            PanelStudentLogin.Visible = false;
            LnkStudentLogin.CssClass = "";
        }

        protected void LnkStaffLogin_Click(object sender, EventArgs e)
        {
            PanelAdminLogin.Visible = false;
            lnkAdminLogin.CssClass = "";

            PanelStaffLogin.Visible = true;
            LnkStaffLogin.CssClass = "LinkSelected";

            PanelStudentLogin.Visible = false;
            LnkStudentLogin.CssClass = "";

        }

        protected void LnkStudentLogin_Click(object sender, EventArgs e)
        {

            PanelAdminLogin.Visible = false;
            lnkAdminLogin.CssClass = "";

            PanelStaffLogin.Visible = false;
            LnkStaffLogin.CssClass = "";

            PanelStudentLogin.Visible = true;
            LnkStudentLogin.CssClass = "LinkSelected";

        }

        protected void LnkStudentRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmStudents.aspx");            
        }

        protected void LnkStaffRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmEmployees.aspx");
           
        }

        
    }
}