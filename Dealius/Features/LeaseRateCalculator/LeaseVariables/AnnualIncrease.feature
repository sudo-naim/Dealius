Feature: AnnualIncrease


Scenario Outline: When rate is entered, Rate Per Sf is calculated automatically
	Given a Tenant Rep Deal is created
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	And user enters <Rate Per Sf> for Rates Per Sf
	When the user enters Annual % Increase <Annual Increase>
	And the user generates schedule
	Then the '2'nd Annual Year (row) Rate Per Sf is <RatePerSfRow1>
	And the '3'd Annual Year (row) Rate Per Sf is <RatePerSfRow2>


Examples: 
	| Start Date | Lease Type | Term | Space Required | Annual Increase | Rate Per Sf | RatePerSfRow1 | RatePerSfRow2 |
	| 01/01/2020 | Assignment | 36   | 100            | 5               | 10          | 10.5          | 11.03         |
