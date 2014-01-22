using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GobalASAX
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (IsPostBack)
                return;

            DisplayTweets();

            //Session["Tweets"] = new List<string> {"Tweet1", "Tweet2"};
            
        }

        protected void ButtonAddTweet_Click(object sender, EventArgs e)
        {
          

            AddTweet(TextBoxTweet.Text);
            
            TextBoxTweet.Text = "";

           
            
            DisplayTweets();



        }

        
        protected void ButtonEndSession_Click(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        void DisplayTweets()
        {
            if (null == Session["Tweets"])
                return;

            List<string> tweets = Session["Tweets"] as List<string>;


            string htmlTweets = "";
            foreach (string tweet in tweets)
            {
                
                if (tweet.Length > 0)
                    htmlTweets += ("<br>" + tweet);
            }

            LiteralTweets.Text = htmlTweets;
        }

        protected void AddTweet(string tweet)
        {


            if (null == Session["Tweets"])
                Session["Tweets"] = new List<string>();

            List<string> tweets = Session["Tweets"] as List<string>;

            tweets.Add(tweet);

        }
      
    }
}
