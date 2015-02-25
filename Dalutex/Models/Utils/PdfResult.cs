//http://www.macoratti.net/12/05/mvc_pdf1.htm

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using System.IO;

namespace Dalutex.Models
{
    public class EspelhoPedidPdf : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            byte[] file = new Relatorios().GerarEspelhoPedido();
            byte[] buffer = new byte[4096];

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/pdf";
            MemoryStream pdfStream = new MemoryStream(file);

            while (true)
            {
                int read = pdfStream.Read(buffer, 0, buffer.Length);
                if (read == 0)
                    break;
                response.OutputStream.Write(buffer, 0, read);
            }
            response.End();
        }
    }
}