Feature: FooterTotalHouse


Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

	@perMonth
Scenario: Footer Total Gross Amount is displayed when schedule is generated
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	And Tenant Rep Fee 5% is entered
	When the user generates schedule
	Then the footer Total Gross Commission is $6000
	