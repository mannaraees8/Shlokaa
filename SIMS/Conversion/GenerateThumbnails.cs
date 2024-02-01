using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace SIMS.Conversion
{
    public class TypeConversion
    {
        private string _tempLocation = Path.GetPathRoot(Environment.SystemDirectory) + "WBTemp";


    

        public bool PdfURLToPdf(string _sourcePath, out string _outputTempFilePath)
        {
            using (var client = new WebClient())
            {
                _outputTempFilePath = GetTempTargetFilePath(_tempLocation);
                client.DownloadFile(_sourcePath, _outputTempFilePath);
                return true;
            }
        }


        public static string GetTempTargetFilePath(string _targetTempPath)
        {
            string _tempFilePath = string.Empty;

            //Check temp directory exists or not  
            if (!Directory.Exists(_targetTempPath))
            {
                //Create new temp directory   
                Directory.CreateDirectory(_targetTempPath);
            }

            var _fileName = "temp.pdf";
            _tempFilePath = _targetTempPath + "/" + _fileName;

            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
            return _tempFilePath;
        }
    }
}