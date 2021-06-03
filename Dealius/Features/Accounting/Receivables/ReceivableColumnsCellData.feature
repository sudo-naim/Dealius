Feature: ReceivableColumnsCellData


Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And transaction information are entered
	| Start Date | Lease Type | Term | Space Required | Rate Per Sf | TenantRepFee% |
	| 01.01.2020 | Assignment | 24   | 100            | 10          | 5             |
	And property information are entered
	And landlord company name is entered
	And a house broker is added
	And payment is added
	And the deal is closed
	And Accounting page is opened
	And Receivables tab is opened

Scenario: Closed deal displays correct row information on Receivables Tab
	When the user filters the closed Deal
	Then the Total Due is 100$
	And Amount Due is 100$
	And open Balance is 100$

Scenario: Check deal displays correct row infromation after receipt added
	When the user filters the closed Deal
	And adds receipt for payment
	| Amount |
	| 100    |
	Then the Total Due is 100$
	And Amount Due is 100$
	And Amount Received is 100$
	And open Balance is 0$
	And View Receipt button is displayed
	And Print Receipt button is displayed
	And Delete Receipt button is displayed

Scenario: Add receipt for receivable with less than full amount
	When the user filters the closed Deal
	And adds receipt for payment
	| Amount |
	| 50     |
	Then the Total Due is 100$
	And Amount Due is 100$
	And Amount Received is 50$
	And open Balance is 50$
	And View Receipt button is displayed
	And Print Receipt button is displayed
	And Delete Receipt button is displayed
	And Clear Open Balance button is displayed

Scenario: Add receipt for receivable with more than full amount
	When the user filters the closed Deal
	And adds receipt for payment
	| Amount |
	| 150    |
	And confirms the value greater than Amount Due
	Then the Total Due is 100$
	And Amount Due is 100$
	And Amount Received is 150$
	And Open Balance is ($50.00)
	And View Receipt button is displayed
	And Print Receipt button is displayed
	And Delete Receipt button is displayed
	And Clear Open Balance button is displayed



#HouseGrossCommission = SpaceRequired * RatePerSf * TotalYears * 100/TenantRepFee = 100$