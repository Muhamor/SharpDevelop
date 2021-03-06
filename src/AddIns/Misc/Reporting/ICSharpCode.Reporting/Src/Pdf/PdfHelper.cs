﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Drawing;
using ICSharpCode.Reporting.Interfaces.Export;
using ICSharpCode.Reporting.PageBuilder.ExportColumns;
using PdfSharp.Drawing;
using System.Drawing.Drawing2D;
using PdfSharp.Drawing.Layout;

namespace ICSharpCode.Reporting.Pdf
{
	/// <summary>
	/// Description of PdfHelper.
	/// </summary>
	public static class PdfHelper
	{
		
		public static void WriteText(XTextFormatter textFormatter,Point columnLocation, ExportText exportColumn)
		{
			XFont font = PdfHelper.CreatePdfFont(exportColumn);
			var rect = new Rectangle(columnLocation,exportColumn.DesiredSize).ToXRect();
			textFormatter.DrawString(exportColumn.Text,
			                         font,
			                         new XSolidBrush(ToXColor(exportColumn.ForeColor)),
			                         rect, XStringFormats.TopLeft);
		}
		
		
		static XFont CreatePdfFont(IExportColumn exportColumn)
		{
			var textColumn = (ExportText)exportColumn;
			return new XFont(textColumn.Font.FontFamily.Name, textColumn.Font.Size);
		}
		
		
		static XColor ToXColor (Color color){
			return XColor.FromArgb(color.R,color.G,color.B);
		}
		
		
		public static XRect CreateDisplayRectangle(IExportColumn column) {
			return column.DisplayRectangle.ToXRect();
		}
		
		
		public static void DrawRectangle (IExportColumn column, XGraphics graphics) {
			FillRectangle(column.DisplayRectangle,column.FrameColor,graphics);
		}
		
		
		public static void FillRectangle(Rectangle rect,Color color,XGraphics graphics) {
			var r = rect.ToXRect();
			graphics.DrawRectangle(new XSolidBrush(ToXColor(color)),r);
		}
		
		
		public static XPen PdfPen(IExportGraphics column) {
			return new XPen(ToXColor(column.ForeColor),column.Thickness);
		}
		
		
		public static XLineCap LineCap (IExportGraphics column) {
			return XLineCap.Round;
			
		}
		
		
		public static XDashStyle DashStyle (IExportGraphics column) {
			XDashStyle style = XDashStyle.Solid;
			
			switch (column.DashStyle) {
				case System.Drawing.Drawing2D.DashStyle.Solid:
					style = XDashStyle.Solid;
					break;
				case System.Drawing.Drawing2D.DashStyle.Dash:
					style  = XDashStyle.Dash;
					break;
				case System.Drawing.Drawing2D.DashStyle.Dot:
					style = XDashStyle.Dot;
					break;
				case System.Drawing.Drawing2D.DashStyle.DashDot:
					style = XDashStyle.DashDot;
					break;
				case System.Drawing.Drawing2D.DashStyle.DashDotDot:
					style = XDashStyle.DashDotDot;
					break;
				case System.Drawing.Drawing2D.DashStyle.Custom:
					
					break;
				default:
					throw new Exception("Invalid value for DashStyle");
			}
			return style;
		}
		
		
		public static Point LocationRelToParent (ExportColumn column) {
			return new Point(column.Parent.Location.X + column.Location.X,
			                 column.Parent.Location.Y + column.Location.Y);
		}
	}
}
