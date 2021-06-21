Feature: HouseBrokerRow

Background: Landlord & Tenant Rep column hide away if $/SF Tenant Rep Fee type is chosen
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And a house broker is added with commission percentage 80%
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: Check first House Broker percentage after generating schedule
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	And Tenant Rep Fee 5% is entered
	When the user generates schedule
	Then percentage of House Broker on first row is 80%

Scenario: Check first House Broker amount earned after generating schedule
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	And Tenant Rep Fee 5% is entered
	When the user generates schedule
	Then amount earned of House Broker on first row is $4800