Feature: FooterTotalCommision

Background: Landlord & Tenant Rep column hide away if $/SF Tenant Rep Fee type is chosen
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And a house broker is added
	And an outside broker is added that doesn't share internal broker's commission
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened
	
Scenario: Total Outside result when landlord rep fee is calculated in percentage
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	And Tenant Rep Fee 5% is entered
	And Landlord Rep Fee 5% is entered
	When the user generates schedule
	Then the footer Total (Commission) Amount is $12000