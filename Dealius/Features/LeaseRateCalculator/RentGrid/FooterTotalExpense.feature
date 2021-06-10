Feature: FooterTotalExpense

Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: Footer Total Expenses amount is displayed when schedule is generated
	And Rate per SF of 50$ is entered
	And the IncludeExpensesInCalculation toggle is clicked
	When the user enters Expense Stop $5
	And the user generates schedule
	Then the footer total expense is $1000