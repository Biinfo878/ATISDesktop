﻿
Imports System.Reflection
Imports System.Windows.Forms

Imports PayanehClassLibrary.CarTruckNobatManagement
Imports PayanehClassLibrary.CarTrucksManagement
Imports PayanehClassLibrary.ConfigurationManagement
Imports PayanehClassLibrary.ProcessesManagement
Imports R2Core
Imports R2Core.ExceptionManagement
Imports R2CoreGUI
Imports R2Core.DesktopProcessesManagement
Imports R2CoreParkingSystem.Cars
Imports R2Core.ComputersManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreGUI.ProcessesManagement
Imports R2CoreParkingSystem.ConfigurationManagement
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.ProcessesManagement
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions

Public Class FrmcTurnRegisterRequest
    Inherits FrmcGeneral

    Private _NSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure
    Private _NSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure = Nothing
    Public Event _SodorNobatSuccessEvent()

#Region "General Properties"
#End Region

#Region "Subroutins And Functions"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            InitializeSpecial()
            FrmRefresh()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Protected Overrides Sub SetNSSProcess()
        Try
            SetProcess(R2CoreMClassDesktopProcessesManagement.GetNSSProcess(PayanehClassLibraryProcesses.FrmcSodoorNobat))
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub FrmRefresh()
        Try
            UcMoneyWallet.UCRefresh()
            UcCarAndDriverPresenter.UCRefresh()
            UcTerafficCardPresenter.UCRefresh()
            UcucCarTruckNobatCollection.UCRefresh()
            UcButtonSodoorNobat.UCEnable = False
            UcCarEnterExitStatus.UCRefresh()
            UcCarTruckPnlCarTruckTurns.UCRefreshGeneral()
            UcucCarTruckNobatCollectionPnlCarTruckTurns.UCRefresh()
            UcCarImagePnlCarTruckTurns.UCRefresh()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region "Events"
#End Region

#Region "Event Handlers"

    Private Sub FrmcSodoorNobat__RFIDCardReadedEvent(CardNo As String) Handles Me._RFIDCardReadedEvent
        Try
            FrmRefresh()
            _NSSTrafficCard = R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(CardNo)
            If Not R2CoreTransportationAndLoadNotificationMClassTurnsManagement.IsTerraficCardTypeforTurnRegisteringActive(_NSSTrafficCard) Then Throw New Exception("نوع کارت تردد مطابقت نمی کند")
            UcucCarTruckNobatCollection.UCViewCollection(_NSSTrafficCard)
            UcButtonSodoorNobat.UCEnable = True
            UcTerafficCardPresenter.UCShowTrafficCard(_NSSTrafficCard)
            UcCarAndDriverPresenter.UCSetCar(R2CoreParkingSystemMClassCars.GetnIdCarFromCardId(_NSSTrafficCard.CardId))
            UcMoneyWallet.UCViewMoneyWalletOnlyCharge(_NSSTrafficCard)
            _NSSTruck = R2CoreTransportationAndLoadNotificationMClassTrucksManagement.GetNSSTruck(UcCarAndDriverPresenter.UCGetnIdCar)
            UcCarEnterExitStatus.UCViewStatus(_NSSTrafficCard)
        Catch exxxx As ViewCarTruckTurnsTerraficCardNotSupportException
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, exxxx.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch exx As GetNSSException
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, "اطلاعات پایه کارت تردد و خودرو و روابط آن ها تکمیل نیست", "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch exxx As GetDataException
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, "اطلاعات پایه کارت تردد و خودرو و روابط آن ها تکمیل نیست", "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try

        Try
            StartReading()
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "خطا در عملکرد دستگاه کارت خوان", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try

    End Sub

    Private Sub FrmcSodoorNobat__RFIDCardStartToReadEvent() Handles Me._RFIDCardStartToReadEvent

    End Sub

    Private Sub UcButtonSodoorNobat_UCClickedEvent() Handles UcButtonSodoorNobat.UCClickedEvent
        Try
            'کنترل حضور ناوگان در پارکینگ - درصورتی که طبق کانفیگ باید حضورداشته باشد ولی حضور نداشته باشد آنگاه اکسپشن پرتاب می گردد
            R2CoreTransportationAndLoadNotificationMClassTurnsManagement.TruckPresentInParkingForTurnRegisteringControl(_NSSTruck)
            Dim TurnId As Int64 = Int64.MinValue
            Dim TurnRegisterRequestId = TurnRegisterRequest.PayanehClassLibraryMClassTurnRegisterRequestManagement.RealTimeTurnRegisterRequest(_NSSTruck, False, True, TurnId, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.SuccessProccess, "نوبت صادر شد" & vbCrLf & "شماره درخواست : " + TurnRegisterRequestId.ToString & vbCrLf & "شماره نوبت :" + TurnId.ToString, String.Empty, FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
            RaiseEvent _SodorNobatSuccessEvent()
        Catch ex As Exception When TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException OrElse TypeOf ex Is CarIsNotPresentInParkingException OrElse TypeOf ex Is SequentialTurnIsNotActiveException OrElse TypeOf ex Is TurnPrintingInfNotFoundException OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException OrElse TypeOf ex Is GetNobatException OrElse TypeOf ex Is GetNSSException OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me, False)
        Catch ex As Exception When TypeOf ex Is GetNobatExceptionCarTruckHasNobat OrElse TypeOf ex Is GetNobatExceptionCarTruckIsShahri
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Information, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me, False)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me, True)
        End Try
    End Sub

    Private Sub FrmSodoorNobat_SodoorNobatSuccessEventHandler() Handles Me._SodorNobatSuccessEvent
        Try
            'Dim NSSCarTruck As R2StandardCarTruckStructure = PayanehClassLibraryMClassCarTrucks.GetNSSCarTruckbyCarId(UcCarAndDriverPresenter.UCGetnIdCar)
            'UcucCarTruckNobatCollection.UCViewInformation(NSSCarTruck)
            UcMoneyWallet.UCViewMoneyWalletOnlyCharge(_NSSTrafficCard)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcucCarTruckNobatCollectionPnlCarTruckTurns__UCTerraficCardReadedEvent(NSSTerraficCard As R2CoreParkingSystemStandardTrafficCardStructure) Handles UcucCarTruckNobatCollectionPnlCarTruckTurns._UCTerraficCardReadedEvent
        Try
            UcCarTruckPnlCarTruckTurns.UCViewCarInformation(R2CoreParkingSystemMClassCars.GetnIdCarFromCardId(NSSTerraficCard.CardId))
            UcCarImagePnlCarTruckTurns.UCViewCarEnterExitImage(NSSTerraficCard)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcCarTruckPnlCarTruckTurns_UCViewCarTruckInformationCompletedEvent(CarId As String) Handles UcCarTruckPnlCarTruckTurns.UCViewCarTruckInformationCompletedEvent
        Try
            Dim NSSCarTruck As R2StandardCarTruckStructure = PayanehClassLibraryMClassCarTrucksManagement.GetNSSCarTruckByCarId(CarId)
            UcucCarTruckNobatCollectionPnlCarTruckTurns.UCViewCollection(NSSCarTruck)
            UcCarImagePnlCarTruckTurns.UCViewCarEnterExitImage(NSSCarTruck.NSSCar)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub FrmcSodoorNobat__MenuRunRequestedEvent(UC As UCMenu) Handles Me._MenuRunRequestedEvent
        Try
            If UC.UCNSSMenu.MenuPanel = "PnlSodoorNobat" Then
                StartReading()
            ElseIf UC.UCNSSMenu.MenuPanel = "PnlCarandDriverInformation" Then
                R2CoreGUIMClassProcessesManagement.OpenProccess(R2CoreMClassDesktopProcessesManagement.GetNSSProcess(PayanehClassLibraryProcesses.FrmcCarAndDriversInformation), R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
            ElseIf UC.UCNSSMenu.MenuPanel = "PnlAccounting" Then
                UcAccountingCollection.UCViewAccounting(_NSSTrafficCard)
            ElseIf UC.UCNSSMenu.MenuPanel = "PnlConsole" Then
                UcucLoggCollection.UCViewLoggOptional1(_NSSTrafficCard.CardNo)
            ElseIf UC.UCNSSMenu.MenuPanel = "PnlCarTruckTurns" Then
                UcucCarTruckNobatCollectionPnlCarTruckTurns.UCForceToReadTerraficCard()
            End If
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub FrmcSodoorNobat__MenuRunCompletedEvent(UC As UCMenu) Handles Me._MenuRunCompletedEvent
        Try
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
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