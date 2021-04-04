using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace mobile
{


    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            
            InitializeComponent();
            recieve();

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                recieve();

                return true;
            });

        }

        BackgroundWorker bw = new BackgroundWorker();
        BackgroundWorker bw1 = new BackgroundWorker();

        void recieve()
        {

            var url = "http://api.beanchat.isaacthoman.me/api/App";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();



            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                string test1 = (string)result.Trim('"');
                test1 = test1.Replace("@newline", "" + System.Environment.NewLine);
                string[] array = test1.Split('\n');
                array = array.Reverse().Take(14).ToArray();

                msgDisplayBox.Text = string.Join(System.Environment.NewLine, array.Reverse());

                return;
            }

        }

        void RefreshButton_Clicked(object sender, EventArgs e)
        {


            recieve();

        }
        void SendButton_Clicked(object sender, EventArgs e)
        {
            var url = "http://api.beanchat.isaacthoman.me/api/App?message="+ sendBox.Text;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.ContentType = "application/json";

            var data = "";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();


            recieve();
            sendBox.Text = "";
            

        }


    }
}