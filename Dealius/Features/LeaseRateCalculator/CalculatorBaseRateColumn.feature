Feature: RateType
	When a user a changes the Rent Type then the 
	column title under Base Rate changes to Monthly Rate
	or Annual Rate according to the Rent Type

Background: 
	Given a Tenant Rep Deal is created
	And the Lease calculator is opened

Scenario: Base Rate displays Annual Rate header column
	And Rate Type option 'Per Year' is selected
	Then Base Rate column header displays 'Annual Rate'
	And Base Rate column header 'Monthly Rate' is not displayed

Scenario: Base Rate displays Monthly Rate header column
	And Rate Type option 'Per Month' is selected
	Then Base Rate column header displays 'Monthly Rate'
	And Base Rate column header 'Monthly Rate' is not displayed