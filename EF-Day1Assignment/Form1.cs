using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_Day1Assignment
{
    public partial class Form1 : Form
    {
        CompanyEntities context = new CompanyEntities();

        public Form1()
        {
            InitializeComponent();
            cmbDepartments.SelectedIndex = -1;
            cmbDepartments.DisplayMember = "Name";
            cmbDepartments.ValueMember = "Name";
            cmbDepartments.DataSource = context.Departments.ToList();
            //cmbDepartments.SelectedIndex = -1;
        }
        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var query = context.Departments.Where(dept => dept.Name == cmbDepartments.SelectedValue).FirstOrDefault();
            var colection = query.Employees.ToList();

            dataGridView1.DataSource = colection;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[3].Value.ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var emp = new Employee
            {
                Name = textBox2.Text,
                BirthDate = DateTime.Now,
                City = textBox3.Text,
                DeptID = 1,
            };

            context.Employees.Add(emp);
            context.SaveChanges();
            dataGridView1.DataSource = null;
            var query2 = context.Departments.Where(dept => dept.Name == cmbDepartments.SelectedValue).FirstOrDefault();
            var colection = query2.Employees.ToList();
            dataGridView1.DataSource = colection;
            MessageBox.Show("Done ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            var query = context.Employees.Where(deptt => deptt.ID == id).FirstOrDefault();
            query.Name = textBox2.Text;
            query.City = textBox3.Text;
            context.SaveChanges();
            dataGridView1.DataSource = null;
            var query2 = context.Departments.Where(dept => dept.Name == cmbDepartments.SelectedValue).FirstOrDefault();
            var colection = query2.Employees.ToList();
            dataGridView1.DataSource = colection;
            MessageBox.Show("Done ");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            var query = context.Employees.Where(deptt => deptt.ID == id).FirstOrDefault();
            try
            {
                DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    context.Employees.Remove(query);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FK");
            }

            dataGridView1.DataSource = null;
            var query2 = context.Departments.Where(dept => dept.Name == cmbDepartments.SelectedValue).FirstOrDefault();
            var colection = query2.Employees.ToList();
            dataGridView1.DataSource = colection;
            MessageBox.Show("Done ");
        }
    }
}
