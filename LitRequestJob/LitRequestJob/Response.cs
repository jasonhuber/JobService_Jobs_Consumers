using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage.Entity.Interfaces;
using Sage.Platform.Orm;
using Sage.Platform.Scheduling;
using System.ComponentModel;
using System.Data.OleDb;
using System.Net.Mail;

namespace LitRequestJob
{
    [Description("This is a class to check for completed lit requests.")]
    public class Response: SystemJobBase
    {
        private static readonly string _entityDisplayName =typeof(ILitRequest).GetDisplayName();


        protected override void OnExecute()
        {
            try
            {

            base.Phase = "Just starting";
            
            OleDbConnection conn = new System.Data.OleDb.OleDbConnection("Provider=SLXOLEDB.1;Data Source=SRVXX;Initial Catalog=SALESLOGIX_EVAL;Extended Properties='PORT=1706;LOG=ON;SVRCERT=12345;ACTIVITYSECURITY=OFF;TIMEZONE=NONE'");
            conn.Open();
            string sql = "select description from LITREQUEST where FILLSTATUS is null";
            OleDbCommand comm = new OleDbCommand(sql, conn);
            OleDbDataReader dr;
            dr = comm.ExecuteReader();
            base.Phase = "Got the requests back.";

            int i = 0;
            while (dr.Read())
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.EnableSsl = true;

                mail.From = new MailAddress("slxjobs1@gmail.com");
                mail.To.Add("slxjobs1@gmail.com");
                mail.Subject = "New Literature request pending";
                mail.Body = "You have a literature request pending. " + dr.GetString(0);
                //                                                     
                SmtpServer.Credentials = new System.Net.NetworkCredential("slxjobs1@gmail.com", "getpassfromtrainer");
                SmtpServer.Send(mail);
                i++;
           }
            base.Phase = "Done.";
            Context.Result = "Mailed all the things. " + i + " Items sent";
        
            }
            catch (Exception ex)
            {

                base.PhaseDetail = ex.Message + ex.InnerException;
            }

           }
    }
}
