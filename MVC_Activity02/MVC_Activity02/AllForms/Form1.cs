using MVC_Activity02.Context;
using MVC_Activity02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC_Activity02
{
    public partial class customerForm1 : Form
    {
        Customer model = new Customer();
        public customerForm1()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            txtFirstname.Text = txtLastname.Text = txtAddress.Text = txtStatus.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            model.Id = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Clear();
            PopulatedDataGridView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            model.Firstname = txtFirstname.Text.Trim();
            model.Lastname = txtLastname.Text.Trim();
            model.Address = txtAddress.Text.Trim();
            model.Status = txtStatus.Text.Trim();
            using (AppDbContext db = new AppDbContext())
            {
                //insert Data to database
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    db.Customers.Add(model);
                    db.SaveChanges();
                }
                //update data to database
                else
                {
                    model.UpdatedDate = DateTime.Now;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            Clear();
            PopulatedDataGridView();
            MessageBox.Show("Successfully Submited!");
        }

        //function to show data in Data GridView
        void PopulatedDataGridView()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var customers = db.Customers.ToList(); 
                var customerList = customers.Select(c => new CustomerListDto
                {
                    Id = c.Id,
                    Address = c.Address,
                    Status = c.Status,
                    FullName = $"{c.Lastname}, {c.Firstname}"
                }).ToList();
                dgvCustomer.DataSource = customerList;
            }
        }

        private void dgvCustomer_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCustomer.CurrentRow.Index != -1)
            {
               model.Id = Convert.ToInt32(dgvCustomer.CurrentRow.Cells["Id"].Value);
                using (AppDbContext db = new AppDbContext())
                {
                    model = db.Customers.AsNoTracking().Where(c => c.Id == model.Id).FirstOrDefault();
                    txtFirstname.Text = model.Firstname;
                    txtLastname.Text = model.Lastname;
                    txtAddress.Text = model.Address;
                    txtStatus.Text = model.Status;
                }
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Width = 100;
            btnDelete.Height = 30;
            if (MessageBox.Show("Are You Sure to Delete this Record??", "--Delete Record--", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = db.Customers.FirstOrDefault(c => c.Id == model.Id);
                    db.Customers.Remove(data);
                    db.SaveChanges();

                    PopulatedDataGridView();
                    Clear();
                    MessageBox.Show("Successfully Deleted Record!");
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var customer = db.Customers.ToList();
                var customerList = customer.Select(c => new CustomerListDto
                {
                    Id = c.Id,
                    Address = c.Address,
                    Status = c.Status,
                    FullName = $"{c.Lastname}, {c.Firstname}"
                }).ToList();

                var search = customerList.Where(c =>
                    c.FullName.Contains(txtSearch.Text) ||
                    c.Address.Contains(txtSearch.Text)).ToList();
                dgvCustomer.DataSource = search;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
