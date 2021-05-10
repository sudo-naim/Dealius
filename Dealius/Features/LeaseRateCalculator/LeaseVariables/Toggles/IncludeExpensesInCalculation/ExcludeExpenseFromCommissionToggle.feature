Feature: ExcludeExpenseFromCommissionToggle


Scenario Outline: ExcludeExpenseFromCommission toggle is disabled when Tenant Rep Fee $/Sf is selected
	Given a Tenant Rep Deal is created
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	And $/SF Tenant Rep fee type is selected
	When the user clicks the IncludeExpensesInCalculation toggle
	Then the ExcludeExpenseFromCommission is disabled

Examples: 
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |


Scenario Outline: Expenses are excluded from commission when ExcludeExpenseFromCommission toggle is YES
	Given a Tenant Rep Deal is created
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	And user enters <Rate Per Sf> for Rates Per Sf
	And Tenant Rep Fee <Percentage>% is entered
	When the user clicks the IncludeExpensesInCalculation toggle
	And the user enters Expense Stop <Expense Stop>
	And the user clicks the ExcludeExpenseFromCommission toggle
	And the user generates schedule
	Then 1st row of RentsGrid table has $<House> House 
	And 2nd row of RentsGrid table has $<House> House

Examples: 
	| Start Date | Lease Type | Term | Space Required | Rate Per Sf | Percentage | Expense Stop | House |
	| 01/01/2020 | Assignment | 24   | 100            | 10          | 5          | 5            | 50    |
