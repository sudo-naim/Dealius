﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.7.0.0
//      SpecFlow Generator Version:3.7.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Dealius.Features.LeaseRateCalculator.LeaseVariables
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.7.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class TenantClientRepFeeTypeFeature : object, Xunit.IClassFixture<TenantClientRepFeeTypeFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "TenantClientRepFeeType.feature"
#line hidden
        
        public TenantClientRepFeeTypeFeature(TenantClientRepFeeTypeFeature.FixtureData fixtureData, Dealius_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/LeaseRateCalculator/LeaseVariables", "TenantClientRepFeeType", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Tenant Rep column hides away if $/SF Tenant Rep Fee type is chosen")]
        [Xunit.TraitAttribute("FeatureTitle", "TenantClientRepFeeType")]
        [Xunit.TraitAttribute("Description", "Tenant Rep column hides away if $/SF Tenant Rep Fee type is chosen")]
        [Xunit.InlineDataAttribute("01/01/2020", "Assignment", "24", "100", new string[0])]
        public virtual void TenantRepColumnHidesAwayIfSFTenantRepFeeTypeIsChosen(string startDate, string leaseType, string term, string spaceRequired, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Start Date", startDate);
            argumentsOfScenario.Add("Lease Type", leaseType);
            argumentsOfScenario.Add("Term", term);
            argumentsOfScenario.Add("Space Required", spaceRequired);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Tenant Rep column hides away if $/SF Tenant Rep Fee type is chosen", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 6
 testRunner.Given("a Tenant Rep Deal is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 7
 testRunner.And(string.Format("{0} {1} {2} {3} is entered", startDate, leaseType, term, spaceRequired), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 8
 testRunner.And("lease rate calculator page is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 9
 testRunner.When("the user selects $/SF Tenant Rep fee type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
 testRunner.Then("Tenant Rep Column hides away", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Tenant Rep Fee is added to each annual year (row) if user enters a tenant rep fee" +
            " in percentage")]
        [Xunit.TraitAttribute("FeatureTitle", "TenantClientRepFeeType")]
        [Xunit.TraitAttribute("Description", "Tenant Rep Fee is added to each annual year (row) if user enters a tenant rep fee" +
            " in percentage")]
        [Xunit.InlineDataAttribute("01/01/2020", "Assignment", "24", "100", "5", new string[0])]
        public virtual void TenantRepFeeIsAddedToEachAnnualYearRowIfUserEntersATenantRepFeeInPercentage(string startDate, string leaseType, string term, string spaceRequired, string percentage, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Start Date", startDate);
            argumentsOfScenario.Add("Lease Type", leaseType);
            argumentsOfScenario.Add("Term", term);
            argumentsOfScenario.Add("Space Required", spaceRequired);
            argumentsOfScenario.Add("percentage", percentage);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Tenant Rep Fee is added to each annual year (row) if user enters a tenant rep fee" +
                    " in percentage", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 16
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 17
 testRunner.Given("a Tenant Rep Deal is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 18
 testRunner.And(string.Format("{0} {1} {2} {3} is entered", startDate, leaseType, term, spaceRequired), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 19
 testRunner.And("lease rate calculator page is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 20
 testRunner.When(string.Format("the user enters {0} for Tenant Rep Fee", percentage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 21
 testRunner.And("the user generates schedule", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 22
 testRunner.Then(string.Format("all rows (Annual Years) have {0} added on the Tenant Rep Column", percentage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Tenant Rep Fee is added to each annual year (row) if user enters a tenant rep fee" +
            " in $/SF")]
        [Xunit.TraitAttribute("FeatureTitle", "TenantClientRepFeeType")]
        [Xunit.TraitAttribute("Description", "Tenant Rep Fee is added to each annual year (row) if user enters a tenant rep fee" +
            " in $/SF")]
        [Xunit.InlineDataAttribute("01/01/2020", "Assignment", "24", "100", "10", "500", new string[0])]
        public virtual void TenantRepFeeIsAddedToEachAnnualYearRowIfUserEntersATenantRepFeeInSF(string startDate, string leaseType, string term, string spaceRequired, string price, string clientRepFee, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Start Date", startDate);
            argumentsOfScenario.Add("Lease Type", leaseType);
            argumentsOfScenario.Add("Term", term);
            argumentsOfScenario.Add("Space Required", spaceRequired);
            argumentsOfScenario.Add("price", price);
            argumentsOfScenario.Add("Client Rep Fee", clientRepFee);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Tenant Rep Fee is added to each annual year (row) if user enters a tenant rep fee" +
                    " in $/SF", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 28
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 29
 testRunner.Given("a Tenant Rep Deal is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 30
 testRunner.And(string.Format("{0} {1} {2} {3} is entered", startDate, leaseType, term, spaceRequired), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 31
 testRunner.And("lease rate calculator page is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 32
 testRunner.When("the user selects $/SF Tenant Rep fee type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 33
 testRunner.And(string.Format("the user enters {0} $ per Sf for Tenant Rep Fee", price), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 34
 testRunner.And("the user generates schedule", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 35
 testRunner.Then(string.Format("all rows (Annual Years) show ${0} under the House column", clientRepFee), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.7.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                TenantClientRepFeeTypeFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                TenantClientRepFeeTypeFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
