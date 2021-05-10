Feature: IncludeExpensesToggle


Scenario Outline: When Lease Expansion toggle is on, it displays Square footage column to the RentsGrid table
	Given a Tenant Rep Deal is created
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user clicks the IncludeExpensesInCalculation toggle
	Then Expense Column shows on RentsGrid table

	#use the toggles correct value of the name attribute 
	#(toggle names:LeaseExpansion, ExcludeARI, AmortizeFreeRent, IncludeExpensesInCalculation...)

Examples: 
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |