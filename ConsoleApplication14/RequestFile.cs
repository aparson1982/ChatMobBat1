using System;
using System.Net;

namespace ConsoleApplication14
{
    public class RequestFile
    {
        public async void RequestFromWeb()
        {
            //Downloads the text file then deletes the file at the url.

            //  String logFilePath = RequestParser.GetCurrentWorkingDirectory() + "\\NewJobs\\testdelete.txt";
            using (var wc = new System.Net.WebClient())
            {
                await wc.DownloadFileTaskAsync(new Uri("http://problemdescription.000webhostapp.com/requestInfo.txt"), @"C:\Users\Robert\Desktop\Chatt Mobility\Download Here\testdelete.txt");  //directory needs to be fixed
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://problemdescription.000webhostapp.com/clearfile.php");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
    }
}