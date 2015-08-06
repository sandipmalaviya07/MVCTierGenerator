using EntityGeneratorMVC.DataBaseCommand;
using EntityGeneratorMVC.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityGeneratorMVC.Generator;
using System.IO;
using EntityGeneratorMVC.FileUtility;
namespace EntityGeneratorMVC
{
    public partial class EntityGenerator : Form
    {
        public EntityGenerator()
        {
            InitializeComponent();
            gridviewDatabaseTable.DataSource = DataBaseOperation.GetDataBaseTable();
            txtSaveLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        private void btnGenerator_Click(object sender, EventArgs e)
        {
            List<string> _listContext = new List<string>();
            foreach (DataGridViewRow item in this.gridviewDatabaseTable.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)item.Cells["checkboxEntityGenerator"];
                object objCheckForNot = chk.Value;
                if (Convert.ToBoolean(objCheckForNot) == true)
                {
                    _listContext.Add(Convert.ToString(item.Cells[1].Value));
                    var listMapper = DataBaseOperation.GetDataTableSchema(Convert.ToString(item.Cells[1].Value));
                    string EntityModel = listMapper.ToCollection<EntityMapper>().EntityModelGenerator(Convert.ToString(item.Cells[1].Value), txtNameSpacetxt.Text == "" ? null : txtNameSpacetxt.Text);
                    string fileNameModel = Convert.ToString(item.Cells[1].Value) + "Model.cs";
                    FileSave.SaveFileInDirectoryModel(txtSaveLocation.Text, fileNameModel, EntityModel);


                    string EntityBusinessLogic = listMapper.ToCollection<EntityMapper>().EntityMethodGenerator(Convert.ToString(item.Cells[1].Value), txtNameSpacetxt.Text == "" ? null : txtNameSpacetxt.Text);
                    string fileNamesBussinessLogic = Convert.ToString(item.Cells[1].Value) + "Services.cs";
                    FileSave.SaveFileInDirectoryBussinessLogic(txtSaveLocation.Text, fileNamesBussinessLogic, EntityBusinessLogic);
                   
                }
            }
            string EntityContext = _listContext.EntityContextGenerator(txtNameSpacetxt.Text == "" ? null : txtNameSpacetxt.Text);
            string fileNamesContext = "DbContext.cs";
            FileSave.SaveFileInDirectoryContext(txtSaveLocation.Text, fileNamesContext, EntityContext);
            MessageBox.Show("Successfull Created.");
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileLocation.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtSaveLocation.Text = saveFileLocation.SelectedPath;
            }
        }

    }
}
