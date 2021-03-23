Feature: RateType
	When a user a changes the Rent Type then the 
	column title under Base Rate changes to Monthly Rate
	or Annual Rate according to the Rent Type

Background: 
	Given a user adds a new lease deal
	And a user opens the Lease Rate Calculator for that deal

Scenario: Base Rate column title shows 'Annual Rate' if the 'Per Year' Rate Type option is selected
	And Rate Type option 'Per Year' is selected
	Then Base Rate column header displays 'Annual Rate'

Scenario: Base Rate column title shows 'Monthly Rate' if the 'Per Month' Rate Type option is selected
	When the user selects Rate Type option 'Per Month'
	Then Base Rate column header displays 'Monthly Rate'

Scenario: Change Rate Type after schedule is generated
	And Rate Type option 'Per Year' is selected
	And generates schedule
	And Base Rate column header displays 'Annual Rate'
	When the user selects Rate Type option 'Per Month'
	Then Base Rate column header displays 'Monthly Rate'