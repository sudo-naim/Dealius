Feature: DealInformation
	A table that displays deal information that the user
	has provided.

Background: 
	Given a user adds a new lease deal

Scenario: The Lease Rate Calculator displays deal information table
	When the user clicks calculate on Transaction Information section
	And enters lease deal information
	And the user clicks continue
	Then the lease rate calculator page is opened
	And all Deal Information is displayed correctly