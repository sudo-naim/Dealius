Feature: PayExpsensePayable

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
	And expense is added
	| Expense Type | Vendor Name | Expense Amount |
	| Other        | Expense1    | 50             |
	And the deal is closed
	And Accounting page is opened
	And Receivables tab is opened
	And the closed Deal is filtered out
	And receipt for payment is added
	| Amount |
	| 100    |
	When the user opens the Payables tab
	And searches deal ID on payables tab


Scenario: Payable for expense is displayed with correct amount due
	Then Amount Due for Payee 'Expense1' is 50$

Scenario: Delete expense payable
	When user click payment for payee 'Expense1'
	And a user makes the payment
	And delets payment of payee 'Expense1'
	And refreshes the page
	Then Amount Paid for payee 'Expense1' is cleared
	And Payment Method for payee 'Expense1' is cleared
	And Payment Reference for payee 'Expense1' is cleared
	And Payment Date for payee 'Expense1' is cleared
