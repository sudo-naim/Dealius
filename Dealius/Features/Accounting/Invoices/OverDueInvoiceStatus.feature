Feature: OverDueInvoiceStatus


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
	And payment is added on a day before todays date
	And the deal is closed
	And Accounting page is opened

Scenario: Invoice status is marked Overdue after closing deal 
	And Invoices tab is opened
	When user find the deal row
	Then invoice status of that deal is Overdue