using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace instagramImages
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlAddress = "https://www.instagram.com/explore/tags/hdss/"; //tagı dinamik olarak değiştir.
            string regexi = string.Format("display_src\":\"(.*?)\"");

            string data = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            MatchCollection mth = Regex.Matches (data,regexi);
            

            foreach (Match item in mth)
            {
                Console.WriteLine(item.Groups[1].Value); // Gelen Değeri kontrol et. Dilersen "\" karakterini gelen değerden silebilirsin.
            }

            Console.ReadKey();
        }
    }
}