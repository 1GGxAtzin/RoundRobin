using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Round_Robin
{
    public partial class Form1 : Form
    {
        Queue<int> cola= new Queue<int>();
        List<int> tr = new List<int>();
        List<int> pila = new List<int>();
        List<int> rotativo = new List<int>();
        int tiempo = 0;
        bool flag = false;
        bool terminado = true;
        Thread hilo;
        Thread hilo2;
        public Form1()
        {
            InitializeComponent();
            chart1.Palette = ChartColorPalette.None;
            chart1.Titles.Add("Procesos");

        }
        void procesos()
        {
            _tiempo.Text = "0";
            int min = 100, max = 0, suma = 0, prom;
            List<int> visitado = new List<int>();
            List<int> ocupados = new List<int>();
            for (int i = 0; i < 8; ++i)
            {
                visitado.Add(-1);
                ocupados.Add(1);
            }
            int j = 0;
            int x;
            int cont=8;
            bool flag= false, calculado = false;
            while (cola.Count != 0)
            {
                if(cola.Peek() == -1)
                {
                    flag = false;
                    foreach(int i in cola)
                    {
                        if (i != -1)
                        {
                            flag = true;
                        }
                    }
                    if(flag == false)
                    {
                        return;
                    }
                    j++;
                    if (j == 8)
                    {
                        j = 0;
                    }
                    cola.Enqueue(cola.Dequeue());
                    continue;
                }
                x = cola.Dequeue();
                if (visitado[j] == -1)
                {
                    if (tiempo <= min)
                    {
                        min = tiempo;
                    }
                    if (tiempo > max)
                    {
                        max = tiempo;
                    }
                    suma += tiempo;
                    visitado[j] = tiempo;
                }
                chart1.Series["Procesos"].Points[j].Color = Color.Red;
                for(int i=0; i<rotativo[j] && x>0  ; ++i)
                {
                    Thread.Sleep(1000);
                    tiempo++;
                    _tiempo.Text = "" + tiempo;
                    _ocupado.Text = "" + ocupados[j]++;
                    x --;
                }
                if (x > 0)
                {
                    chart1.Series["Procesos"].Points[j].Color = Color.Blue;
                    cola.Enqueue(x);

                }
                else {
                    cola.Enqueue(-1);
                    chart1.Series["Procesos"].Points[j].Color = Color.Gray;
                }
                j++;
                prom = suma / 7;
                min_tr.Text = "" + min;
                max_tr.Text = "" + max;
                med_tr.Text = "" + prom;
                if (j == 8)
                {
                    j = 0;
                    prom = suma / 7;
                    min_tr.Text = "" + min;
                    max_tr.Text = "" + max;
                    med_tr.Text = "" + prom;
                    if (!calculado)
                    {
                        calculado = true;
                        prom = suma / 7;
                        int aux = 0;
                        for (int i = 0; i < visitado.Count(); ++i)
                        {
                            aux += (int)(Math.Pow(visitado[i] - prom, 2));
                        }
                        min_tr.Text = "" + min;
                        max_tr.Text = "" + max;
                        med_tr.Text = "" + prom;
                        desves_tr.Text = (Math.Sqrt(aux / 7)).ToString();
                    }
                }
            }
            terminado = true;
        }
        void procesos2()
        {
            _tiempo.Text = "0";
            int min = 100, max = 0, suma = 0, prom;
            List<int> visitado = new List<int>();
            List<int> ocupados = new List<int>();
            for (int i = 0; i < 8; ++i)
            {
                visitado.Add(-1);
                ocupados.Add(1);
            }
            int j = 0;
            int x;
            int cont = 8;
            bool flag = false, calculado = false;
            while (cola.Count != 0)
            {
                if (cola.Peek() == -1)
                {
                    flag = false;
                    foreach (int i in cola)
                    {
                        if (i != -1)
                        {
                            flag = true;
                        }
                    }
                    if (flag == false)
                    {
                        return;
                    }
                    j++;
                    if (j == 8)
                    {
                        j = 0;
                    }
                    cola.Enqueue(cola.Dequeue());
                    continue;
                }
                x = cola.Dequeue();
                if (visitado[j] == -1)
                {
                    if (tiempo <= min)
                    {
                        min = tiempo;
                    }
                    if (tiempo > max)
                    {
                        max = tiempo;
                    }
                    suma += tiempo;
                    visitado[j] = tiempo;
                }
                chart1.Series["Procesos"].Points[j].Color = Color.Red;
                for (int i = 0; i < pila[j] && x > 0; ++i)
                {
                    Thread.Sleep(100);
                    tiempo++;
                    _tiempo.Text = "" + tiempo;
                    _ocupado.Text = "" + ocupados[j]++;
                    x--;
                }
                if (x > 0)
                {
                    chart1.Series["Procesos"].Points[j].Color = Color.Blue;
                    cola.Enqueue(x);

                }
                else
                {
                    cola.Enqueue(-1);
                    chart1.Series["Procesos"].Points[j].Color = Color.Gray;
                }
                j++;
                prom = suma / 7;
                min_tr.Text = "" + min;
                max_tr.Text = "" + max;
                med_tr.Text = "" + prom;
                if (j == 8)
                {
                    j = 0;
                    prom = suma / 7;
                    min_tr.Text = "" + min;
                    max_tr.Text = "" + max;
                    med_tr.Text = "" + prom;
                    if (!calculado)
                    {
                        calculado = true;
                        prom = suma / 7;
                        int aux = 0;
                        for (int i = 0; i < visitado.Count(); ++i)
                        {
                            aux += (int)(Math.Pow(visitado[i] - prom, 2));
                        }
                        min_tr.Text = "" + min;
                        max_tr.Text = "" + max;
                        med_tr.Text = "" + prom;
                        desves_tr.Text = (Math.Sqrt(aux / 7)).ToString();
                    }
                }
            }
            terminado = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!terminado)
            {
                MessageBox.Show("Debe presionar primero el boton terminar");
                return;
            }
            int min=20, max=0, suma=0;
            Random random = new Random();
            for(int i= 0; i<8; ++i)
            {
                tr.Add(0);
                rotativo.Add(random.Next(8,8));
                if(rotativo[i]<= min)
                {
                    min = rotativo[i];
                }
                if (rotativo[i] > max)
                {
                    max = rotativo[i];
                }
                suma += rotativo[i];

            }
            int prom = suma/8;
            int aux=0;
            for(int i=0; i< rotativo.Count(); ++i)
            {
                aux += (int)(Math.Pow(rotativo[i] - prom, 2));
            }
;
            a3.Text = min.ToString();
            b3.Text = (suma / 8).ToString();
            c3.Text = max.ToString();
            cola.Enqueue(8);
            cola.Enqueue(20);
            cola.Enqueue(17);
            cola.Enqueue(5);
            cola.Enqueue(18);
            cola.Enqueue(28);
            cola.Enqueue(16);
            cola.Enqueue(21);

            int j = 1;
            foreach(int x in cola)
            {
               Color[] colors = new Color[] { Color.Green, Color.LightGreen, Color.YellowGreen, Color.Yellow, Color.Maroon, Color.Red, Color.Aqua, Color.Black};
                chart1.Series["Procesos"].Points.AddXY(j, x);
                chart1.Series["Procesos"].Color = Color.Transparent;
                chart1.Series["Procesos"].BorderColor = Color.Red;
                j++;
            }
            _idle.Text = ""+8;
            if (terminado)
            {
                hilo = new Thread(procesos);
                CheckForIllegalCrossThreadCalls = false;
                hilo.Start();
                terminado = false;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                hilo2.Abort();
                cola.Clear();
                chart1.Series.Clear();
                terminado = true;

                chart1.Palette = ChartColorPalette.None;
                chart1.Series.Add("Procesos");

                min_tr.Text = "";
                max_tr.Text = "";
                med_tr.Text = "";
                desves_tr.Text = "";

                a3.Text = "";
                b3.Text = "";
                c3.Text = "";

                _tiempo.Text = "";
                _ocupado.Text = "";
                _idle.Text = "";
                tiempo = 0;
                flag = false;
            }
            else { 
            hilo.Abort();
            cola.Clear();
            chart1.Series.Clear();
            terminado = true;

            chart1.Palette = ChartColorPalette.None;
            chart1.Series.Add("Procesos");

            min_tr.Text = "";
            max_tr.Text = "";
            med_tr.Text = "";
            desves_tr.Text = "";

            a3.Text = "";
            b3.Text = "";
            c3.Text = "";

            _tiempo.Text = "";
            _ocupado.Text = "";
            _idle.Text = "";
            tiempo = 0;
        }
    }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!terminado)
            {
                MessageBox.Show("Debe presionar primero el boton terminar");
                return;
            }
            int min = 20, max = 0, suma = 0;
            Random random = new Random();
            for (int i = 0; i < 8; ++i)
            {
                tr.Add(0);
                rotativo.Add(random.Next(8, 8));
                if (rotativo[i] <= min)
                {
                    min = rotativo[i];
                }
                if (rotativo[i] > max)
                {
                    max = rotativo[i];
                }
                suma += rotativo[i];

            }
            int prom = suma / 8;
            int aux = 0;
            for (int i = 0; i < rotativo.Count(); ++i)
            {
                aux += (int)(Math.Pow(rotativo[i] - prom, 2));
            }
;
            a3.Text = min.ToString();
            b3.Text = (suma / 8).ToString();
            c3.Text = max.ToString();
            cola.Enqueue(8);
            cola.Enqueue(20);
            cola.Enqueue(17);
            cola.Enqueue(5);
            cola.Enqueue(18);
            cola.Enqueue(28);
            cola.Enqueue(16);
            cola.Enqueue(21);

            int j = 1;
            foreach (int x in cola)
            {
                pila.Add(x);
                Color[] colors = new Color[] { Color.Green, Color.LightGreen, Color.YellowGreen, Color.Yellow, Color.Maroon, Color.Red, Color.Aqua, Color.Black };
                chart1.Series["Procesos"].Points.AddXY(j, x);
                chart1.Series["Procesos"].Color = Color.Transparent;
                chart1.Series["Procesos"].BorderColor = Color.Red;
                j++;
            }
            _idle.Text = "" + 8;
            if (terminado)
            {
                hilo2 = new Thread(procesos2);
                CheckForIllegalCrossThreadCalls = false;
                hilo2.Start();
                flag = true;
                terminado = false;
            }
        }
    }
}
