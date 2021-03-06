using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ProyectoFinal
{
    public partial class Form1 : Form
    {
        string datoCH1;
        string datoCH2;
        string datoCH3;
        string datoCH4;
        string datoCH5;
        string datoCH6;
        string datoCH7;
        string datoCH8;

        double valueCH1;
        double valueCH2;
        double valueCH3;
        double valueCH4;
        double valueCH5;
        double valueCH6;
        double valueCH7;
        double valueCH8;

        int rangeselect;
        int canalesselect;
        double xrange;
        int ymax;
        int span = 120;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonOpen.Enabled = true;
            buttonClose.Enabled = false;
            buttonStart.Enabled = false;
            buttonStop.Enabled = false;

            string[] rates = { "9600", "38400", "57600", "115200", "250000" };
            comboBoxBaud.DataSource = rates;

            string[] portList = SerialPort.GetPortNames();
            comboBoxPort.Items.AddRange(portList);

            string[] rangos = { "+-10", "+-5", "0-2.5" };
            comboBoxRange.DataSource = rangos;

            string[] canales = { "1", "2", "3", "4", "5", "6", "7", "8" };
            comboBoxChls.DataSource = canales;

            //chart1.ChartAreas["ChartArea1"].AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.WriteLine("$stop");
                    serialPort1.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBoxPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBoxBaud.Text);

                if (serialPort1.IsOpen) return;
                serialPort1.Open();

                buttonOpen.Enabled = false;
                buttonClose.Enabled = true;
                comboBoxBaud.Enabled = false;
                comboBoxPort.Enabled = false;
                buttonStart.Enabled = true;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == false) return;

                serialPort1.WriteLine("$stop");
                serialPort1.Close();
                timer1.Stop();

                buttonOpen.Enabled = true;
                buttonClose.Enabled = false;
                comboBoxBaud.Enabled = true;
                comboBoxPort.Enabled = true;

                chart1.Series["CH1"].Points.Clear();
                chart2.Series["CH2"].Points.Clear();
                chart3.Series["CH3"].Points.Clear();
                chart4.Series["CH4"].Points.Clear();
                chart5.Series["CH5"].Points.Clear();
                chart6.Series["CH6"].Points.Clear();
                chart7.Series["CH7"].Points.Clear();
                chart8.Series["CH8"].Points.Clear();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //split data receive from serialport
                string[] arrCH = serialPort1.ReadLine().Split(',');

                datoCH1 = arrCH[0];
                datoCH2 = arrCH[1];
                datoCH3 = arrCH[2];
                datoCH4 = arrCH[3];
                datoCH5 = arrCH[4];
                datoCH6 = arrCH[5];
                datoCH7 = arrCH[6];
                datoCH8 = arrCH[7];

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                canalesselect = Convert.ToInt32(comboBoxChls.Text);

                switch (canalesselect)
                {
                    case 1:
                        valueCH1 = xrange * Convert.ToInt32(datoCH1);

                        chart1.Invoke((MethodInvoker)(() => chart1.Series["CH1"].Points.AddY(valueCH1)));
                        if (chart1.Series[0].Points.Count > span)
                        {
                            chart1.Series[0].Points.RemoveAt(0);
                        }
                        chart1.ChartAreas[0].AxisY.Maximum = ymax;
                        break;

                    case 2:
                        valueCH1 = xrange * Convert.ToInt32(datoCH1);
                        valueCH2 = xrange * Convert.ToInt64(datoCH2);

                        chart1.Invoke((MethodInvoker)(() => chart1.Series["CH1"].Points.AddY(valueCH1)));
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["CH2"].Points.AddY(valueCH2)));

                        if (chart1.Series[0].Points.Count > span)
                        {
                            chart1.Series[0].Points.RemoveAt(0);
                        }
                        chart1.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart2.Series[0].Points.Count > span)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                        }
                        chart2.ChartAreas[0].AxisY.Maximum = ymax;
                        break;

                    case 3:
                        valueCH1 = xrange * Convert.ToInt32(datoCH1);
                        valueCH2 = xrange * Convert.ToInt64(datoCH2);
                        valueCH3 = xrange * Convert.ToInt64(datoCH3);

                        chart1.Invoke((MethodInvoker)(() => chart1.Series["CH1"].Points.AddY(valueCH1)));
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["CH2"].Points.AddY(valueCH2)));
                        chart3.Invoke((MethodInvoker)(() => chart3.Series["CH3"].Points.AddY(valueCH3)));

                        if (chart1.Series[0].Points.Count > span)
                        {
                            chart1.Series[0].Points.RemoveAt(0);
                        }
                        chart1.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart2.Series[0].Points.Count > span)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                        }
                        chart2.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart3.Series[0].Points.Count > span)
                        {
                            chart3.Series[0].Points.RemoveAt(0);
                        }
                        chart3.ChartAreas[0].AxisY.Maximum = ymax;
                        break;

                    case 4:
                        valueCH1 = xrange * Convert.ToInt32(datoCH1);
                        valueCH2 = xrange * Convert.ToInt64(datoCH2);
                        valueCH3 = xrange * Convert.ToInt64(datoCH3);
                        valueCH4 = xrange * Convert.ToInt64(datoCH4);

                        chart1.Invoke((MethodInvoker)(() => chart1.Series["CH1"].Points.AddY(valueCH1)));
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["CH2"].Points.AddY(valueCH2)));
                        chart3.Invoke((MethodInvoker)(() => chart3.Series["CH3"].Points.AddY(valueCH3)));
                        chart4.Invoke((MethodInvoker)(() => chart4.Series["CH4"].Points.AddY(valueCH4)));

                        if (chart1.Series[0].Points.Count > span)
                        {
                            chart1.Series[0].Points.RemoveAt(0);
                        }
                        chart1.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart2.Series[0].Points.Count > span)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                        }
                        chart2.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart3.Series[0].Points.Count > span)
                        {
                            chart3.Series[0].Points.RemoveAt(0);
                        }
                        chart3.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart4.Series[0].Points.Count > span)
                        {
                            chart4.Series[0].Points.RemoveAt(0);
                        }
                        chart4.ChartAreas[0].AxisY.Maximum = ymax;
                        break;

                    case 5:
                        valueCH1 = xrange * Convert.ToInt32(datoCH1);
                        valueCH2 = xrange * Convert.ToInt64(datoCH2);
                        valueCH3 = xrange * Convert.ToInt64(datoCH3);
                        valueCH4 = xrange * Convert.ToInt64(datoCH4);
                        valueCH5 = xrange * Convert.ToInt64(datoCH5);

                        chart1.Invoke((MethodInvoker)(() => chart1.Series["CH1"].Points.AddY(valueCH1)));
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["CH2"].Points.AddY(valueCH2)));
                        chart3.Invoke((MethodInvoker)(() => chart3.Series["CH3"].Points.AddY(valueCH3)));
                        chart4.Invoke((MethodInvoker)(() => chart4.Series["CH4"].Points.AddY(valueCH4)));
                        chart5.Invoke((MethodInvoker)(() => chart5.Series["CH5"].Points.AddY(valueCH5)));

                        if (chart1.Series[0].Points.Count > span)
                        {
                            chart1.Series[0].Points.RemoveAt(0);
                        }
                        chart1.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart2.Series[0].Points.Count > span)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                        }
                        chart2.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart3.Series[0].Points.Count > span)
                        {
                            chart3.Series[0].Points.RemoveAt(0);
                        }
                        chart3.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart4.Series[0].Points.Count > span)
                        {
                            chart4.Series[0].Points.RemoveAt(0);
                        }
                        chart4.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart5.Series[0].Points.Count > span)
                        {
                            chart5.Series[0].Points.RemoveAt(0);
                        }
                        chart5.ChartAreas[0].AxisY.Maximum = ymax;
                        break;

                    case 6:
                        valueCH1 = xrange * Convert.ToInt32(datoCH1);
                        valueCH2 = xrange * Convert.ToInt64(datoCH2);
                        valueCH3 = xrange * Convert.ToInt64(datoCH3);
                        valueCH4 = xrange * Convert.ToInt64(datoCH4);
                        valueCH5 = xrange * Convert.ToInt64(datoCH5);
                        valueCH6 = xrange * Convert.ToInt64(datoCH6);

                        chart1.Invoke((MethodInvoker)(() => chart1.Series["CH1"].Points.AddY(valueCH1)));
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["CH2"].Points.AddY(valueCH2)));
                        chart3.Invoke((MethodInvoker)(() => chart3.Series["CH3"].Points.AddY(valueCH3)));
                        chart4.Invoke((MethodInvoker)(() => chart4.Series["CH4"].Points.AddY(valueCH4)));
                        chart5.Invoke((MethodInvoker)(() => chart5.Series["CH5"].Points.AddY(valueCH5)));
                        chart6.Invoke((MethodInvoker)(() => chart6.Series["CH6"].Points.AddY(valueCH6)));

                        if (chart1.Series[0].Points.Count > span)
                        {
                            chart1.Series[0].Points.RemoveAt(0);
                        }
                        chart1.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart2.Series[0].Points.Count > span)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                        }
                        chart2.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart3.Series[0].Points.Count > span)
                        {
                            chart3.Series[0].Points.RemoveAt(0);
                        }
                        chart3.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart4.Series[0].Points.Count > span)
                        {
                            chart4.Series[0].Points.RemoveAt(0);
                        }
                        chart4.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart5.Series[0].Points.Count > span)
                        {
                            chart5.Series[0].Points.RemoveAt(0);
                        }
                        chart5.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart6.Series[0].Points.Count > span)
                        {
                            chart6.Series[0].Points.RemoveAt(0);
                        }
                        chart6.ChartAreas[0].AxisY.Maximum = ymax;
                        break;

                    case 7:
                        valueCH1 = xrange * Convert.ToInt32(datoCH1);
                        valueCH2 = xrange * Convert.ToInt64(datoCH2);
                        valueCH3 = xrange * Convert.ToInt64(datoCH3);
                        valueCH4 = xrange * Convert.ToInt64(datoCH4);
                        valueCH5 = xrange * Convert.ToInt64(datoCH5);
                        valueCH6 = xrange * Convert.ToInt64(datoCH6);
                        valueCH7 = xrange * Convert.ToInt64(datoCH7);

                        chart1.Invoke((MethodInvoker)(() => chart1.Series["CH1"].Points.AddY(valueCH1)));
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["CH2"].Points.AddY(valueCH2)));
                        chart3.Invoke((MethodInvoker)(() => chart3.Series["CH3"].Points.AddY(valueCH3)));
                        chart4.Invoke((MethodInvoker)(() => chart4.Series["CH4"].Points.AddY(valueCH4)));
                        chart5.Invoke((MethodInvoker)(() => chart5.Series["CH5"].Points.AddY(valueCH5)));
                        chart6.Invoke((MethodInvoker)(() => chart6.Series["CH6"].Points.AddY(valueCH6)));
                        chart7.Invoke((MethodInvoker)(() => chart7.Series["CH7"].Points.AddY(valueCH7)));

                        if (chart1.Series[0].Points.Count > span)
                        {
                            chart1.Series[0].Points.RemoveAt(0);
                        }
                        chart1.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart2.Series[0].Points.Count > span)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                        }
                        chart2.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart3.Series[0].Points.Count > span)
                        {
                            chart3.Series[0].Points.RemoveAt(0);
                        }
                        chart3.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart4.Series[0].Points.Count > span)
                        {
                            chart4.Series[0].Points.RemoveAt(0);
                        }
                        chart4.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart5.Series[0].Points.Count > span)
                        {
                            chart5.Series[0].Points.RemoveAt(0);
                        }
                        chart5.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart6.Series[0].Points.Count > span)
                        {
                            chart6.Series[0].Points.RemoveAt(0);
                        }
                        chart6.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart7.Series[0].Points.Count > span)
                        {
                            chart7.Series[0].Points.RemoveAt(0);
                        }
                        chart7.ChartAreas[0].AxisY.Maximum = ymax;
                        break;

                    case 8:
                        valueCH1 = xrange * Convert.ToInt32(datoCH1);
                        valueCH2 = xrange * Convert.ToInt64(datoCH2);
                        valueCH3 = xrange * Convert.ToInt64(datoCH3);
                        valueCH4 = xrange * Convert.ToInt64(datoCH4);
                        valueCH5 = xrange * Convert.ToInt64(datoCH5);
                        valueCH6 = xrange * Convert.ToInt64(datoCH6);
                        valueCH7 = xrange * Convert.ToInt64(datoCH7);
                        valueCH8 = xrange * Convert.ToInt64(datoCH8);

                        chart1.Invoke((MethodInvoker)(() => chart1.Series["CH1"].Points.AddY(valueCH1)));
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["CH2"].Points.AddY(valueCH2)));
                        chart3.Invoke((MethodInvoker)(() => chart3.Series["CH3"].Points.AddY(valueCH3)));
                        chart4.Invoke((MethodInvoker)(() => chart4.Series["CH4"].Points.AddY(valueCH4)));
                        chart5.Invoke((MethodInvoker)(() => chart5.Series["CH5"].Points.AddY(valueCH5)));
                        chart6.Invoke((MethodInvoker)(() => chart6.Series["CH6"].Points.AddY(valueCH6)));
                        chart7.Invoke((MethodInvoker)(() => chart7.Series["CH7"].Points.AddY(valueCH7)));
                        chart8.Invoke((MethodInvoker)(() => chart8.Series["CH8"].Points.AddY(valueCH8)));

                        if (chart1.Series[0].Points.Count > span)
                        {
                            chart1.Series[0].Points.RemoveAt(0);
                        }
                        chart1.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart2.Series[0].Points.Count > span)
                        {
                            chart2.Series[0].Points.RemoveAt(0);
                        }
                        chart2.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart3.Series[0].Points.Count > span)
                        {
                            chart3.Series[0].Points.RemoveAt(0);
                        }
                        chart3.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart4.Series[0].Points.Count > span)
                        {
                            chart4.Series[0].Points.RemoveAt(0);
                        }
                        chart4.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart5.Series[0].Points.Count > span)
                        {
                            chart5.Series[0].Points.RemoveAt(0);
                        }
                        chart5.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart6.Series[0].Points.Count > span)
                        {
                            chart6.Series[0].Points.RemoveAt(0);
                        }
                        chart6.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart7.Series[0].Points.Count > span)
                        {
                            chart7.Series[0].Points.RemoveAt(0);
                        }
                        chart7.ChartAreas[0].AxisY.Maximum = ymax;

                        if (chart8.Series[0].Points.Count > span)
                        {
                            chart8.Series[0].Points.RemoveAt(0);
                        }
                        chart8.ChartAreas[0].AxisY.Maximum = ymax;
                        break;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                string rangebox = Convert.ToString(comboBoxRange.Text);

                if (rangebox == "+-10")
                {
                    rangeselect = 1;
                }

                else if (rangebox == "+-5")
                {
                    rangeselect = 2;
                }

                else if (rangebox == "0-2.5")
                {
                    rangeselect = 3;
                }

                switch (rangeselect)
                {
                    case 1:
                        xrange = 0.00030517578;
                        ymax = 11;
                        serialPort1.WriteLine("$R1$start");
                        timer1.Start();
                        buttonStart.Enabled = false;
                        buttonStop.Enabled = true;
                        break;
                    case 2:
                        xrange = (0.00015258789);
                        ymax = 6;
                        serialPort1.WriteLine("$R2$start");
                        timer1.Start();
                        buttonStart.Enabled = false;
                        buttonStop.Enabled = true;
                        break;
                    case 3:
                        xrange = (0.00007629395);
                        ymax = 3;

                        if (checkBox1.Checked == true)
                        {
                            serialPort1.WriteLine("$R3$start");
                            timer1.Start();
                            buttonStart.Enabled = false;
                            buttonStop.Enabled = true;
                        }
                        else if (rangeselect == 3 && checkBox1.Checked != true)
                        {
                            MessageBox.Show("Confirmar cambio de Bornera");
                        }
                        break;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                serialPort1.WriteLine("$stop");
                buttonStart.Enabled = true;
                checkBox1.CheckState = CheckState.Unchecked;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

    }
}