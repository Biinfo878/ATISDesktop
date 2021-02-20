﻿
Imports System.ComponentModel
Imports System.Reflection

Imports R2Core.DateAndTimeManagement
Imports R2CoreGUI
Imports R2Core.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.AnnouncementHalls
Imports R2CoreTransportationAndLoadNotification.Goods
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation
Imports R2CoreTransportationAndLoadNotification.LoaderTypes
Imports R2CoreTransportationAndLoadNotification.LoadSedimentation
Imports R2CoreTransportationAndLoadNotification.LoadSources
Imports R2CoreTransportationAndLoadNotification.LoadTargets
Imports R2CoreTransportationAndLoadNotification.TransportCompanies

Public Class UCLoadCapacitorLoadManipulation
    Inherits UCLoadCapacitorLoad

    Public Event UCLoadCapacitorLoadRegisteredEvent(NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure)
    Public Event UCLoadCapacitorLoadEditedEvent(NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure)
    Public Event UCLoadCapacitorLoadDeletedEvent(NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure)
    Public Event UCLoadCapacitorLoadCancelledEvent(NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure)
    Public Event UCLoadCapacitorLoadFreeLinedEvent(NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure)
    Public Event UCLoadCapacitorLoadSedimentedEvent(NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure)
    Private _DateTime As New R2DateTime

#Region "General Properties"




    Private _DisableUCSearcherLoadSources As Boolean = True
    <Browsable(False)>
    Public Property DisableUCSearcherLoadSources() As Boolean
        Get
            Return _DisableUCSearcherLoadSources
        End Get
        Set(value As Boolean)
            _DisableUCSearcherLoadSources = value
            UcSearcherLoadSources.Enabled = Not value
        End Set
    End Property

#End Region

#Region "Subroutins And Functions"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            UCRefreshGeneral()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try

    End Sub

    Public Sub UCRefreshGeneral()
        Try
            MyBase.UCRefreshGeneral()
            UCRefresh()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub UCRefresh()
        Try
            UcNumbernEstelamId.UCRefresh()
            UcPersianTextBoxLoadCapacitorLoadDateTimeComposite.UCRefresh()
            UcPersianTextBoxLoadPermissionStatus.UCRefresh()
            UcNumbernCarNum.UCRefresh()
            UcNumbernCarNumKol.UCRefresh()
            UcNumberTransportPrice.UCRefresh()
            UcPersianTextBoxStrBarname.UCRefresh()
            UcPersianTextBoxAddress.UCRefresh()
            UcPersianTextBoxStrDescription.UCRefresh()
            UcSearcherTransportCompanies.UCRefreshGeneral()
            UcSearcherGoods.UCRefreshGeneral()
            UcSearcherLoadTargets.UCRefreshGeneral()
            UcSearcherLoaderTypes.UCRefreshGeneral()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub



#End Region

#Region "Events"
#End Region

#Region "Event Handlers"
    Private Sub UCLoadCapacitorLoadManipulation_UCNSSCurrentChanged(NSSCurrent As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) Handles Me.UCNSSCurrentChanged
    End Sub

    Private Sub UCLoadCapacitorLoadManipulation_UCViewNSSRequested() Handles Me.UCViewNSSRequested
        Try
            UcNumbernEstelamId.UCValue = UCNSSCurrent.nEstelamId
            UcPersianTextBoxLoadCapacitorLoadDateTimeComposite.UCValue = UCNSSCurrent.dTimeElam & " - " & UCNSSCurrent.dDateElam
            UcPersianTextBoxLoadPermissionStatus.UCValue = R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement.GetNSSLoadCapacitorLoadStatus(UCNSSCurrent.LoadStatus).LoadStatusName
            UcNumbernCarNum.UCValue = UCNSSCurrent.nCarNum
            UcSearcherTransportCompanies.UCViewNSS(R2CoreTransportationAndLoadNotificationMClassTransportCompaniesManagement.GetNSSTransportCompany(UCNSSCurrent.nCompCode))
            UcSearcherGoods.UCViewNSS(R2CoreTransportationAndLoadNotificationMClassGoodsManagement.GetNSSGood(UCNSSCurrent.nBarCode))
            UcSearcherLoadTargets.UCViewNSS(R2CoreTransportationAndLoadNotificationMclassLoadTargetsManagement.GetNSSLoadTarget(UCNSSCurrent.nCityCode))
            UcSearcherLoadSources.UCViewNSS(R2CoreTransportationAndLoadNotificationMclassLoadSourcesManagement.GetNSSLoadSource(UCNSSCurrent.nBarSource))
            UcNumbernCarNumKol.UCValue = UCNSSCurrent.nCarNumKol
            UcNumberTransportPrice.UCValue = UCNSSCurrent.StrPriceSug
            UcPersianTextBoxStrBarname.UCValue = UCNSSCurrent.StrBarName
            UcSearcherLoaderTypes.UCViewNSS(R2CoreTransportationAndLoadNotificationMClassLoaderTypesManagement.GetNSSLoaderType(UCNSSCurrent.nTruckType))
            UcPersianTextBoxAddress.UCValue = UCNSSCurrent.StrAddress
            UcPersianTextBoxStrDescription.UCValue = UCNSSCurrent.StrDescription
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcButtonNew_UCClickedEvent() Handles UcButtonNew.UCClickedEvent
        Try
            UCRefreshGeneral()
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcButtonLoadCapacitorLoadRegisteringEditing_UCClickedEvent() Handles UcButtonLoadCapacitorLoadRegisteringEditing.UCClickedEvent
        Try
            Dim NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure
            If UcNumbernEstelamId.UCValue = 0 Then
                'ثبت بار جدید
                NSS = New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(0, UcPersianTextBoxStrBarname.UCValue, UcSearcherLoadTargets.UCGetSelectedNSS.OCode, UcSearcherGoods.UCGetSelectedNSS.OCode, UcSearcherTransportCompanies.UCGetSelectedNSS.OCode, UcSearcherLoaderTypes.UCGetSelectedNSS.OCode, UcPersianTextBoxAddress.UCValue, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, UcNumbernCarNumKol.UCValue, UcNumberTransportPrice.UCValue, UcPersianTextBoxStrDescription.UCValue, _DateTime.GetCurrentDateShamsiFull(), _DateTime.GetCurrentTime(), UcNumbernCarNumKol.UCValue, R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered, UcSearcherLoadSources.UCGetSelectedNSS.OCode, R2CoreTransportationAndLoadNotificationMClassAnnouncementHallsManagement.GetNSSAnnouncementHallByLoaderTypeId(UcSearcherLoaderTypes.UCGetSelectedNSS.OCode).AHId, R2CoreTransportationAndLoadNotificationMClassAnnouncementHallsManagement.GetNSSAnnouncementHallSubGroupByLoaderTypeId(UcSearcherLoaderTypes.UCGetSelectedNSS.OCode).AHSGId)
                UcNumbernEstelamId.UCValue = R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManipulationManagement.LoadCapacitorLoadRegistering(NSS)
                RaiseEvent UCLoadCapacitorLoadRegisteredEvent(UCNSSCurrent)
            Else
                'ویرایش اطلاعات بار
                NSS = New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(UcNumbernEstelamId.UCValue, UcPersianTextBoxStrBarname.UCValue, UcSearcherLoadTargets.UCGetSelectedNSS.OCode, UcSearcherGoods.UCGetSelectedNSS.OCode, UcSearcherTransportCompanies.UCGetSelectedNSS.OCode, UcSearcherLoaderTypes.UCGetSelectedNSS.OCode, UcPersianTextBoxAddress.UCValue, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, UcNumbernCarNumKol.UCValue, UcNumberTransportPrice.UCValue, UcPersianTextBoxStrDescription.UCValue, _DateTime.GetCurrentDateShamsiFull(), _DateTime.GetCurrentTime(), UcNumbernCarNumKol.UCValue, R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered, UcSearcherLoadSources.UCGetSelectedNSS.OCode, R2CoreTransportationAndLoadNotificationMClassAnnouncementHallsManagement.GetNSSAnnouncementHallByLoaderTypeId(UcSearcherLoaderTypes.UCGetSelectedNSS.OCode).AHId, R2CoreTransportationAndLoadNotificationMClassAnnouncementHallsManagement.GetNSSAnnouncementHallSubGroupByLoaderTypeId(UcSearcherLoaderTypes.UCGetSelectedNSS.OCode).AHSGId)
                R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManipulationManagement.LoadCapacitorLoadEditing(NSS,R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
                RaiseEvent UCLoadCapacitorLoadEditedEvent(UCNSSCurrent)
            End If
            UCViewNSS(R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement.GetNSSLoadCapacitorLoad(UcNumbernEstelamId.UCValue))
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.SuccessProccess, "ثبت بار انجام شد", "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcButtonLoadCapacitorLoadDeleting_UCClickedEvent() Handles UcButtonLoadCapacitorLoadDeleting.UCClickedEvent
        Try
           DIM NSS=R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement.GetNSSLoadCapacitorLoad(UcNumbernEstelamId.UCValue)
            R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManipulationManagement.LoadCapacitorLoadDeleting(NSS,R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
            UCRefreshGeneral()
            RaiseEvent UCLoadCapacitorLoadDeletedEvent(UCNSSCurrent)
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.SuccessProccess, "حذف بار انجام شد", "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcButtonLoadCapacitorLoadFreeLining_UCClickedEvent() Handles UcButtonLoadCapacitorLoadFreeLining.UCClickedEvent
        Try
            R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManipulationManagement.LoadCapacitorLoadFreeLining(UcNumbernEstelamId.UCValue,R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
            RaiseEvent UCLoadCapacitorLoadFreeLinedEvent(UCNSSCurrent)
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.SuccessProccess, "وضعیت بار تغییر یافت", "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcButtonLoadCapacitorLoadCancelling_UCClickedEvent() Handles UcButtonLoadCapacitorLoadCancelling.UCClickedEvent
        Try
            DIM NSS =R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement.GetNSSLoadCapacitorLoad(UcNumbernEstelamId.UCValue)
            R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManipulationManagement.LoadCapacitorLoadCancelling(NSS,R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
            RaiseEvent UCLoadCapacitorLoadCancelledEvent(UCNSSCurrent)
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.SuccessProccess, "وضعیت بار تغییر یافت", "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcButtonLoadPermissions_UCClickedEvent() Handles UcButtonLoadPermissions.UCClickedEvent

    End Sub

    Private Sub UcSearcherTransportCompanies_UC13PressedEvent() Handles UcSearcherTransportCompanies.UC13PressedEvent
        UcSearcherGoods.UCFocus()
    End Sub

    Private Sub UcSearcherGoods_UC13PressedEvent() Handles UcSearcherGoods.UC13PressedEvent
        UcSearcherLoadTargets.UCFocus()
    End Sub

    Private Sub UcSearcherLoadTargets_UC13PressedEvent() Handles UcSearcherLoadTargets.UC13PressedEvent
        UcSearcherLoaderTypes.UCFocus()
    End Sub

    Private Sub UcSearcherLoadSources_UC13PressedEvent() Handles UcSearcherLoadSources.UC13PressedEvent
        UcSearcherLoaderTypes.UCFocus()
    End Sub

    Private Sub UcNumbernCarNumKol_UC13Pressed(UserNumber As String) Handles UcNumbernCarNumKol.UC13Pressed
        UcNumberTransportPrice.UCFocus()
    End Sub

    Private Sub UcNumberTransportPrice_UC13Pressed(UserNumber As String) Handles UcNumberTransportPrice.UC13Pressed
        UcPersianTextBoxStrBarname.UCFocus()
    End Sub

    Private Sub UcPersianTextBoxStrBarname_UC13PressedEvent(PersianText As String) Handles UcPersianTextBoxStrBarname.UC13PressedEvent
        UcPersianTextBoxAddress.UCFocus()
    End Sub

    Private Sub UcSearcherLoaderTypes_UC13PressedEvent() Handles UcSearcherLoaderTypes.UC13PressedEvent
        UcNumbernCarNumKol.UCFocus()
    End Sub

    Private Sub UcPersianTextBoxAddress_UC13PressedEvent(PersianText As String) Handles UcPersianTextBoxAddress.UC13PressedEvent
        UcPersianTextBoxStrDescription.UCFocus()
    End Sub

    Private Sub UcPersianTextBoxStrDescription_UC13PressedEvent(PersianText As String) Handles UcPersianTextBoxStrDescription.UC13PressedEvent
        UcButtonLoadCapacitorLoadRegisteringEditing.UCFocus()
    End Sub

    Private Sub UcButtonLoadCapacitorAccounting_UCClickedEvent() Handles UcButtonLoadCapacitorAccounting.UCClickedEvent
        Try
            UcucLoadCapacitorAccountingCollection.UCViewAccounting(UcNumbernEstelamId.UCValue)
            UcucLoadCapacitorAccountingCollection.BringToFront()
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcucLoadCapacitorAccountingCollection_UCPictureExitClickedEvent() Handles UcucLoadCapacitorAccountingCollection.UCPictureExitClickedEvent
        UcucLoadCapacitorAccountingCollection.SendToBack()
    End Sub

    Private Sub UcButtonLoadCapacitorLoadSedimentation_UCClickedEvent() Handles UcButtonLoadCapacitorLoadSedimentation.UCClickedEvent
        Try
            R2CoreTransportationAndLoadNotificationMClassLoadSedimentationManagement.SedimentingLoadCapacitorLoad(UcNumbernEstelamId.UCValue,R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
            RaiseEvent UCLoadCapacitorLoadSedimentedEvent(UCNSSCurrent)
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.SuccessProccess, "وضعیت بار تغییر یافت", "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch ex As LoadCapacitor.Exceptions.LoadCapacitorLoadNotFoundException
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try

    End Sub





#End Region

#Region "Override Methods"
#End Region

#Region "Abstract Methods"
#End Region

#Region "Implemented Members"
#End Region




End Class
