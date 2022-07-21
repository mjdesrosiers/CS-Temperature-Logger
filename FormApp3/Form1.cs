using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments;
using NationalInstruments.DAQmx;
using NWaves.Signals;
using NWaves.Filters;
using Task = NationalInstruments.DAQmx.Task;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FormApp3
{
    public partial class Form1 : Form
    {
        AnalogSingleChannelReader reader;
        AIChannel temperature;
        bool connectedStatus = false;
        long thermocoupleID = 31500308;
        SqlConnection connection;
        String connectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        List<DateTime> x = new List<DateTime>();
        List<float> y = new List<float>();
        int selectedIdx = -1;

        private void attachToDevice()
        {
            String[] devices = DaqSystem.Local.Devices;
            if (devices.Length == 0) return;
            string thermocoupleDeviceId = null;
            for (int i = 0; i < devices.Length; i++)
            {
                Device thisDevice = DaqSystem.Local.LoadDevice(devices[i]);
                string dev_id = thisDevice.DeviceID;
                long serial_number = thisDevice.SerialNumber;
                if (serial_number == thermocoupleID)
                {
                    thermocoupleDeviceId = dev_id;
                    break;
                }
            }
            if (thermocoupleDeviceId == null) return;
            Task tempTask = new Task();

            temperature = tempTask.AIChannels.CreateThermocoupleChannel(
                    "Dev1/ai0",
                    "Temperature",
                    0,
                    100,
                    AIThermocoupleType.J,
                    AITemperatureUnits.DegreesF
                );
            tempTask.Stream.Timeout = 1000;

            reader = new AnalogSingleChannelReader(tempTask.Stream);
            
            connectedStatus = true;
        }

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            connection.Open();
            String query = "SELECT * from TemperatureLog";
            SqlCommand command = new SqlCommand(query);
            command.Connection = connection;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int x_ = Convert.ToInt32(reader["SampleTime"]);
                    double t = Convert.ToDouble(reader["Temperature"]); ;
                    if (x_ > 1000) {
                        DateTime dt0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        x.Add(dt0.AddSeconds(x_).ToLocalTime());
                        y.Add(((float)t));
                    }
                }
            }
        }

        int i = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateConnctionMessage();
            if (!connectedStatus)
            {
                attachToDevice();
                return;
            }
            try {
                float sample = (float) reader.ReadSingleSample();
                DateTime now = System.DateTime.Now;
                x.Add(now);
                y.Add(sample);

                 // send new value to database
                String query = "INSERT INTO TemperatureLog (SampleTime,Temperature) VALUES (@SampleTime,@Temperature)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SampleTime", ((DateTimeOffset)now).ToUnixTimeSeconds());
                command.Parameters.AddWithValue("@Temperature", sample);
                int affected = command.ExecuteNonQuery();


                StripChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                StripChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;



                IEnumerable<DateTime> selected_x;
                IEnumerable<float> selected_y;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                if (selectedIdx < 0)
                {
                    selected_x = x; // x.ToArray();
                    selected_y = y; //y.ToArray();                    
                } else
                {
                    Console.WriteLine("-3) Took " + stopWatch.ElapsedMilliseconds.ToString() + " ms");
                    var selected_date = dates.ElementAt(selectedIdx);
                    Console.WriteLine("-2) Took " + stopWatch.ElapsedMilliseconds.ToString() + " ms");
                    var indices = (from dt in x
                                  where dt.Date == selected_date
                                  select x.IndexOf(dt)).AsQueryable();
                    var dateTemps = x.Zip(y, (first, second) => (first, second));
                    var dateTempsByDate = dateTemps.ToLookup(x => x.first.Date, x => x);
                    var thisDayDateTemps = dateTempsByDate[selected_date];
                    Console.WriteLine("-1) Took " + stopWatch.ElapsedMilliseconds.ToString() + " ms");

                    var plot_x = from pair in thisDayDateTemps
                                 select pair.first;
                    var plot_y = from pair in thisDayDateTemps
                                 select pair.second;
                    
                    Console.WriteLine("3) Took " + stopWatch.ElapsedMilliseconds.ToString() + " ms");

                    selected_x = plot_x.ToList(); //x.ToArray();
                    selected_y = plot_y.ToList(); //y.ToArray();
                    Console.WriteLine("4) Took " + stopWatch.ElapsedMilliseconds.ToString() + " ms");
                    
                    

                }
                List<DateTime> dtttt = selected_x.ToArray().ToList();
                int count = 1;
                Console.WriteLine("Number of points: " + count.ToString()); //  Length.ToString());
                Console.WriteLine("5) Took " + stopWatch.ElapsedMilliseconds.ToString() + " ms");
                if (count > 10000)
                {
                    //int modulus = selected_x.Count() / 10000;
                    //selected_x = selected_x.Where((x, i) => i % modulus == 0).ToArray();
                    //selected_y = selected_y.Where((x, i) => i % modulus == 0).ToArray();
                }
                Console.WriteLine("6) Took " + stopWatch.ElapsedMilliseconds.ToString() + " ms");
                StripChart.Series[0].Points.DataBindXY(selected_x, selected_y);
                Console.WriteLine("7) Took " + stopWatch.ElapsedMilliseconds.ToString() + " ms\n");
                stopWatch.Stop();
                var available_dates = from samp in selected_x
                            select samp.Date;
                available_dates = available_dates.Distinct();
                if (available_dates.Count() > 1)
                {
                    StripChart.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
                    StripChart.Series[1].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
                }
                else
                {
                    StripChart.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
                    StripChart.Series[1].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
                }

                StripChart.ChartAreas[0].AxisY.Minimum = Math.Round(selected_y.Min() - 1);
                StripChart.ChartAreas[0].AxisY.Maximum = Math.Round(selected_y.AsQueryable().Max() + 1);

                try
                {
                    DiscreteSignal ds = new DiscreteSignal(1, y.ToArray());
                    SavitzkyGolayFilter sgf = new SavitzkyGolayFilter(15, 0);
                    ds = sgf.ApplyTo(ds);
                    // StripChart.Series[1].Points.DataBindXY(x, ds.Samples);
                }
                catch { }
                
                i++;
            } catch
            {
                Console.WriteLine("Disconnected! 😢");
                connectedStatus = false;
            }
            
        }

        private void updateConnctionMessage()
        {
            if (connectedStatus)
            {
                label_ConnectionDisplay.Text = "Connected";
                label_ConnectionDisplay.ForeColor = Color.Green;
            } else
            {
                label_ConnectionDisplay.Text = "Disconnected";
                label_ConnectionDisplay.ForeColor = Color.Red;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int BUFFER_TOP = 50;
            int BUFFER_BOT = 100;
            int BUFFER_LEFT = 50;
            int BUFFER_RIGHT = 50;
            Size newSize = Form1.ActiveForm.Size;
            Console.Write("New form size is: ");  Console.WriteLine(newSize.ToString());
            newSize.Width = newSize.Width - BUFFER_LEFT - BUFFER_RIGHT;
            newSize.Height = newSize.Height - BUFFER_TOP - BUFFER_BOT;
            StripChart.Size = newSize;
            Console.Write("New plot size is: "); Console.WriteLine(newSize.ToString());
            // StripChart.Location = new Point(BUFFER_TOP, BUFFER_LEFT);
            StripChart.Left = BUFFER_LEFT;
            StripChart.Top = BUFFER_TOP;
        }

        private void StripChart_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        List<DateTime> dates;
        private void timer2_Tick(object sender, EventArgs e)
        {
            var t_dates = from samp in x
                          select samp.Date.ToString("dd/MM/yy");
            
            t_dates = t_dates.Distinct();
            t_dates = t_dates.OrderBy(name => name);

            t_dates = t_dates.Prepend("All");
            if ((dates != null) && (t_dates.Count()-1 == dates.Count()))
            {
                return;
            } else
            {
                Console.WriteLine(t_dates.Count().ToString() + " / " + (dates == null ? 0 : dates.Count()));
            }
            
            
            System.Object[] io = new System.Object[t_dates.Count()];
            for (int i = 0; i < t_dates.Count(); i++)
            {
                io[i] = t_dates.ElementAt(i);
            }
            comboBox_Dates.Items.Clear();
            comboBox_Dates.Items.AddRange(io);
            comboBox_Dates.SelectedIndex = 0;

            var n_dates = from samp in x
                          select samp.Date;
            dates = n_dates.Distinct().ToList();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (connection.State != ConnectionState.Closed) {
                connection.Close();
            }
            
        }

        private void comboBox_Dates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dates != null)
            {
                selectedIdx = comboBox_Dates.SelectedIndex - 1;                
            }
        }
    }
}
