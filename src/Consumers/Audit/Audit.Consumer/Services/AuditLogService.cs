using Audit.Consumer.Context;
using Audit.Consumer.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace Audit.Consumer.Services
{
    public interface IAuditLogService
    {
        Task SendMailAsync();
        Task<List<AuditLog>> GetAuditLogs();
        Task CreateAuditLog(AuditLog auditLog);
    }
    public class AuditLogService : IAuditLogService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;

        public AuditLogService(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public async Task<List<AuditLog>> GetAuditLogs()
            => await _databaseContext
                .Set<AuditLog>()
                .AsNoTracking()
                .Where(x => x.Date >= DateTime.UtcNow.Date && x.Date < DateTime.UtcNow.Date.AddDays(1))
                .ToListAsync();

        public async Task CreateAuditLog(AuditLog auditLog)
        {
            await _databaseContext.Set<AuditLog>().AddAsync(auditLog);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task SendMailAsync()
        {
            var auditLogs = await GetAuditLogs();
            var fileBytes = ExportAuditLogsToExcel(auditLogs);

            string fromAddress = _configuration.GetSection("SmtpSettings:FromEmail").Value!;
            string fromPassword = _configuration.GetSection("SmtpSettings:Password").Value!;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromAddress);
            message.Subject = "Daily Audit Logs";
            message.To.Add(new MailAddress("srexperius34@gmail.com"));
            message.Body = "Daily audit logs as excel.";
            message.IsBodyHtml = true;

            MemoryStream stream = new MemoryStream(fileBytes);
            Attachment attachment = new Attachment(stream, "AuditLogs.xlsx");
            message.Attachments.Add(attachment);

            var smtpClient = new SmtpClient(_configuration.GetSection("SmtpSettings:SmtpClient").Value)
            {
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress, fromPassword)
            };

            smtpClient.Send(message);
        }

        private byte[] ExportAuditLogsToExcel(List<AuditLog> auditLogs)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet();

                    Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
                    Sheet sheet = new Sheet() { Id = document.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "AuditLogs" };
                    sheets.Append(sheet);

                    var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                    var headerRow = new Row();

                    headerRow.Append(
                        new Cell() { DataType = CellValues.String, CellValue = new CellValue("Date") },
                        new Cell() { DataType = CellValues.String, CellValue = new CellValue("Id") },
                        new Cell() { DataType = CellValues.String, CellValue = new CellValue("Order Id") },
                        new Cell() { DataType = CellValues.String, CellValue = new CellValue("Action") },
                        new Cell() { DataType = CellValues.String, CellValue = new CellValue("Message") }
                    );
                    sheetData.AppendChild(headerRow);
                    foreach (var auditLog in auditLogs)
                    {
                        var infoRow = new Row();
                        infoRow.Append(
                            new Cell() { DataType = CellValues.Date, CellValue = new CellValue(auditLog.Date) },
                            new Cell() { DataType = CellValues.String, CellValue = new CellValue(auditLog.Id.ToString()) },
                            new Cell() { DataType = CellValues.String, CellValue = new CellValue(auditLog.OrderId != null ? auditLog.OrderId.ToString() : string.Empty) },
                            new Cell() { DataType = CellValues.String, CellValue = new CellValue(auditLog.Action) },
                            new Cell() { DataType = CellValues.String, CellValue = new CellValue(auditLog.Message) }
                        );
                        sheetData.AppendChild(infoRow);
                    }
                    worksheetPart.Worksheet.Save();
                }
                return stream.ToArray();
            }
        }
    }
}
