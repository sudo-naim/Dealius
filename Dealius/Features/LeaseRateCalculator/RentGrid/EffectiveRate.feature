Feature: EffectiveRate

Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: Effective Rate per SF is shown when user generates schedule per year
	And Rate per SF of 50$ is entered
	When the user generates schedule
	Then the Effective Rate is 50$ per SF

	@perMonth
Scenario: Effective Rate per SF is shown when user generates schedule per month
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	When the user generates schedule
	Then the Effective Rate is 600$ per SF