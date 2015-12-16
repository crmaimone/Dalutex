//http://www.macoratti.net/12/05/mvc_pdf1.htm

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using System.IO;
using Microsoft.Reporting.WebForms;
using Dalutex.Models.DataModels;
using System.Configuration;
using System.Data.Entity.Validation;

namespace Dalutex.Models
{
    public class EspelhoPedidoPdf : ActionResult
    {
        public decimal IDPedidoBloco { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            try
            {
                byte[] buffer;
                HttpResponseBase response;
                MemoryStream pdfStream;

                CreatePdfStream(out buffer, out pdfStream);
                
                response = context.HttpContext.Response;
                response.ContentType = "application/pdf";
            
                while (true)
                {
                    int read = pdfStream.Read(buffer, 0, buffer.Length);
                    if (read == 0)
                        break;
                    response.OutputStream.Write(buffer, 0, read);
                }

                pdfStream.Close();
                response.End();
            }
            catch(Exception ex)
            {
                this.Handle(ex);
            }
        }

        public void CreatePdfStream(out byte[] buffer, out MemoryStream pdfStream)
        {
            LocalReport relatorio = new LocalReport();

            //Caminho onde o arquivo do Report Viewer está localizado
            relatorio.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CAMINHO_ESPELHO_PEDIDO"]);
            relatorio.EnableExternalImages = true;

            //Caso as imagens estejam na mesma pasta da aplicação
            //string strImagens = url.AbsoluteUri.Replace(url.AbsolutePath, "").Replace(url.Query, "") + ConfigurationManager.AppSettings["PASTA_DESENHOS"].Replace("~", "").Replace("\\", "/");
            //Caso as imagens estejam publicadas em outro site.
            string strImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"].Replace("~", "").Replace("\\", "/");
            relatorio.SetParameters(new ReportParameter("PASTA_DESENHOS", strImagens));

            using (var ctx = new TIDalutexContext())
            {
                //Define o nome do nosso DataSource e qual rotina irá preenche-lo, no caso, nosso método criado anteriormente
                relatorio.DataSources.Clear();
                relatorio.DataSources.Add(new ReportDataSource("dsPrePedido", ctx.VW_IMPRESSAO_WEB.Where(x => x.PEDIDO_BLOCO == IDPedidoBloco).ToList()));
            }

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
              "<DeviceInfo>" +
              " <OutputFormat>PDF</OutputFormat>" +
              " <PageWidth>11.69in</PageWidth>" +
              " <PageHeight>8.27in</PageHeight>" +
              " <MarginTop>0.3in</MarginTop>" +
              " <MarginLeft>0.2in</MarginLeft>" +
              " <MarginRight>0.2in</MarginRight>" +
              " <MarginBottom>0.2in</MarginBottom>" +
              "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] file;

            //Renderiza o relatório em bytes
            file = relatorio.Render(
            reportType,
            deviceInfo,
            out mimeType,
            out encoding,
            out fileNameExtension,
            out streams,
            out warnings);

            buffer = new byte[4096];

            pdfStream = new MemoryStream(file);
        }

        protected void Handle(Exception ex)
        {
            throw new Exception(ex.Message + Environment.NewLine + GetInnerException(ex));
        }

        protected string GetInnerException(Exception e, string strIn = "")
        {
            strIn = strIn + e.Message;

            if (e.InnerException != null)
                return GetInnerException(e.InnerException, strIn + Environment.NewLine);
            else
                return strIn;
        }
    }
}