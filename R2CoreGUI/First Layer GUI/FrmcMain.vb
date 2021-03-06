
Imports System.Reflection
Imports R2Core
Imports R2Core.AudioVideoManagement
Imports R2Core.PublicProc
Imports R2Core.BaseStandardClass
Imports R2Core.AuthenticationManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement



Public Class Frmcmain

    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Try
            Me.Location = New System.Drawing.Point(5, 5)
            Me.Width = Screen.AllScreens(0).WorkingArea.Width - 10
            PnlMain.BackColor = Color.FromName(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.FirstPageColor, 0))
            LblApplicationDomainDisplayTitle.Text = R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.ApplicationDomainDisplayTitle, 0)
            LblSystemDisplayTitle.Text = R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SystemDisplayTitle, 0)
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents picexit As System.Windows.Forms.PictureBox
    Friend WithEvents LblSystemDisplayTitle As System.Windows.Forms.Label
    Friend WithEvents PicMinimize As System.Windows.Forms.PictureBox
    Friend WithEvents LblApplicationDomainDisplayTitle As System.Windows.Forms.Label
    Friend WithEvents UcCollectionofUCActiveForm As UCCollectionofUCActiveForm
    Friend WithEvents UcShutDown As UCShutDown
    Friend WithEvents UcUserImage As UCUserImage
    Friend WithEvents UcDateTime As UCDateTime
    Friend WithEvents PnlMain As Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmcmain))
        Me.picexit = New System.Windows.Forms.PictureBox()
        Me.LblSystemDisplayTitle = New System.Windows.Forms.Label()
        Me.PicMinimize = New System.Windows.Forms.PictureBox()
        Me.LblApplicationDomainDisplayTitle = New System.Windows.Forms.Label()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.UcDateTime = New R2CoreGUI.UCDateTime()
        Me.UcUserImage = New R2CoreGUI.UCUserImage()
        Me.UcShutDown = New R2CoreGUI.UCShutDown()
        Me.UcCollectionofUCActiveForm = New R2CoreGUI.UCCollectionofUCActiveForm()
        CType(Me.picexit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicMinimize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'picexit
        '
        Me.picexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picexit.BackColor = System.Drawing.Color.Transparent
        Me.picexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picexit.Image = CType(resources.GetObject("picexit.Image"), System.Drawing.Image)
        Me.picexit.Location = New System.Drawing.Point(962, 8)
        Me.picexit.Name = "picexit"
        Me.picexit.Size = New System.Drawing.Size(27, 20)
        Me.picexit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picexit.TabIndex = 27
        Me.picexit.TabStop = False
        '
        'LblSystemDisplayTitle
        '
        Me.LblSystemDisplayTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblSystemDisplayTitle.BackColor = System.Drawing.Color.Transparent
        Me.LblSystemDisplayTitle.Font = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblSystemDisplayTitle.ForeColor = System.Drawing.Color.White
        Me.LblSystemDisplayTitle.Location = New System.Drawing.Point(503, 20)
        Me.LblSystemDisplayTitle.Name = "LblSystemDisplayTitle"
        Me.LblSystemDisplayTitle.Size = New System.Drawing.Size(202, 23)
        Me.LblSystemDisplayTitle.TabIndex = 195
        Me.LblSystemDisplayTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PicMinimize
        '
        Me.PicMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PicMinimize.BackColor = System.Drawing.Color.Transparent
        Me.PicMinimize.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicMinimize.Image = CType(resources.GetObject("PicMinimize.Image"), System.Drawing.Image)
        Me.PicMinimize.Location = New System.Drawing.Point(937, 8)
        Me.PicMinimize.Name = "PicMinimize"
        Me.PicMinimize.Size = New System.Drawing.Size(19, 20)
        Me.PicMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicMinimize.TabIndex = 200
        Me.PicMinimize.TabStop = False
        '
        'LblApplicationDomainDisplayTitle
        '
        Me.LblApplicationDomainDisplayTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblApplicationDomainDisplayTitle.BackColor = System.Drawing.Color.Transparent
        Me.LblApplicationDomainDisplayTitle.Font = New System.Drawing.Font("B Homa", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblApplicationDomainDisplayTitle.ForeColor = System.Drawing.Color.White
        Me.LblApplicationDomainDisplayTitle.Location = New System.Drawing.Point(553, 20)
        Me.LblApplicationDomainDisplayTitle.Name = "LblApplicationDomainDisplayTitle"
        Me.LblApplicationDomainDisplayTitle.Size = New System.Drawing.Size(421, 67)
        Me.LblApplicationDomainDisplayTitle.TabIndex = 201
        Me.LblApplicationDomainDisplayTitle.Text = "پایانه امیر کبیر اصفهان"
        Me.LblApplicationDomainDisplayTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PnlMain
        '
        Me.PnlMain.BackColor = System.Drawing.Color.LightSlateGray
        Me.PnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlMain.Controls.Add(Me.UcDateTime)
        Me.PnlMain.Controls.Add(Me.UcUserImage)
        Me.PnlMain.Controls.Add(Me.UcShutDown)
        Me.PnlMain.Controls.Add(Me.UcCollectionofUCActiveForm)
        Me.PnlMain.Controls.Add(Me.LblSystemDisplayTitle)
        Me.PnlMain.Controls.Add(Me.PicMinimize)
        Me.PnlMain.Controls.Add(Me.picexit)
        Me.PnlMain.Controls.Add(Me.LblApplicationDomainDisplayTitle)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.Location = New System.Drawing.Point(1, 1)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(1003, 109)
        Me.PnlMain.TabIndex = 203
        '
        'UcDateTime
        '
        Me.UcDateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcDateTime.BackColor = System.Drawing.Color.Transparent
        Me.UcDateTime.Location = New System.Drawing.Point(711, 7)
        Me.UcDateTime.Name = "UcDateTime"
        Me.UcDateTime.Size = New System.Drawing.Size(170, 24)
        Me.UcDateTime.TabIndex = 207
        Me.UcDateTime.UCBackColor = System.Drawing.Color.Transparent
        Me.UcDateTime.UCClockIconAppierance = True
        Me.UcDateTime.UCEnable = True
        Me.UcDateTime.UCFont = New System.Drawing.Font("B Homa", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcDateTime.UCForeColor = System.Drawing.Color.White
        '
        'UcUserImage
        '
        Me.UcUserImage.BackColor = System.Drawing.Color.Transparent
        Me.UcUserImage.Location = New System.Drawing.Point(3, 8)
        Me.UcUserImage.Name = "UcUserImage"
        Me.UcUserImage.Padding = New System.Windows.Forms.Padding(2)
        Me.UcUserImage.Size = New System.Drawing.Size(72, 63)
        Me.UcUserImage.TabIndex = 206
        '
        'UcShutDown
        '
        Me.UcShutDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcShutDown.Location = New System.Drawing.Point(887, 2)
        Me.UcShutDown.Name = "UcShutDown"
        Me.UcShutDown.Padding = New System.Windows.Forms.Padding(3)
        Me.UcShutDown.Size = New System.Drawing.Size(32, 30)
        Me.UcShutDown.TabIndex = 205
        '
        'UcCollectionofUCActiveForm
        '
        Me.UcCollectionofUCActiveForm.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcCollectionofUCActiveForm.BackColor = System.Drawing.Color.Transparent
        Me.UcCollectionofUCActiveForm.Location = New System.Drawing.Point(3, 77)
        Me.UcCollectionofUCActiveForm.Name = "UcCollectionofUCActiveForm"
        Me.UcCollectionofUCActiveForm.Padding = New System.Windows.Forms.Padding(3)
        Me.UcCollectionofUCActiveForm.Size = New System.Drawing.Size(986, 30)
        Me.UcCollectionofUCActiveForm.TabIndex = 202
        '
        'Frmcmain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.Red
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1005, 111)
        Me.Controls.Add(Me.PnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "Frmcmain"
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "R2PrimaryParkingSystem"
        Me.TransparencyKey = System.Drawing.Color.Aqua
        CType(Me.picexit,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PicMinimize,System.ComponentModel.ISupportInitialize).EndInit
        Me.PnlMain.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

#End Region

    Public Event ExitIconPressed()


#Region "General Properties"
#End Region

#Region "Subroutins And Functions"


#End Region

#Region "Events"
#End Region

#Region "Event Handlers"

    Private Sub picexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picexit.Click
        RaiseEvent ExitIconPressed()
    End Sub

    Private Sub PicMinimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

#End Region

#Region "Override Methods"
#End Region

#Region "Abstract Methods"
#End Region

#Region "Implemented Members"
#End Region







End Class
