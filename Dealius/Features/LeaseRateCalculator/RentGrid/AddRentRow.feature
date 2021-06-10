Feature: AddRentRow

Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: Add Rent button adds a row (Annual Year) on the Rents Grid Table
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	When the user generates schedule
	And clicks the add rent button
	Then an additional row is added