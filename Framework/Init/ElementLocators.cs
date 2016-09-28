using System;
//using OpenQA.Selenium;

namespace Framework.Init
{
    public static class ElementLocators
    {

        #region //########### SearchRoute Page Elements ###########//

        public static String SearchRoute_lbl_From = "//label[contains(.,'From')]";
        public static String SearchRoute_txt_From = "//input[@name='searchFromInput']";
        public static String SearchRoute_lbl_To = "//label[contains(.,'To')]";
        public static String SearchRoute_txt_To = "//input[@name='searchToInput']";
        public static String SearchRoute_btn_Search = "//button[@aria-label='Search']";
        public static String SearchRoute_grd_Result = "//md-list-item[3]";

        #endregion

      

    }
}
