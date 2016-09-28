using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Framework.Init;
using System.Configuration;

namespace RouteMe
{
    [Binding]
    public class RouteSearchFeatureSetps
    {
        IWebDriver _driver;

        public static Int32 intLoginPassCnt = 0;
        public static Int32 intLoginFailCnt = 0;
        public static Int32 intLoginWarningCnt = 0;
        string strFromLocation = "Auckland, New Zealand";
        string strToLocation = "Hamilton, New Zealand";


        static string ProjectUrl = Convert.ToString(ConfigurationSettings.AppSettings.Get("ProjectUrl"));
        static string CloseBrowser = Convert.ToString(ConfigurationSettings.AppSettings.Get("CloseBrowser"));


        [Given(@"I navigate to page ""(.*)""")]
        public void GivenINavigateToPage(string p0)
        {
            Report.AddToHtmlReportTCHeader("Search Route Test Cases");
            Report.AddToHtmlReport("TC_001: Verify the Route Search Functionality .", true, false, true);
            _driver = new ChromeDriver(@"c:\");
            Report.AddToHtmlReport("STEP 1: Insert Url in Browser Addressbar.", false, true);

            _driver.Navigate().GoToUrl(ProjectUrl);
            _driver.Manage().Window.Maximize();

            Report.AddToHtmlReportPassed("Chrome Browser Open for " + ProjectUrl + " .");
            new Common(_driver).pause(12000);

        }

        [Given(@"I have entered ""(.*)"" in From Listbox")]
        public void GivenIHaveEnteredInFromListbox(string p0)
        {

            Report.AddToHtmlReport("STEP 2: Enter start point and destination point in 'From' and 'To' auto suggestion listbox respectively on RouteMe page.", false, true);
 
            new Common(_driver).FindElement(By.XPath(ElementLocators.SearchRoute_lbl_From), "'From' label text verification on RouteMe page.");
            IWebElement Home_txt_From = new Common(_driver).FindElement(By.XPath(ElementLocators.SearchRoute_txt_From),"'From' auto suggestion listbox on RouteMe page.");
            Common.enterText(Home_txt_From, strFromLocation, true);
            new Common(_driver).pause(8000);
            try
            {
                IWebElement SearchRoute_txt_ToValue = _driver.FindElement(By.XPath("//ul[@id='ul-0']/li[2]"));
                SearchRoute_txt_ToValue.Click();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'From' auto suggestion listbox on RouteMe page.");
                intLoginFailCnt++;
            }

        }

        [Given(@"I have entered ""(.*)"" in To Listbox")]
        public void GivenIHaveEnteredInToListbox(string p0)
        {
            new Common(_driver).FindElement(By.XPath(ElementLocators.SearchRoute_lbl_To), "'To' label text verification on RouteMe page.");
            IWebElement Home_txt_To = new Common(_driver).FindElement(By.XPath(ElementLocators.SearchRoute_txt_To),"'To' auto suggestion listbox on RouteMe page.");
            Common.enterText(Home_txt_To, strToLocation, true);
            new Common(_driver).pause(8000);

            try
            {
                IWebElement SearchRoute_txt_ToValue = _driver.FindElement(By.XPath("//ul[@id='ul-1']/li[2]"));
                SearchRoute_txt_ToValue.Click();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(_driver, ex, "'To' auto suggestion listbox on RouteMe page.");
                intLoginFailCnt++;
            }

            Report.AddToHtmlReport("<br>Data Entered: ", false, true, true);
            Report.AddToHtmlReport("From: " + strFromLocation, false);
            Report.AddToHtmlReport("To: " + strToLocation + "<br>", false);

        }

        [When(@"I press search Button")]
        public void WhenIPressSearchButton()
        {
            Report.AddToHtmlReport("STEP 3: Click on 'Search' button on RouteMe page.", false, true);

            new Common(_driver).FindElementClick(By.XPath(ElementLocators.SearchRoute_btn_Search), "'Search' button on RouteMe page.");
            new Common(_driver).pause(5000);
        }

        [Then(@"the result route path should be visible on the screen")]
        public void ThenTheResultRoutePathShouldBeVisibleOnTheScreen()
        {

            new Common(_driver).FindElementClick(By.XPath(ElementLocators.SearchRoute_grd_Result), "'Search Result pane' on RouteMe page.");
            
            new Common(_driver).pause(5000);

            new Common(_driver).FindElement(By.XPath("//md-list[@class='direction-detail flex']"), "'Directions pane' on RouteMe page.");
            
            if (CloseBrowser == "1")
            {
                _driver.Close();
                Report.AddToHtmlReportPassed("Browser Close.");
            }
            
            Report.AddToHtmlReportFeatureFinish();
            Report.GenerateHtmlReport();

            if (intLoginFailCnt == 0)
            {
                intLoginPassCnt++;
            }
            
            Report.AddToHtmlSummaryReport("RouteMe - Test Cases", intLoginPassCnt, intLoginFailCnt, intLoginWarningCnt);
            Report.AddToHtmlSummaryReportTotal();
            Report.GenerateHtmlSummaryReport();
        }

    }
}
