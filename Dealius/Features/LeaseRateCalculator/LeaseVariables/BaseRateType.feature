Feature: RateType
	When a user a changes the Rent Type then the 
	column title under Base Rate changes to Monthly Rate
	or Annual Rate according to the Rent Type

Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: Base Rate displays Annual Rate header column
	And Rate Type option 'Per Year' is selected
	Then Base Rate column header displays 'Annual Rate'
	And Base Rate column header 'Monthly Rate' is not displayed

Scenario: Base Rate displays Monthly Rate header column
	And Rate Type option 'Per Month' is selected
	Then Base Rate column header displays 'Monthly Rate'
	And Base Rate column header 'Annual Rate' is not displayed