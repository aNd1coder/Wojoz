﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace EntLib.Base.Doc
{
  public  class WordHelper
    {
     
        //private Microsoft.Office.Interop.Word.ApplicationClass oWordApplic; 
        //private Microsoft.Office.Interop.Word.Document oDoc;
        //object missing = System.Reflection.Missing.Value;   
  
        //public Microsoft.Office.Interop.Word.ApplicationClass WordApplication   
        //{   
        //    get { return oWordApplic; }   
        //}   
  
        //public WordHelper()   
        //{     
        //    oWordApplic = new Microsoft.Office.Interop.Word.ApplicationClass();   
        //}

        //public WordHelper(Microsoft.Office.Interop.Word.ApplicationClass wordapp)   
        //{   
        //    oWordApplic = wordapp;   
        //}  
 
        //#region 文件操作   
  
        //// Open a file (the file must exists) and activate it   
        //public void Open(string strFileName)   
        //{   
        //    object fileName = strFileName;
        //    object readOnly = false;
        //    object isVisible = true;   
  
        //    oDoc = oWordApplic.Documents.Open(ref fileName, ref missing, ref readOnly,   
        //        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,   
        //        ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);   
  
        //    oDoc.Activate();   
        //}   
  
        //// Open a new document   
        //public void Open()   
        //{   
        //    oDoc = oWordApplic.Documents.Add(ref missing, ref missing, ref missing, ref missing);   
  
        //    oDoc.Activate();   
        //}   
  
        //public void Quit()   
        //{   
        //    oWordApplic.Application.Quit(ref missing, ref missing, ref missing);   
        //}   
  
        ///// <summary>   
        ///// 附加dot模版文件   
        ///// </summary>   
        //private void LoadDotFile(string strDotFile)   
        //{   
        //    if (!string.IsNullOrEmpty(strDotFile))   
        //    {   
        //        Microsoft.Office.Interop.Word.Document wDot = null;   
        //        if (oWordApplic != null)   
        //        {   
        //            oDoc = oWordApplic.ActiveDocument;   
  
        //            oWordApplic.Selection.WholeStory();   
  
        //            //string strContent = oWordApplic.Selection.Text;   
  
        //            oWordApplic.Selection.Copy();   
        //            wDot = CreateWordDocument(strDotFile, true);   
  
        //            object bkmC = "Content";   
  
        //            if (oWordApplic.ActiveDocument.Bookmarks.Exists("Content") == true)   
        //            {   
        //                oWordApplic.ActiveDocument.Bookmarks.get_Item   
        //                (ref bkmC).Select();   
        //            }   
  
        //            //对标签"Content"进行填充   
        //            //直接写入内容不能识别表格什么的   
        //            //oWordApplic.Selection.TypeText(strContent);   
        //            oWordApplic.Selection.Paste();   
        //            oWordApplic.Selection.WholeStory();   
        //            oWordApplic.Selection.Copy();   
        //            wDot.Close(ref missing, ref missing, ref missing);   
  
        //            oDoc.Activate();   
        //            oWordApplic.Selection.Paste();   
  
        //        }   
        //    }   
        //}   
  
        /////     
        ///// 打开Word文档,并且返回对象oDoc   
        ///// 完整Word文件路径+名称     
        ///// 返回的Word.Document oDoc对象    
        //public Microsoft.Office.Interop.Word.Document CreateWordDocument(string FileName, bool HideWin)   
        //{   
        //    if (FileName == "") return null;   
  
        //    oWordApplic.Visible = HideWin;   
        //    oWordApplic.Caption = "";   
        //    oWordApplic.Options.CheckSpellingAsYouType = false;   
        //    oWordApplic.Options.CheckGrammarAsYouType = false;   
  
        //    Object filename = FileName;   
        //    Object ConfirmConversions = false;   
        //    Object ReadOnly = true;   
        //    Object AddToRecentFiles = false;   
  
        //    Object PasswordDocument = System.Type.Missing;   
        //    Object PasswordTemplate = System.Type.Missing;   
        //    Object Revert = System.Type.Missing;   
        //    Object WritePasswordDocument = System.Type.Missing;   
        //    Object WritePasswordTemplate = System.Type.Missing;   
        //    Object Format = System.Type.Missing;   
        //    Object Encoding = System.Type.Missing;   
        //    Object Visible = System.Type.Missing;   
        //    Object OpenAndRepair = System.Type.Missing;   
        //    Object DocumentDirection = System.Type.Missing;   
        //    Object NoEncodingDialog = System.Type.Missing;   
        //    Object XMLTransform = System.Type.Missing;   
        //    try  
        //    {   
        //        Microsoft.Office.Interop.Word.Document wordDoc = oWordApplic.Documents.Open(ref filename, ref ConfirmConversions,   
        //        ref ReadOnly, ref AddToRecentFiles, ref PasswordDocument, ref PasswordTemplate,   
        //        ref Revert, ref WritePasswordDocument, ref WritePasswordTemplate, ref Format,   
        //        ref Encoding, ref Visible, ref OpenAndRepair, ref DocumentDirection,   
        //        ref NoEncodingDialog, ref XMLTransform);   
        //        return wordDoc;   
  
        //    }   
        //    catch (Exception ex)   
        //    {
        //        throw new Exception(ex.Message);   
        //    }   
        //}   
  
        //public void SaveAs(Microsoft.Office.Interop.Word.Document oDoc, string strFileName)   
        //{   
        //    object fileName = strFileName;   
        //    if (File.Exists(strFileName))   
        //    {
        //        throw new Exception("文件已经存在!");
        //    }   
        //    else  
        //    {   
        //        oDoc.SaveAs(ref fileName, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,   
        //                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);   
        //    }   
        //}   
  
        //public void SaveAsHtml(Microsoft.Office.Interop.Word.Document oDoc, string strFileName)   
        //{   
        //    object fileName = strFileName;   
  
        //    //wdFormatWebArchive保存为单个网页文件   
        //    //wdFormatFilteredHTML保存为过滤掉word标签的htm文件，缺点是有图片的话会产生网页文件夹   
        //    if (File.Exists(strFileName))   
        //    {
        //        throw new Exception("文件已经存在!");
        //    }   
        //    else  
        //    {   
        //        object Format = (int)Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatWebArchive;   
        //        oDoc.SaveAs(ref fileName, ref Format, ref missing, ref missing, ref missing, ref missing, ref missing,   
        //            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);   
        //    }   
        //}   
  
        //public void Save()   
        //{   
        //    oDoc.Save();
        //}

        //public void Close()
        //{
        //    oDoc.Close(ref missing, ref missing, ref missing);
        //    //关闭wordApp组件对象 
        //    oWordApplic.Quit(ref missing, ref missing, ref missing); 
        //}
  
        //public void SaveAs(string strFileName)   
        //{   
        //    object FileName =strFileName;
        //    object FileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF;
        //    object LockComments = false;
        //    object AddToRecentFiles = true;
        //    object ReadOnlyRecommended = false;
        //    object EmbedTrueTypeFonts = false;
        //    object SaveNativePictureFormat = true;
        //    object SaveFormsData = true;
        //    object SaveAsAOCELetter = false;
        //    object Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingEBCDICSimplifiedChineseExtendedAndSimplifiedChinese;
        //    object InsertLineBreaks = false;
        //    object AllowSubstitutions = false;
        //    object LineEnding = Microsoft.Office.Interop.Word.WdLineEndingType.wdCRLF;
        //    object AddBiDiMarks = false;

        //    try
        //    {
        //        oDoc.SaveAs(ref FileName, ref FileFormat, ref LockComments,
        //            ref missing, ref AddToRecentFiles, ref missing,
        //            ref ReadOnlyRecommended, ref EmbedTrueTypeFonts,
        //            ref SaveNativePictureFormat, ref SaveFormsData,
        //            ref SaveAsAOCELetter, ref Encoding, ref InsertLineBreaks,
        //            ref AllowSubstitutions, ref LineEnding, ref AddBiDiMarks);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}   
  
        //// Save the document in HTML format   
        //public void SaveAsHtml(string strFileName)   
        //{   
        //    object fileName = strFileName;   
        //    object Format = (int)Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;   
        //    oDoc.SaveAs(ref fileName, ref Format, ref missing, ref missing, ref missing, ref missing, ref missing,   
        //        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);   
        //}  
 
        //#endregion  
 
        //#region 添加菜单(工具栏)项   
  
        ////添加单独的菜单项   
        //public void AddMenu(Microsoft.Office.Core.CommandBarPopup popuBar)   
        //{   
        //    Microsoft.Office.Core.CommandBar menuBar = null;   
        //    menuBar = this.oWordApplic.CommandBars["Menu Bar"];   
        //    popuBar = (Microsoft.Office.Core.CommandBarPopup)this.oWordApplic.CommandBars.FindControl(Microsoft.Office.Core.MsoControlType.msoControlPopup, missing, popuBar.Tag, true);   
        //    if (popuBar == null)   
        //    {   
        //        popuBar = (Microsoft.Office.Core.CommandBarPopup)menuBar.Controls.Add(Microsoft.Office.Core.MsoControlType.msoControlPopup, missing, missing, missing, missing);   
        //    }   
        //}   
  
        ////添加单独工具栏   
        //public void AddToolItem(string strBarName,string strBtnName)   
        //{   
        //    Microsoft.Office.Core.CommandBar toolBar = null;   
        //    toolBar = (Microsoft.Office.Core.CommandBar)this.oWordApplic.CommandBars.FindControl(Microsoft.Office.Core.MsoControlType.msoControlButton, missing, strBarName, true);   
        //    if (toolBar == null)   
        //    {   
        //        toolBar = (Microsoft.Office.Core.CommandBar)this.oWordApplic.CommandBars.Add(   
        //             Microsoft.Office.Core.MsoControlType.msoControlButton,   
        //             missing, missing, missing);   
        //        toolBar.Name = strBtnName;   
        //        toolBar.Visible = true;   
        //    }   
        //}  
 
        //#endregion  
 
        //#region 移动光标位置   
  
        //// Go to a predefined bookmark, if the bookmark doesn't exists the application will raise an error   
        //public void GotoBookMark(string strBookMarkName)   
        //{   
        //    // VB :  Selection.GoTo What:=wdGoToBookmark, Name:="nome"   
        //    object Bookmark = (int)Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark;   
        //    object NameBookMark = strBookMarkName;   
        //    oWordApplic.Selection.GoTo(ref Bookmark, ref missing, ref missing, ref NameBookMark);   
        //}   
  
        //public void GoToTheEnd()   
        //{   
        //    // VB :  Selection.EndKey Unit:=wdStory   
        //    object unit;   
        //    unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;   
        //    oWordApplic.Selection.EndKey(ref unit, ref missing);   
        //}   
  
        //public void GoToLineEnd()   
        //{
        //    object unit = Microsoft.Office.Interop.Word.WdUnits.wdLine;   
        //    object ext = Microsoft.Office.Interop.Word.WdMovementType.wdExtend;   
        //    oWordApplic.Selection.EndKey(ref unit, ref ext);   
        //}   
  
        //public void GoToTheBeginning()   
        //{   
        //    // VB : Selection.HomeKey Unit:=wdStory   
        //    object unit;   
        //    unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;   
        //    oWordApplic.Selection.HomeKey(ref unit, ref missing);   
        //}   
  
        //public void GoToTheTable(int ntable)   
        //{
        //    object what;   
        //    what = Microsoft.Office.Interop.Word.WdUnits.wdTable;   
        //    object which;   
        //    which = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst;   
        //    object count;   
        //    count = 1;   
        //    oWordApplic.Selection.GoTo(ref what, ref which, ref count, ref missing);   
        //    oWordApplic.Selection.Find.ClearFormatting();   
  
        //    oWordApplic.Selection.Text = "";   
        //}   
  
        //public void GoToRightCell()   
        //{   
        //    // Selection.MoveRight Unit:=wdCell   
        //    object direction;   
        //    direction = Microsoft.Office.Interop.Word.WdUnits.wdCell;   
        //    oWordApplic.Selection.MoveRight(ref direction, ref missing, ref missing);   
        //}   
  
        //public void GoToLeftCell()   
        //{   
        //    // Selection.MoveRight Unit:=wdCell   
        //    object direction;   
        //    direction = Microsoft.Office.Interop.Word.WdUnits.wdCell;   
        //    oWordApplic.Selection.MoveLeft(ref direction, ref missing, ref missing);   
        //}   
  
        //public void GoToDownCell()   
        //{   
        //    // Selection.MoveRight Unit:=wdCell   
        //    object direction;   
        //    direction = Microsoft.Office.Interop.Word.WdUnits.wdLine;   
        //    oWordApplic.Selection.MoveDown(ref direction, ref missing, ref missing);   
        //}   
  
        //public void GoToUpCell()   
        //{   
        //    // Selection.MoveRight Unit:=wdCell   
        //    object direction;   
        //    direction = Microsoft.Office.Interop.Word.WdUnits.wdLine;   
        //    oWordApplic.Selection.MoveUp(ref direction, ref missing, ref missing);   
        //}  
 
        //#endregion  
 
        //#region 插入操作   
  
        //public void InsertText(string strText)   
        //{   
        //    oWordApplic.Selection.TypeText(strText);   
        //}   
  
        //public void InsertLineBreak()   
        //{   
        //    oWordApplic.Selection.TypeParagraph();   
        //}   
  
        ///// <summary>   
        ///// 插入多个空行   
        ///// </summary>   
        ///// <param name="nline"></param>   
        //public void InsertLineBreak(int nline)   
        //{   
        //    for (int i = 0; i < nline; i++)   
        //        oWordApplic.Selection.TypeParagraph();   
        //}   
  
        //public void InsertPagebreak()   
        //{   
        //    // VB : Selection.InsertBreak Type:=wdPageBreak   
        //    object pBreak = (int)Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;   
        //    oWordApplic.Selection.InsertBreak(ref pBreak);   
        //}   
  
        //// 插入页码   
        //public void InsertPageNumber()   
        //{   
        //    object wdFieldPage = Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage;   
        //    object preserveFormatting = true;   
        //    oWordApplic.Selection.Fields.Add(oWordApplic.Selection.Range, ref wdFieldPage, ref missing, ref preserveFormatting);   
        //}
  
        //// 插入页码   
        //public void InsertPageNumber(string strAlign)   
        //{   
        //    object wdFieldPage = Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage;   
        //    object preserveFormatting = true;   
        //    oWordApplic.Selection.Fields.Add(oWordApplic.Selection.Range, ref wdFieldPage, ref missing, ref preserveFormatting);   
        //    SetAlignment(strAlign);
        //}   
  
        //public void InsertImage(string strPicPath, float picWidth, float picHeight)   
        //{   
        //    string FileName = strPicPath;   
        //    object LinkToFile = false;   
        //    object SaveWithDocument = true;   
        //    object Anchor = oWordApplic.Selection.Range;   
        //    oWordApplic.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor).Select();

        //    oWordApplic.Selection.InlineShapes[1].Width = picWidth; // 图片宽度    
        //    oWordApplic.Selection.InlineShapes[1].Height = picHeight; // 图片高度
  
        //    // 将图片设置为四面环绕型
        //    Microsoft.Office.Interop.Word.Shape s = oWordApplic.Selection.InlineShapes[1].ConvertToShape();   
        //    s.WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapInline;   
        //}   
  
        //public void InsertLine(float left, float top, float width, float weight, int r, int g, int b)   
        //{   
        //    //SetFontColor("red");   
        //    //SetAlignment("Center");   
        //    object Anchor = oWordApplic.Selection.Range;   
        //    //int pLeft = 0, pTop = 0, pWidth = 0, pHeight = 0;   
        //    //oWordApplic.ActiveWindow.GetPoint(out pLeft, out pTop, out pWidth, out pHeight,missing);   
        //    //MessageBox.Show(pLeft + "," + pTop + "," + pWidth + "," + pHeight);   
        //    object rep = false;   
        //    //left += oWordApplic.ActiveDocument.PageSetup.LeftMargin;   
        //    left = oWordApplic.CentimetersToPoints(left);   
        //    top = oWordApplic.CentimetersToPoints(top);   
        //    width = oWordApplic.CentimetersToPoints(width);   
        //    Microsoft.Office.Interop.Word.Shape s = oWordApplic.ActiveDocument.Shapes.AddLine(0, top, width, top, ref Anchor);   
        //    s.Line.ForeColor.RGB = RGB(r, g, b);   
        //    s.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;   
        //    s.Line.Style = Microsoft.Office.Core.MsoLineStyle.msoLineSingle;   
        //    s.Line.Weight = weight;   
        //}


        ///// <summary>
        ///// 添加页眉
        ///// </summary>
        ///// <param name="Content">页眉内容</param>
        ///// <param name="Alignment">对齐文式</param>
        //public void InsertHeader(string Content, string Alignment)
        //{
        //    oWordApplic.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdOutlineView;
        //    oWordApplic.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekPrimaryHeader;
        //    oWordApplic.ActiveWindow.ActivePane.Selection.InsertAfter(Content);
        //    SetAlignment(Alignment);// 设置右对齐
        //    oWordApplic.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;
        //}

        ///// <summary>
        ///// 添加页脚
        ///// </summary>
        ///// <param name="Content">页脚内容</param>
        ///// <param name="Alignment">对齐文式</param>
        //public void InsertFooter(string Content, string Alignment)
        //{
        //    oWordApplic.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdOutlineView;
        //    oWordApplic.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekPrimaryFooter;
        //    oWordApplic.ActiveWindow.ActivePane.Selection.InsertAfter(Content);
        //    SetAlignment(Alignment);// 设置对齐
        //    oWordApplic.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;
        //}

        ///// <summary>
        ///// 插入页码
        ///// </summary>
        ///// <param name="strformat">样式</param>
        ///// <param name="strAlign">格式</param>
        //public void InsertAllPageNumber(string strformat,string strAlign)
        //{
        //    object IncludeFootnotesAndEndnotes = false;
        //    int pageSum =oDoc.ComputeStatistics(Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages, ref IncludeFootnotesAndEndnotes);
        //    GoToTheBeginning();
        //    for (int i = 0; i < pageSum;i++ )
        //    {
        //        InsertPageNumber();
        //        oDoc.Application.Browser.Next();//下一页
        //    }
        //}
 
        //#endregion  
 
        //#region 设置样式   
  
        ///// <summary>   
        ///// Change the paragraph alignement   
        ///// </summary>   
        ///// <param name="strType"></param>   
        //public void SetAlignment(string strType)   
        //{   
        //    switch (strType.ToLower())   
        //    {   
        //        case "center":   
        //            oWordApplic.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;   
        //            break;   
        //        case "left":   
        //            oWordApplic.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;   
        //            break;   
        //        case "right":   
        //            oWordApplic.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;   
        //            break;   
        //        case "justify":   
        //            oWordApplic.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;   
        //            break;   
        //    }   
  
        //}   
  
  
        //// if you use thif function to change the font you should call it again with    
        //// no parameter in order to set the font without a particular format   
        //public void SetFont(string strType)   
        //{   
        //    switch (strType)   
        //    {   
        //        case "Bold":   
        //            oWordApplic.Selection.Font.Bold = 1;   
        //            break;   
        //        case "Italic":   
        //            oWordApplic.Selection.Font.Italic = 1;   
        //            break;   
        //        case "Underlined":   
        //            oWordApplic.Selection.Font.Subscript = 0;   
        //            break;   
        //    }   
        //}   
  
        //// disable all the style    
        //public void SetFont()   
        //{   
        //    oWordApplic.Selection.Font.Bold = 0;   
        //    oWordApplic.Selection.Font.Italic = 0;   
        //    oWordApplic.Selection.Font.Subscript = 0;   
  
        //}   
  
        //public void SetFontName(string strType)   
        //{   
        //    oWordApplic.Selection.Font.Name = strType;   
        //}   
  
        //public void SetFontSize(float nSize)   
        //{   
        //    SetFontSize(nSize, 100);   
        //}   
  
        //public void SetFontSize(float nSize, int scaling)   
        //{   
        //    if (nSize > 0f)   
        //        oWordApplic.Selection.Font.Size = nSize;   
        //    if (scaling > 0)   
        //        oWordApplic.Selection.Font.Scaling = scaling;   
        //}   
  
        //public void SetFontColor(string strFontColor)   
        //{   
        //    switch (strFontColor.ToLower())   
        //    {   
        //        case "blue":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlue;   
        //            break;   
        //        case "gold":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorGold;   
        //            break;   
        //        case "gray":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorGray875;   
        //            break;   
        //        case "green":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorGreen;   
        //            break;   
        //        case "lightblue":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorLightBlue;   
        //            break;   
        //        case "orange":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorOrange;   
        //            break;   
        //        case "pink":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorPink;   
        //            break;   
        //        case "red":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorRed;   
        //            break;   
        //        case "yellow":   
        //            oWordApplic.Selection.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorYellow;   
        //            break;   
        //    }   
        //}   
  
        //public void SetPageNumberAlign(string strType, bool bHeader)   
        //{   
        //    object alignment;   
        //    object bFirstPage = false;   
        //    object bF = true;   
        //    //if (bHeader == true)   
        //    //WordApplic.Selection.HeaderFooter.PageNumbers.ShowFirstPageNumber = bF;   
        //    switch (strType)   
        //    {   
        //        case "Center":   
        //            alignment = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberCenter;   
        //            //WordApplic.Selection.HeaderFooter.PageNumbers.Add(ref alignment,ref bFirstPage);   
        //            //Microsoft.Office.Interop.Word.Selection objSelection = WordApplic.pSelection;   
        //            oWordApplic.Selection.HeaderFooter.PageNumbers[1].Alignment = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberCenter;   
        //            break;   
        //        case "Right":   
        //            alignment = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberRight;   
        //            oWordApplic.Selection.HeaderFooter.PageNumbers[1].Alignment = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberRight;   
        //            break;   
        //        case "Left":   
        //            alignment = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberLeft;   
        //            oWordApplic.Selection.HeaderFooter.PageNumbers.Add(ref alignment, ref bFirstPage);   
        //            break;   
        //    }   
        //}   
  
        ///// <summary>   
        ///// 设置页面为标准A4公文样式   
        ///// </summary>   
        //private void SetA4PageSetup()   
        //{   
        //    oWordApplic.ActiveDocument.PageSetup.TopMargin = oWordApplic.CentimetersToPoints(3.7f);   
        //    //oWordApplic.ActiveDocument.PageSetup.BottomMargin = oWordApplic.CentimetersToPoints(1f);   
        //    oWordApplic.ActiveDocument.PageSetup.LeftMargin = oWordApplic.CentimetersToPoints(2.8f);   
        //    oWordApplic.ActiveDocument.PageSetup.RightMargin = oWordApplic.CentimetersToPoints(2.6f);   
        //    //oWordApplic.ActiveDocument.PageSetup.HeaderDistance = oWordApplic.CentimetersToPoints(2.5f);   
        //    //oWordApplic.ActiveDocument.PageSetup.FooterDistance = oWordApplic.CentimetersToPoints(1f);   
        //    oWordApplic.ActiveDocument.PageSetup.PageWidth = oWordApplic.CentimetersToPoints(21f);   
        //    oWordApplic.ActiveDocument.PageSetup.PageHeight = oWordApplic.CentimetersToPoints(29.7f);   
        //}  
 
        //#endregion  
 
        //#region 替换   
  
        /////<summary>   
        ///// 在word 中查找一个字符串直接替换所需要的文本   
        ///// </summary>   
        ///// <param name="strOldText">原文本</param>   
        ///// <param name="strNewText">新文本</param>   
        ///// <returns></returns>   
        //public bool Replace(string strOldText, string strNewText)   
        //{   
        //    if (oDoc == null)   
        //        oDoc = oWordApplic.ActiveDocument;   
        //    this.oDoc.Content.Find.Text = strOldText;   
        //    object FindText, ReplaceWith, Replace;//    
        //    FindText = strOldText;//要查找的文本   
        //    ReplaceWith = strNewText;//替换文本   
        //    Replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;/**//*wdReplaceAll - 替换找到的所有项。  
        //                                              * wdReplaceNone - 不替换找到的任何项。  
        //                                            * wdReplaceOne - 替换找到的第一项。  
        //                                            * */  
        //    oDoc.Content.Find.ClearFormatting();//移除Find的搜索文本和段落格式设置   
        //    if (oDoc.Content.Find.Execute(   
        //        ref FindText, ref missing,   
        //        ref missing, ref missing,   
        //        ref missing, ref missing,   
        //        ref missing, ref missing, ref missing,   
        //        ref ReplaceWith, ref Replace,   
        //        ref missing, ref missing,   
        //        ref missing, ref missing))   
        //    {   
        //        return true;   
        //    }   
        //    return false;   
        //}   
  
        //public bool SearchReplace(string strOldText, string strNewText)   
        //{   
        //    object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;   
  
        //    //首先清除任何现有的格式设置选项，然后设置搜索字符串 strOldText。   
        //    oWordApplic.Selection.Find.ClearFormatting();   
        //    oWordApplic.Selection.Find.Text = strOldText;   
  
        //    oWordApplic.Selection.Find.Replacement.ClearFormatting();   
        //    oWordApplic.Selection.Find.Replacement.Text = strNewText;   
  
        //    if (oWordApplic.Selection.Find.Execute(   
        //        ref missing, ref missing, ref missing, ref missing, ref missing,   
        //        ref missing, ref missing, ref missing, ref missing, ref missing,   
        //        ref replaceAll, ref missing, ref missing, ref missing, ref missing))   
        //    {   
        //        return true;   
        //    }   
        //    return false;   
        //}  
 
        //#endregion 
  
        //#region rgb转换函数
        ///// <summary>   
        ///// rgb转换函数   
        ///// </summary>   
        ///// <param name="r"></param>   
        ///// <param name="g"></param>   
        ///// <param name="b"></param>   
        ///// <returns></returns>   
        //int RGB(int r, int g, int b)   
        //{   
        //    return ((b << 16) | (ushort)(((ushort)g << 8) | r));
        //}
        //#endregion

        //#region RGBToColor
        ///// <summary>
        ///// RGBToColor
        ///// </summary>
        ///// <param name="color">颜色值</param>
        ///// <returns>Color</returns>
        //Color RGBToColor(int color)   
        //{   
        //    int r = 0xFF & color;   
        //    int g = 0xFF00 & color;   
        //    g >>= 8;   
        //    int b = 0xFF0000 & color;   
        //    b >>= 16;   
        //    return Color.FromArgb(r, g, b);
        //}
        //#endregion

        //#region 读取相关

        ///// <summary>
        ///// 读取第i段内容
        ///// </summary>
        ///// <param name="i">段索引</param>
        ///// <returns>string</returns>
        //public string readParagraph(int i)
        //{
        //    try
        //    {
        //        string temp = oDoc.Paragraphs[i].Range.Text.Trim();
        //        return temp;
        //    }
        //    catch (Exception e) {
        //        throw new Exception(e.Message);
        //    }
        //}

        ///// <summary>
        ///// 获得总段数
        ///// </summary>
        ///// <returns>int</returns>
        //public int getParCount()
        //{
        //    return oDoc.Paragraphs.Count;
        //}
        //#endregion
      
    }
}
