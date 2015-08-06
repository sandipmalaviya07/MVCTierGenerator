using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using EntityGeneratorMVC.Generator;
using EntityGeneratorMVC.Utility;

namespace EntityGeneratorMVC
{
    public partial class SqlLoginPage : Form
    {
        public static string ConnectionString = "";
        public SqlLoginPage()
        {
            InitializeComponent();
            BindingList<AutheticationMapper> autheticationmapper = new BindingList<AutheticationMapper>();
            autheticationmapper.Add(new AutheticationMapper("Window Authentication", 1));
            autheticationmapper.Add(new AutheticationMapper("SQL Server Authentication", 2));
            drpAuthenticationType.DataSource = autheticationmapper;
            drpAuthenticationType.DisplayMember = "AuthType";
            drpAuthenticationType.ValueMember = "ID";
        }

        private void btn_LoginServer_Click(object sender, EventArgs e)
        {
            ConnectionString = ConnectionStringMapper.ConnectionString(drpAuthenticationType.SelectedValue.ToString(), txtServerName.Text, txtDatabaseName.Text, txtUserName.Text, txtServerPassword.Text);
            SqlQuery.ConnectionString(ConnectionString);
            if (SqlQuery.Authentication())
            {

                EntityGenerator generatorForm = new EntityGenerator();
                generatorForm.ShowDialog();

            }
            else
            {
                MessageBox.Show("Invalid username or password. Try again");
            }
        }
        
    }
}
