﻿using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.LoadAllocation;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.LoadPermission;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R2PrimaryTestCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class PermissionsIssued
        { public string ReportItem { get; set; } }

        private void Button1_Click(object sender, EventArgs e)
        {
            var InstanceReport = new R2CoreTransportationAndLoadNotificationInstanceLoadPermissionManager();
          var Lst=  InstanceReport.ReportingInformationProviderLoadPermissionsIssuedOrderByPriorityReport(7);
            List<PermissionsIssued> _PermissionsIssued = new List<PermissionsIssued>();
            for (int Loopx = 0; Loopx <= Lst.Count - 1; Loopx++)
            {
                var Item = new PermissionsIssued();
                //Item.ReportItem = Lst[Loopx];
                _PermissionsIssued.Add(Item);
            }

            //var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager();
            //var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationInstanceLoadAllocationManager();

            //var Lst = InstanceLoadAllocation.GetLoadAllocationsforTruckDriver(InstanceSoftwareUsers.GetNSSUser("50299aa592ccef6eaa8b603bc587192e").UserId);


            //var InstanceLoadCapacitorLoad = new  R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager();

            //var Lst = InstanceLoadCapacitorLoad.GetLoadCapacitorLoads(Convert.ToInt64("2"), Convert.ToInt64("7"), 4, false, true, R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TargetProvince, Int64.MinValue, Convert.ToInt64("11"));

            //StringBuilder hash = new StringBuilder();
            //MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            //byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes("0D992C8C-3F8A-428A-8638-25B94D04BEA7" + ":" + DateTime.Now.Day));
            //for (int i = 0; i < bytes.Length; i++)
            //{ hash.Append(bytes[i].ToString("x2")); }
            //var x = hash.ToString();

        }
    }
}
