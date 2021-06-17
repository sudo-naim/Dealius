Feature: ClearOpenBalance


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

Scenario: Clear Open Balance when open balance is a positive number (underpayed)
	And the closed Deal is filtered out
	And receipt for payment is added
	| Amount |
	| 50     |
	When user clears open Balance for receivable
	Then Open Balance is $0.00

Scenario: Clear Open Balance when open balance is a negative number (overpayed)
	And the closed Deal is filtered out
	And receipt for over payment is added
	| Amount |
	| 150    |
	When user clears open Balance for receivable
	Then Open Balance is $0.00

Scenario: Check house Net Value after receivable Clear Open Balance
	And the closed Deal is filtered out
	And receipt for over payment is added
	| Amount |
	| 150    |
	When user clears open Balance for receivable
	And user navigates to Payables Summary form
	Then total house net is $60.00
