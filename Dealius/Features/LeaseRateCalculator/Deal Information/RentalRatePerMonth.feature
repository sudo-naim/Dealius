Feature: RentalRatePerMonth
	Rental Rate is a field under the Deal Information table which is 
	located on the Lease Rate Calculator Page. It calculates the 
	monthly rate of the rent in accordance to user deal input information

	Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: Rental Rate recalculation by Monthly Rate Per SF
	And Rate Type option 'Per Month' is selected
	And Rate per SF of 50$ is entered
	When the user generates schedule
	Then the Rental Rate is $5000 per month

Scenario: Rental Rate by Yearly Rate Per SF
	And Rate Type option 'Per Year' is selected
	And Rate per SF of 50$ is entered
	When the user generates schedule
	Then the Rental Rate is $416.67 per month