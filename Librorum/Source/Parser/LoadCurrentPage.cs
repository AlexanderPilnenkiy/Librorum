﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace Librorum.Source.Parser
{
    class LoadCurrentPage
    {
        public string LoadPage(string url)
        {
            var result = "";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    if (receiveStream != null)
                    {
                        StreamReader readStream;
                        if (response.CharacterSet == null)
                            readStream = new StreamReader(receiveStream);
                        else
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        result = readStream.ReadToEnd();
                        readStream.Close();
                    }
                    response.Close();
                }
            }
            catch
            {

            }
            return result;
        }
    }
}
