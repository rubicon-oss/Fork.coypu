﻿using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_an_element_by_css : DriverSpecs
    { 
            [Test]
            public void Finds_present_examples()
            {
                var shouldFind = "#inspectingContent p.css-test span";
                Driver.FindCss(shouldFind, Root).Text.should_be("This");

                shouldFind = "ul#cssTest li:nth-child(3)";
                Driver.FindCss(shouldFind, Root).Text.should_be("Me! Pick me!");
            }

            [Test]
            public void Finds_present_examples_by_text()
            {
                var shouldFind = "#inspectingContent p.css-test span";
                Driver.FindCss(shouldFind, Root, new Regex("^This$")).Text.should_be("This");

                shouldFind = "ul#cssTest li:nth-child(3)";
                Driver.FindCss(shouldFind, Root, new Regex("Pick me")).Text.should_be("Me! Pick me!");
            }


            [Test]
            public void Does_not_find_missing_examples()
            {
                const string shouldNotFind = "#inspectingContent p.css-missing-test";
                Assert.Throws<MissingHtmlException>(() => Driver.FindCss(shouldNotFind, Root), "Expected not to find something at: " + shouldNotFind);
            }

            [Test]
            public void Does_not_find_missing_examples_by_text()
            {
                const string shouldFind = "ul#cssTest li:nth-child(3)";
                Regex missingTextPattern = new Regex("Drop me");
                Assert.Throws<MissingHtmlException>(() => Driver.FindCss(shouldFind, Root, missingTextPattern), string.Format("Expected not to find something at: {0} with text: ", shouldFind, missingTextPattern));
            }

            
            [Test]
            public void Only_finds_visible_elements()
            {
                const string shouldNotFind = "#inspectingContent p.css-test img.invisible";
                Assert.Throws<MissingHtmlException>(() => Driver.FindCss(shouldNotFind,Root), "Expected not to find something at: " + shouldNotFind);
            }
        }
    
}