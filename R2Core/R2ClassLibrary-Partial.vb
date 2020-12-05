﻿
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Timers
Imports System.ComponentModel

Imports R2Core.ComputersManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.ExceptionManagement
Imports R2Core.MonetaryCreditSupplySources
Imports R2Core.LoggingManagement
Imports R2Core.BaseStandardClass
Imports R2Core.DateAndTimeManagement
Imports R2Core.UserManagement
Imports R2Core.UserManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement
Imports R2Core.SecurityAlgorithmsManagement.Exceptions

Imports PcPosDll



Namespace MonetarySupply

    ' On All of Location of Code Where UCMonetarySupply Used so ConfigurationIndex must be set to UCConfigurationIndex Property
    ' For Example UCMoneyWalletCharge has ConfigurationIndex Property so send it to UCMonetarySupply

    Public Enum MonetarySupplyResult
        None = 0
        Success = 1
        UnSuccess = 2
    End Enum

    Public Class R2CoreMonetarySupply

        Private _CurrentNSS As R2CoreStandardMonetaryCreditSupplySourceStructure = Nothing
        Private _Amount As Int64
        Public Event MonetarySupplySuccessEvent(TransactionId As Int64, Amount As Int64, SupplyReport As String)
        Public Event MonetarySupplyUnSuccessEvent(TransactionId As Int64, Amount As Int64, SupplyReport As String)
        Private WithEvents _MonetaryCreditSupplySource As R2CoreMonetaryCreditSupplySource = Nothing
        Private WithEvents _MonetarySupplyWatcher As System.Windows.Forms.Timer = New System.Windows.Forms.Timer

        Public Sub New(YourNSS As R2CoreStandardMonetaryCreditSupplySourceStructure, YourAmount As Int64)
            Try
                _CurrentNSS = YourNSS
                _Amount = YourAmount
            Catch ex As Exception
                '_MonetaryCreditSupplySource.Dispose()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub StartSupply()
            Try
                _MonetaryCreditSupplySource = R2CoreMonetaryCreditSupplySourcesManagement.GetMonetaryCreditSupplySourceInstance(_CurrentNSS, _Amount)
                _MonetaryCreditSupplySource.Initialize()
                _MonetaryCreditSupplySource.DoCreditSupply()
                _MonetarySupplyWatcher.Interval = 100
                _MonetarySupplyWatcher.Enabled = True
                _MonetarySupplyWatcher.Start()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Sub _MonetarySupplyWatcher_Tick(sender As Object, e As EventArgs) Handles _MonetarySupplyWatcher.Tick
            Try
                If _MonetaryCreditSupplySource.MonetaryCreditSupplyResult <> MonetarySupplyResult.None Then
                    _MonetarySupplyWatcher.Enabled = False
                    _MonetarySupplyWatcher.Stop()
                    If _MonetaryCreditSupplySource.MonetaryCreditSupplyResult = MonetarySupplyResult.Success Then
                        RaiseEvent MonetarySupplySuccessEvent(_MonetaryCreditSupplySource.TransactionId, _MonetaryCreditSupplySource.Amount, _MonetaryCreditSupplySource.SupplyReport)
                    Else
                        RaiseEvent MonetarySupplyUnSuccessEvent(_MonetaryCreditSupplySource.TransactionId, _MonetaryCreditSupplySource.Amount, _MonetaryCreditSupplySource.SupplyReport)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

End Namespace

Namespace MonetaryCreditSupplySources

    ' When new MonetaryCreditSupplySource must to add to ApplicationDomain then 
    ' 1-R2primary.dbo.TblMonetaryCreditSupplySources
    ' 2-R2Primary.dbo.TblConfigurationOfComputers for 65 Config
    ' 3-Developmnet of Classes - Inheritance of R2CoreMonetaryCreditSupplySource

    Public MustInherit Class R2CoreMonetaryCreditSupplySources
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Cash As Int64 = 1
        Public Shared ReadOnly Property Pos As Int64 = 2
        Public Shared ReadOnly Property BankWebService As Int64 = 3
    End Class

    Public Class R2CoreStandardMonetaryCreditSupplySourceStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            MCSSId = 0
            MCSSName = String.Empty
            MCSSTitle = String.Empty
            MCSSColor = Color.Navy
            AssemblyDll = String.Empty
            AssemblyPath = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.TrueString
        End Sub

        Public Sub New(ByVal YourMCSSId As Int64, ByVal YourMCSSName As String, ByVal YourMCSSTitle As String, ByVal YourMCSSColor As Color, YourAssemblyPath As String, YourAssemblyDll As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourMCSSId, YourMCSSName)
            MCSSId = YourMCSSId
            MCSSName = YourMCSSName
            MCSSTitle = YourMCSSTitle
            MCSSColor = YourMCSSColor
            AssemblyDll = YourAssemblyDll
            AssemblyPath = YourAssemblyPath
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property MCSSId As Int64
        Public Property MCSSName As String
        Public Property MCSSTitle As String
        Public Property MCSSColor As Color
        Public Property AssemblyDll As String
        Public Property AssemblyPath As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean
    End Class

    Public MustInherit Class R2CoreMonetaryCreditSupplySourcesManagement
        Public Shared Function GetNSSMonetaryCreditSupplySource(YourMCSSId As String) As R2CoreStandardMonetaryCreditSupplySourceStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where MCSSId=" & YourMCSSId & "", 3600, Ds).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardMonetaryCreditSupplySourceStructure(Ds.Tables(0).Rows(0).Item("MCSSId"), Ds.Tables(0).Rows(0).Item("MCSSName").trim, Ds.Tables(0).Rows(0).Item("MCSSTitle").trim, Color.FromName(Ds.Tables(0).Rows(0).Item("MCSSColor").trim), Ds.Tables(0).Rows(0).Item("AssemblyPath").trim, Ds.Tables(0).Rows(0).Item("AssemblyDll").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMonetaryCreditSupplySources() As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
            Try
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where ViewFlag=1 and Active=1 and Deleted=0 ", 3600, DS)
                Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardMonetaryCreditSupplySourceStructure(DS.Tables(0).Rows(Loopx).Item("MCSSId"), DS.Tables(0).Rows(Loopx).Item("MCSSName").trim, DS.Tables(0).Rows(Loopx).Item("MCSSTitle").trim, Color.FromName(DS.Tables(0).Rows(Loopx).Item("MCSSColor").trim), DS.Tables(0).Rows(Loopx).Item("AssemblyPath").trim, DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetThisComputerDefaultNSS(YourConfigurationIndex As Int16) As R2CoreStandardMonetaryCreditSupplySourceStructure
            Try
                Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Return GetNSSMonetaryCreditSupplySource(ConfigValue.Split("@")(0))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetThisComputerCollectionBitMap(YourConfigurationIndex As Int16) As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
            Try
                Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
                Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Dim Bitmap() As String = ConfigValue.Split("@")(1).Split("-")
                For Loopx As Int16 = 0 To Bitmap.Count - 1
                    Dim NSS As R2CoreStandardMonetaryCreditSupplySourceStructure = GetNSSMonetaryCreditSupplySource(Bitmap(Loopx).Split(":")(0))
                    NSS.Active = Bitmap(Loopx).Split(":")(1)
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMonetaryCreditSupplySourceInstance(YourNSS As R2CoreStandardMonetaryCreditSupplySourceStructure, YourAmount As Int64) As R2CoreMonetaryCreditSupplySource
            Try
                Dim AssemblyClassName As String = YourNSS.AssemblyPath
                Dim Instance As Object = Activator.CreateInstance(Type.GetType(AssemblyClassName), New Object() {YourAmount})
                Return Instance
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public MustInherit Class R2CoreMonetaryCreditSupplySource
        Protected _DateTime As R2DateTime = New R2DateTime

        Public Sub New(YourAmount As Int64)
            Try
                _Amount = YourAmount
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Protected _Amount As Int64 = 0
        <Browsable(False)>
        Public ReadOnly Property Amount As Int64
            Get
                Return _Amount
            End Get
        End Property

        Protected _SupplyReport As String = String.Empty
        <Browsable(False)>
        Public ReadOnly Property SupplyReport As String
            Get
                Return _SupplyReport
            End Get
        End Property


        Protected _TransactionId As Int64 = 0
        <Browsable(False)>
        Public ReadOnly Property TransactionId As Int64
            Get
                Return _TransactionId
            End Get
        End Property

        Protected _MonetaryCreditSupplyResult As MonetarySupply.MonetarySupplyResult = MonetarySupply.MonetarySupplyResult.None
        <Browsable(False)>
        Public ReadOnly Property MonetaryCreditSupplyResult As MonetarySupply.MonetarySupplyResult
            Get
                Return _MonetaryCreditSupplyResult
            End Get
        End Property

        Public MustOverride Sub DoCreditSupply()

        Public MustOverride Sub Initialize()

        Public MustOverride Sub Dispose()


    End Class

    Namespace Cash

        Public Class R2CoreCash
            Inherits MonetaryCreditSupplySources.R2CoreMonetaryCreditSupplySource


            Public Sub New(YourAmount As Int64)
                MyBase.New(YourAmount)
                Try
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Overrides Sub Initialize()

            End Sub

            Public Overrides Sub DoCreditSupply()
                _SupplyReport = "عملیات موفق"
                _TransactionId = Convert.ToUInt64(_DateTime.GetCurrentDateShamsiFull.Replace("/", "") + _DateTime.GetCurrentTime.Replace(":", "") + Int((1000 - 100 + 1) * Rnd() + 100).ToString)
                _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
            End Sub

            Public Overrides Sub Dispose()
            End Sub

        End Class

    End Namespace

    Namespace Pos

        Namespace PCPos

            Public Class R2CorePCPos
                Inherits R2CoreMonetaryCreditSupplySource

                Private TargetedPosDevice As PosDevice = New PosDevice
                Public WithEvents _PCPOS As PcPosBusiness = New PcPosBusiness
                Public WithEvents _SearchPos As PcPosDiscovery = New PcPosDiscovery
                Public Event SearchAsyncCompleted(ResultCount As Int16, Description As String)

#Region "General Properties"

                Public ReadOnly Property GetTargetPosIPAddress As String
                    Get
                        Return R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.AttachedPoses, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0)
                    End Get
                End Property

#End Region

#Region "Subroutins And Functions"

                Public Sub New(YourAmount As Int64)
                    MyBase.New(YourAmount)
                    Try
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

                Private Sub LoggingPosResult(YourPosResult As PosResult)
                    Try
                        Dim DataStruct As DataStruct = GetPosResultComposit(YourPosResult)
                        _TransactionId = DataStruct.Data1
                        R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Note, "پوز - خرید", DataStruct.Data1, DataStruct.Data2, DataStruct.Data3, DataStruct.Data4, DataStruct.Data5, R2CoreMClassLoginManagement.GetNSSSystemUser().UserId, _DateTime.GetCurrentDateTimeMilladiFormated(), _DateTime.GetCurrentDateShamsiFull))
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

                Private Function GetPosResultComposit(YourPosResult As PosResult) As R2Core.BaseStandardClass.DataStruct
                    Try
                        Dim TransactionId As Long = Convert.ToUInt64(_DateTime.GetCurrentDateShamsiFull.Replace("/", "") + _DateTime.GetCurrentTime.Replace(":", "") + Int((1000 - 100 + 1) * Rnd() + 100).ToString)
                        Dim DataStruct As DataStruct = New DataStruct
                        DataStruct.Data1 = TransactionId
                        DataStruct.Data2 = YourPosResult.PcPosStatusCode.ToString + "-" + YourPosResult.PcPosStatus + "-" + YourPosResult.OptionalField + "-" + YourPosResult.Amount
                        DataStruct.Data3 = YourPosResult.ResponseCode + "-" + YourPosResult.Rrn + "-" + YourPosResult.CardNo
                        DataStruct.Data4 = YourPosResult.TerminalId + "-" + YourPosResult.ProcessingCode + "-" + YourPosResult.TransactionNo
                        DataStruct.Data5 = YourPosResult.TransactionDate + "-" + YourPosResult.TransactionTime
                        Return DataStruct
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function

                Public Function SearchPCPosAsync()
                    Try
                        _SearchPos.SearchPcPosByIp(100, 2000, GetTargetPosIPAddress)
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function

#End Region

#Region "Events"
#End Region

#Region "Event Handlers"

                Private Sub _PCPOS_OnSaleResult(sender As Object, e As PosResult) Handles _PCPOS.OnSaleResult
                    Try
                        LoggingPosResult(e)
                        If e.ResponseCode = "00" Then
                            _SupplyReport = e.PcPosStatus
                            _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                        Else
                            _SupplyReport = "شکست عملیات هنگام ارتباط با دستگاه پوز"
                            _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.UnSuccess
                        End If
                    Catch ex As Exception
                        MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

                Private Sub _SearchPos_OnSearchPcPos(sender As Object, e As IEnumerable(Of PosDevice)) Handles _SearchPos.OnSearchPcPos
                    Try
                        If e.Count < 1 Then
                            TargetedPosDevice = Nothing
                            RaiseEvent SearchAsyncCompleted(e.Count, (New Exceptions.PCPosWithTargetIpNotFoundException).Message)
                        Else
                            TargetedPosDevice = e(0)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

#End Region

#Region "Override Methods"
                Public Overrides Sub Initialize()
                    Try
                        TargetedPosDevice.IpAddress = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.AttachedPoses, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0)
                        TargetedPosDevice.Port = 8888
                        'SearchPCPosAsync()
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try

                End Sub

                Public Overrides Sub DoCreditSupply()
                    Try
                        _PCPOS = New PcPosBusiness
                        _PCPOS.Amount = Amount
                        _PCPOS.RetryTimeOut = New Integer() {1800000, 1800000, 1800000}
                        _PCPOS.ResponseTimeout = New Integer() {1800000, 1800000, 1800000}
                        _PCPOS.Ip = TargetedPosDevice.IpAddress
                        _PCPOS.Port = TargetedPosDevice.Port
                        _PCPOS.ConnectionType = PcPosConnectionType.Lan
                        _PCPOS.AsyncSaleTransaction()
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

                Public Overrides Sub Dispose()
                    Try
                        _PCPOS.Dispose()
                    Catch ex As Exception
                    End Try
                    Try
                        _SearchPos.Dispose()
                    Catch ex As Exception
                    End Try
                End Sub




#End Region

#Region "Abstract Methods"
#End Region

#Region "Implemented Members"
#End Region

            End Class

            Namespace Exceptions
                Public Class PCPosWithTargetIpNotFoundException
                    Inherits ApplicationException
                    Public Overrides ReadOnly Property Message As String
                        Get
                            Return "دستگاه پوز آماده نیست و یا ارتباط با آن فعلا مقدور نمی باشد"
                        End Get
                    End Property
                End Class

            End Namespace

        End Namespace

    End Namespace

    Namespace Bank

        Public Class R2CoreBank
            Inherits MonetaryCreditSupplySources.R2CoreMonetaryCreditSupplySource

            Public Sub New(YourAmount As Int64)
                MyBase.New(YourAmount)
                Try
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Overrides Sub Initialize()

            End Sub

            Public Overrides Sub DoCreditSupply()
                _SupplyReport = "عملیات موفق"
                _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
            End Sub

            Public Overrides Sub Dispose()
            End Sub


        End Class

    End Namespace

End Namespace

Namespace MonetarySettingTools

    'When new MonetarySettingTool must to add to ApplicationDomain then 
    ' 1-R2primary.dbo.TblMonetarySettingTools
    ' 2-R2Primary.dbo.TblConfigurationOfComputers for 66 Config
    ' 3-Developmnet of Classes - Inheritance of UCMonetarySettingToolInstrument

    Public MustInherit Class R2CoreMonetarySettingTools
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property UserPad As Int64 = 1
        Public Shared ReadOnly Property R2PrimaryWebService As Int64 = 3
    End Class

    Public Class R2CoreStandardMonetarySettingToolStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            MSTId = 0
            MSTName = String.Empty
            MSTTitle = String.Empty
            MSTColor = Color.Navy
            AssemblyDll = String.Empty
            AssemblyPath = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.TrueString
        End Sub

        Public Sub New(ByVal YourMSTId As Int64, ByVal YourMSTName As String, ByVal YourMSTTitle As String, ByVal YourMSTColor As Color, YourAssemblyPath As String, YourAssemblyDll As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourMSTId, YourMSTName)
            MSTId = YourMSTId
            MSTName = YourMSTName
            MSTTitle = YourMSTTitle
            MSTColor = YourMSTColor
            AssemblyDll = YourAssemblyDll
            AssemblyPath = YourAssemblyPath
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property MSTId As Int64
        Public Property MSTName As String
        Public Property MSTTitle As String
        Public Property MSTColor As Color
        Public Property AssemblyDll As String
        Public Property AssemblyPath As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean
    End Class

    Public MustInherit Class R2CoreMonetarySettingToolsManagement
        Public Shared Function GetNSSMonetarySettingTool(YourMSTId As String) As R2CoreStandardMonetarySettingToolStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetarySettingTools  Where MSTId=" & YourMSTId & "", 3600, Ds).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardMonetarySettingToolStructure(Ds.Tables(0).Rows(0).Item("MSTId"), Ds.Tables(0).Rows(0).Item("MSTName").trim, Ds.Tables(0).Rows(0).Item("MSTTitle").trim, Color.FromName(Ds.Tables(0).Rows(0).Item("MSTColor").trim), Ds.Tables(0).Rows(0).Item("AssemblyPath").trim, Ds.Tables(0).Rows(0).Item("AssemblyDll").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMonetarySettingTools() As List(Of R2CoreStandardMonetarySettingToolStructure)
            Try
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetarySettingTools Where ViewFlag=1 and Active=1 and Deleted=0", 3600, DS)
                Dim Lst As New List(Of R2CoreStandardMonetarySettingToolStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardMonetarySettingToolStructure(DS.Tables(0).Rows(Loopx).Item("MSTId"), DS.Tables(0).Rows(Loopx).Item("MSTName").trim, DS.Tables(0).Rows(Loopx).Item("MSTTitle").trim, Color.FromName(DS.Tables(0).Rows(Loopx).Item("MSTColor").trim), DS.Tables(0).Rows(Loopx).Item("AssemblyPath").trim, DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetThisComputerDefaultNSS(YourConfigurationIndex As Int16) As R2CoreStandardMonetarySettingToolStructure
            Try
                Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetarySettingTools, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Return GetNSSMonetarySettingTool(ConfigValue.Split("@")(0))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetThisComputerCollectionBitMap(YourConfigurationIndex As Int16) As List(Of R2CoreStandardMonetarySettingToolStructure)
            Try
                Dim Lst As New List(Of R2CoreStandardMonetarySettingToolStructure)
                Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetarySettingTools, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Dim Bitmap() As String = ConfigValue.Split("@")(1).Split("-")
                For Loopx As Int16 = 0 To Bitmap.Count - 1
                    Dim NSS As R2CoreStandardMonetarySettingToolStructure = GetNSSMonetarySettingTool(Bitmap(Loopx).Split(":")(0))
                    NSS.Active = Bitmap(Loopx).Split(":")(1)
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMonetarySettingToolInstrumentInstance(YourNSS As R2CoreStandardMonetarySettingToolStructure) As Object
            Try
                Dim PathDll As String = Application.StartupPath + "\" + YourNSS.AssemblyDll.Trim
                Dim Assembly_ As Assembly = Assembly.LoadFrom(PathDll)
                Dim ProjAndForm As String = YourNSS.AssemblyPath.Trim()
                Dim FormInstanceType = Assembly_.GetType(ProjAndForm)
                Dim objForm = CType(Activator.CreateInstance(FormInstanceType), Object)
                Return objForm
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

End Namespace

Namespace SecurityAlgorithmsManagement


    Public Class ExchangeKey
        Public Sub New()
            ExchangeKey = Int64.MinValue
            UserId = Int64.MinValue
            StartDateTime = Now
        End Sub

        Public Sub New(YourExchangeKey As Int64, YourUserId As Int64, YourStartDateTime As DateTime)
            ExchangeKey = YourExchangeKey
            UserId = YourUserId
            StartDateTime = YourStartDateTime
        End Sub

        Public ExchangeKey As Int64
        Public UserId As Int64
        Public StartDateTime As DateTime
    End Class

    Public Class ExchangeKeyManager

        Private _LstUsers As List(Of ExchangeKey) = New List(Of ExchangeKey)

        Public Sub New()
        End Sub

        Public Function Login(YourUserShenaseh As String, YourUserPassword As String) As Int64
            Try
                R2CoreMClassLoginManagement.AuthenticationUserbyShenasehPassword(New R2CoreStandardUserStructure(Nothing, Nothing, YourUserShenaseh, YourUserPassword, Nothing, Nothing, Nothing))
                Dim NSS = R2CoreMClassLoginManagement.GetNSSUser(YourUserShenaseh, YourUserPassword)
                If _LstUsers.Exists(Function(x) x.UserId = NSS.UserId) Then
                    _LstUsers.Where(Function(x) x.UserId = NSS.UserId)(0).StartDateTime = Now
                    Return _LstUsers.Where(Function(x) x.UserId = NSS.UserId)(0).ExchangeKey
                End If
                Dim EKTemp = R2CoreMClassSecurityAlgorithmsManagement.GetNewExchangeKey()
                _LstUsers.Add(New ExchangeKey(EKTemp, NSS.UserId, Now))
                Return EKTemp
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException OrElse TypeOf (ex) Is GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub AuthenticationExchangeKey(YourExchangeKey As Int64)
            Try
                If _LstUsers.Exists(Function(x) x.ExchangeKey = YourExchangeKey) Then
                    If DateDiff(DateInterval.Minute, _LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0).StartDateTime, Now) <= 1 Then
                    Else
                        _LstUsers.Remove(_LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0))
                        Throw New ExchangeKeyTimeRangePassedException
                    End If
                Else
                    Throw New ExchangeKeyNotExistException
                End If
            Catch ex As ExchangeKeyTimeRangePassedException
                Throw ex
            Catch ex As ExchangeKeyNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSUser(YourExchangeKey As Int64) As R2CoreStandardUserStructure
            Try
                AuthenticationExchangeKey(YourExchangeKey)
                Return R2CoreMClassLoginManagement.GetNSSUser(_LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0).UserId)
            Catch ex As ExchangeKeyTimeRangePassedException
                Throw ex
            Catch ex As ExchangeKeyNotExistException
                Throw ex
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreMClassSecurityAlgorithmsManagement
        Public Shared Function GetNewExchangeKey() As Int64
            Try
                Dim RandGen As New Random
                Return RandGen.Next(10000, 1000000)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions
        Public Class ExchangeKeyNotExistException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کلید تبادل مورد نظر نامعتبر نیست"
                End Get
            End Property
        End Class

        Public Class ExchangeKeyTimeRangePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مدت زمان مجاز به پایان رسیده است"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

