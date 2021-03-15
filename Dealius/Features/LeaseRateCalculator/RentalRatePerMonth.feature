Feature: RentalRatePerMonth
	Rental Rate is a field under the Deal Information table which is 
	located on the Lease Rate Calculator Page. It calculates the 
	monthly rate of the rent in accordance to user deal input information

	Background: 
	Given a user adds a new lease deal
	And a user opens the Lease Rate Calculator for that deal

Scenario: Rental Rate recalculation by Rate Per SF
	When the user enters the Rate per SF
	And generates schedule
	Then the Rental Rate is recalculated accordingly

Scenario: Rental Rate recalculation by Rate
	When the user enters the Rate
	And generates schedule
	Then the Rental Rate is recalculated accordingly

Scenario: 