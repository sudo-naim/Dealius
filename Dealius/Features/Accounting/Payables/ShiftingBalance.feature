Feature: ShiftingBalance

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
	And 50% of fee on 1st payment is added
	And 50% of fee on 2nd payment is added
	And the deal is closed
	And Accounting page is opened
	And Receivables tab is opened

Scenario: Amount Paid displays correct amount after shifting balance
	And receipt is added
	When a user adds payment details for the 1st payable
	| Payment Method | Reference | Amount |
	| Check          | a         | 40     |
	And a user adds payment details for the 2nd payable
	| Payment Method | Reference | Amount |
	| Check          | a         | 1      |
	And user shifts open balance
	And searches deal ID on payables tab
	Then Amount Paid for Payee 'User Broker' of Payment 1 of 2 is 40$
	And Open Expense for Payee 'User Broker' of Payment 2 of 2 is 20$
	And Amount Paid for Payee 'Fr. Fee' of Payment 1 of 2 is 1$

Scenario: Open Expense on second payment displays correct amount after balance is shifted
	And receipt is added
	When a user adds payment details for the 1st payable
	| Payment Method | Reference | Amount |
	| Check          | a         | 40     |
	And a user adds payment details for the 2nd payable
	| Payment Method | Reference | Amount |
	| Check          | a         | 1      |
	And user shifts open balance
	And searches deal ID on payables tab
	Then Open Expense for Payee 'User Broker' of Payment 2 of 2 is 20$

Scenario: Amount Paid and Open Expense displays correct amount after not shifting balance
	And receipt is added
	When a user adds payment details for the 1st payable
	| Payment Method | Reference | Amount |
	| Check          | a         | 40     |
	And a user adds payment details for the 2nd payable
	| Payment Method | Reference | Amount |
	| Check          | a         | 1      |
	And user does not shift open balance
	And searches deal ID on payables tab
	Then Amount Paid for Payee 'User Broker' of Payment 1 of 2 is 40$
	And Open Expense for Payee 'User Broker' of Payment 2 of 2 is 30$
	And Amount Paid for Payee 'Fr. Fee' of Payment 1 of 2 is 1$