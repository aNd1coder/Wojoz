----------------------------------------------------------------------
--      SP_<%=this.SourceTable.Name%>_Add
----------------------------------------------------------------------
/*
USE [WojozDB]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_<%=this.SourceTable.Name%>_Add]
    <%for(int i=0; i<this.SourceTable.NonPrimaryKeyColumns.Count;i++){%>
    <%if(GetCSharpDataTypeByDBColumn(this.SourceTable.NonPrimaryKeyColumns[i]) == "int")
    {
    %>
    @<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%> INT<% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.NonPrimaryKeyColumns[i]) == "DateTime")
    {
    %>
    @<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%> DATETIME<% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.NonPrimaryKeyColumns[i]) == "decimal")
    {
    %>
    @<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%> DECIMAL<% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.NonPrimaryKeyColumns[i]) == "float")
    {
    %>
    @<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%> FLOAT<% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.NonPrimaryKeyColumns[i]) == "double")
    {
    %>
    @<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%> FLOAT<% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.NonPrimaryKeyColumns[i]) == "bool")
    {
    %>
    @<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%> BIT<% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %>
    <%
    }
    else
    {%>
    @<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%> NVARCHAR(<%=this.SourceTable.NonPrimaryKeyColumns[i].Size%>)<% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %>
    <%
    }
    }
    %>
AS
BEGIN
    INSERT INTO <%=this.SourceTable.Name%> (<%for(int i=0; i<this.SourceTable.NonPrimaryKeyColumns.Count;i++){%><%=this.SourceTable.NonPrimaryKeyColumns[i].Name%><% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %><%}%>)
    VALUES(<%for(int i=0; i<this.SourceTable.NonPrimaryKeyColumns.Count;i++){%><%="@"+this.SourceTable.NonPrimaryKeyColumns[i].Name%><% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %><%}%>)
    SELECT @@identity;
END
GO


----------------------------------------------------------------------
--      SP_<%=this.SourceTable.Name%>_Update
----------------------------------------------------------------------
USE [WojozDB]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_<%=this.SourceTable.Name%>_Update]
    <%for(int i=0; i<this.SourceTable.Columns.Count;i++){%>
    <%if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "int")
    {
    %>
    @<%=this.SourceTable.Columns[i].Name%> INT<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "DateTime")
    {
    %>
    @<%=this.SourceTable.Columns[i].Name%> DATETIME<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "decimal")
    {
    %>
    @<%=this.SourceTable.Columns[i].Name%> DECIMAL<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "float")
    {
    %>
    @<%=this.SourceTable.Columns[i].Name%> FLOAT<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "double")
    {
    %>
    @<%=this.SourceTable.Columns[i].Name%> FLOAT<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
    <%
    }
    %>
    <%else if(GetCSharpDataTypeByDBColumn(this.SourceTable.Columns[i]) == "bool")
    {
    %>
    @<%=this.SourceTable.Columns[i].Name%> BIT<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
    <%
    }
    else
    {%>
    @<%=this.SourceTable.Columns[i].Name%> NVARCHAR(<%=this.SourceTable.Columns[i].Size%>)<% if (i < SourceTable.Columns.Count - 1) { %>,<% } %>
    <%
    }
    }
    %>
AS
BEGIN
    UPDATE dbo.<%=this.SourceTable.Name%> SET 
    <%for(int i=0; i<this.SourceTable.NonPrimaryKeyColumns.Count;i++){%>
    [<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%>] = @<%=this.SourceTable.NonPrimaryKeyColumns[i].Name%><% if (i < SourceTable.NonPrimaryKeyColumns.Count - 1) { %>,<% } %>
    <%}%> 
    WHERE [<%=this.SourceTable.PrimaryKey.MemberColumns[0].Name%>] = @<%=this.SourceTable.PrimaryKey.MemberColumns[0].Name%>
END
GO
*/