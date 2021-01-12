﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
'
Namespace R2PrimaryFileSharingWS
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="R2PrimaryFileSharingWebServiceSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class R2PrimaryFileSharingWebService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private WebMethodLoginOperationCompleted As System.Threading.SendOrPostCallback
        
        Private WebMethodSaveFileOperationCompleted As System.Threading.SendOrPostCallback
        
        Private WebMethodGetFileOperationCompleted As System.Threading.SendOrPostCallback
        
        Private WebMethodIOFileExistOperationCompleted As System.Threading.SendOrPostCallback
        
        Private WebMethodDeleteFileOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.R2Core.My.MySettings.Default.R2Core_R2PrimaryFileSharingWS_R2PrimaryFileSharingWebService
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event WebMethodLoginCompleted As WebMethodLoginCompletedEventHandler
        
        '''<remarks/>
        Public Event WebMethodSaveFileCompleted As WebMethodSaveFileCompletedEventHandler
        
        '''<remarks/>
        Public Event WebMethodGetFileCompleted As WebMethodGetFileCompletedEventHandler
        
        '''<remarks/>
        Public Event WebMethodIOFileExistCompleted As WebMethodIOFileExistCompletedEventHandler
        
        '''<remarks/>
        Public Event WebMethodDeleteFileCompleted As WebMethodDeleteFileCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WebMethodLogin", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function WebMethodLogin(ByVal YourUserShenaseh As String, ByVal YourUserPassword As String) As Long
            Dim results() As Object = Me.Invoke("WebMethodLogin", New Object() {YourUserShenaseh, YourUserPassword})
            Return CType(results(0),Long)
        End Function
        
        '''<remarks/>
        Public Overloads Sub WebMethodLoginAsync(ByVal YourUserShenaseh As String, ByVal YourUserPassword As String)
            Me.WebMethodLoginAsync(YourUserShenaseh, YourUserPassword, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub WebMethodLoginAsync(ByVal YourUserShenaseh As String, ByVal YourUserPassword As String, ByVal userState As Object)
            If (Me.WebMethodLoginOperationCompleted Is Nothing) Then
                Me.WebMethodLoginOperationCompleted = AddressOf Me.OnWebMethodLoginOperationCompleted
            End If
            Me.InvokeAsync("WebMethodLogin", New Object() {YourUserShenaseh, YourUserPassword}, Me.WebMethodLoginOperationCompleted, userState)
        End Sub
        
        Private Sub OnWebMethodLoginOperationCompleted(ByVal arg As Object)
            If (Not (Me.WebMethodLoginCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent WebMethodLoginCompleted(Me, New WebMethodLoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WebMethodSaveFile", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Sub WebMethodSaveFile(ByVal YourRawGroupId As Long, ByVal YourFileName As String, <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> ByVal YourFile() As Byte, ByVal YourExchangeKey As Long)
            Me.Invoke("WebMethodSaveFile", New Object() {YourRawGroupId, YourFileName, YourFile, YourExchangeKey})
        End Sub
        
        '''<remarks/>
        Public Overloads Sub WebMethodSaveFileAsync(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourFile() As Byte, ByVal YourExchangeKey As Long)
            Me.WebMethodSaveFileAsync(YourRawGroupId, YourFileName, YourFile, YourExchangeKey, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub WebMethodSaveFileAsync(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourFile() As Byte, ByVal YourExchangeKey As Long, ByVal userState As Object)
            If (Me.WebMethodSaveFileOperationCompleted Is Nothing) Then
                Me.WebMethodSaveFileOperationCompleted = AddressOf Me.OnWebMethodSaveFileOperationCompleted
            End If
            Me.InvokeAsync("WebMethodSaveFile", New Object() {YourRawGroupId, YourFileName, YourFile, YourExchangeKey}, Me.WebMethodSaveFileOperationCompleted, userState)
        End Sub
        
        Private Sub OnWebMethodSaveFileOperationCompleted(ByVal arg As Object)
            If (Not (Me.WebMethodSaveFileCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent WebMethodSaveFileCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WebMethodGetFile", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function WebMethodGetFile(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("WebMethodGetFile", New Object() {YourRawGroupId, YourFileName, YourExchangeKey})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub WebMethodGetFileAsync(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long)
            Me.WebMethodGetFileAsync(YourRawGroupId, YourFileName, YourExchangeKey, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub WebMethodGetFileAsync(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long, ByVal userState As Object)
            If (Me.WebMethodGetFileOperationCompleted Is Nothing) Then
                Me.WebMethodGetFileOperationCompleted = AddressOf Me.OnWebMethodGetFileOperationCompleted
            End If
            Me.InvokeAsync("WebMethodGetFile", New Object() {YourRawGroupId, YourFileName, YourExchangeKey}, Me.WebMethodGetFileOperationCompleted, userState)
        End Sub
        
        Private Sub OnWebMethodGetFileOperationCompleted(ByVal arg As Object)
            If (Not (Me.WebMethodGetFileCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent WebMethodGetFileCompleted(Me, New WebMethodGetFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WebMethodIOFileExist", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function WebMethodIOFileExist(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long) As Boolean
            Dim results() As Object = Me.Invoke("WebMethodIOFileExist", New Object() {YourRawGroupId, YourFileName, YourExchangeKey})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub WebMethodIOFileExistAsync(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long)
            Me.WebMethodIOFileExistAsync(YourRawGroupId, YourFileName, YourExchangeKey, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub WebMethodIOFileExistAsync(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long, ByVal userState As Object)
            If (Me.WebMethodIOFileExistOperationCompleted Is Nothing) Then
                Me.WebMethodIOFileExistOperationCompleted = AddressOf Me.OnWebMethodIOFileExistOperationCompleted
            End If
            Me.InvokeAsync("WebMethodIOFileExist", New Object() {YourRawGroupId, YourFileName, YourExchangeKey}, Me.WebMethodIOFileExistOperationCompleted, userState)
        End Sub
        
        Private Sub OnWebMethodIOFileExistOperationCompleted(ByVal arg As Object)
            If (Not (Me.WebMethodIOFileExistCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent WebMethodIOFileExistCompleted(Me, New WebMethodIOFileExistCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WebMethodDeleteFile", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Sub WebMethodDeleteFile(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long)
            Me.Invoke("WebMethodDeleteFile", New Object() {YourRawGroupId, YourFileName, YourExchangeKey})
        End Sub
        
        '''<remarks/>
        Public Overloads Sub WebMethodDeleteFileAsync(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long)
            Me.WebMethodDeleteFileAsync(YourRawGroupId, YourFileName, YourExchangeKey, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub WebMethodDeleteFileAsync(ByVal YourRawGroupId As Long, ByVal YourFileName As String, ByVal YourExchangeKey As Long, ByVal userState As Object)
            If (Me.WebMethodDeleteFileOperationCompleted Is Nothing) Then
                Me.WebMethodDeleteFileOperationCompleted = AddressOf Me.OnWebMethodDeleteFileOperationCompleted
            End If
            Me.InvokeAsync("WebMethodDeleteFile", New Object() {YourRawGroupId, YourFileName, YourExchangeKey}, Me.WebMethodDeleteFileOperationCompleted, userState)
        End Sub
        
        Private Sub OnWebMethodDeleteFileOperationCompleted(ByVal arg As Object)
            If (Not (Me.WebMethodDeleteFileCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent WebMethodDeleteFileCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")>  _
    Public Delegate Sub WebMethodLoginCompletedEventHandler(ByVal sender As Object, ByVal e As WebMethodLoginCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class WebMethodLoginCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Long
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Long)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")>  _
    Public Delegate Sub WebMethodSaveFileCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")>  _
    Public Delegate Sub WebMethodGetFileCompletedEventHandler(ByVal sender As Object, ByVal e As WebMethodGetFileCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class WebMethodGetFileCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")>  _
    Public Delegate Sub WebMethodIOFileExistCompletedEventHandler(ByVal sender As Object, ByVal e As WebMethodIOFileExistCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class WebMethodIOFileExistCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")>  _
    Public Delegate Sub WebMethodDeleteFileCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
End Namespace
