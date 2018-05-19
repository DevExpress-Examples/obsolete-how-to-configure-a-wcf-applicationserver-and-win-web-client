Namespace ClientServer_Wcf_StandardAuth.Win
    Partial Public Class ClientServer_Wcf_StandardAuthWindowsFormsApplication
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
            Me.module3 = New ClientServer_Wcf_StandardAuth.Module.ClientServer_Wcf_StandardAuthModule()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' ClientServer_Wcf_StandardAuthWindowsFormsApplication
            ' 
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.securityModule1)
            Me.Modules.Add(Me.module3)
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        #End Region

        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
        Private module3 As ClientServer_Wcf_StandardAuth.Module.ClientServer_Wcf_StandardAuthModule
        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
    End Class
End Namespace
