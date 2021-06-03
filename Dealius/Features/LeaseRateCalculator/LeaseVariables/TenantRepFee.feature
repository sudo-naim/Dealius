Feature: TenantRepFee



Scenario Outline: Tenant Rep column hides away if $/SF Tenant Rep Fee type is chosen
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user selects $/SF Tenant Rep fee type
	Then Tenant Rep Column hides away

Examples: 
	| Start Date | Lease Type | Term | Space Required |
	| 01/01/2020 | Assignment | 24   | 100            |

Scenario Outline: Tenant Rep Fee is added to each annual year (row) if user enters a tenant rep fee in percentage 
	Given a Tenant Rep Deal is created
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user enters <percentage> for Tenant Rep Fee
	And the user generates schedule
	Then all rows (Annual Years) have <percentage> added on the Tenant Rep Column

Examples: 
	| Start Date | Lease Type | Term | Space Required | percentage |
	| 01/01/2020 | Assignment | 24   | 100            | 5          |

Scenario Outline: Tenant Rep Fee is added to each annual year (row) if user enters a tenant rep fee in $/SF
	Given a Tenant Rep Deal is created
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user selects $/SF Tenant Rep fee type
	And the user enters <price> $ per Sf for Tenant Rep Fee
	And the user generates schedule
	Then all rows (Annual Years) show $<Client Rep Fee> under the House column

	#ClientRepFee = Term * price / rows = 500
Examples: 
	| Start Date | Lease Type | Term | Space Required | price | Client Rep Fee |
	| 01/01/2020 | Assignment | 24   | 100            | 10    | 500            |

	#Since the term is 24 months it means it has 2 Annual Years and the Rents Grid table will
	#display 2 rows. So the number of rows for the Rents Grid table is equal to:
	#Term divided by 12 (rounded up) example: 
	#if Term= 28 then: 28 / 12 = 2.3 => 3 rows

	#The fields (client rep fee) under Gross Commission House column on the Rents Grid table are equal to:
	# $/Sf times Square Footage divided by all table rows
	# or $/Sf * SquareFootage / tableRows
	 