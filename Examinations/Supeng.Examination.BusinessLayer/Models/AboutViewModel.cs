using System;
using System.Reflection;

namespace Supeng.Examination.BusinessLayer.Models
{
    public class AboutViewModel
    {
        public string SystemName
        {
            get { return "考试系统"; }
        }

        public string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public string CopyRight
        {
            get { return string.Format("{0} CopyRight Sue Su", DateTime.Now.Year); }
        }

        public string ContactUser
        {
            get { return "Sue Su"; }
        }

        public string Mobile
        {
            get { return "13825634085"; }
        }

        public string EmailAddress
        {
            get { return "einstein_supeng@hotmail.com"; }
        }
    }
}