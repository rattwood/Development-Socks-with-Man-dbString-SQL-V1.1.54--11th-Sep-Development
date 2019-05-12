
Imports System.Timers

Public Class audioAlarms
    Shared _timer As Timer


    Shared Sub Start()
        _timer = New Timer(3000)
        AddHandler _timer.Elapsed, New ElapsedEventHandler(AddressOf Handler)
        _timer.Enabled = True


        ' Media.SystemSounds.Beep.Play()

    End Sub




    Shared Sub Handler(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        ' Media.SystemSounds.Beep.Play()
        If My.Settings.audioAlarm = True Then
            My.Computer.Audio.Play(My.Resources.toray_warning, AudioPlayMode.WaitToComplete)
        End If
    End Sub



End Class
