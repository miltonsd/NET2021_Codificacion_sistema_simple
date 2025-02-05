﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class Comisiones : Form
    {
        public Comisiones()
        {
            InitializeComponent();
            this.dgvComisiones.AutoGenerateColumns = false;   
        }

        public void Listar()
        {   
            ComisionLogic cl = new ComisionLogic();
            PlanLogic pl = new PlanLogic();
            try
            {
                List<Comision> comisiones = cl.GetAll();
                List<Plan> planes = pl.GetAll();
                var consultaComisiones =
                    from c in comisiones
                    join p in planes
                    on c.IDPlan equals p.ID
                    select new
                    {
                        ID = c.ID,
                        Descripcion = c.Descripcion,
                        AnioEspecialidad = c.AnioEspecialidad,
                        Plan = p.Descripcion
                    };
                this.dgvComisiones.DataSource = consultaComisiones.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Comisiones_Load(object sender, EventArgs e)
        {
            Listar();
            dgvComisiones.ClearSelection();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            ComisionDesktop formComision = new ComisionDesktop(ApplicationForm.ModoForm.Alta);
            formComision.ShowDialog();
            this.Listar();
            dgvComisiones.ClearSelection();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvComisiones.SelectedRows.Count != 0)
            {
                int ID = (int)this.dgvComisiones.SelectedRows[0].Cells["Id"].Value;
                ComisionDesktop formComision = new ComisionDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formComision.ShowDialog();
                this.Listar();
                dgvComisiones.ClearSelection();
            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila para realizar la modificación.");
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvComisiones.SelectedRows.Count != 0)
            {
                int ID = (int)this.dgvComisiones.SelectedRows[0].Cells["Id"].Value;
                ComisionDesktop formComision = new ComisionDesktop(ID, ApplicationForm.ModoForm.Baja);
                formComision.ShowDialog();
                this.Listar();
                dgvComisiones.ClearSelection();
            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila para realizar la baja.");
            }
        }
    }
}