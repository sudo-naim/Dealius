Feature: DealInformation
	A table that displays deal information that the user
	has provided on the Deal Profile.

Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |

Scenario: The Lease Rate Calculator displays deal information table
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |
	Then the lease rate calculator page is opened
	And all Deal Information is displayed correctly