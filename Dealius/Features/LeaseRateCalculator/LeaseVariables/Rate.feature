Feature: Rate And Rate Per Sf

Scenario Outline: When rate is entered, Rate Per Sf is calculated automatically
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user enters Rate: <Rate> and presses tab
	Then Rate per Sf shows <Rate Per Sf> 

Examples: 
	| Start Date | Lease Type | Term | Space Required | Rate  | Rate Per Sf |
	| 01/01/2020 | Assignment | 24   | 100            | 4000  | 40          |
	| 01/01/2020 | Assignment | 24   | 200            | 10000 | 50          |


Scenario Outline: When rate per sf is entered, Rate is calculated automatically
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user enters Rates Per Sf: <Rate Per Sf> and presses tab
	Then Rate shows <Rate>

Examples: 
	| Start Date | Lease Type | Term | Space Required | Rate  | Rate Per Sf |
	| 01/01/2020 | Assignment | 24   | 100            | 4000  | 40          |
	| 01/01/2020 | Assignment | 24   | 200            | 10000 | 50          |