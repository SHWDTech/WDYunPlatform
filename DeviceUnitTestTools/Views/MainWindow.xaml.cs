using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Controls;
using DeviceUnitTestTools.Models;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;

namespace DeviceUnitTestTools.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Timer _refreashDataTimer = new Timer();

        public MainWindow()
        {
            InitializeComponent();
            InitApplication();
        }

        private void InitApplication()
        {
            try
            {
                var devices = ProcessInvoke.GetInstance<DeviceProcess>().GetAllDevices();
                foreach (var device in devices)
                {
                    CmbDeviceSelect.Items.Add(new { Key = device.Id, Value = device.DeviceNodeId });
                }
                if (CmbDeviceSelect.Items.Count > 0)
                {
                    CmbDeviceSelect.SelectedIndex = 0;
                }

               // GetMonitorDatas(null, null);

                _refreashDataTimer.Enabled = true;
                _refreashDataTimer.Interval = 20000;
                _refreashDataTimer.Elapsed += GetMonitorDatas;
                _refreashDataTimer.Start();
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("初始化失败", ex);
            }
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var displayName = Globals.GetPropertyDisplayName(e.PropertyDescriptor);

            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }

        }

        private void GetMonitorDatas(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var monitorDatas =
                    ProcessInvoke.GetInstance<MonitorDataProcess>()
                        .GetTestDeviceMonitorDatas(Guid.Parse(CmbDeviceSelect.SelectedValue.ToString()));
                var dataGroups = monitorDatas.GroupBy(obj => obj.ProtocolDataId);
                var dataList = new List<UnitTestMonitorData>();
                foreach (var dataGroup in dataGroups)
                {
                    var record = GetMonitorDate(dataGroup);
                    dataList.Add(record);
                }

                MonitorDataGrid.ItemsSource = dataList;
            });
        }

        private UnitTestMonitorData GetMonitorDate(IGrouping<Guid, MonitorData> dataGroup)
        {
            var data = new UnitTestMonitorData();
            foreach (var monitorData in dataGroup)
            {
                if (monitorData.DoubleValue == null) continue;
                switch (monitorData.DataName)
                {
                    case ProtocolDataName.Cpm:
                        data.Tp = monitorData.DoubleValue.Value;
                        break;
                    case ProtocolDataName.Noise:
                        data.Db = monitorData.DoubleValue.Value;
                        break;
                    case ProtocolDataName.Temperature:
                        data.Temperature = monitorData.DoubleValue.Value;
                        break;
                    case ProtocolDataName.Humidity:
                        data.Humidity = monitorData.DoubleValue.Value;
                        break;
                    case ProtocolDataName.WindSpeed:
                        data.WindSpeed = monitorData.DoubleValue.Value;
                        break;
                    case ProtocolDataName.WindDirection:
                        data.WindDirection = Convert.ToInt32(monitorData.DoubleValue.Value);
                        break;
                }
            }

            data.UpdateTime = dataGroup.First().UpdateTime;

            return data;
        }
    }
}
