
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Office = Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;

namespace PruebaSegmentacionCajas
{
    public partial class NRBFNN : Form
    {

        DataTable dt;
        List<List<String>> cajas = new List<List<String>>();
        List<List<String>> cajasAsignacion = new List<List<String>>();
        List<List<String>> cajasAsignacion2 = new List<List<String>>();
        List<List<String>> indiceCajasAsignadas = new List<List<String>>();
        List<List<String>> IncideCajaOcupada = new List<List<string>>() { };
         List<String> IndicecajaSola2 = new List<string>();
        bool band4 = false;
        List<mVar> cajasTemp = new List<mVar>();
         List<float> fffondo = new List<float>();
        List<float> OOObjeto = new List<float>();

        int i = 0;
        
         public List<Color> ColoresE = new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Gray, Color.Aqua, Color.Black };
        public List<Brush> Brocha = new List<Brush>() { Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Purple, Brushes.Gray, Brushes.Aqua, Brushes.Black };
        List<int> cuentaClases = new List<int>();
        float[,] dendritas = new float[220, 7];
        public NRBFNN()
        {
            InitializeComponent();
            //  p1 = pictureBox1.CreateGraphics();
            for (int i = 0; i < ColoresE.Count; i++)
            {
                cuentaClases.Add(0);
            }

            dt = new DataTable(); //creas una tabla 
            dt.Columns.Add("Clase"); //le creas las columnas 
            dt.Columns.Add("R1");
            dt.Columns.Add("R2");
            dt.Columns.Add("CX");
            dt.Columns.Add("CY");

        }
        #region variables para abrir Excel
        private static object vk_missing = System.Reflection.Missing.Value;
        private static object vk_visible = true;
        private static object vk_false = false;
        private static object vk_True = true;
        private object vk_update_Links = 0;
        private object vk_Read_Only = vk_True;
        private object vk_Format = 1;
        private object vk_Password = vk_missing;
        private object vk_write_res_Password = vk_missing;
        private object vk_ignore_read_only_recommend = vk_True;
        private object vk_Origin = vk_missing;
        private object vk_Delimiter = vk_missing;
        private object vk_Editable = vk_false;
        private object vk_notify = vk_false;
        private object vk_converter = vk_missing;
        private object vk_add_to_mry = vk_false;
        private object vk_local = vk_false;
        private object vk_corrup_Load = vk_false;
        #endregion




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        double M = 0.3;
        public List<String> hiperBox()//, int matrizi)
        {
            //M = 0;
            
            List<String> rasgos = new List<String>();
            List<double> extraidos = new List<double>();
            rasgos.Clear();

            for (int fila = 0; fila < dataGridView1.ColumnCount; fila++)
            {
                extraidos.Clear();
                for (int colum = 0; colum < dataGridView1.RowCount - 1; colum++)
                {
                    extraidos.Add(double.Parse(NMMatriz[colum][fila]));
                }
                double izq = extraidos.Min();
                double der = extraidos.Max();
                double resta = (der - izq)*M;
                izq -= resta;
                der += resta;
                rasgos.Add(izq + "");
                rasgos.Add(der + "");               
            }
            return rasgos;
        }
        public String verificaCajaAsignada(List<String> NMMatriz)//, List<List<String>> cajas)
        { 
            for (int colum = 0; colum < NMMatriz.Count - 1; colum++)
            {
                  if (NMMatriz[colum].CompareTo(NMMatriz[colum + 1]) != 0)
                {
                    salir = "1";
                    break;
                }
            }
            return salir;
        }
        String salir = "0", mensaje = "";
        List<String> patronCaja = new List<string>() { };
        public void asignarCaja(List<List<String>> NMMatriz, List<List<String>> cajas)
        {
            int filaCini;
            for (int fila = 0; fila < NMMatriz.Count; fila++)
            { 
                mensaje += "\n";
                filaCini = 0;
                patronCaja.Clear();
                for (int colum = 1; colum < NMMatriz[fila].Count - 2; colum++)
                {
                    filaCini += 2;
                    for (int columC = 0; columC < cajas.Count; columC++)
                    {    if (float.Parse(NMMatriz[fila][colum]) >= float.Parse(cajas[columC][filaCini]) && float.Parse(NMMatriz[fila][colum]) <= float.Parse(cajas[columC][filaCini + 1]))
                        {
                            patronCaja.Add(columC + "");
                         }
                    }
                }
                for (int p = 0; p < patronCaja.Count(); p++)
                {
                    mensaje += patronCaja[p] + "   ";
                }
                if (verificaCajaAsignada(patronCaja).Equals("1"))
                {
                }
                else
                {

                    NMMatriz[fila][NomCajaasignado] = "Hc" + patronCaja[0];
                }
            }
        }
        public void asignarMulticajas1( )
        {
            int filaCini = 0, columCC = 0;
            for (int fila = 0; fila < NMMatriz.Count; fila++)
            {
                mensaje += "\n";
                filaCini = 0;
                i = 0;
                band = true;
                    for (int colum = 1; colum < NMMatriz[fila].Count - 3; colum++)
                    {
                    filaCini += 2;
                        for (int columC = columCC; columC < cajas.Count; columC++)
                        {
                            if (i < cajas.Count && band == true)
                            {
                                cajasAsignacion.Add(new List<String>());
                            }
                            if (float.Parse(NMMatriz[fila][colum]) >= float.Parse(cajas[columC][filaCini]) && float.Parse(NMMatriz[fila][colum]) <= float.Parse(cajas[columC][filaCini + 1]))
                            {
                                cajasAsignacion[i].Add("Hc" + columC);
                            }
                            else
                            {
                                cajasAsignacion[i].Add("0");

                            }
                            i++;
                        }
                        i = 0;
                        band = false;
                    }
                    regresa = true;
                    while (regresa == true)
                    {
                    String box = BuscaRutaPrimera(cajasAsignacion, NMMatriz[fila][0]);
                        if (!box.Equals("00"))
                        {
                            NMMatriz[fila][NomCajaasignado] = box;

                        }
                    }
                    NMMatriz[fila][indCaja] = columCC + "";
                    cajasAsignacion.RemoveRange(0, cajasAsignacion.Count);
            }

        }
        public void asignarMulticajas(String mensajeIndCaja)
        {
            int filaCini = 0, columCC = 0;
             for (int fila = 0; fila < NMMatriz.Count; fila++)
            { 
                    mensaje += "\n";
                    filaCini = 0;
                    i = 0;                     
                    band = true;
                    for (int colum = 1; colum < NMMatriz[fila].Count - 3; colum++)
                    {
                    filaCini += 2;
                        for (int columC = columCC; columC < cajas.Count; columC++)
                        {
                        if (i < cajas.Count && band == true)
                            {
                                cajasAsignacion.Add(new List<String>());
                             }
                            if (float.Parse(NMMatriz[fila][colum]) >= float.Parse(cajas[columC][filaCini]) && float.Parse(NMMatriz[fila][colum]) <= float.Parse(cajas[columC][filaCini + 1]))
                            {
                                 cajasAsignacion[i].Add("Hc" + columC);
                             }
                            else
                            {
                                 cajasAsignacion[i].Add("0");
                                 
                            }
                             i++;
                        }
                        i = 0;
                        band = false;
                    }
                    regresa = true;
                    while (regresa == true)
                    {
                        String box = BuscaRutaPrimera(cajasAsignacion, NMMatriz[fila][0]);
                        if (!box.Equals("00"))
                        {
                            NMMatriz[fila][NomCajaasignado] = box; 
                        } 
                    }
                     NMMatriz[fila][indCaja] = columCC + "";
                    cajasAsignacion.RemoveRange(0, cajasAsignacion.Count); 
            } 
        }
        public void asignarMulticajasResp(String mensajeIndCaja)//List<List<String>> NMMatriz, String mensajeIndCaja)
        {
            int filaCini = 0, columCC = 0;
            cajasAsignacion.Clear();
            for (int fila = 0; fila < NMMatriz.Count; fila++)
            {
                    band = false;
                    mensaje += "\n";
                    filaCini = 0;
                    i = 0;
                    columCC = cajas.Count - 2;
                    band = true;
                    for (int colum = 1; colum < NMMatriz[fila].Count - 3; colum++)
                    {
                         filaCini += 2;
                        for (int columC = columCC; columC < cajas.Count; columC++)
                        {
                            if (i < cajas.Count && band == true)
                            {
                                cajasAsignacion.Add(new List<String>());
                            }
                            if (float.Parse(NMMatriz[fila][colum]) >= float.Parse(cajas[columC][filaCini]) && float.Parse(NMMatriz[fila][colum]) <= float.Parse(cajas[columC][filaCini + 1]))
                            {
                                cajasAsignacion[i].Add("Hc" + columC);
                            }
                            else
                            {
                                cajasAsignacion[i].Add("0");
                            }
                            i++;
                        }
                        i = 0;
                        band = false;
                    }
                    regresa = true;
                    while (regresa == true)
                    {

                       // NMMatriz[fila][NomCajaasignado] = BuscaRutaPrimera(cajasAsignacion, NMMatriz[fila][0]);
                        String box = BuscaRutaPrimera(cajasAsignacion, NMMatriz[fila][0]);
                        if (!box.Equals("00"))
                        {
                            NMMatriz[fila][NomCajaasignado] = box;

                        }

                    }
                    NMMatriz[fila][indCaja] = columCC + "";
                    cajasAsignacion.RemoveRange(0, cajasAsignacion.Count);
                

            }

        }
        bool regresa = true;
        public String BuscaRutaPrimera(List<List<String>> cajasAsignacion, String clase)
        {
            List<String> IndicecajaSola = new List<string>();
            int c = 0;
            IndicecajaSola.Clear();
             mensaje = "";
            band3 = false;
            {
                regresa = false;
                band3=false;
                IndicecajaSola.Clear();
                int noPertenece = 0;
                for (c = 0; c < cajasAsignacion[cajasAsignacion.Count - 1].Count; c++)
                {
                    noPertenece = 0;
                    for (int f = 0; f < cajasAsignacion.Count; f++)
                    {
                        if (!cajasAsignacion[f][c].Equals("0")) 
                        {
                            if (band3 == false)
                            {
                                IndicecajaSola.Add(clase);
                                band3 = true;
                            }
                            if (band3 == true)
                            {
                                IndicecajaSola.Add(cajasAsignacion[f][c].Substring(2));
                                if (f < cajasAsignacion.Count - 1)
                                {
                                    if (!cajasAsignacion[f + 1][c].Equals("0"))
                                    {
                                        regresa = true;
                                        cajasAsignacion[f][c] = "0";
                                    }
                                }
                                break;
                            } 
                        }
                        else
                        {
                            noPertenece ++; 
                        } 
                    }
                    if (noPertenece == 2)
                    {
                        mensaje = "00";
                        break;
                    }

                }
                if (noPertenece < 2)
                {
                    mensaje = obtenerIndiceCaja(IndicecajaSola, 0);
                    band4 = false;
                    String mensaje2 = "";
                    String mensaje3 = "";
                    for (int yy = 0; yy < IncideCajaOcupada.Count(); yy++)
                    {
                        mensaje2 = "";
                        mensaje3 = "";
                        for (int y = 1; y < IncideCajaOcupada[yy].Count - 1; y++)
                        {
                            mensaje2 += IncideCajaOcupada[yy][y];
                            mensaje3 += IndicecajaSola[y];
                        }
                        if (mensaje2.Equals(mensaje3) == true)
                        {
                            band4 = true;
                            break;
                        }
                    }
                    if (band4 == false)
                    {
                        IndicecajaSola.Add("0");
                        IncideCajaOcupada.Add(IndicecajaSola);
                    }
                }
            }
            return mensaje;
        }
        public String BuscaRutaAdelanteAtras(List<List<String>> cajasAsignacion, String clase)
        {
            List<String> IndicecajaSola = new List<string>();
            int c = 0, cuentaC = 0;
            IndicecajaSola.Clear();
            mensaje = "";
            mensaje = "";
            band3 = false;
            for (c = 0; c < cajasAsignacion[cajasAsignacion.Count - 1].Count; c++)
            {
                cuentaC = 0;
                for (int f = 0; f < cajasAsignacion.Count; f++)
                {
                    if (!cajasAsignacion[f][c].Equals("0"))
                    {
                        if (band3 == false)
                        { 
                            IndicecajaSola.Add(clase);
                            band3 = true; 
                        }
                        if (band3 == true)
                        {
                            cuentaC++; 
                            if (cuentaC > 1)
                            {
                                 if (c == 0)
                                {
                                    if (!cajasAsignacion[f][c + 1].Equals("0"))
                                    {
                                         IndicecajaSola.RemoveAt(IndicecajaSola.Count - 1);
                                         IndicecajaSola.Add(cajasAsignacion[f][c].Substring(2));
                                     }
                                }
                                else
                                {
                                    if (!cajasAsignacion[f][c - 1].Equals("0"))
                                    {
                                         IndicecajaSola.RemoveAt(IndicecajaSola.Count - 1);
                                         IndicecajaSola.Add(cajasAsignacion[f][c].Substring(2));
                                     }
                                    else
                                    {
                                        IndicecajaSola.RemoveAt(IndicecajaSola.Count - 1);
                                        IndicecajaSola.Add(cajasAsignacion[f - 1][c].Substring(2));
                                    }
                                }

                            }
                            else
                            {
                                IndicecajaSola.Add(cajasAsignacion[f][c].Substring(2));

                            }
                             
                        }


                    }

                }
            }
             band4 = false;
            for (int yy = 0; yy < IncideCajaOcupada.Count(); yy++)
            {
                String mensaje2 = "";
                for (int y = 1; y < IncideCajaOcupada[yy].Count - 1; y++)
                {
                    mensaje2 += IncideCajaOcupada[yy][y];
                    mensaje += IndicecajaSola[y];
                 }

                if (mensaje2.Equals(mensaje) == true)
                {
                    band4 = true;
                    break;
                }

            }
            if (band4 == false)
            { IndicecajaSola.Add("0");
                IncideCajaOcupada.Add(IndicecajaSola);
                 
            }
            return mensaje;
        }

        public String BuscaRutaCLase(List<List<String>> cajasAsignacion, String clase)
        {
            List<String> IndicecajaSola = new List<string>();
            int c = 0, escoge = 0, cuentaC = 0;
            IndicecajaSola.Clear();
            mensaje = "";
            band3 = false;
            for (c = 0; c < cajasAsignacion[cajasAsignacion.Count - 1].Count; c++)
            {
                cuentaC = 0;
                 for (int f = 0; f < cajasAsignacion.Count; f++)
                {
                    if (!cajasAsignacion[f][c].Equals("0"))
                    {
                        if (band3 == false)
                        {
                             IndicecajaSola.Add(clase);
                            band3 = true;
                            escoge = f;
                        }
                        if (band3 == true)
                        {
                            cuentaC++;
                             IndicecajaSola.Add(cajasAsignacion[f][c].Substring(2));
                             if (cuentaC > 1)
                            {
                                 IndicecajaSola.RemoveAt(IndicecajaSola.Count - 1);
                                 IndicecajaSola.Add(cajasAsignacion[f][c].Substring(2));
                             } 
                            break;
                        } 
                    }
                }
            }
            mensaje = obtenerIndiceCaja(IndicecajaSola, 0);
            band4 = false;
            for (int yy = 0; yy < IncideCajaOcupada.Count(); yy++)
            {
                String mensaje2 = "";
                for (int y = 1; y < IncideCajaOcupada[yy].Count - 1; y++)
                {
                    mensaje2 += IncideCajaOcupada[yy][y];
                    mensaje += IndicecajaSola[y];
                }
                if (mensaje2.Equals(mensaje) == true)
                {
                    band4 = true;
                    break;
                }
            }
            if (band4 == false)
            {
                 IndicecajaSola.Add("0");
                IncideCajaOcupada.Add(IndicecajaSola);
             }
            return mensaje;
        }
        public String obtenerIndiceCaja(List<String> IncideCajaOcupadaTemp, int c)
        {
            mensaje = "";
            for (int y = c; y < IncideCajaOcupadaTemp.Count; y++)
            {
                mensaje += IncideCajaOcupadaTemp[y];
             }
            return mensaje;
        }

        public String obtenerIndiceCajaMatriz(List<List<String>> IncideCajaOcupadaTemp, int c)
        {
            mensaje = "";
            for (int y = 1; y < IncideCajaOcupadaTemp[c].Count - 1; y++)
            {
                mensaje += IncideCajaOcupadaTemp[c][y];
            }
            return mensaje;
        }
        List<double> ArgMax = new List<double>() { };
        public int obtenerExpMax(List<List<String>> IncideCajaOcupadaTemp)
        {
            int ind = 0;
            aux.Clear();
            exponente.Clear();
            desicion.Clear();
            for (int numClass = 0; numClass < NumClases.Count; numClass++)
            {
                exponente.Clear();
                 for (int x = 0; x < IncideCajaOcupadaTemp.Count; x++)
                {
                    if (IncideCajaOcupadaTemp[x][0].Equals(NumClases[numClass]))
                    {
                        exponente.Add(double.Parse(IncideCajaOcupadaTemp[x][indexp]));
                    }
                }
                desicion.Add((exponente.Max()));
             }
            ind = desicion.IndexOf(desicion.Max());
             return ind;

        }
        public double obtenerExpMax1(List<List<String>> IncideCajaOcupadaTemp)
        {
            double ind = 0;
            ArgMax.Clear();
            exponente.Clear();
            for (int xx = 0; xx < NumClases.Count; xx++)
            {
                 exponente.Clear();
                for (int x = 0; x < IncideCajaOcupadaTemp.Count; x++)
                {
                     if (IncideCajaOcupadaTemp[x][0].Equals(NumClases[xx]))
                    {
                        exponente.Add(double.Parse(IncideCajaOcupadaTemp[x][indexp]));
                    }
                }
                ArgMax.Add(exponente.Max());
            } 
            ind = ArgMax.Max();
            return ind;
            }
        List<String> aux = new List<string>();
        List<double> distMinMult = new List<double>();
        List<int> indClase = new List<int>();
        String Cajaliberada = "1";
        public String verificaPatrones(String mensaje)//List<List<String>> NMMatriz, String mensaje)
        {
            String Imprimeliberados = "";
            aux.Clear();
            indClase.Clear();
            Cajaliberada = "1";
            int contador = 0;
            int solo = 0;
            Imprimeliberados = mensaje + "  liberados \n";
            bool checa = false;
            for (int fila = 0; fila < NMMatriz.Count; fila++)
            {    
                if (NMMatriz[fila][NomCajaasignado].Substring(1, NMMatriz[fila][NomCajaasignado].Count() - 1).Equals(mensaje))// && !NMMatriz[fila][liberado].Equals("true"))
                {
                    if (fila == 2104)
                    {
                        if (checa == true && fila == 2104)
                        {
                            imp += fila + "clase  " + NMMatriz[fila][0] + "\n";
                        }
                    }
                    aux.Add(NMMatriz[fila][Tipoclase]);
                    indClase.Add(fila);
                    solo++;
                }
                else
                {
                    contador++;
                }
            }
            if (solo == 1)
            {
                Cajaliberada = "0";
                NMMatriz[indClase[aux.Count - 1]][liberado] = "true";
                Imprimeliberados += indClase[aux.Count - 1] + "   ";
            }
            else
            {
                if (contador < NMMatriz.Count() - 1)
                {
                    band2 = false;
                    for (int fila = 0; fila < aux.Count - 1; fila++)
                    {
                        if (aux[fila] != aux[fila + 1])
                        {
                            band2 = true;
                            break;
                        }
                    }
                    if (band2 == false)
                    {
                        for (int fila = 0; fila < aux.Count; fila++)
                        {
                            NMMatriz[indClase[fila]][liberado] = "true";
                            Imprimeliberados += indClase[fila] + "   ";
                        }
                        Cajaliberada = "0";
                    }
                }
                if (contador == NMMatriz.Count())
                {
                    Cajaliberada = "-1";

                }
            }
            return Cajaliberada;
        }         
        List<List<double>> DesvCajas = new List<List<double>>() { };
        List<List<double>> MatrizInversa = new List<List<double>>() { };
        List<double> ObtenerLinea = new List<double>();
        public void ObtenerDesv(List<List<String>> cajas)
        {
             int rows;
             for (rows = 0; rows < cajas.Count; rows++)
            {
                for (int colum = 2; colum < cajas[rows].Count; colum += 2)
                { 
                    double sum = 0, cont = 0;
                    double lineaX = double.Parse(cajas[rows][colum]);
                    for (int i = int.Parse(cajas[rows][colum]); i <= int.Parse(cajas[rows][colum + 1]); i++)
                    {
                        cont++;
                        sum += Math.Pow((double)(lineaX - Centroides[rows][colum - 2]), 2);// (int)double.Parse(cajas[rows].CentroX)), 2);
                        lineaX = lineaX + 1; 
                    }
                    DesvCajas[rows].Add((sum / (cont - 1)));
                }
             }
            for (int f = 0; f < DesvCajas.Count; f++)
            {
                for (int c = 0; c < DesvCajas[f].Count; c++)
                {
                    mensaje += DesvCajas[f][c] + "  ";
                }
                mensaje += "\n";
            }
            MessageBox.Show("desviaciones \n" + mensaje);
        } 
        bool band = true, band2 = false, band3 = true;
        public void LLenaGridView(String Patron, String R1, String R2, String CX, String CY)
        {
            DataRow row = dt.NewRow(); 
            row["Clase"] = Patron; 
            row["R1"] = R1;
            row["R2"] = R2;
            row["CX"] = CX;
            row["CY"] = CY;
            dt.Rows.Add(row); 
            dataGridView1.DataSource = dt; 
            dataGridView1.Update(); 
            dataGridView1.Refresh();
        }
        List<List<double>> ResultCapaInteg = new List<List<double>>() { };
        List<double> maxArg = new List<double>();
        List<List<double>> integradora = new List<List<double>>();
        List<List<double>> integradorarecibe = new List<List<double>>();
        public List<List<double>> BuscaGaussiana(int x, int y, String clase)
        {
             integradora.Clear();
             double CentroX = 0, valor = 0, ex = 0;
             double potY = 0;
            int i = 0;
            for (i = 0; i < Centroides.Count; i++)
            { 
                for (int j = 0; j < Centroides[i].Count; j++)
                {
                    CentroX = Centroides[i][j];
                     valor = x;
                     double resta = CentroX - valor;
                    double med = resta * resta;
                     potY += (med / DesvCajas[i][j]);
                 }
            }
            ex = Math.Exp(-(potY));
            integradora[i].Add(ex);
            MessageBox.Show("GAUSSIANA  " + ex);
            return integradora;
             
        }
        int g = 0;
        String imp = "";
        List<List<double>> Gauss = new List<List<double>>() { };
        List<List<double>> Med_Desv = new List<List<double>>() { };
        List<List<double>> matriz_Cova = new List<List<double>>() { };
        List<List<double>> matriz_Inver = new List<List<double>>() { };
        List<double> transpuesta = new List<double>() { };
        List<double> Mult_transpuesta_Inversa = new List<double>() { };
        
        public void multTransp_Inver(double Px, double Py)
        {
            transpuesta.Add(Px - Med_Desv[0][0]);
            transpuesta.Add(Py - Med_Desv[0][1]);
            String Result_Mult_transp_Inver = "";
            for (int t = 0; t < NumRasgos; t++)
            {
                Mult_transpuesta_Inversa.Add(0);
                for (int tt = 0; tt < NumRasgos; tt++)
                {
                    Mult_transpuesta_Inversa[t] += transpuesta[t] * matriz_Inver[tt][t];

                }
                Result_Mult_transp_Inver += Mult_transpuesta_Inversa[t] + "  ";
            }
            double Result = 0;
            for (int t = 0; t < NumRasgos; t++)
            {
                for (int tt = 0; tt < transpuesta.Count; tt++)
                {
                    Result += Mult_transpuesta_Inversa[t] * transpuesta[tt];
                }
             }
            MessageBox.Show("mult Trans_entradas no Inversa  \n" + (Result * .5) + " \nexp " + Math.Exp((Result * .5)));
        }
        public String TotalCajas(List<List<String>> cajas)
        {
            mensaje = ""; 
            for (int colum = 0; colum < cajas.Count(); colum++)
            {
                mensaje += "\n";
                for (int f = 2; f < cajas[colum].Count(); f++)
                {
                    mensaje += cajas[colum][f] + "   ";
                }
            } 
            return mensaje;
        }

        public List<List<String>> SepararPatrones(List<List<String>> cajas, int c)
        {
            List<String> rasgosIzq = new List<String>();
            List<String> rasgoDerInf = new List<String>();
            rasgosIzq.Clear();
            rasgoDerInf.Clear();
            int indCajaDividir = 0;
            int indCar = 0;
            int i = 1;
            for (int col = 0; col < cajas[indCajaDividir].Count() - 3; col += 2)
            {
                String act = "", inhc = "", inhf = "";
                if (col == 0)
                {
                    rasgosIzq.Add("0");
                    rasgosIzq.Add("0");
                    IncideCajaOcupada[c][0] = "-1";
                    rasgoDerInf.Add("0");
                    rasgoDerInf.Add("0");
                }
                else
                {
                    indCar = int.Parse(IncideCajaOcupada[c][i]);
                    i++;
                    act = cajas[indCar][col];
                    inhc = (float.Parse(cajas[indCar][col]) + float.Parse(cajas[indCar][col + 1])) / 2 + "";
                    inhf = cajas[indCar][col + 1];

                    rasgosIzq.Add(act);
                    rasgosIzq.Add(inhc);
                    rasgoDerInf.Add(inhc);
                    rasgoDerInf.Add(inhf);

                }
            }
            cajas.Add(rasgosIzq);
            cajas[cajas.Count - 1].Add("activa");
            id++;
            cajas[cajas.Count - 1].Add("Hc" + id);
            cajas.Add(rasgoDerInf);
            cajas[cajas.Count - 1].Add("activa");
            id++;
            cajas[cajas.Count - 1].Add("Hc" + id);
            return cajas;
        }

        String mensajeIndiceCaja = "";
        public void totalCajas()
        {
            for (int f = 0; f < IncideCajaOcupada.Count(); f++)
            {
                mensajeIndiceCaja = obtenerIndiceCajaMatriz(IncideCajaOcupada, f);
                if (IncideCajaOcupada[f][0].Equals("-1"))
                {
                    IncideCajaOcupada.RemoveAt(f);
                    f--;
                }
            }
            MessageBox.Show(" total de cajas " + IncideCajaOcupada.Count());
        }

        private void buttonHiperBox_Click(object sender, EventArgs e)
        {
            
            cajas.Add(hiperBox());//NMMatriz));//delimita todos los patrones
            cajas[cajas.Count - 1].Add("activa");
            elim = cajas[cajas.Count - 1].Count() - 1;
            cajas[cajas.Count - 1].Add("Hc" + id);
            mensaje = "";
            for (int h = 0; h < cajas[0].Count - 1; h += 2)
            {
                IndicecajaSola2.Add("0");
            }
            IncideCajaOcupada.Add(IndicecajaSola2);
            indexp = IncideCajaOcupada[0].Count() - 1;
 
            int cuentaPatronesEntran = 0, cuentadifetrentes = 0;
            Boolean Verificado = false;
            Boolean Dividir = false;
            String cajaDividir = "";
            
            for (int e1 = 0; e1 < IncideCajaOcupada.Count; e1++)
            {
                
                int filaCini = 0, columCC = 0;
                string mensajeIndiceCajaInd = IncideCajaOcupada[e1][0];
                mensajeIndiceCaja = obtenerIndiceCajaMatriz(IncideCajaOcupada, e1);
                band = true;
                {
                    mensaje = "";
                    imp = "";
                    cuentaPatronesEntran = 0;
                    cuentadifetrentes = 0;
                    for (int fila = 0; fila < NMMatriz.Count; fila++)
                    {

                            filaCini = 0;
                            i = 0;
                            columCC = cajas.Count - 2;
                            int k = 0;
                            Boolean ver = false;
                            cajasAsignacion.Add(new List<String>());
                            for (int colum = 1; colum < NMMatriz[fila].Count - 3; colum++)
                            {
                                filaCini += 2;
                                int inn = int.Parse(IncideCajaOcupada[e1][colum]);
                                if (float.Parse(NMMatriz[fila][colum]) >= float.Parse(cajas[inn][filaCini]) && float.Parse(NMMatriz[fila][colum]) <= float.Parse(cajas[inn][filaCini + 1]))
                                {
                                    cajasAsignacion[i].Add("Hc" + inn);
                                        if (cajasAsignacion[i][k].Equals("0"))
                                        {
                                            ver = true;
                                            break;
                                        }
                                    k++;
                                }
                                else
                                {
                                    cajasAsignacion[i].Add("0");
                                    ver = true;
                                    break;
                                }
                            }
                            if (ver == false)
                            {
                                if (cuentaPatronesEntran >= 1)
                                {
                                    if (mensaje.Equals(NMMatriz[fila][0]))
                                    {
                                        imp += NMMatriz[fila][0] + "   ";
                                        mensaje = NMMatriz[fila][0] + "";
                                        NMMatriz[fila][NomCajaasignado] =mensajeIndiceCajaInd+mensajeIndiceCaja;
                                        cajasAsignacion.Clear();
                                        cuentaPatronesEntran++;
                                        Verificado = false;
                                    }
                                    else
                                    {
                                        imp += NMMatriz[fila][0] + "   ";
                                        cuentadifetrentes++;
                                        cuentaPatronesEntran = 0;
                                        NMMatriz[fila][NomCajaasignado] = mensajeIndiceCajaInd + mensajeIndiceCaja;
                                        cajasAsignacion.Clear();
                                        cajaDividir += e1 + "   ";
                                        Verificado = true;
                                        Dividir = true; 
                                    }
                                }
                                else
                                {
                                    imp += NMMatriz[fila][0] + "   ";
                                    mensaje = NMMatriz[fila][0] + "";
                                    cuentaPatronesEntran++;
                                    NMMatriz[fila][NomCajaasignado] = mensajeIndiceCajaInd + mensajeIndiceCaja;
                                    cajasAsignacion.Clear();
                                    Verificado = false;
                                }
                            band = true;
                            }
                            else
                            { 
                                cajasAsignacion.Clear();
                                band = false;
                                ver = false;
                            }
                            k++;                  
                    }
                    if (Dividir == true)
                    {
                        cajas = SepararPatrones(cajas, e1);
                        asignarMulticajasResp(mensajeIndiceCaja);
                        Dividir = false;
                    }
                }
            }
            totalCajas();
        }
        public void recorreEliminar(List<List<String>> NMMatriz, int e1)
        {
            cajas[e1][elim] = "eliminada";
            for (int t = 0; t < NMMatriz.Count; t++)
            {
                if (int.Parse(NMMatriz[t][indCaja]) > e1)
                    NMMatriz[t][indCaja] = (int.Parse(NMMatriz[t][indCaja]) - 1) + "";
            }
        }
        public void visualizaDatagrid(List<List<String>> NMMatriz)
        {
            for (int f = 0; f < NMMatriz.Count; f++)
            {
                if (NMMatriz[f][liberado].Equals("true"))
                {
                    LLenaGridView(f + ".-", NMMatriz[f][Tipoclase], NMMatriz[f][NomCajaasignado], NMMatriz[f][indCaja], "");
                }
            }
        }         
        public void importar(String archivo, String BD)
        {
            String carpeta = "";
            switch (BD)
            {
                case "p1Training":
                case "p1Test":
                    carpeta = "p1";
                    M = 0.0018;
                    break;
                case "p2Training":
                case "p2Test":
                    carpeta = "p2";
                    break;
                case "segmentationTraining10":
                case "segmentationTest10":
                    carpeta = "segmentation";
                    M = .54;
                    break;
                 case "LetterRecognitionTraining":
                 case "LetterRecognitionTest":
                    carpeta = "LetterRecognition";
                    break;
                 case "nRasgos1":
                    carpeta = "nRasgos1";
                    break;
                case "DigitsRecognitionTest":
                case "DigitsRecognitionTraining":
                    carpeta = "DigitsRecognition";
                    M = 0.25;
                    break;
                case "GlassTest":
                case "GlassTraining":
                    carpeta = "Glass";
                    M = 0.19;
                    break;
                case "IrisTest":
                case "IrisTraining":
                    carpeta = "Iris";
                    M = 0.3;
                    break;
                case "LiverTest":
                case "LiverTraining":
                    carpeta = "Liver";
                    M = 0.88;
                    break;
                case "PageBlocksTest":
                case "PageBlocksTraining":
                    carpeta = "PageBlocks";
                    M = 0.29;
                    break;
            }
             String origen = "C:\\BD\\" + carpeta + "\\";
            try
            {
                OleDbConnection conecta = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + origen + archivo + ".xlsx; Extended Properties= Excel 12.0; ");
                conecta.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [Hoja$]", conecta);
                DataSet dat = new DataSet();
                adapter.Fill(dat, "Hoja");
                dataGridView1.DataSource = dat.Tables[0];

            }
            catch (Exception d)
            {
                MessageBox.Show("error" + d.Message);
            }

        }
        int[] patron = new int[100];
        List<String> NumClases = new List<string>() { };
        List<List<String>> NMMatriz = new List<List<String>>() { };
        int liberado = 0, Tipoclase = 0, NomCajaasignado = 0, elim = 0, id = 0, indCaja = 0;
        public List<List<String>> LlenaMatriz(List<List<String>> NMMatriz)
        {
            int i = 0;
            int rows = dataGridView1.RowCount;
            int colum = dataGridView1.ColumnCount;
            for (i = 0; i < rows - 1; i++)
            {
                 NMMatriz.Add(new List<String>() { });
                 NMMatriz[i].Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                 patron[int.Parse(NMMatriz[i][Tipoclase])]++;
                 if (NumClases.Contains(NMMatriz[i][Tipoclase]) == false)
                {
                    NumClases.Add(NMMatriz[i][Tipoclase]);
                }
                mensaje = "";
                 for (int h = 1; h < colum; h++)
                {
                    NMMatriz[i].Add(dataGridView1.Rows[i].Cells[h].Value.ToString());
                }
                NMMatriz[i].Add("false");
                liberado = NMMatriz[i].Count - 1;
                for (int h = 0; h < NMMatriz[i].Count - 1; h++)
                {
                    mensaje += "0";
                }
                NMMatriz[i].Add(mensaje);
                NomCajaasignado = NMMatriz[i].Count - 1;
                NMMatriz[i].Add(mensaje);
                indCaja = NMMatriz[i].Count() - 1;
                }
             return NMMatriz;
               }

        String[] nomArch = { "p1Test", "p2Test", "segmentationTest10", "LetterRecognitionTest", "nRasgos1", "DigitsRecognitionTest", "GlassTest", "IrisTest", "LiverTest", "PageBlocksTest", "p1Training", "p2Training", "segmentationTraining10", "LetterRecognitionTraining", "DigitsRecognitionTraining", "GlassTraining", "IrisTraining", "LiverTraining", "PageBlocksTraining" };

        private void button5_Click(object sender, EventArgs e)
        {
            NMMatriz.Clear();
            importar(nomArch[comboBox1.SelectedIndex], comboBox1.SelectedItem.ToString());
            NMMatriz = LlenaMatriz(NMMatriz);
        }         
        List<String> letras = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public void ExportaExcel(DataGridView DataGridView1)
        {
            String ruta = @"C:\Users\Griselda Cortés\Documents\mi\respaldoMini\doctorado\Estancia\Prueba\ExportaDataSet.xlsx";//Application.StartupPath + @"\ExportaDataSet.xlsx";
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(ruta, vk_update_Links, vk_Read_Only, vk_Format, vk_Password, vk_write_res_Password, vk_ignore_read_only_recommend, vk_Origin, vk_Delimiter, vk_Editable, vk_notify, vk_converter, vk_add_to_mry, vk_local, vk_corrup_Load);
            Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1);
            MessageBox.Show("columna" + DataGridView1.ColumnCount);
            for (int i = 1; i < DataGridView1.ColumnCount; i++)
            {
                if (DataGridView1.Columns[i].Visible == true)
                {
                     excelWorkSheet.Cells[i, i] = DataGridView1.Columns[i].HeaderText;
                }
            }
            int fila = 2;
            for (int i = 1; i < DataGridView1.Rows.Count; i++)
            {
                fila++;
                for (int j = 1; i < DataGridView1.Columns.Count; j++)
                {                 
                         excelWorkSheet.Cells[fila, j] = DataGridView1[i, j].Value.ToString(); 
                }
            }
            excelApp.Visible = true;
        }
        public void ExportaExcelDataGrid()
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                 Excel.Application excelApp = new Excel.Application();
                 Excel.Workbook excelBook = excelApp.Workbooks.Add();
                 Excel._Worksheet excelWorksheet = (Excel._Worksheet)excelBook.Worksheets.get_Item(1);
                 excelApp.Visible = false;
                 for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        excelWorksheet.Cells[i + 1, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                 excelBook.SaveAs(fichero.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                 excelBook.Close(true);
                 excelApp.Quit();
            }
        }
        List<List<double>> sumX = new List<List<double>>() { };
        List<List<double>> medX = new List<List<double>>() { };
        double[] sumY = new double[10];
        double[] medY = new double[10];
        double[] dist = new double[10]; 
        int NumRasgos = 2;
        List<List<double>> Centroides = new List<List<double>>() { };
        List<double> centroCaja = new List<double>();
        List<double> RestaVarMedia = new List<double>();
        List<double> MultInversa = new List<double>();
        int indexp = 0;
        public void calculaDesvEstandar()
        {
            int indcolum = 0, indcCentroides = 0, cuentCentroides = -1;
            for (int ca = 0; ca < IncideCajaOcupada.Count(); ca++)
            {
                Centroides.Add(new List<double>() { });
                cuentCentroides++;
                indcCentroides = 0;
                indcolum = 2;
                 for (int cb = 1; cb < IncideCajaOcupada[ca].Count() - 1; cb++)
                {
                    int fila = int.Parse(IncideCajaOcupada[ca][cb]);
                     Centroides[ca].Add((double.Parse(cajas[fila][indcolum]) + double.Parse(cajas[fila][indcolum + 1])) / 2);
                     indcolum += 2;
                    indcCentroides++;
                }
            }
            ObtenerDesv();
        }
        public void ObtenerDesv()
        {
            double sum = 0, cont = 0, incrementa =.01;
            int indcolum = 2;
            DesvCajas.Clear();
            for (int ca = 0; ca < IncideCajaOcupada.Count(); ca++)
            {
                DesvCajas.Add(new List<double>() { });
                indcolum = 2;
                for (int cb = 1; cb < IncideCajaOcupada[ca].Count() - 1; cb++)
                {
                    int fila = int.Parse(IncideCajaOcupada[ca][cb]);
                    double lineaX = double.Parse(cajas[fila][indcolum]);
                    sum = 0;
                    cont = 0;
                    while (lineaX <= double.Parse(cajas[fila][indcolum + 1]))
                    {
                        double media = lineaX - Centroides[ca][cb - 1];
                        sum += Math.Pow(media, 2);
                        lineaX = lineaX + incrementa;
                        cont++;
                    }
                    indcolum += 2;
                    DesvCajas[ca].Add(sum / (cont ));
                }

            }

        }
        public void ObtenerCentroides22(List<List<String>> NMMatriz)
        {
            mensaje = "";
            int cuentDesv = -1, indcolum = 0;//, indcCentroides = 0, indDesv = 0;
            double sum = 0;
             cuentaAsigDiferente = 0;
            cuentaAsigCorrecta = 0;
            for (int m = 0; m < NMMatriz.Count(); m++)
            {
                band = false;
                 exponente.Clear();
                MatrizInversa.Clear();
                varianzas.Clear();
                cuentDesv = -1;                
                for (int h = 0; h < IncideCajaOcupada.Count() - 1; h++)
                {
                    IncideCajaOcupada[h][indexp] = "0";
                }
                band = true;
                for (int ca = 0; ca < IncideCajaOcupada.Count(); ca++)
                {
                    indcolum = 2;
                    if (band == true)
                    {
                        MatrizInversa.Add(new List<double>() { });
                        multiplicaInversa.Clear();
                        multMatrizMedInv.Clear();
                        RestaVarMedia.Clear();
                        cuentDesv++;
                    }
                    for (int cb = 1; cb < IncideCajaOcupada[ca].Count() - 1; cb++)
                    {
                             int fila = int.Parse(IncideCajaOcupada[ca][cb]);
                            double inv = 1 / DesvCajas[ca][cb - 1] * 1;
                            MatrizInversa[cuentDesv].Add(inv);
                            RestaVarMedia.Add(double.Parse(NMMatriz[m][cb]) - Centroides[ca][cb - 1]);
                            multiplicaInversa.Add((RestaVarMedia[RestaVarMedia.Count - 1] * MatrizInversa[cuentDesv][MatrizInversa[cuentDesv].Count - 1]));
                            multMatrizMedInv.Add((RestaVarMedia[RestaVarMedia.Count - 1] * multiplicaInversa[multiplicaInversa.Count - 1]));
                            indcolum += 2;
                            band = true;

                         
                    } 
                    if (band == true)
                    {
                        sum = 0;
                        for (int u = 0; u < multMatrizMedInv.Count; u++)
                        {
                            sum += multMatrizMedInv[u];
                        }
                        IncideCajaOcupada[ca][indexp] = "" + ((Math.Exp(sum * (-.5))));
                    }
                }
                int indcc = obtenerExpMax(IncideCajaOcupada);
                NMMatriz[m][indCaja] = IncideCajaOcupada[indcc][0] + obtenerIndiceCajaMatriz(IncideCajaOcupada, indcc);
                if (!NMMatriz[m][Tipoclase].Equals(NumClases[indcc] + ""))
                    {
                         cuentaAsigDiferente++;
                     }
                    else
                    {
                        cuentaAsigCorrecta++;
                    }                 
            }
        }
        public void ObtenerCentroides(List<List<String>> NMMatriz)
        {
            mensaje = "";
            int cuentCentroides = -1, indcolum = 0, indcCentroides = 0, indDesv = 0;
            double sum = 0, cont = 0;
            String imprimeCajas = "", imprimeDesv = "", ImprimeCaja = "", imprimeRestaVarMedia = ""; ;
            cuentaAsigDiferente = 0;
            cuentaAsigCorrecta = 0;
            for (int m = 0; m < NMMatriz.Count(); m++)
            {
                    band = true;
                    exponente.Clear();
                    Centroides.Clear();
                    DesvCajas.Clear();
                    varianzas.Clear();
                    cuentCentroides = -1;
                    for (int h = 0; h < IncideCajaOcupada.Count() - 1; h++)
                    {
                        IncideCajaOcupada[h][indexp] = "0";
                    }
                    for (int ca = 0; ca < IncideCajaOcupada.Count(); ca++)
                    {
                        band = true;
                        RestaVarMedia.Clear();
                        indcolum = 2;
                        ImprimeCaja = "";
                        if (band == true)
                        {
                            Centroides.Add(new List<double>() { });
                            indcCentroides = 0;
                            DesvCajas.Add(new List<double>() { });
                            indDesv = 0;
                            cuentCentroides++;
                        }
                         for (int cb = 1; cb < IncideCajaOcupada[ca].Count() - 1; cb++)
                        {
                            if (int.Parse(IncideCajaOcupada[ca][0]) == int.Parse(NMMatriz[m][Tipoclase]))
                            {
                                 int fila = int.Parse(IncideCajaOcupada[ca][cb]);
                                 Centroides[cuentCentroides].Add((double.Parse(cajas[fila][indcolum]) + double.Parse(cajas[fila][indcolum + 1])) / 2);
                                 imprimeCajas += IncideCajaOcupada[ca][cb] + "---" + cajas[fila][indcolum] + "   " + cajas[fila][indcolum + 1] + "   ";
                                 double lineaX = double.Parse(cajas[fila][indcolum]);
                                ImprimeCaja += IncideCajaOcupada[ca][cb];
                                sum = 0;
                                 cont = 0;
                                     while (lineaX <= double.Parse(cajas[fila][indcolum + 1])) 
                                    {
                                        sum += Math.Pow((double)(lineaX - Centroides[cuentCentroides][indcCentroides]), 2);// (int)double.Parse(cajas[rows].CentroX)), 2);
                                         lineaX = lineaX + 0.1;
                                        cont++;
                                    }
                                  DesvCajas[cuentCentroides].Add(sum / (cont - 1));
                                 RestaVarMedia.Add(double.Parse(NMMatriz[m][indcCentroides + 1]) - Centroides[cuentCentroides][indcCentroides]);
                                 imprimeDesv += DesvCajas[cuentCentroides][indDesv] + "---";
                                 imprimeRestaVarMedia += RestaVarMedia[RestaVarMedia.Count - 1] + "   ";
                                indDesv++;
                                indcCentroides++;
                                indcolum += 2;
                                band = true;
                            }
                            else
                            {
                                IncideCajaOcupada[ca][indexp] = "0";
                                band = false;
                                cuentCentroides--;
                                Centroides.RemoveAt(Centroides.Count - 1);
                                break;
                            }
                        }
                        if (band == true)
                        {
                            MatrizVarianzas(cuentCentroides);
                             XRestaMatrizInversa();
                            IncideCajaOcupada[ca][indexp] = "" + Math.Exp(XRestInvX_Mu() * (-.5));
                             imprimeExp += IncideCajaOcupada[ca][indexp] + "   ";                  
                        }
                    }
                     double indcc = obtenerExpMax(IncideCajaOcupada);
            }
        }
        public void CalculaExponente(List<List<String>> NMMatriz)
        {
            mensaje = "";
            double distanciaMax = 0;
            int cuentCentroides = -1, indcolum = 0, indcCentroides = 0, indDesv = 0;
            cuentaAsigDiferente = 0;
            cuentaAsigCorrecta = 0;
            for (int m = 0; m < NMMatriz.Count(); m++)
            {
                    band = false;
                    band = true;
                    exponente.Clear();
                    varianzas.Clear();
                    cuentCentroides = -1;
                    for (int h = 0; h < IncideCajaOcupada.Count() - 1; h++)
                    {
                        IncideCajaOcupada[h][indexp] = "0";
                    }
                    for (int ca = 0; ca < IncideCajaOcupada.Count(); ca++)
                    {
                        band = true;
                        RestaVarMedia.Clear();
                        indcolum = 2; 
                        if (band == true)
                        {
                             indcCentroides = 0;
                        }
                        for (int cb = 1; cb < IncideCajaOcupada[ca].Count() - 1; cb++)
                        {
                               cuentCentroides = ca;
                               RestaVarMedia.Add(double.Parse(NMMatriz[m][indcCentroides + 1]) - Centroides[cuentCentroides][indcCentroides]);
                               indDesv++;
                               indcCentroides++;
                               indcolum += 2;
                               band = true;
                             }
                             MatrizVarianzas(cuentCentroides);
                             XRestaMatrizInversa();
                            IncideCajaOcupada[ca][indexp] = "" + Math.Exp(XRestInvX_Mu() * (-.5));                           
                    }
                    int indcc = 0;
                    distanciaMax = obtenerExpMax(IncideCajaOcupada);
                    for (int f = 0; f < IncideCajaOcupada.Count; f++)
                    {
                        if (IncideCajaOcupada[f][indexp].Equals(distanciaMax + ""))
                        {
                            indcc = f;
                            break;
                        }
                    }
                    NMMatriz[m][indCaja] = IncideCajaOcupada[indcc][0];// NumClases[indcc] + "";
                    if (!NMMatriz[m][Tipoclase].Equals(NMMatriz[m][indCaja]))
                    {
                         cuentaAsigDiferente++;
                    }
                    else
                    {
                        cuentaAsigCorrecta++;                       
                    }                  
            }
        }
        public void ObtenerCentroidesUnicamente(List<List<String>> NMMatriz)
        {
            mensaje = "";
            int cuentCentroides = -1, indcolum = 0, indcCentroides = 0, indDesv = 0;
            double sum = 0, cont = 0;
            String imprimeCajas = "", imprimeDesv = "", ImprimeCaja = "", imprimeCentroides = "";
            cuentaAsigDiferente = 0;
            cuentaAsigCorrecta = 0;
            exponente.Clear();
            Centroides.Clear();
            DesvCajas.Clear();
            varianzas.Clear();
            cuentCentroides = -1;
            for (int ca = 0; ca < IncideCajaOcupada.Count(); ca++)
            {
                band = true;
                RestaVarMedia.Clear();
                indcolum = 2;
                ImprimeCaja = "";
                Centroides.Add(new List<double>() { });
                indcCentroides = 0;
                DesvCajas.Add(new List<double>() { });
                indDesv = 0;
                cuentCentroides++;
                for (int cb = 1; cb < IncideCajaOcupada[ca].Count() - 1; cb++)
                {
                     int fila = int.Parse(IncideCajaOcupada[ca][cb]);
                    Centroides[cuentCentroides].Add((double.Parse(cajas[fila][indcolum]) + double.Parse(cajas[fila][indcolum + 1])) / 2);
                    imprimeCajas += IncideCajaOcupada[ca][cb] + "---" + cajas[fila][indcolum] + "   " + cajas[fila][indcolum + 1] + "   ";
                    imprimeCentroides += Centroides[cuentCentroides][indcCentroides] + "   ";
                    double lineaX = double.Parse(cajas[fila][indcolum]);
                    ImprimeCaja += IncideCajaOcupada[ca][cb];
                    sum = 0;
                    cont = 0;
                    for (int i = (int)double.Parse(cajas[fila][indcolum]); i <= (int)double.Parse(cajas[fila][indcolum + 1]); i++)
                    {
                        if (lineaX <= double.Parse(cajas[fila][indcolum + 1]))
                        {
                            sum += Math.Pow((double)(lineaX - Centroides[cuentCentroides][indcCentroides]), 2);
                            lineaX = lineaX + 1;
                            cont++;
                        }

                    }
                    DesvCajas[cuentCentroides].Add(sum / (cont - 1));
                    imprimeDesv += DesvCajas[cuentCentroides][indDesv] + "---";
                    indDesv++;
                    indcCentroides++;
                    indcolum += 2;
                    band = true;
                }
                imprimeCentroides += "\n";
            }
        }
        String imprimeExp = "";
        int cuentaAsigDiferente = 0, cuentaAsigCorrecta;
        public double XRestInvX_Mu()
        {
            double determinante = 0;
            for (int f = 0; f < RestaVarMedia.Count; f++)
            {
                determinante += multiplicaInversa[f] * RestaVarMedia[f];
            }
            return determinante;
        }
        public void XRestaMatrizInversa()
        {
            multiplicaInversa.Clear();
            String imprimeMultInversa = "";
            for (int f = 0; f < RestaVarMedia.Count; f++)
            {
                 multiplicaInversa.Add(0.0);
                for (int c = 0; c < RestaVarMedia.Count; c++)
                {
                    multiplicaInversa[f] += RestaVarMedia[c] * varianzas[c][f];
                }
                imprimeMultInversa += "" + multiplicaInversa[f] + "  ";
            }
        }
        List<List<double>> varianzas = new List<List<double>>() { };
        List<double> multiplicaInversa = new List<double>() { };
        List<double> multMatrizMedInv = new List<double>() { };

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void newTest()
        {
            NMMatriz.Clear();

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NMMatriz.Clear();
            importar(nomArch[comboBox1.SelectedIndex], comboBox1.SelectedItem.ToString());
            NMMatriz = LlenaMatriz(NMMatriz);
        }

        private void hiperboxToolStripMenuItem_Click(object sender, EventArgs e)
        {

            cajas.Add(hiperBox());//NMMatriz));//delimita todos los patrones
            cajas[cajas.Count - 1].Add("activa");
            elim = cajas[cajas.Count - 1].Count() - 1;
            cajas[cajas.Count - 1].Add("Hc" + id);
            mensaje = "";
            for (int h = 0; h < cajas[0].Count - 1; h += 2)
            {
                IndicecajaSola2.Add("0");
            }
            IncideCajaOcupada.Add(IndicecajaSola2);
            indexp = IncideCajaOcupada[0].Count() - 1;

            int cuentaPatronesEntran = 0, cuentadifetrentes = 0;
            Boolean Verificado = false;
            Boolean Dividir = false;
            String cajaDividir = "";

            for (int e1 = 0; e1 < IncideCajaOcupada.Count; e1++)
            {

                int filaCini = 0, columCC = 0;
                string mensajeIndiceCajaInd = IncideCajaOcupada[e1][0];
                mensajeIndiceCaja = obtenerIndiceCajaMatriz(IncideCajaOcupada, e1);
                band = true;
                {
                    mensaje = "";
                    imp = "";
                    cuentaPatronesEntran = 0;
                    cuentadifetrentes = 0;
                    for (int fila = 0; fila < NMMatriz.Count; fila++)
                    {

                        filaCini = 0;
                        i = 0;
                        columCC = cajas.Count - 2;
                        int k = 0;
                        Boolean ver = false;
                        cajasAsignacion.Add(new List<String>());
                        for (int colum = 1; colum < NMMatriz[fila].Count - 3; colum++)
                        {
                            filaCini += 2;
                            int inn = int.Parse(IncideCajaOcupada[e1][colum]);
                            if (float.Parse(NMMatriz[fila][colum]) >= float.Parse(cajas[inn][filaCini]) && float.Parse(NMMatriz[fila][colum]) <= float.Parse(cajas[inn][filaCini + 1]))
                            {
                                cajasAsignacion[i].Add("Hc" + inn);
                                if (cajasAsignacion[i][k].Equals("0"))
                                {
                                    ver = true;
                                    break;
                                }
                                k++;
                            }
                            else
                            {
                                cajasAsignacion[i].Add("0");
                                ver = true;
                                break;
                            }
                        }
                        if (ver == false)
                        {
                            if (cuentaPatronesEntran >= 1)
                            {
                                if (mensaje.Equals(NMMatriz[fila][0]))
                                {
                                    imp += NMMatriz[fila][0] + "   ";
                                    mensaje = NMMatriz[fila][0] + "";
                                    NMMatriz[fila][NomCajaasignado] = mensajeIndiceCajaInd + mensajeIndiceCaja;
                                    cajasAsignacion.Clear();
                                    cuentaPatronesEntran++;
                                    Verificado = false;
                                }
                                else
                                {
                                    imp += NMMatriz[fila][0] + "   ";
                                    cuentadifetrentes++;
                                    cuentaPatronesEntran = 0;
                                    NMMatriz[fila][NomCajaasignado] = mensajeIndiceCajaInd + mensajeIndiceCaja;
                                    cajasAsignacion.Clear();
                                    cajaDividir += e1 + "   ";
                                    Verificado = true;
                                    Dividir = true;
                                }
                            }
                            else
                            {
                                imp += NMMatriz[fila][0] + "   ";
                                mensaje = NMMatriz[fila][0] + "";
                                cuentaPatronesEntran++;
                                NMMatriz[fila][NomCajaasignado] = mensajeIndiceCajaInd + mensajeIndiceCaja;
                                cajasAsignacion.Clear();
                                Verificado = false;
                            }
                            band = true;
                        }
                        else
                        {
                            cajasAsignacion.Clear();
                            band = false;
                            ver = false;
                        }
                        k++;
                    }
                    if (Dividir == true)
                    {
                        cajas = SepararPatrones(cajas, e1);
                        asignarMulticajasResp(mensajeIndiceCaja);
                        Dividir = false;
                    }
                }
            }
            totalCajas();
        }

        private void trainningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calculaDesvEstandar();
            ObtenerCentroides22(NMMatriz);
            MessageBox.Show("listo clasificados correctamente " + cuentaAsigCorrecta + "diferente " + cuentaAsigDiferente + "/" + NMMatriz.Count());

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObtenerCentroides22(NMMatriz);
            MessageBox.Show("listo clasificados correctamente " + cuentaAsigCorrecta + "noooooooooo" + cuentaAsigDiferente + "/" + NMMatriz.Count());

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newTest();
            //dataGridView1.Rows.Clear();
            
            
        }

        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int ind = dataGridView1.RowCount;
            while ((ind = dataGridView1.RowCount) > 1)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
            

        }

        private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ObtenerCentroides22(NMMatriz);
            MessageBox.Show("listo clasificados correctamente " + cuentaAsigCorrecta + "\nIncorrectamente" + cuentaAsigDiferente + "/" + NMMatriz.Count());

        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        List<double> exponente = new List<double>() { };
        List<double> desicion = new List<double>() { };
        public void MatrizVarianzas(int filaDesvCajas)
        {
            double ext = 0;
            String impMatrizVarianzas = "";
            varianzas.Clear();
            for (int f = 0; f < DesvCajas[filaDesvCajas].Count; f++)
            {
                varianzas.Add(new List<double>() { });

                for (int c = 0; c < DesvCajas[filaDesvCajas].Count; c++)
                {
                    if (f == c)
                    {
                        ext = 1 / DesvCajas[filaDesvCajas][c] * 1;
                        varianzas[f].Add(ext);
                        impMatrizVarianzas += varianzas[f][c] + "---";
                    }
                    else
                    {
                        varianzas[f].Add(0);
                        impMatrizVarianzas += varianzas[f][c] + "---";
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ObtenerCentroides22(NMMatriz);
            MessageBox.Show("listo clasificados correctamente " + cuentaAsigCorrecta + "noooooooooo" + cuentaAsigDiferente + "/" + NMMatriz.Count());
        }
        private void button6_Click(object sender, EventArgs e)
        {
            calculaDesvEstandar();
            ObtenerCentroides22(NMMatriz);
            MessageBox.Show("listo clasificados correctamente " + cuentaAsigCorrecta + "diferente " + cuentaAsigDiferente + "/" + NMMatriz.Count());
        }

        private void radioButtonPrueba_CheckedChanged(object sender, EventArgs e)
        {
            //NomCajaasignado++;
            //band5 = true;
        }         
        List<double> minimo = new List<double>();
        List<double> minimoFinal = new List<double>();
        List<double> minimoFinalCajas = new List<double>();     
    }
}
        

 
