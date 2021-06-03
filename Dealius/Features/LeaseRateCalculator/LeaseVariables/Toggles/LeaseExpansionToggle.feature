Feature: LeaseExpansionToggle


Scenario Outline: When Lease Expansion toggle is on, it displays Square footage column to the RentsGrid table
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user clicks the LeaseExpansion toggle
	Then Square Footage Column shows on RentsGrid table

	#use the toggles correct value of the name attribute 
	#(toggle names:LeaseExpansion, ExcludeARI, AmortizeFreeRent, IncludeExpensesInCalculation...)

Examples: 
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |

Scenario Outline: When Lease Expansion toggle is turned off, it hides the Square Footage column from the RentsGrid table
	Given a Tenant Rep Deal is created
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	And the LeaseExpansion toggle is clicked
	When the user clicks the LeaseExpansion toggle
	Then Square Footage Column hides away

Examples: 
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |