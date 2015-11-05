Option Explicit On

Imports System.Runtime.InteropServices
Imports EyeXFramework
Imports Tobii.EyeX.Framework

Public Class Form1



    Private Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure


    Private Declare Function GetCursorPos Lib "user32" _
       (ByRef xyScreen As Integer) As Integer


    <DllImport("oleacc.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function AccessibleObjectFromPoint( _
     ByVal x As Integer, _
     ByVal y As Integer, _
    ByRef ppoleAcc As Accessibility.IAccessible, _
    ByRef pvarElement As Object) As Integer
    End Function

    Private Sub Form1_Load(ByVal sender As Object, _
     ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Interval = 250
        Timer1.Enabled = True
        WebBrowser1.Navigate("http://www.htmq.com/html/span.shtml")
    End Sub

    Private Sub Timer1_Tick(ByVal Sender As Object, ByVal e As EventArgs) Handles Timer1.Tick




        Using eyeXHost = New EyeXHost()

            Using dataStream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered)

                eyeXHost.Start()

                AddHandler dataStream.Next, AddressOf OutputGazePoint

                Console.WriteLine("Listening for gaze data, press any key to exit...")
                Console.ReadKey(True)

            End Using

        End Using






        
    End Sub

    Private Sub OutputGazePoint(sender As Object, e As GazePointEventArgs)


        Dim xy(1) As Integer

        GetCursorPos(xy(0))

        Dim objAcc As Accessibility.IAccessible

        Dim child As Object
        AccessibleObjectFromPoint(e.X, e.Y, objAcc, child)

        List1.Items.Clear()
        On Error Resume Next
        Dim ltwh(3) As Integer
        objAcc.accLocation(ltwh(0), ltwh(1), ltwh(2), ltwh(3), child)
        List1.Items.Add("Pos:" _
            & "Left" & CStr(ltwh(0)) & "," _
            & "Top" & CStr(ltwh(1)) & "," _
            & "Width" & CStr(ltwh(2)) & "," _
            & "Height" & CStr(ltwh(3)))
        List1.Items.Add("Name=" & objAcc.accName(child))
        List1.Items.Add("Value=" & objAcc.accValue(child))
        List1.Items.Add("Description=" & objAcc.accDescription(child))
    End Sub

    
End Class
