﻿Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports QIS.Common

Namespace QIS.Common.BussinessRules
    Public Class ProjectUser
        Inherits BRInteractionBase

#Region " Class Member Declarations "

        Private _projectUserID, _projectID, _UserID As String        
        Private _totalIssue, _totalIssueOpen, _totalIssueFinish, _progress As Decimal

#End Region

        Public Sub New()
            ConnectionString = SysConfig.ConnectionString
        End Sub

#Region " C,R,U,D "
        Public Overrides Function Insert() As Boolean
            Dim cmdToExecute As SqlCommand = New SqlCommand
            cmdToExecute.CommandText = "INSERT INTO projectUser " + _
                                        "(projectUserID, projectID, userID) " + _
                                        "VALUES " + _
                                        "(@projectUserID, @projectID, @userID)"
            cmdToExecute.CommandType = CommandType.Text
            cmdToExecute.Connection = _mainConnection

            Dim strID As String = ID.GenerateIDNumber("projectUser", "projectUserID")

            Try
                cmdToExecute.Parameters.AddWithValue("@projectUserID", strID)
                cmdToExecute.Parameters.AddWithValue("@projectID", _projectID)
                cmdToExecute.Parameters.AddWithValue("@userID", _UserID)                

                ' // Open Connection
                _mainConnection.Open()

                ' // Execute Query
                cmdToExecute.ExecuteNonQuery()

                _projectUserID = strID
                Return True
            Catch ex As Exception
                ExceptionManager.Publish(ex)
            Finally
                _mainConnection.Close()
                cmdToExecute.Dispose()
            End Try
        End Function

        Public Overrides Function Delete() As Boolean
            Dim cmdToExecute As SqlCommand = New SqlCommand
            cmdToExecute.CommandText = "DELETE FROM projectUser " + _
                                        "WHERE projectUserID=@projectUserID"
            cmdToExecute.CommandType = CommandType.Text

            cmdToExecute.Connection = _mainConnection

            Try
                cmdToExecute.Parameters.AddWithValue("@projectUserID", _projectUserID)

                ' // Open Connection
                _mainConnection.Open()

                ' // Execute Query
                cmdToExecute.ExecuteNonQuery()

                Return True
            Catch ex As Exception
                ExceptionManager.Publish(ex)
            Finally
                _mainConnection.Close()
                cmdToExecute.Dispose()
            End Try
        End Function
#End Region

#Region " Custom Functions "
        Public Function SelectProjectByUserID(ByVal UserID As String, Optional ByVal IsAssignedOnly As Boolean = False) As DataTable
            Dim cmdToExecute As SqlCommand = New SqlCommand
            If IsAssignedOnly = False Then
                cmdToExecute.CommandText = "SELECT a.* FROM (SELECT up.*, p.projectName, p.projectDescription, p.projectAliasName, p.isOpenForClient, " + _
                                        "(SELECT caption FROM CommonCode WHERE groupCode='PROJECTSTATUS' AND code=p.projectStatusGCID) AS projectStatusName, " + _
                                        "ISNULL((SELECT CONVERT(VARCHAR,MAX(updateDate),106) + ' ' + CONVERT(VARCHAR,MAX(updateDate),108) FROM issue WHERE projectID=p.projectID),'-') AS lastUpdateDate, " + _
                                        "totalIssue = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID), " + _
                                        "totalOpen = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND issueStatusSCode<>'003'), " + _
                                        "totalFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND issueStatusSCode='003') " + _
                                        "FROM projectUser up " + _
                                        "INNER JOIN Project p ON up.projectID=p.projectID " + _
                                        "WHERE up.UserID=@UserID) a " + _
                                        "ORDER BY a.totalOpen DESC"
            Else
                cmdToExecute.CommandText = "SELECT a.* FROM (SELECT up.*, p.projectName, p.projectDescription, p.projectAliasName, p.isOpenForClient, " + _
                                        "(SELECT caption FROM CommonCode WHERE groupCode='PROJECTSTATUS' AND code=p.projectStatusGCID) AS projectStatusName, " + _
                                        "ISNULL((SELECT CONVERT(VARCHAR,MAX(updateDate),106) + ' ' + CONVERT(VARCHAR,MAX(updateDate),108) FROM issue WHERE projectID=p.projectID),'-') AS lastUpdateDate, " + _
                                        "totalIssue = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND UserIDAssignedTo=@UserID), " + _
                                        "totalOpen = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND UserIDAssignedTo=@UserID AND issueStatusSCode<>'003'), " + _
                                        "totalFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND UserIDAssignedTo=@UserID AND issueStatusSCode='003') " + _
                                        "FROM projectUser up " + _
                                        "INNER JOIN Project p ON up.projectID=p.projectID " + _
                                        "WHERE up.UserID=@UserID AND up.projectID IN " + _
                                        "(SELECT DISTINCT projectID FROM Issue WHERE UserIDAssignedTo=@UserID)) a " + _
                                        "ORDER BY a.totalOpen DESC"
            End If
            cmdToExecute.CommandType = CommandType.Text
            Dim toReturn As DataTable = New DataTable("projectByUserID")
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmdToExecute)

            ' // Use base class' connection object
            cmdToExecute.Connection = _mainConnection

            Try
                cmdToExecute.Parameters.AddWithValue("@UserID", UserID)

                ' // Open connection.
                _mainConnection.Open()

                ' // Execute query.
                adapter.Fill(toReturn)

                Return toReturn
            Catch ex As Exception
                ' // some error occured. Bubble it to caller and encapsulate Exception object
                Throw New Exception(ex.Message)
            Finally
                ' // Close connection.
                _mainConnection.Close()
                cmdToExecute.Dispose()
                adapter.Dispose()
            End Try
        End Function

        Public Function SelectProjectGroupByUserID(ByVal UserID As String, Optional ByVal IsAssignedOnly As Boolean = False) As DataTable
            Dim cmdToExecute As SqlCommand = New SqlCommand
            If IsAssignedOnly = False Then
                cmdToExecute.CommandText = "SELECT DISTINCT pg.sequence, pg.projectGroupID, pg.projectGroupName " + _
                                        "FROM ( " + _
                                        "SELECT pg.sequence, pg.projectGroupID, pg.projectGroupName " + _
                                        "FROM projectUser up " + _
                                        "INNER JOIN Project p ON up.projectID=p.projectID " + _
                                        "INNER JOIN ProjectGroup pg ON p.projectGroupID=pg.projectGroupID " + _
                                        "WHERE up.UserID=@UserID " + _
                                        "UNION ALL " + _
                                        "SELECT pg.sequence, pg.projectGroupID, pg.projectGroupName " + _
                                        "FROM userProfile up " + _
                                        "INNER JOIN projectProfile pp ON up.profileID = pp.profileID " + _
                                        "INNER JOIN Project p ON pp.projectID=p.projectID " + _
                                        "INNER JOIN ProjectGroup pg ON p.projectGroupID=pg.projectGroupID " + _
                                        "WHERE up.UserID=@UserID " + _
                                        ") pg " + _
                                        "ORDER BY pg.sequence, pg.projectGroupID, pg.projectGroupName ASC"
            Else
                cmdToExecute.CommandText = "SELECT DISTINCT pg.sequence, pg.projectGroupID, pg.projectGroupName " + _
                                        "FROM ( " + _
                                        "SELECT pg.sequence, pg.projectGroupID, pg.projectGroupName " + _
                                        "FROM projectUser up " + _
                                        "INNER JOIN Project p ON up.projectID=p.projectID " + _
                                        "INNER JOIN ProjectGroup pg ON p.projectGroupID=pg.projectGroupID " + _
                                        "WHERE up.UserID=@UserID AND up.projectID IN " + _
                                        "(SELECT DISTINCT projectID FROM Issue WHERE UserIDAssignedTo=@UserID) " + _
                                        "UNION ALL " + _
                                        "SELECT pg.sequence, pg.projectGroupID, pg.projectGroupName " + _
                                        "FROM userProfile up " + _
                                        "INNER JOIN projectProfile pp ON up.profileID = pp.profileID " + _
                                        "INNER JOIN Project p ON pp.projectID=p.projectID " + _
                                        "INNER JOIN ProjectGroup pg ON p.projectGroupID=pg.projectGroupID " + _
                                        "WHERE up.UserID=@UserID AND pp.projectID IN " + _
                                        "(SELECT DISTINCT projectID FROM Issue WHERE UserIDAssignedTo=@UserID) " + _
                                        ") pg " + _
                                        "ORDER BY pg.sequence, pg.projectGroupID, pg.projectGroupName ASC"
            End If
            cmdToExecute.CommandType = CommandType.Text
            Dim toReturn As DataTable = New DataTable("projectGroupByUserID")
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmdToExecute)

            ' // Use base class' connection object
            cmdToExecute.Connection = _mainConnection

            Try
                cmdToExecute.Parameters.AddWithValue("@UserID", UserID)

                ' // Open connection.
                _mainConnection.Open()

                ' // Execute query.
                adapter.Fill(toReturn)

                Return toReturn
            Catch ex As Exception
                ' // some error occured. Bubble it to caller and encapsulate Exception object
                Throw New Exception(ex.Message)
            Finally
                ' // Close connection.
                _mainConnection.Close()
                cmdToExecute.Dispose()
                adapter.Dispose()
            End Try
        End Function

        Public Function SelectProjectByProjectGroupID(ByVal ProjectGroupID As String, ByVal UserID As String, Optional ByVal IsAssignedOnly As Boolean = False) As DataTable
            Dim cmdToExecute As SqlCommand = New SqlCommand
            If IsAssignedOnly = False Then
                cmdToExecute.CommandText = "SELECT DISTINCT b.* FROM ( " + _
                                        "SELECT a.*, progress=CASE WHEN(a.totalIssue<>0) THEN(CEILING(CAST(a.totalFinish AS DECIMAL)/CAST(a.totalIssue AS DECIMAL)*100)) " + _
                                        "ELSE(100) END " + _
                                        "FROM (" + _
                                        "SELECT up.projectUserID, up.projectID, up.userID, p.projectName, p.projectDescription, p.projectAliasName, p.HEXColorID, p.isOpenForClient, " + _
                                        "(SELECT caption FROM CommonCode WHERE groupCode='PROJECTSTATUS' AND code=p.projectStatusGCID) AS projectStatusName, " + _
                                        "ISNULL((SELECT CONVERT(VARCHAR,MAX(updateDate),106) + ' ' + CONVERT(VARCHAR,MAX(updateDate),108) FROM issue WHERE projectID=p.projectID),'-') AS lastUpdateDate, " + _
                                        "totalIssue = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID), " + _
                                        "totalOpen = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND issueStatusSCode<>'003'), " + _
                                        "totalDevFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND issueStatusSCode='002-1'), " + _
                                        "totalFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND issueStatusSCode='003') " + _
                                        "FROM projectUser up " + _
                                        "INNER JOIN Project p ON up.projectID=p.projectID " + _
                                        "WHERE p.projectGroupID=@projectGroupID AND up.UserID=@UserID) a " + _
                                        "UNION ALL " + _
                                        "SELECT a.*, progress=CASE WHEN(a.totalIssue<>0) THEN(CEILING(CAST(a.totalFinish AS DECIMAL)/CAST(a.totalIssue AS DECIMAL)*100)) " + _
                                        "ELSE(100) END " + _
                                        "FROM (" + _
                                        "SELECT pp.projectProfileID, pp.projectID, pp.profileID, p.projectName, p.projectDescription, p.projectAliasName, p.HEXColorID, p.isOpenForClient, " + _
                                        "(SELECT caption FROM CommonCode WHERE groupCode='PROJECTSTATUS' AND code=p.projectStatusGCID) AS projectStatusName, " + _
                                        "ISNULL((SELECT CONVERT(VARCHAR,MAX(updateDate),106) + ' ' + CONVERT(VARCHAR,MAX(updateDate),108) FROM issue WHERE projectID=p.projectID),'-') AS lastUpdateDate, " + _
                                        "totalIssue = (SELECT COUNT(issueID) FROM issue WHERE projectID=pp.projectID), " + _
                                        "totalOpen = (SELECT COUNT(issueID) FROM issue WHERE projectID=pp.projectID AND issueStatusSCode<>'003'), " + _
                                        "totalDevFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=pp.projectID AND issueStatusSCode='002-1'), " + _
                                        "totalFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=pp.projectID AND issueStatusSCode='003') " + _
                                        "FROM userProfile up " + _
                                        "INNER JOIN projectProfile pp ON up.ProfileID = pp.ProfileID " + _
                                        "INNER JOIN Project p ON pp.projectID=p.projectID " + _
                                        "WHERE p.projectGroupID=@projectGroupID AND up.UserID=@UserID) a " + _
                                        ") b " + _
                                        "ORDER BY b.progress ASC"
            Else
                cmdToExecute.CommandText = "SELECT DISTINCT b.* FROM ( " + _
                                        "SELECT a.*, progress=CASE WHEN(a.totalIssue<>0) THEN(CEILING(CAST(a.totalFinish AS DECIMAL)/CAST(a.totalIssue AS DECIMAL)*100)) " + _
                                        "ELSE(100) END " + _
                                        "FROM (" + _
                                        "SELECT up.projectUserID, up.projectID, up.userID, p.projectName, p.projectDescription, p.projectAliasName, p.HEXColorID, p.isOpenForClient, " + _
                                        "(SELECT caption FROM CommonCode WHERE groupCode='PROJECTSTATUS' AND code=p.projectStatusGCID) AS projectStatusName, " + _
                                        "ISNULL((SELECT CONVERT(VARCHAR,MAX(updateDate),106) + ' ' + CONVERT(VARCHAR,MAX(updateDate),108) FROM issue WHERE projectID=p.projectID),'-') AS lastUpdateDate, " + _
                                        "totalIssue = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND UserIDAssignedTo=@UserID), " + _
                                        "totalOpen = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND UserIDAssignedTo=@UserID AND issueStatusSCode<>'003'), " + _
                                        "totalDevFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND UserIDAssignedTo=@UserID AND issueStatusSCode='002-1'), " + _
                                        "totalFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=up.projectID AND UserIDAssignedTo=@UserID AND issueStatusSCode='003') " + _
                                        "FROM projectUser up " + _
                                        "INNER JOIN Project p ON up.projectID=p.projectID " + _
                                        "WHERE p.projectGroupID=@projectGroupID AND up.UserID=@UserID AND up.projectID IN " + _
                                        "(SELECT DISTINCT projectID FROM Issue WHERE UserIDAssignedTo=@UserID) " + _
                                        ") a " + _
                                        "UNION ALL " + _
                                        "SELECT a.*, progress=CASE WHEN(a.totalIssue<>0) THEN(CEILING(CAST(a.totalFinish AS DECIMAL)/CAST(a.totalIssue AS DECIMAL)*100)) " + _
                                        "ELSE(100) END " + _
                                        "FROM (" + _
                                        "SELECT pp.projectProfileID, pp.projectID, pp.profileID, p.projectName, p.projectDescription, p.projectAliasName, p.HEXColorID, p.isOpenForClient, " + _
                                        "(SELECT caption FROM CommonCode WHERE groupCode='PROJECTSTATUS' AND code=p.projectStatusGCID) AS projectStatusName, " + _
                                        "ISNULL((SELECT CONVERT(VARCHAR,MAX(updateDate),106) + ' ' + CONVERT(VARCHAR,MAX(updateDate),108) FROM issue WHERE projectID=p.projectID),'-') AS lastUpdateDate, " + _
                                        "totalIssue = (SELECT COUNT(issueID) FROM issue WHERE projectID=pp.projectID AND UserIDAssignedTo=@UserID), " + _
                                        "totalOpen = (SELECT COUNT(issueID) FROM issue WHERE projectID=pp.projectID AND UserIDAssignedTo=@UserID AND issueStatusSCode<>'003'), " + _
                                        "totalDevFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=pp.projectID AND UserIDAssignedTo=@UserID AND issueStatusSCode='002-1'), " + _
                                        "totalFinish = (SELECT COUNT(issueID) FROM issue WHERE projectID=pp.projectID AND UserIDAssignedTo=@UserID AND issueStatusSCode='003') " + _
                                        "FROM userProfile up " + _
                                        "INNER JOIN projectProfile pp ON up.ProfileID = pp.ProfileID " + _
                                        "INNER JOIN Project p ON pp.projectID=p.projectID " + _
                                        "WHERE p.projectGroupID=@projectGroupID AND up.UserID=@UserID AND pp.projectID IN " + _
                                        "(SELECT DISTINCT projectID FROM Issue WHERE UserIDAssignedTo=@UserID) " + _
                                        ") a " + _
                                        ") b " + _
                                        "ORDER BY b.progress ASC"
            End If
            cmdToExecute.CommandType = CommandType.Text
            Dim toReturn As DataTable = New DataTable("projectByProjectGroupID")
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmdToExecute)

            ' // Use base class' connection object
            cmdToExecute.Connection = _mainConnection

            Try
                cmdToExecute.Parameters.AddWithValue("@projectGroupID", ProjectGroupID)
                cmdToExecute.Parameters.AddWithValue("@UserID", UserID)

                ' // Open connection.
                _mainConnection.Open()

                ' // Execute query.
                adapter.Fill(toReturn)

                Return toReturn
            Catch ex As Exception
                ' // some error occured. Bubble it to caller and encapsulate Exception object
                Throw New Exception(ex.Message)
            Finally
                ' // Close connection.
                _mainConnection.Close()
                cmdToExecute.Dispose()
                adapter.Dispose()
            End Try
        End Function

        Public Function SelectProjectProgressByProjectGroupID(ByVal ProjectGroupID As String, ByVal UserID As String, Optional ByVal IsAssignedOnly As Boolean = False) As DataTable
            Dim cmdToExecute As SqlCommand = New SqlCommand
            If IsAssignedOnly = False Then
                cmdToExecute.CommandText = "spSelectProgressByUserIDProjectGroup"
            Else
                cmdToExecute.CommandText = "spSelectProgressByUserIDProjectGroupAssigned"
            End If
            cmdToExecute.CommandType = CommandType.StoredProcedure
            Dim toReturn As DataTable = New DataTable("spSelectProgressByUserIDProjectGroup")
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmdToExecute)

            ' // Use base class' connection object
            cmdToExecute.Connection = _mainConnection

            Try
                cmdToExecute.Parameters.AddWithValue("@projectGroupID", ProjectGroupID)
                cmdToExecute.Parameters.AddWithValue("@UserID", UserID)

                ' // Open connection.
                _mainConnection.Open()

                ' // Execute query.
                adapter.Fill(toReturn)

                If toReturn.Rows.Count > 0 Then
                    _totalIssue = CType(toReturn.Rows(0)("totalIssue"), Decimal)
                    _totalIssueOpen = CType(toReturn.Rows(0)("totalOpen"), Decimal)
                    _totalIssueFinish = CType(toReturn.Rows(0)("totalFinish"), Decimal)
                    _progress = CType(toReturn.Rows(0)("progress"), Decimal)
                End If

                Return toReturn
            Catch ex As Exception
                ' // some error occured. Bubble it to caller and encapsulate Exception object
                Throw New Exception(ex.Message)
            Finally
                ' // Close connection.
                _mainConnection.Close()
                cmdToExecute.Dispose()
                adapter.Dispose()
            End Try
        End Function

        Public Function SelectProjectNotInUserProjectByUserID(ByVal UserID As String) As DataTable
            Dim cmdToExecute As SqlCommand = New SqlCommand
            cmdToExecute.CommandText = "SELECT * FROM Project WHERE projectID NOT IN (SELECT projectID FROM projectUser WHERE UserID=@UserID)"
            cmdToExecute.CommandType = CommandType.Text
            Dim toReturn As DataTable = New DataTable("ProjectNotInUserProjectByUserID")
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmdToExecute)

            ' // Use base class' connection object
            cmdToExecute.Connection = _mainConnection

            Try
                cmdToExecute.Parameters.AddWithValue("@UserID", UserID)

                ' // Open connection.
                _mainConnection.Open()

                ' // Execute query.
                adapter.Fill(toReturn)

                Return toReturn
            Catch ex As Exception
                ' // some error occured. Bubble it to caller and encapsulate Exception object
                Throw New Exception(ex.Message)
            Finally
                ' // Close connection.
                _mainConnection.Close()
                cmdToExecute.Dispose()
                adapter.Dispose()
            End Try
        End Function
#End Region

#Region " Class Property Declarations "
        Public Property [ProjectUserID]() As String
            Get
                Return _projectUserID
            End Get
            Set(ByVal Value As String)
                _projectUserID = Value
            End Set
        End Property

        Public Property [UserID]() As String
            Get
                Return _UserID
            End Get
            Set(ByVal Value As String)
                _UserID = Value
            End Set
        End Property

        Public Property [ProjectID]() As String
            Get
                Return _projectID
            End Get
            Set(ByVal Value As String)
                _projectID = Value
            End Set
        End Property

        Public Property [TotalIssue]() As Decimal
            Get
                Return _totalIssue
            End Get
            Set(ByVal Value As Decimal)
                _totalIssue = Value
            End Set
        End Property

        Public Property [TotalIssueOpen]() As Decimal
            Get
                Return _totalIssueOpen
            End Get
            Set(ByVal Value As Decimal)
                _totalIssueOpen = Value
            End Set
        End Property

        Public Property [TotalIssueFinish]() As Decimal
            Get
                Return _totalIssueFinish
            End Get
            Set(ByVal Value As Decimal)
                _totalIssueFinish = Value
            End Set
        End Property

        Public Property [Progress]() As Decimal
            Get
                Return _progress
            End Get
            Set(ByVal Value As Decimal)
                _progress = Value
            End Set
        End Property
#End Region

    End Class
End Namespace
