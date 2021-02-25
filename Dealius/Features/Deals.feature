Feature: Deals

Background: 
	Given a user adds a new lease deal

Scenario Outline: Verify
	Given a user opens the Lease Rate Calculator for that deal
	When the user enters 15$ for the Rate per SF
	And generates schedule
	Then the the first row results are <Base Rent Months><Rate per SF><Free Rent Months><Base Rate Monthly Rate><Total Lease><Commission Rates Rep><Gross Commission House>

Examples: 
	| Base Rent Months | Rate per SF | Free Rent Months | Base Rate Monthly Rate | Total Lease | Commission Rates Rep | Gross Commission House |
	| 12               |             |                  |                        |             |                      |                        |