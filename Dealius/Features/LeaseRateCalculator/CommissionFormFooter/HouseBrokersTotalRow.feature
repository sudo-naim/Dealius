Feature: HouseBrokersTotal


Background: Landlord & Tenant Rep column hide away if $/SF Tenant Rep Fee type is chosen
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And a house broker is added with commission percentage 100%
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened
	
Scenario: Check House Broker Total amount after generating schedule for single house broker
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	And Tenant Rep Fee 5% is entered
	When the user generates schedule
	Then House Broker Total amount is $6000

Scenario: Check House Broker Total percentage after generating schedule for single house broker
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	And Tenant Rep Fee 5% is entered
	When the user generates schedule
	Then House Broker Total percentage is 100%