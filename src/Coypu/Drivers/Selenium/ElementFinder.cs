using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Coypu.Drivers.Selenium
{
    internal class ElementFinder
    {
        public IEnumerable<IWebElement> FindAll(By by,
                                                Scope scope,
                                                Options options,
                                                Func<IWebElement, bool> predicate = null)
        {
            try
            {
                return SeleniumScope(scope)
                       .FindElements(by)
                       .Where(e => Matches(predicate, e) && IsDisplayed(e, options));
            }
            catch (StaleElementReferenceException e)
            {
                throw new StaleElementException(e);
            }
        }

        public ISearchContext SeleniumScope(Scope scope)
        {
            return (ISearchContext) scope.Now()
                                         .Native;
        }

        public ISearchContext SeleniumScopeWithoutEnsuringDefaultContent(Scope scope)
        {
            var element = scope.Now();

            if (element is SeleniumWindow windowElement)
            {
                return (ISearchContext) windowElement.NativeWindowWithoutEnsuringDefaultContent();
            }

            return (ISearchContext) element.Native;
        }

        private static bool Matches(Func<IWebElement, bool> predicate,
                                    IWebElement element)
        {
            return predicate == null || predicate(element);
        }

        public bool IsDisplayed(IWebElement e,
                                Options options)
        {
            return options.ConsiderInvisibleElements || e.IsDisplayed();
        }
    }
}