using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Omgevingsmonitor_configurator.Box;

using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using System.Text.Json;

namespace Omgevingsmonitor_configurator
{

    public partial class AddSenseBoxForm : Form
    {
        //private TextBox nameTextBox, latitudeTextBox, longitudeTextBox;
        //private ComboBox exposureComboBox;

        private List<OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor> sensors = new List<OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor>();

        public AddSenseBoxForm()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {

            GMaps.Instance.Mode = AccessMode.ServerOnly;
            locatieControl.MapProvider = GMapProviders.OpenStreetMap;
            locatieControl.Position = new PointLatLng(0, 0);//(52.2167510750697, 6.84519588947296);
            locatieControl.MinZoom = 2;
            locatieControl.MaxZoom = 18;
            locatieControl.Zoom = 2;
            locatieControl.CanDragMap = true;

            exposureBox.SelectedIndex = 0;

            PointLatLng center = locatieControl.Position;
            latitudeBox.Text = center.Lat.ToString();
            longitudeBox.Text = center.Lng.ToString();

            sensorGridView.Rows.Add(true, "Temperature");
            sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            {
                title = "Temperature",
                unit = "°C",
                sensorType = "Temperature",
                icon = SensorIcon.temperatureC
            };
            
            sensorGridView.Rows.Add(true, "Humidity");
            sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            {
                title = "Humidity",
                unit = "%",
                sensorType = "Humidity",
                icon = SensorIcon.humidity
            };
            
            //sensorGridView.Rows.Add(true, "VOCraw");
            //sensorGridView.Rows[2].ReadOnly = true;
            //sensorGridView.Rows[2].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            //{
            //    title = "VOCraw",
            //    unit = "VOCr",
            //    sensorType = "VOC",
            //    icon = SensorIcon.co2
            //};

            
            sensorGridView.Rows.Add(true, "VOCindex");
            sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            {
                title = "VOCindex",
                unit = "VOCi",
                sensorType = "VOC",
                icon = SensorIcon.dashboard
            };
            
            sensorGridView.Rows.Add(true, "Soundpresure dBa");
            sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            {
                title = "Soundpresure dBa",
                unit = "dBa",
                sensorType = "Microphone",
                icon = SensorIcon.microphone
            };
            
            sensorGridView.Rows.Add(true, "Battery voltage");
            sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            {
                title = "Battery voltage",
                unit = "V",
                sensorType = "Adc",
                icon = SensorIcon.battery
            };

            sensorGridView.Rows.Add(true, "Solar voltage");
            sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            {
                title = "Solar voltage",
                unit = "V",
                sensorType = "Adc",
                icon = SensorIcon.brightness
            };

            sensorGridView.Rows.Add(false, "PM2.5");
            sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            {
                title = "PM2.5",
                unit = "ppm",
                sensorType = "PM",
                icon = SensorIcon.cloud
            };

            //sensorGridView.Rows.Add(false, "PM10");
            //sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            //{
            //    title = "PM10",
            //    unit = "ppm",
            //    sensorType = "PM",
            //    icon = SensorIcon.cloud
            //};

            sensorGridView.Rows.Add(false, "NOx");
            sensorGridView.Rows[sensorGridView.Rows.Count - 1].Tag = new OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor
            {
                title = "NOx",
                unit = "NOxi",
                sensorType = "PM",
                icon = SensorIcon.cloud
            };
        }

        private void locatieControl_OnPositionChanged(PointLatLng point)
        {
            PointLatLng center = locatieControl.Position;
            latitudeBox.Text = center.Lat.ToString();
            longitudeBox.Text = center.Lng.ToString();
        }

        private async void submitBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in sensorGridView.Rows)
            {
                if (Convert.ToBoolean(r.Cells[0].Value) == true)
                {
                    sensors.Add((OpenSenseMapApiClient.SenseBoxCreateRequest.Sensor)r.Tag);
                }
            }

            if (ValidateInput())
            {
                var newBox = new OpenSenseMapApiClient.SenseBoxCreateRequest
                {
                    Name = nameBox.Text,
                    Exposure = exposureBox.SelectedItem.ToString().ToLower(),
                    Grouptag = "de Omgevingsmonitor",
                    Location = new OpenSenseMapApiClient.SenseBoxCreateRequest.SetLocation
                    {
                        lng = Convert.ToDouble(longitudeBox.Text),
                        lat = Convert.ToDouble(latitudeBox.Text),
                        height = Convert.ToDouble(heightBox.Text)
                    },
                    Sensors = sensors
                };

                JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                var jsonNewBox = JsonSerializer.Serialize(newBox); // Deserialize<Box>(responseBody);
                try
                {
                    var apiClient = new OpenSenseMapApiClient();
                    var createdBox = await apiClient.PostNewSenseBoxAsync(newBox);
                    MessageBox.Show($"SenseBox created successfully with ID: {createdBox._id}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating SenseBox: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text) ||
                exposureBox.SelectedItem == null ||
                !double.TryParse(latitudeBox.Text, out _) ||
                !double.TryParse(longitudeBox.Text, out _) ||
                !double.TryParse(heightBox.Text, out _) ||
                sensors.Count == 0)
            {
                MessageBox.Show("Please fill in all fields and add at least one sensor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void sensirionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            sen50Select.Enabled = sensirionCheckBox.Checked;
            sen55Select.Enabled = sensirionCheckBox.Checked;
            sensorGridView.Rows[6].Cells[0].Value = sensirionCheckBox.Checked;
            sensorGridView.Rows[7].Cells[0].Value = sen55Select.Checked && sensirionCheckBox.Checked;
        }

        private void sen50Select_CheckedChanged(object sender, EventArgs e)
        {
            sensorGridView.Rows[7].Cells[0].Value = sen55Select.Checked && sensirionCheckBox.Checked;
        }

        private void sen55Select_CheckedChanged(object sender, EventArgs e)
        {
            sensorGridView.Rows[7].Cells[0].Value = sen55Select.Checked && sensirionCheckBox.Checked;
        }

        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void PM10Button_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (sensirionCheckBox.Checked)
        //    {
        //        sensorGridView.Rows[7].Cells[0].Value = PM10Button.Checked;
        //        sensorGridView.Rows[6].Cells[0].Value = PM25Button.Checked;
        //    }

        //}

        //private void PM25Button_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (sensirionCheckBox.Checked)
        //    {
        //        sensorGridView.Rows[7].Cells[0].Value = PM10Button.Checked;
        //        sensorGridView.Rows[6].Cells[0].Value = PM25Button.Checked;
        //    }
        //}
    }

}
