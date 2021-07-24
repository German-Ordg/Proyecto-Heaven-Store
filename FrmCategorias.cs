﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pantallas_proyecto
{
    public partial class FrmCategorias : Form
    {
        public FrmCategorias()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        ClsConexionBD connect = new ClsConexionBD();
        int Record_Id;

        public void MostrarDatos()
        {
            try
            {
                string consulta = "SELECT codigo_categoria as Código, descripcion_categoria as Categoria FROM Categoria_Producto";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, connect.conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                DgvCategoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DgvCategoria.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Limpiar() {
            txtCodigo.Clear();
            txtCategoria.Clear();
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            FrmMenuCRUD CRUD = new FrmMenuCRUD();
            CRUD.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tslFecha.Text = DateTime.Now.ToLongDateString();
            tslHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            MostrarDatos();
        }

        validaciones validacion = new validaciones();

        public void validar() {
            if (validacion.Espacio_Blanco(errorProvider1, txtCategoria) || validacion.Solo_Letras(errorProvider1, txtCategoria)) {

                if (validacion.Espacio_Blanco(errorProvider1, txtCategoria))
                {
                    errorProvider1.SetError(txtCategoria, "No se puede dejar en blanco las categorias");
                }
                else
                {
                    if (validacion.Solo_Letras(errorProvider1, txtCategoria))
                        errorProvider1.SetError(txtCategoria, "Solo es permitido ingresar letras");
                }
            }
        }


        private void DgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Record_Id = Convert.ToInt32(DgvCategoria.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtCodigo.Text = (DgvCategoria.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtCategoria.Text = (DgvCategoria.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (validacion.Espacio_Blanco(errorProvider1, txtCategoria) || validacion.Solo_Letras(errorProvider1, txtCategoria))
                {

                    if (validacion.Espacio_Blanco(errorProvider1, txtCategoria))
                    {
                        errorProvider1.SetError(txtCategoria, "No se puede dejar en blanco las categorias");
                    }
                    else
                    {
                        if (validacion.Solo_Letras(errorProvider1, txtCategoria))
                            errorProvider1.SetError(txtCategoria, "Solo es permitido ingresar letras");
                    }
                }
                else { 

                    string query = "INSERT INTO Categoria_Producto (descripcion_categoria) VALUES (@categoria)";
                    connect.abrir();
                    SqlCommand comando = new SqlCommand(query, connect.conexion);
                    comando.Parameters.AddWithValue("@categoria", txtCategoria.Text);
                    comando.ExecuteNonQuery();
                    connect.abrir();
                    MessageBox.Show("Nueva Categoria Insertado");
                    Limpiar();
                    MostrarDatos();
                }
            }
            catch (Exception ex){

                MessageBox.Show(ex.Message);
            
            }


        }



        private void BtnModificar_Click(object sender, EventArgs e)
        {
            
            try {
                if (validacion.Espacio_Blanco(errorProvider1, txtCategoria) || validacion.Solo_Letras(errorProvider1, txtCategoria))
                {

                    if (validacion.Espacio_Blanco(errorProvider1, txtCategoria))
                    {
                        errorProvider1.SetError(txtCategoria, "No se puede dejar en blanco las categorias");
                    }
                    else
                    {
                        if (validacion.Solo_Letras(errorProvider1, txtCategoria))
                            errorProvider1.SetError(txtCategoria, "Solo es permitido ingresar letras");
                    }
                }
                else { 

                string query = "Update Categoria_Producto set descripcion_categoria= '" + txtCategoria.Text + "' where Codigo_Categoria='" + Record_Id + "'";
                connect.abrir();
                SqlCommand comando = new SqlCommand(query, connect.conexion);
                comando.ExecuteNonQuery();
                connect.abrir();
                MessageBox.Show("Se Modificó Correctamente");
                Limpiar();
                MostrarDatos();

                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }
    }
}