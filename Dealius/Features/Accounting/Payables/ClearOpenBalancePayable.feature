Feature: ClearOpenBalancePayable

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

Scenario: Clear open balance for payables payment when open balance is a negative number (overpayed)
	And receipt is added
	When a user enters payment details
	And inputs amount 100$ for first payment
	And user clears open Balance payable for Payee 'User Broker'
	Then amount paid for Payee 'User Broker' is 100$
	Then open expense for Payee 'User Broker' is $0.00

Scenario: Clear open balance for payables payment when open balance is a positive number (underpayed)
	And receipt is added
	When a user enters payment details
	And inputs amount 20$ for first payment
	And user clears open Balance payable for Payee 'User Broker'
	Then amount paid for Payee 'User Broker' is 20$
	Then open expense for Payee 'User Broker' is $0.00

	#Additional information concerning these scenarios:
	# - Payment1: Amount due for broker is 60$ (maximum amount allowed is 100$)
	# - Payment2: franchise fee is 2$ (maximum amount allowed is 2$)

Scenario: NEW NEW NEW
	And receipt is added
	When a user enters payment details
	And inputs amount 20$ for first payment
	And user clears open Balance payable for Payee 'User Broker'
	And opens make payment page
	Then total house net is $80.00