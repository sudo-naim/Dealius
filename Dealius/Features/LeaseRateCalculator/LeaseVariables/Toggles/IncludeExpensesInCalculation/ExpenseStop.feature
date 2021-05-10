Feature: ExpenseStop


Scenario Outline: Expense is added to the Rents Grid table rows when user enters Expense Stop
	Given a Tenant Rep Deal is created
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	And $/SF Tenant Rep fee type is selected
	And the IncludeExpensesInCalculation toggle is clicked
	When the user enters Expense Stop <Expense Stop>
	And the user generates schedule
	Then 1st row of RentsGrid table has $<Expense> Expense
	And 2nd row of RentsGrid table has $<Expense> Expense

Examples: 
	| Start Date | Lease Type | Term | Space Required | Expense Stop | Expense |
	| 01/01/2020 | Assignment | 24   | 100            | 5            | 500     |


#Expense is calculated based on formula:  Square Footage x Expense Stop / 12 x (Months - Free Months)