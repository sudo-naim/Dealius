Feature: RentAbatement


Scenario Outline: Rent Grid table displays Amount column when Amount option for Rent Abatement is selected
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user selects Amount for Rent Abatement type
	Then Months Column hides away
	And Amount Column shows on RentsGrid table

Examples: 
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |


Scenario Outline: Rent Abatement Months input field, adds Free Months to the first row (annual year) of the table
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user selects Months for Rent Abatement type
	And the user enters Rent Abatement Months <Months>
	And the user generates schedule
	Then 1st row of RentsGrid table has <FreeRentMonths> Free Rent Months

Examples: 
	| Start Date | Lease Type | Term | Space Required | Rate Per Sf | Months | FreeRentMonths |
	| 01/01/2020 | Assignment | 24   | 100            | 10          | 4      | 4              |


Scenario Outline: Rent Abatement Amount input field, adds Free Amount to the first row (annual year) of the table
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	And user enters <Rate Per Sf> for Rates Per Sf
	When the user selects Amount for Rent Abatement type
	And enters Rent Abatement Amount <Amount>
	And the user generates schedule
	Then 1st row of RentsGrid table has <Free Rent Amount> Free Rent Amount

Examples: 
	| Start Date | Lease Type | Term | Space Required | Rate Per Sf | Amount | Free Rent Amount |
	| 01/01/2020 | Assignment | 24   | 100            | 10          | 4      | 4                |