using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Text;
using System.IO;
using System.Timers;

namespace GobalASAX
{
    public class Global : System.Web.HttpApplication
    {

        //System.Timers.Timer _timer = null;

        public static void SendMailMessage(MailMessage mail, string sFrom, string sUserName, string sPassword)
        {
            //set the addresses
            if (null == mail.From)
                mail.From = new MailAddress(sFrom);
            //mail.From = new MailAddress(sFrom);

            //send the message
            SmtpClient smtp = new SmtpClient("mail.rivervalleycommunity.org");

            System.Net.NetworkCredential SmtpUser;
            SmtpUser = new System.Net.NetworkCredential();
            SmtpUser.UserName = sUserName;
            SmtpUser.Password = sPassword;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = SmtpUser;


            smtp.Send(mail);
        }
        
        
        public static void SendMailMessage(MailMessage mail)
        {
            SendMailMessage(mail, "\"River Valley Community Church\" <2bc17e6f292e@rivervalleycommunity.org>",
                "2bc17e6f292e@rivervalleycommunity.org", "Myza2828");
        }

        public static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {

        }



        protected void Application_Start(object sender, EventArgs e)
        {
            //_timer = new System.Timers.Timer(5*60*1000);//Every 5 Minutes
            //_timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            //_timer.Enabled = true;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

            //string SessionGUID = "";
            //try
            //{
                
            //    SessionGUID =  Guid.NewGuid().ToString();
            //    Session["GUID"] = SessionGUID;
                string sPath = Server.MapPath("Global.asax.Trace.txt");
            //    File.AppendAllText(sPath, "\r\n" + DateTime.Now + " Session " + SessionGUID + " Started");
                Session["TraceFilePath"] = sPath;
            //}
            //catch 
            //{ 
            
            //}
            
         
         
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
         
            List<string> SessionTweets = Session["Tweets"] as List<string>;

            if (null == SessionTweets)
                return;
            
            string sPath = Session["TraceFilePath"] as string;
            //string SessionGUID = "UNKNOWN";
            //string temp = Session["GUID"] as string;
            //if (temp != null)
            //    SessionGUID = temp.Clone() as string;
           
          
            //try
            //{
            //    if (null != sPath)
            //        File.AppendAllText(sPath, "\r\n"+ DateTime.Now.ToLongTimeString() + " Session " + SessionGUID + " Ended");
            //}
            //catch { }

          
            foreach (string tweet in SessionTweets)
            {
                #region SendTweet

                using (MailMessage tweetMessage = new MailMessage())
                {
                    try
                    {
                        tweetMessage.Body = tweet;
                        tweetMessage.To.Add("tweet@tweetymail.com");
                        tweetMessage.To.Add("judek@yahoo.com");
                        SendMailMessage(tweetMessage);
                    }
                    catch (Exception exp)
                    {
                        try
                        {
                            if (null != sPath)
                            {
                                File.AppendAllText(sPath, "\r\n Send Mail Error:" + DateTime.Now + exp.Message + "\r\n" + exp.StackTrace);
                                File.AppendAllText(sPath, "\r\n Message Body:\r\n" + tweetMessage);
                                File.AppendAllText(sPath, "\r\n Abborting...");
                            }
                            break;//If one mail breaks the all probably will so stop trying
                        }
                        catch {break; }
                    }
       
                }

                #endregion
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //_timer.Enabled = false;
            //_timer.Dispose();

        }
    }
}