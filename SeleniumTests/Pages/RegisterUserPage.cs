using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class RegisterUserPage
    {
        private readonly IWebDriver driver;

        public RegisterUserPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement RegisterBtn => driver.FindElement(ByRegisterButton);
        public IWebElement UserName => driver.FindElement(ByUserName);
        public IWebElement Passworld => driver.FindElement(ByPassword);
        public IWebElement GenderMale => driver.FindElement(ByGenderMale);
        public IWebElement GenderFemale => driver.FindElement(ByGenderFemale);
        public IWebElement Age => driver.FindElement(ByAge);
        public IWebElement Nationality => driver.FindElement(ByNationality);
        public IWebElement SaveBtn => driver.FindElement(BySaveButton);

        public void RegisterUser(string userName, string password, string gender, string age, string nationality)
        {
            UserName.SendKeys(userName);
            Passworld.SendKeys(password);

            switch (gender)
            {
                case "male":
                    GenderMale.Click();
                    break;
                case "female":
                    GenderFemale.Click();
                    break;
                default:
                    GenderMale.Click();
                    break;
            }
            
            Age.SendKeys(age);
            Nationality.SendKeys(nationality);           
        }

        //Private Element Identifiers should not be exposed to other pages
        private By ByRegisterButton => By.Id("RegisterButton");
        private By ByUserName => By.Id("UserName");
        private By ByPassword => By.Id("PasswordHash");
        private By ByGenderMale => By.Id("genderMale");
        private By ByGenderFemale => By.Id("genderFemale");
        private By ByAge => By.Id("Age");
        private By ByNationality => By.Id("Nationality");
        private By BySaveButton => By.Id("SaveButton");
    }
}
