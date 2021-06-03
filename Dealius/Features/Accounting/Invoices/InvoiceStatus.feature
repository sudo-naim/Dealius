Feature: InvoiceStatus

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


Scenario: Invoice status is marked Pending after closing deal 
	And Invoices tab is opened
	When user find the deal row
	Then invoice status of that deal is Pending

Scenario: Invoice status is marked Emailed when an email of the invoice is sent
	And Invoices tab is opened
	When user find the deal row
	And emails the invoice
	Then invoice status of that deal is Emailed

Scenario: Invoice status is marked Partial when the payment is not paid fully
	And Receivables tab is opened
	And the closed Deal is filtered out
	And receipt for payment is added
	| Amount |
	| 50     |
	And Invoices tab is opened
	When user find the deal row
	Then invoice status of that deal is Partial

Scenario: Invoice status is marked Paid when the payment is fully paid
	And Receivables tab is opened
	And the closed Deal is filtered out
	And receipt for payment is added
	| Amount |
	| 100    |
	And Invoices tab is opened
	When user find the deal row
	Then invoice status of that deal is Paid

Scenario: Invoice status is marked Write Off when the payment is written off
	And Receivables tab is opened
	And the closed Deal is filtered out
	And receipt for payment is added
	| Amount |
	| 100    |
	And a receivable is writen off
	And Invoices tab is opened
	When user find the deal row
	Then invoice status of that deal is Write Off
