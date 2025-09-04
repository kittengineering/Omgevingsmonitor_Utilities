using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omgevingsmonitor_configurator
{
    public partial class BoxDetailsForm : Form
    {
        private Box _box;
        private TableLayoutPanel _mainLayout;
        private Label _nameLabel, _createdAtLabel, _updatedAtLabel, _exposureLabel, _modelLabel, _lastMeasurementLabel;
        private Label _locationLabel;

        private void BoxDetailsForm_Load(object sender, EventArgs e)
        {

        }

        private ListBox _sensorsListBox;

        public BoxDetailsForm(Box box)
        {
            _box = box;
            InitializeComponent();
            InitializeComponents();
            PopulateData();
        }

        private void InitializeComponents()
        {
            this.Text = "Box Details";
            this.Size = new Size(500, 600);

            _mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 8,
                Padding = new Padding(10)
            };

            _nameLabel = CreateLabel("Name:");
            _createdAtLabel = CreateLabel("Created At:");
            _updatedAtLabel = CreateLabel("Updated At:");
            _exposureLabel = CreateLabel("Exposure:");
            _modelLabel = CreateLabel("Model:");
            _lastMeasurementLabel = CreateLabel("Last Measurement:");
            _locationLabel = CreateLabel("Location:");

            _sensorsListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font(FontFamily.GenericSansSerif, 10)
            };

            _mainLayout.Controls.Add(new Label { Text = "Name:", Dock = DockStyle.Fill }, 0, 0);
            _mainLayout.Controls.Add(_nameLabel, 1, 0);
            _mainLayout.Controls.Add(new Label { Text = "Created At:", Dock = DockStyle.Fill }, 0, 1);
            _mainLayout.Controls.Add(_createdAtLabel, 1, 1);
            _mainLayout.Controls.Add(new Label { Text = "Updated At:", Dock = DockStyle.Fill }, 0, 2);
            _mainLayout.Controls.Add(_updatedAtLabel, 1, 2);
            _mainLayout.Controls.Add(new Label { Text = "Exposure:", Dock = DockStyle.Fill }, 0, 3);
            _mainLayout.Controls.Add(_exposureLabel, 1, 3);
            _mainLayout.Controls.Add(new Label { Text = "Model:", Dock = DockStyle.Fill }, 0, 4);
            _mainLayout.Controls.Add(_modelLabel, 1, 4);
            _mainLayout.Controls.Add(new Label { Text = "Last Measurement:", Dock = DockStyle.Fill }, 0, 5);
            _mainLayout.Controls.Add(_lastMeasurementLabel, 1, 5);
            _mainLayout.Controls.Add(new Label { Text = "Location:", Dock = DockStyle.Fill }, 0, 6);
            _mainLayout.Controls.Add(_locationLabel, 1, 6);
            _mainLayout.Controls.Add(new Label { Text = "Sensors:", Dock = DockStyle.Fill }, 0, 7);
            _mainLayout.Controls.Add(_sensorsListBox, 1, 7);

            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.Controls.Add(_mainLayout);
        }

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font(FontFamily.GenericSansSerif, 10)
            };
        }

        private void PopulateData()
        {
            _nameLabel.Text = _box.Name;
            _createdAtLabel.Text = _box.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
            _updatedAtLabel.Text = _box.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss");
            _exposureLabel.Text = _box.Exposure;
            _modelLabel.Text = _box.Model;
            _lastMeasurementLabel.Text = _box.LastMeasurementAt.ToString("yyyy-MM-dd HH:mm:ss");

            if (_box.currentLocation != null)
            {
                _locationLabel.Text = $"({_box.currentLocation.Coordinates[1]}, {_box.currentLocation.Coordinates[0]}, {_box.currentLocation.Coordinates[2]})";
            }

            foreach (var sensor in _box.Sensors)
            {
                string lastMeasurement = sensor.lastMeasurement != null
                    ? $"{sensor.lastMeasurement.Value} {sensor.unit} at {sensor.lastMeasurement.CreatedAt:yyyy-MM-dd HH:mm:ss}"
                    : "No measurement";

                _sensorsListBox.Items.Add($"{sensor.title}: {lastMeasurement}");
            }
        }
    }
}
