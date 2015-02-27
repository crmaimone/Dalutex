using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace Dalutex.Models
{
    //#region HtmlPdfPage Class

    ///// <summary>
    ///// A page to insert into a HtmlToPdfBuilder Class
    ///// </summary>
    //public class HtmlPdfPage
    //{

    //    #region Constructors

    //    /// <summary>
    //    /// The default information for this page
    //    /// </summary>
    //    public HtmlPdfPage()
    //    {
    //        this._Html = new StringBuilder();
    //    }

    //    #endregion

    //    #region Fields

    //    //parts for generating the page
    //    internal StringBuilder _Html;

    //    #endregion

    //    #region Working With The Html

    //    /// <summary>
    //    /// Appends the formatted HTML onto a page
    //    /// </summary>
    //    public virtual void AppendHtml(string content, params object[] values)
    //    {
    //        this._Html.AppendFormat(content, values);
    //    }

    //    #endregion

    //}

    //#endregion

    ///// <summary>
    ///// Simplifies generating HTML into a PDF file
    ///// </summary>
    //public class HtmlToPdfBuilder
    //{
    //    #region Constants and Fields

    //    private const string DOCUMENT_HTML_END = "</body></html>";

    //    private const string DOCUMENT_HTML_START = "<html><head></head><body>";

    //    private const string REGEX_GET_STYLES = @"(?<selector>[^\{\s]+\w+(\s\[^\{\s]+)?)\s?\{(?<style>[^\}]*)\}";

    //    private const string REGEX_GROUP_SELECTOR = "selector";

    //    private const string REGEX_GROUP_STYLE = "style";

    //    private const string STYLE_DEFAULT_TYPE = "style";

    //    private readonly List<HtmlPdfPage> _pages;

    //    private readonly StyleSheet _styles;

    //    #endregion

    //    #region Constructors and Destructors

    //    /// <summary>
    //    /// Creates a new PDF document template. Use PageSizes.{DocumentSize}
    //    /// </summary>
    //    public HtmlToPdfBuilder(Rectangle size)
    //    {
    //        PageSize = size;
    //        _pages = new List<HtmlPdfPage>();
    //        _styles = new StyleSheet();
    //    }

    //    #endregion

    //    #region Public Events

    //    /// <summary>
    //    /// Method to override to have additional control over the document
    //    /// </summary>
    //    public event RenderEvent AfterRender;

    //    /// <summary>
    //    /// Method to override to have additional control over the document
    //    /// </summary>
    //    public event RenderEvent BeforeRender;

    //    #endregion

    //    #region Public Properties

    //    /// <summary>
    //    /// The page size to make this document
    //    /// </summary>
    //    public Rectangle PageSize { get; set; }

    //    /// <summary>
    //    /// Returns a list of the pages available
    //    /// </summary>
    //    public HtmlPdfPage[] Pages
    //    {
    //        get
    //        {
    //            return _pages.ToArray();
    //        }
    //    }

    //    #endregion

    //    #region Public Indexers

    //    /// <summary>
    //    /// Returns the page at the specified index
    //    /// </summary>
    //    public HtmlPdfPage this[int index]
    //    {
    //        get
    //        {
    //            return _pages[index];
    //        }
    //    }

    //    #endregion

    //    #region Public Methods

    //    /// <summary>
    //    /// Appends and returns a new page for this document
    //    /// </summary>
    //    public HtmlPdfPage AddPage()
    //    {
    //        var page = new HtmlPdfPage();
    //        _pages.Add(page);
    //        return page;
    //    }

    //    /// <summary>
    //    /// Appends a style for this sheet
    //    /// </summary>
    //    public void AddStyle(string selector, string styles)
    //    {
    //        _styles.LoadTagStyle(selector, STYLE_DEFAULT_TYPE, styles);
    //    }

    //    /// <summary>
    //    /// Imports a stylesheet into the document
    //    /// </summary>
    //    public void ImportStylesheet(string path)
    //    {
    //        // load the file
    //        string content = File.ReadAllText(path);

    //        // use a little regular expression magic
    //        foreach (Match match in Regex.Matches(content, REGEX_GET_STYLES))
    //        {
    //            string selector = match.Groups[REGEX_GROUP_SELECTOR].Value;
    //            string style = match.Groups[REGEX_GROUP_STYLE].Value;
    //            AddStyle(selector, style);
    //        }
    //    }

    //    /// <summary>
    //    /// Moves a page after another
    //    /// </summary>
    //    public void InsertAfter(HtmlPdfPage page, HtmlPdfPage after)
    //    {
    //        _pages.Remove(page);
    //        _pages.Insert(Math.Min(_pages.IndexOf(after) + 1, _pages.Count), page);
    //    }

    //    /// <summary>
    //    /// Moves a page before another
    //    /// </summary>
    //    public void InsertBefore(HtmlPdfPage page, HtmlPdfPage before)
    //    {
    //        _pages.Remove(page);
    //        _pages.Insert(Math.Max(_pages.IndexOf(before), 0), page);
    //    }

    //    /// <summary>
    //    /// Removes the page from the document
    //    /// </summary>
    //    public void RemovePage(HtmlPdfPage page)
    //    {
    //        _pages.Remove(page);
    //    }

    //    /// <summary>
    //    /// Renders the PDF to an array of bytes
    //    /// </summary>
    //    public byte[] RenderPdf()
    //    {
    //        // Document is inbuilt class, available in iTextSharp
    //        var file = new MemoryStream();
    //        var document = new Document(PageSize);
    //        PdfWriter writer = PdfWriter.GetInstance(document, file);

    //        // allow modifications of the document
    //        if (BeforeRender is RenderEvent)
    //        {
    //            BeforeRender(writer, document);
    //        }

    //        // header
    //        document.Add(new Header("style", string.Empty));
    //        document.Open();

    //        // render each page that has been added
    //        foreach (HtmlPdfPage page in _pages)
    //        {
    //            document.NewPage();

    //            // generate this page of text
    //            var output = new MemoryStream();
    //            var html = new StreamWriter(output, Encoding.UTF8);

    //            // get the page output
    //            html.Write(string.Concat(DOCUMENT_HTML_START, page._Html.ToString(), DOCUMENT_HTML_END));
    //            html.Close();
    //            html.Dispose();

    //            // read the created stream
    //            var generate = new MemoryStream(output.ToArray());
    //            var reader = new StreamReader(generate);
    //            foreach (object item in HTMLWorker.ParseToList(reader, _styles))
    //            {
    //                document.Add((IElement)item);
    //            }

    //            // cleanup these streams
    //            html.Dispose();
    //            reader.Dispose();
    //            output.Dispose();
    //            generate.Dispose();
    //        }

    //        // after rendering
    //        if (AfterRender is RenderEvent)
    //        {
    //            AfterRender(writer, document);
    //        }

    //        // return the rendered PDF
    //        document.Close();
    //        return file.ToArray();
    //    }

    //    #endregion
    //}

    //#region Rendering Delegate

    ///// <summary>
    ///// Delegate for rendering events
    ///// </summary>
    //public delegate void RenderEvent(PdfWriter writer, Document document);

    //#endregion
}
