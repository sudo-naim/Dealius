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
namespace Dealius.Features.LeaseRateCalculator.LeaseVariables.Toggles
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.7.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ExcludeExpenseFromCommissionToggleFeature : object, Xunit.IClassFixture<ExcludeExpenseFromCommissionToggleFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ExcludeExpenseFromCommissionToggle.feature"
#line hidden
        
        public ExcludeExpenseFromCommissionToggleFeature(ExcludeExpenseFromCommissionToggleFeature.FixtureData fixtureData, Dealius_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/LeaseRateCalculator/LeaseVariables/Toggles", "ExcludeExpenseFromCommissionToggle", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        [Xunit.SkippableTheoryAttribute(DisplayName="ExcludeExpenseFromCommission toggle is disabled when Tenant Rep Fee $/Sf is selec" +
            "ted")]
        [Xunit.TraitAttribute("FeatureTitle", "ExcludeExpenseFromCommissionToggle")]
        [Xunit.TraitAttribute("Description", "ExcludeExpenseFromCommission toggle is disabled when Tenant Rep Fee $/Sf is selec" +
            "ted")]
        [Xunit.InlineDataAttribute("01/01/2020", "Assignment", "24", "100", new string[0])]
        public virtual void ExcludeExpenseFromCommissionToggleIsDisabledWhenTenantRepFeeSfIsSelected(string startDate, string leaseType, string term, string spaceRequired, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Start Date", startDate);
            argumentsOfScenario.Add("Lease Type", leaseType);
            argumentsOfScenario.Add("Term", term);
            argumentsOfScenario.Add("Space Required", spaceRequired);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ExcludeExpenseFromCommission toggle is disabled when Tenant Rep Fee $/Sf is selec" +
                    "ted", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 4
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
#line 5
 testRunner.Given("a Tenant Rep Deal is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 6
 testRunner.And(string.Format("deal info {0} {1} {2} {3} is entered", startDate, leaseType, term, spaceRequired), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 7
 testRunner.And("lease rate calculator page is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 8
 testRunner.And("$/SF Tenant Rep fee type is selected", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 9
 testRunner.When("the user clicks the IncludeExpensesInCalculation toggle", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
 testRunner.Then("the ExcludeExpenseFromCommission is disabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Expenses are excluded from commission when ExcludeExpenseFromCommission toggle is" +
            " YES")]
        [Xunit.TraitAttribute("FeatureTitle", "ExcludeExpenseFromCommissionToggle")]
        [Xunit.TraitAttribute("Description", "Expenses are excluded from commission when ExcludeExpenseFromCommission toggle is" +
            " YES")]
        [Xunit.InlineDataAttribute("01/01/2020", "Assignment", "24", "100", "10", "5", "5", "50", new string[0])]
        public virtual void ExpensesAreExcludedFromCommissionWhenExcludeExpenseFromCommissionToggleIsYES(string startDate, string leaseType, string term, string spaceRequired, string ratePerSf, string percentage, string expenseStop, string house, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Start Date", startDate);
            argumentsOfScenario.Add("Lease Type", leaseType);
            argumentsOfScenario.Add("Term", term);
            argumentsOfScenario.Add("Space Required", spaceRequired);
            argumentsOfScenario.Add("Rate Per Sf", ratePerSf);
            argumentsOfScenario.Add("Percentage", percentage);
            argumentsOfScenario.Add("Expense Stop", expenseStop);
            argumentsOfScenario.Add("House", house);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expenses are excluded from commission when ExcludeExpenseFromCommission toggle is" +
                    " YES", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 17
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
#line 18
 testRunner.Given("a Tenant Rep Deal is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 19
 testRunner.And(string.Format("deal info {0} {1} {2} {3} is entered", startDate, leaseType, term, spaceRequired), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 20
 testRunner.And("lease rate calculator page is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 21
 testRunner.And(string.Format("user enters {0} for Rates Per Sf", ratePerSf), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 22
 testRunner.And(string.Format("Tenant Rep Fee {0}% is entered", percentage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 23
 testRunner.When("the user clicks the IncludeExpensesInCalculation toggle", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 24
 testRunner.And(string.Format("the user enters Expense Stop {0}", expenseStop), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 25
 testRunner.And("the user clicks the ExcludeExpenseFromCommission toggle", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
 testRunner.And("the user generates schedule", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
 testRunner.Then(string.Format("1st row of RentsGrid table has ${0} House", house), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 28
 testRunner.And(string.Format("2nd row of RentsGrid table has ${0} House", house), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
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
                ExcludeExpenseFromCommissionToggleFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ExcludeExpenseFromCommissionToggleFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
