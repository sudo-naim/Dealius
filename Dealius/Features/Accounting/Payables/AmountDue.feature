Feature: AmountDue

Background: 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And transaction information are entered
	| Start Date | Lease Type | Term | Space Required | Rate Per Sf | TenantRepFee% |
	| 01.01.2020 | Assignment | 24   | 100            | 10          | 5             |
	And property information are entered
	And landlord company name is entered
	And a house broker is added with commission percentage 100%
	And payment is added
	And the deal is closed
	And Accounting page is opened
	And Receivables tab is opened

Scenario: Amount Due amount after receipt is underpayed
	And the closed Deal is filtered out
	And receipt for payment is added
	| Amount |
	| 50     |
	When the user opens the Payables tab
	And searches deal ID on payables tab
	Then Amount Due for Payee 'User Broker' is 30.00$
	And Amount Due for Payee 'Fr. Fee' is 1.00$

Scenario: Amount Due amount after receipt has Cleared Open Balance
	And the closed Deal is filtered out
	And receipt for payment is added
	| Amount |
	| 50     |
	When user clears open Balance for receivable
	And the user opens the Payables tab
	And searches deal ID on payables tab
	Then Amount Due for Payee 'User Broker' is 30.00$
	And Amount Due for Payee 'Fr. Fee' is 1.00$

	