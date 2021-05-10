Feature: TenantRepLeaseCalculatorSchedule


Background: 
	Given a Tenant Rep Deal is created
	And deal information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened

Scenario: User generates Per Month schedule for specific Rate Per SF
	And Rate Type option 'Per Month' is selected
	And Rate per SF is entered
	When the user generates schedule
	Then all rows for the schedule generated are displayed
	And all the rows data are displayed correctly

Scenario: User generates Per Year schedule for specific Rate Per SF
	And Rate Type option 'Per Year' is selected
	And Rate per SF is entered
	When the user generates schedule
	Then all rows for the schedule generated are displayed
	And all the rows data are displayed correctly

Scenario: Delete a row (Annual Year) on the Rents Grid Table
	And Rate Type option 'Per Month' is selected
	And Rate per SF is entered
	When the user generates schedule
	And deletes the 1st row
	Then the row is deleted from the table

Scenario: Add Rent button adds a row (Annual Year) on the Rents Grid Table
	And Rate Type option 'Per Month' is selected
	And Rate per SF is entered
	When the user generates schedule
	And clicks the add rent button
	Then an additional row is added

Scenario: Footer Total Lease Amount
	And Rate Type option 'Per Month' is selected
	And Rate per SF is entered
	When the user generates schedule
	Then Total Lease footer cell displays correct value