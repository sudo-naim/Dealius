 Feature: DeletePayment


Background: 
	Given a Tenant Rep Deal is created
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

Scenario: Delete Receivable payment
	And the closed Deal is filtered out
	And receipt for payment is added
	| Amount |
	| 100    |
	When a user deletes payment
	Then Deal is shown under Open Receivables list
	And Amount Received is empty