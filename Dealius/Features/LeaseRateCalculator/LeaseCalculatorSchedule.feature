Feature: LeaseCalculatorSchedule
#----------------------
#----------------------
Background: 
	Given a Tenant Rep Deal is created
	And the Lease calculator is opened

Scenario: User generates schedule for specific Rate Per SF
	And Rate Type option 'Per Month' is selected
	And Rate per SF is entered
	When the user generates schedule
	Then all rows for the schedule generated are displayed
	And all the row input fields are displayed correctly