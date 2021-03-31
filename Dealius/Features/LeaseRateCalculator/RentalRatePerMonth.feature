Feature: RentalRatePerMonth
	Rental Rate is a field under the Deal Information table which is 
	located on the Lease Rate Calculator Page. It calculates the 
	monthly rate of the rent in accordance to user deal input information

	Background: 
	Given a Tenant Rep Deal is created
	And the Lease calculator is opened

Scenario: Rental Rate recalculation by Rate Per SF
	And Rate per SF is entered
	When the user generates schedule
	Then the Rental Rate is recalculated accordingly

Scenario: Rental Rate recalculation by Rate
	And Rate per SF is entered
	When the user generates schedule
	Then the Rental Rate is recalculated accordingly
