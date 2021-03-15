Feature: LeaseCalculatorSchedule

Background: 
	Given a user adds a new lease deal


Scenario: User generates schedule for specific Rate Per SF
	And a user opens the Lease Rate Calculator for that deal
	When the user enters the Rate per SF
	#When the user enters 15$ for the Rate per SF
	And generates schedule
	Then all columns of the first row are correct

Scenario: Base rate is calculated automatically when users input Rate per SF
	And a user opens the Lease Rate Calculator for that deal
	When the user enters the Rate per SF
	And presses tab
	Then the rate input field is filled in automatically
#	Then the the first row results are <Base Rent Months>, <Rate per SF>, <Free Rent Months>, <Base Rate Monthly Rate>, <Total Lease>, <Commission Rates Rep>, <Gross Commission House>
#
#Examples: 
#	| Rate Per SF | Base Rent Months | Rate per SF | Free Rent Months | Base Rate Monthly Rate | Total Lease | Commission Rates Rep | Gross Commission House |
#	| 15          | 12               | 15          | 0                | 7500                   | 90000       |                      | 0                      |