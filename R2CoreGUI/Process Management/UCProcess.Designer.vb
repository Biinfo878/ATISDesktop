﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCProcess
      Inherits UCGeneral

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.GLabel = New gLabel.gLabel()
        Me.PnlMain.SuspendLayout
        Me.SuspendLayout
        '
        'PnlMain
        '
        Me.PnlMain.BackColor = System.Drawing.Color.Transparent
        Me.PnlMain.Controls.Add(Me.GLabel)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.ForeColor = System.Drawing.Color.White
        Me.PnlMain.Location = New System.Drawing.Point(3, 3)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(343, 30)
        Me.PnlMain.TabIndex = 0
        '
        'GLabel
        '
        Me.GLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GLabel.Font = New System.Drawing.Font("B Homa", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178,Byte))
        Me.GLabel.ForeColor = System.Drawing.Color.Blue
        Me.GLabel.Glow = 15
        Me.GLabel.GlowColor = System.Drawing.Color.DarkGray
        Me.GLabel.Location = New System.Drawing.Point(0, 0)
        Me.GLabel.MouseOver = true
        Me.GLabel.MouseOverColor = System.Drawing.Color.LightSteelBlue
        Me.GLabel.MouseOverForeColor = System.Drawing.Color.Black
        Me.GLabel.Name = "GLabel"
        Me.GLabel.ShadowColor = System.Drawing.Color.Black
        Me.GLabel.ShadowOffset = New System.Drawing.Point(1, 1)
        Me.GLabel.Size = New System.Drawing.Size(343, 30)
        Me.GLabel.TabIndex = 0
        Me.GLabel.Text = "اطلاعات پایه"
        '
        'UCProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.PnlMain)
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "UCProcess"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Size = New System.Drawing.Size(349, 36)
        Me.PnlMain.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents PnlMain As Panel
    Friend WithEvents GLabel As gLabel.gLabel
End Class
