Feature: FooterTotalFreeMonths

Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

@perMonth
Scenario: Footer Total Free Months amount is displayed when schedule is generated
	When the user enters Rent Abatement Months 3
	And the user generates schedule
	Then footer rent grid Total Free Months is 3