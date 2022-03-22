using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gauss_e_Jacobi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            numericUpDown4.Enabled = false;
            button4.Enabled = false;
            numericUpDown5.Enabled = false;
            comboBox1.Enabled = false;
        }
        double[,] matriz;
        int linhas, colunas,linhasMatriz, colunasMatriz = 0;
        private void PrepararGrid()
        {
            //Quantidade de linhas = Tamanho da matriz
            dataGridView1.RowCount = matriz.GetLength(0);
            dataGridView1.ColumnCount = matriz.GetLength(1);
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                //define o cabeçalho das linhas com o nome "Linha" + posicao do for... linha 1, linha 2...
                dataGridView1.Rows[i].HeaderCell.Value = "Linha " + i.ToString();
            }
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                dataGridView1.Columns[j].HeaderCell.Value = "Coluna " + j.ToString();
            }
        }
        private void MostrarGrid()
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matriz[i, j]; //seta o valor a ser mostrado no grid de dacordo com a posição correspondente da matriz
                }
            }
        }
        private void button1_Click(object sender, EventArgs e) //Btn enviar
        {
            if(textBox1.Text != "")
            {
                matriz[linhas, colunas] = Convert.ToDouble(textBox1.Text);
                MostrarGrid();
                if (colunas < matriz.GetLength(1))
                {
                    colunas++;
                }
                if (linhas < matriz.GetLength(0) && (colunas == matriz.GetLength(1)))
                {
                    colunasMatriz = colunas;
                    linhas++;
                    colunas = 0;
                }
                if (linhas > matriz.GetLength(0) - 1)
                {
                    linhasMatriz = linhas;
                    button1.Enabled = false;
                    comboBox1.Enabled = true;
                }
                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("Informe um valor!","Erro!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }



        private void button2_Click(object sender, EventArgs e) //Btn criar matriz
        {
            if (numericUpDown2.Value < numericUpDown3.Value)
            {
                matriz = new double[(int)numericUpDown2.Value, (int)numericUpDown3.Value];
                PrepararGrid();
                MostrarGrid();
                linhas = colunas = 0;
                button1.Enabled = true;
                button6.Enabled = true;
            }
            else
            {
                MessageBox.Show("A matriz deve ser de ordem 0! (linhas x colunas + 1)", "Erro!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e) //Btn calcular
        {
            int max; double parada; int interacoes = 0;
            max = int.Parse(numericUpDown4.Text);
            parada = double.Parse(numericUpDown5.Text);
            if (comboBox1.Text == "Gauss - Seidel")                   ////  G A U S S - S E I D E L   \\\\
            {
                richTextBox1.Clear();
                double[] s = new double[linhasMatriz];
                while(interacoes < max)
                {
                    interacoes++;
                    for (int i = 0; i < linhasMatriz; i++)
                    {
                        double soma = 0;
                        for (int j = 0; j < colunasMatriz - 1; j++)
                        {
                            if (j == i) continue;
                            soma += matriz[i, j] * s[j];
                        }
                        s[i] = Math.Round((matriz[i, colunasMatriz - 1] - soma) / matriz[i, i],4);
                    }
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText("interação " + interacoes + Environment.NewLine + Environment.NewLine);
                    for (int i = 0; i < linhasMatriz; i++)
                    {
                        richTextBox1.AppendText("x" + (i + 1) + " = " + s[i] + Environment.NewLine);
                    }
                }
            }

            else if (comboBox1.Text == "Jacobi")                   ////  J A C O B I   \\\\
            {
                richTextBox1.Clear();
                double[] s = new double[linhasMatriz];//linhas
                double[] st = new double[linhasMatriz];
                while (interacoes < max)
                {
                    interacoes++;
                    for (int i = 0; i < linhasMatriz; i++)
                    {
                        double suma = 0;
                        for (int j = 0; j < colunasMatriz - 1; j++)
                        {
                            if (j == i) continue;

                            suma += matriz[i, j] * s[j];
                        }
                        st[i] = (matriz[i, colunasMatriz - 1] - suma) / matriz[i, i];
                    }
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText("interação " + interacoes + Environment.NewLine + Environment.NewLine);
                    for (int i = 0; i < linhasMatriz; i++)
                    {
                        s[i] = Math.Round(st[i],4);
                        richTextBox1.AppendText("x" + (i + 1) + " = " +s[i] + Environment.NewLine);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione o método primeiro!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            {
                double numeros = 0;
                for (int i = 0; i < numericUpDown3.Value; i++)
                {
                    Random random = new Random();
                    numeros = random.Next(1, 20);
                }
                matriz[linhas, colunas] = Convert.ToDouble(numeros);
                MostrarGrid();
                if (colunas < matriz.GetLength(1))
                {
                    colunas++;
                }
                if (linhas < matriz.GetLength(0) && (colunas == matriz.GetLength(1)))
                {
                    colunasMatriz = colunas;
                    linhas++;
                    colunas = 0;
                }
                if (linhas > matriz.GetLength(0) - 1)
                {
                    linhasMatriz = linhas;
                    button6.Enabled = false;
                    comboBox1.Enabled = true;
                }
            }
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            button1.Enabled = true;
            numericUpDown4.Enabled = true;
            button4.Enabled = true;
            numericUpDown5.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if(numericUpDown2.Value < 250)
            {
                numericUpDown3.Value = numericUpDown2.Value + 1;
            }
            else
            {
                numericUpDown2.Value = 249;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

            if (numericUpDown3.Value <= 250)
            {
                numericUpDown2.Value = numericUpDown3.Value - 1;
            }
            else
            {
                numericUpDown3.Value = 250;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = true;
            comboBox1.Enabled = true;
            numericUpDown4.Enabled = true;
            numericUpDown5.Enabled = true;
        }    
    }
}
