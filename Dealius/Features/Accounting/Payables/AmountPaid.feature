Feature: AmountPaid

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

Scenario: Amount Paid column displays correct amount after making a payment
	And receipt is added
	When a user makes the payment
	Then amount paid for Payee 'User Broker' is 60$
	And amount paid for Payee 'Fr. Fee' is 2$ 
	#payee name is case sensitive also spaces are taken into account
	#so text should be exactly as displayed under payee column
