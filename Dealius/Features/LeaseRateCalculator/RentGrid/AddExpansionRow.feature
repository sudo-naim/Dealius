Feature: AddExpansionRow

Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: Additional row for an expansion is added when the Add Expansion button is clicked
	And generates schedule
	When the user clicks the LeaseExpansion toggle
	And the user clicks the Add Expansion plus button on the 2nd row
	Then an additional row for expansion is added under it