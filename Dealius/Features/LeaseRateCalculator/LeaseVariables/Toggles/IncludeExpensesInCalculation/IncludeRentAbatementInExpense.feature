Feature: IncludeRentAbatementInExpense


Scenario Outline: Rent abatement is included in expenses when IncludeRentAbatementInExpense toggle is YES
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	And Rent Abatement Months <Months> is entered
	When the user clicks the IncludeExpensesInCalculation toggle
	And the user enters Expense Stop $<Expense Stop>
	And the user clicks the IncludeRentAbatementInExpense toggle
	And the user generates schedule
	Then 1st row of RentsGrid table has $<Expense> Expense

Examples: 
	| Start Date | Lease Type | Term | Space Required | Months | Expense Stop | Expense |
	| 01/01/2020 | Assignment | 24   | 100            | 3      | 5            | 375     |

