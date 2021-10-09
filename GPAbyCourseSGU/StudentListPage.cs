using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPAbyCourseSGU
{
    public class StudentListPage
    {
        public IWebDriver driver;
        public IWebElement table => driver.FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_ctl00_gvDSSinhVien']"));
        public ICollection<IWebElement> rows => table.FindElements(By.TagName("tr"));

        public StudentListPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public List<Student> GetListStudent(String url)
        {
            driver.Navigate().GoToUrl(url);
            return rows.Where(x => x.FindElement(By.TagName("td")) != null).Select(x => new Student
            {
                MSSV = x.FindElement(By.XPath("td[2]")).Text,
                FirstName = x.FindElement(By.XPath("td[3]")).Text,
                LastName = x.FindElement(By.XPath("td[4]")).Text,
                Class = x.FindElement(By.XPath("td[5]")).Text
            }).ToList();
        }

    }
}
