﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using R2CoreTransportationAndLoadNotification.LoadPermission;
using R2CoreTransportationAndLoadNotification.LoadCapacitor;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.LoadAllocation;
using PayanehClassLibrary.LoadNotification.LoadPermission;
using static ATISWeb.TransportationAndLoadNotification.LoadAllocationManagement.WcLoadCapacitorLoadLoadAllocationLoadPermissionIssue;
using ATISWeb.TransportationAndLoadNotification.LoadPermissionManagement.LoadPermissionPrinting;

namespace ATISWeb.TransportationAndLoadNotification.LoadAllocationManagement
{
    public partial class WcLoadCapacitorLoadLoadPermissionsIssued : System.Web.UI.UserControl
    {
        #region "General Properties"

        private LoadCapacitorLoadsListType _WcCurrentListTypeIssued = LoadCapacitorLoadsListType.None;
        public LoadCapacitorLoadsListType WcCurrentListTypeIssued
        {
            get { return _WcCurrentListTypeIssued; }
            set
            { _WcCurrentListTypeIssued = value; WcLoadCapacitorLoadsCollectionSummaryIntelligently.WcCurrentListType = value; }
        }


        #endregion

        #region "Subroutins And Functions"

        public void WcViewInformation(Int64 YournEstelamId)
        {
            try
            {
                var Lst = R2CoreTransportationAndLoadNotificationMClassLoadPermissionManagement.GetLoadPermissionsIssued(YournEstelamId);
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("LoadAllocationId", typeof(string)));
                dt.Columns.Add(new DataColumn("StrDescription", typeof(string)));
                dt.Columns.Add(new DataColumn("IssuedLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("strAddress", typeof(string)));
                dt.Columns.Add(new DataColumn("Mobile", typeof(string)));
                dt.Columns.Add(new DataColumn("StrExitDateTime", typeof(string)));
                dt.Columns.Add(new DataColumn("StrSmartCardNo", typeof(string)));
                dt.Columns.Add(new DataColumn("StrNationalCode", typeof(string)));
                dt.Columns.Add(new DataColumn("StrDrivingLicenceNo", typeof(string)));
                dt.Columns.Add(new DataColumn("TruckDriver", typeof(string)));
                dt.Columns.Add(new DataColumn("TruckSmartCardNo", typeof(string)));
                dt.Columns.Add(new DataColumn("Truck", typeof(string)));

                for (int i = 0; i <= Lst.Count - 1; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["LoadAllocationId"] = R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.GetNSSLoadAllocation(Lst[i].nEstelamId, Lst[i].TurnId).LAId.ToString();
                    dr["StrDescription"] = Lst[i].StrDescription.Trim();
                    dr["IssuedLocation"] = Lst[i].IssuedLocation.Trim();
                    dr["strAddress"] = Lst[i].strAddress.Trim();
                    dr["Mobile"] = Lst[i].Mobile.Trim();
                    dr["StrExitDateTime"] = Lst[i].LoadPermissionDate.Trim() + "-" + Lst[i].LoadPermissionTime.Trim();
                    dr["StrSmartcardNo"] = Lst[i].StrSmartCardNo.Trim();
                    dr["StrNationalCode"] = Lst[i].StrNationalCode.Trim();
                    dr["StrDrivingLicenceNo"] = Lst[i].StrDrivingLicenceNo.Trim();
                    dr["TruckDriver"] = Lst[i].TruckDriver.Trim();
                    dr["Truck"] = Lst[i].Truck.Trim();
                    dr["TruckSmartCardNo"] = Lst[i].TruckSmartCardNo.Trim();
                    dt.Rows.Add(dr);

                }

                GridViewLoadCapacitorLoadLoadPermissionsIssued.DataSource = dt;
                GridViewLoadCapacitorLoadLoadPermissionsIssued.DataBind();
            }
            catch (Exception ex)
            { Page.ClientScript.RegisterStartupScript(GetType(), "WcViewAlert", "WcViewAlert('1','" + MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message + "');", true); }
        }

        #endregion

        #region "Events"
        #endregion

        #region "Event Handlers"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack) { WcLoadCapacitorLoadsCollectionSummaryIntelligently.WcViewInformation(); }
                WcLoadCapacitorLoadsCollectionSummaryIntelligently.WcLoadCapacitorLoadSelectedEvent += WcLoadCapacitorLoadsCollectionSummaryIntelligently_WcLoadCapacitorLoadSelectedEvent;
                GridViewLoadCapacitorLoadLoadPermissionsIssued.SelectedIndexChanged += GridViewLoadCapacitorLoadLoadPermissionsIssued_SelectedIndexChanged;
            }
            catch (Exception ex)
            { Page.ClientScript.RegisterStartupScript(GetType(), "WcViewAlert", "WcViewAlert('1','" + MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message + "');", true); }
        }

        public PermissionPrintingDataStructure PPDS = new PermissionPrintingDataStructure();
        private void GridViewLoadCapacitorLoadLoadPermissionsIssued_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int64 LAId = Int64.Parse(GridViewLoadCapacitorLoadLoadPermissionsIssued.SelectedRow.Cells[0].Text);
                var NSS = R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.GetNSSLoadAllocation(LAId);
                PermissionPrinting.GetInformationforRemotePermissionPrinting(NSS.nEstelamId, NSS.TurnId, ref PPDS.StrExitDate, ref PPDS.StrExitTime, ref PPDS.nEstelamId, ref PPDS.TurnId, ref PPDS.CompanyName, ref PPDS.CarTruckLoaderTypeName, ref PPDS.pelak, ref PPDS.Serial, ref PPDS.DriverTruckFullNameFamily, ref PPDS.DriverTruckDrivingLicenseNo, ref PPDS.ProductName, ref PPDS.TargetCityName, ref PPDS.StrPriceSug, ref PPDS.StrDescription, ref PPDS.PermissionUserName, ref PPDS.OtherNote, ref PPDS.LAId);
                PPDS.TurnId = NSS.TurnId.ToString();
                PPDS.nEstelamId = NSS.nEstelamId.ToString();
            }
            catch (Exception ex)
            { Page.ClientScript.RegisterStartupScript(GetType(), "WcViewAlert", "WcViewAlert('1','" + MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message + "');", true); }
        }

        private void WcLoadCapacitorLoadsCollectionSummaryIntelligently_WcLoadCapacitorLoadSelectedEvent(object sender, LoadCapacitorManagement.WcLoadCapacitorLoadsCollectionSummaryIntelligently.nEstelamIdEventArgs e)
        {
            try
            { WcViewInformation(e.nEstelamId); }
            catch (Exception ex)
            { Page.ClientScript.RegisterStartupScript(GetType(), "WcViewAlert", "WcViewAlert('1','" + MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message + "');", true); }
        }







        #endregion

        #region "Override Methods"
        #endregion

        #region "Abstract Methods"
        #endregion

        #region "Implemented Members"
        #endregion
    }
}