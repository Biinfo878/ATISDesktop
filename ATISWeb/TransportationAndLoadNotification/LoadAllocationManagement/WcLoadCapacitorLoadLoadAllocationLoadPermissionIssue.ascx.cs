﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ATISWeb.LoginManagement;
using ATISWeb.LoginManagement.Exceptions;
using ATISWeb.TransportationAndLoadNotification.LoadCapacitorManagement;
using PayanehClassLibrary.LoadNotification.LoadPermission;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.TransportCompanies;
using R2CoreTransportationAndLoadNotification.LoadPermission.LoadPermissionPrinting;
using ATISWeb.TransportationAndLoadNotification.LoadPermissionManagement.LoadPermissionPrinting;

namespace ATISWeb.TransportationAndLoadNotification.LoadAllocationManagement
{
    public partial class WcLoadCapacitorLoadLoadAllocationLoadPermissionIssue : System.Web.UI.UserControl
    {

        #region "General Properties"

        #endregion

        #region "Subroutins And Functions"
        #endregion

        #region "Events"
        #endregion

        #region "Event Handlers"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                WcLoadCapacitorLoadsCollectionSummaryIntelligently.WcCurrentListType = LoadCapacitorLoadsListType.Sedimented;
                WcLoadCapacitorLoadsCollectionSummaryIntelligently.WcLoadCapacitorLoadSelectedEvent += WcLoadCapacitorLoadsCollectionSummaryIntelligently_WcLoadCapacitorLoadSelectedEvent;
                if (!IsPostBack) { WcLoadCapacitorLoadsCollectionSummaryIntelligently.WcViewInformation(); }
                BtnLoadAllocationLoadPermissionIssue.Click += BtnLoadAllocationLoadPermissionIssue_Click;
                BtnPrint.Click += BtnPrint_Click;
            }
            catch (Exception ex)
            { Page.ClientScript.RegisterStartupScript(GetType(), "WcViewAlert", "WcViewAlert('1','" + MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message + "');", true); }
        }

        private void WcLoadCapacitorLoadsCollectionSummaryIntelligently_WcLoadCapacitorLoadSelectedEvent(object sender, WcLoadCapacitorLoadsCollectionSummaryIntelligently.nEstelamIdEventArgs e)
        {
            try
            { WcViewerNSSLoadCapacitorLoad.WcViewNSS(e.nEstelamId); }
            catch (Exception ex)
            { Page.ClientScript.RegisterStartupScript(GetType(), "WcViewAlert", "WcViewAlert('1','" + MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message + "');", true); }
        }

        public PermissionPrintingDataStructure PPDS = new PermissionPrintingDataStructure();
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#AlertShower').modal('show');", true);
            }
            catch (Exception ex)
            { Page.ClientScript.RegisterStartupScript(GetType(), "WcViewAlert", "WcViewAlert('1','" + MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message + "');", true); }
        }

        private void BtnLoadAllocationLoadPermissionIssue_Click(object sender, EventArgs e)
        {
            try
            { LoadNotificationLoadPermissionManagement.CarTruckRelationDriverTruck(WcSmartCardsInquiry.WcGetTruckSmartCardNo(), WcSmartCardsInquiry.WcGetTruckDriverSmartCardNo(), ATISWebMClassLoginManagement.GetNSSCurrentUser()); }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "WcViewAlert", "WcViewAlert('1','" + MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message + "');", true);
                return;
            }

            try
            {
                BtnLoadAllocationLoadPermissionIssue.Enabled = false;
                Int64 myCompanyCode = R2CoreTransportationAndLoadNotificationMClassTransportCompaniesManagement.GetNSSTransportCompnay(ATISWebMClassLoginManagement.GetNSSCurrentUser()).TCId;
                Int64 mynEstelamId = WcViewerNSSLoadCapacitorLoad.WcGetNSSCurrent.nEstelamId;
                LoadNotificationLoadPermissionManagement.CarTruckRelationDriverTruck(WcSmartCardsInquiry.WcGetTruckSmartCardNo(), WcSmartCardsInquiry.WcGetTruckDriverSmartCardNo(), ATISWebMClassLoginManagement.GetNSSCurrentUser());
                Int64 myTurnId = LoadNotificationLoadPermissionManagement.TransportCompanyLoadCapacitorSedimentLoadAllocationAndPermisiion(myCompanyCode, mynEstelamId, WcSmartCardsInquiry.WcGetTruckSmartCardNo(), WcSmartCardsInquiry.WcGetTruckDriverSmartCardNo(), ATISWebMClassLoginManagement.GetNSSCurrentUser());
                PermissionPrinting.GetInformationforRemotePermissionPrinting(mynEstelamId, myTurnId, ref PPDS.StrExitDate, ref PPDS.StrExitTime, ref PPDS.nEstelamId, ref PPDS.TurnId, ref PPDS.CompanyName, ref PPDS.CarTruckLoaderTypeName, ref PPDS.pelak, ref PPDS.Serial, ref PPDS.DriverTruckFullNameFamily, ref PPDS.DriverTruckDrivingLicenseNo, ref PPDS.ProductName, ref PPDS.TargetCityName, ref PPDS.StrPriceSug, ref PPDS.StrDescription, ref PPDS.PermissionUserName, ref PPDS.OtherNote,ref PPDS.LAId);
                PPDS.TurnId = myTurnId.ToString();
                PPDS.nEstelamId = mynEstelamId.ToString();
            }
            catch (PleaseReloginException ex)
            { Response.Redirect("/LoginManagement/Wflogin.aspx"); }
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