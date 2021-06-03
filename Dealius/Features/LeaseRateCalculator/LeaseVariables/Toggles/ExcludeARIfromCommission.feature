Feature: ExcludeARIfromCommissionToggle


Scenario Outline: Exclude ARI 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	And user enters <Rate Per Sf> for Rates Per Sf
	And Annual % Increase <Annual Increase %> is entered
	And Tenant Rep Fee <Percentage>% is entered
	When the user clicks the ExcludeARI toggle
	And the user generates schedule
	Then 1st row of RentsGrid table has $<House> House 
	And 2nd row of RentsGrid table has $<House> House


Examples: 
	| Start Date | Lease Type | Term | Space Required | Rate Per Sf | Annual Increase % | Percentage | House |
	| 01/01/2020 | Assignment | 24   | 100            | 10          | 5                 | 5          | 50    |