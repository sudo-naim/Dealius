Feature: RentalRatePerMonth
	Rental Rate is a field under the Deal Information table which is 
	located on the Lease Rate Calculator Page. It calculates the 
	monthly rate of the rent in accordance to user deal input information

	Background: 
	Given a Tenant Rep Deal is created
	And deal information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: Rental Rate recalculation by Monthly Rate Per SF
	And Rate Type option 'Per Month' is selected
	And Rate per SF is entered
	When the user generates schedule
	Then the Rental Rate is calculated accordingly

Scenario: Rental Rate by Yearly Rate Per SF
	And Rate Type option 'Per Year' is selected
	And Rate per SF is entered
	When the user generates schedule
	Then the Rental Rate is calculated accordingly