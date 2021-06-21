Feature: DeletePayment

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

Scenario: Deleting a payment on payables tab clears payment amount paid, method, reference and date
	And receipt is added
	When a user makes the payment
	And delets payment of payee 'User Broker'
	And refreshes the page
	Then Amount Paid for payee 'User Broker' is cleared
	And Payment Method for payee 'User Broker' is cleared
	And Payment Reference for payee 'User Broker' is cleared
	And Payment Date for payee 'User Broker' is cleared

Scenario: Deleting a payment on payables tab clears payment amount paid, method, reference and date for Fr. Fee
	And receipt is added
	When a user makes the payment
	And delets payment of payee 'Fr. Fee'
	And refreshes the page
	Then Amount Paid for payee 'Fr. Fee' is cleared
	And Payment Method for payee 'Fr. Fee' is cleared
	And Payment Reference for payee 'Fr. Fee' is cleared
	And Payment Date for payee 'Fr. Fee' is cleared

	#payee name is case sensitive also spaces are taken into account
	#so text should be exactly as displayed under payee column