﻿Imports R2CoreGUI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCBlackListCompositBlackListViewer
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
        Me.UcLabel = New R2CoreGUI.UCLabel()
        Me.PnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlMain
        '
        Me.PnlMain.Controls.Add(Me.UcLabel)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.Location = New System.Drawing.Point(5, 5)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(604, 115)
        Me.PnlMain.TabIndex = 0
        '
        'UcLabel
        '
        Me.UcLabel.BackColor = System.Drawing.Color.Transparent
        Me.UcLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcLabel.Location = New System.Drawing.Point(0, 0)
        Me.UcLabel.Name = "UcLabel"
        Me.UcLabel.Padding = New System.Windows.Forms.Padding(1)
        Me.UcLabel.Size = New System.Drawing.Size(604, 115)
        Me.UcLabel.TabIndex = 0
        Me.UcLabel.UCBackColor = System.Drawing.Color.Black
        Me.UcLabel.UCFont = New System.Drawing.Font("B Homa", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.UcLabel.UCForeColor = System.Drawing.Color.White
        Me.UcLabel.UCTextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.UcLabel.UCValue = ""
        '
        'UCBlackListCompositBlackListViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.PnlMain)
        Me.Name = "UCBlackListCompositBlackListViewer"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Size = New System.Drawing.Size(614, 125)
        Me.PnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents UcLabel As R2CoreGUI.UCLabel
End Class
