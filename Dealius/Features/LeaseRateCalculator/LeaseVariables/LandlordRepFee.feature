Feature: LandlordRepFee

Scenario: Landlord & Tenant Rep column hide away if $/SF Tenant Rep Fee type is chosen
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And a house broker is added
	And an outside broker is added that doesn't share internal broker's commission
	And deal transaction information is entered
	| Start Date | Lease Type | Term | Space Required |
	| 01.01.2020 | Assignment | 24   | 100            |
	And lease rate calculator page is opened
	When the user selects $/SF Landlord Rep fee type
	Then Tenant Rep Column hides away
	And Landlord Rep Column hides away


Scenario Outline: Landlord Rep Fee is added to each annual year (row) if user enters a landlord rep fee in percentage 
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And a house broker is added
	And an outside broker is added that doesn't share internal broker's commission
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user enters <percentage> for Landlord Rep Fee
	And the user generates schedule
	Then all rows (Annual Years) have <percentage> added on the Landlord Rep Column

Examples: 
	| Start Date | Lease Type | Term | Space Required | percentage |
	| 01/01/2020 | Assignment | 24   | 100            | 5          |

Scenario Outline: Landlord Rep Fee is added to each annual year (row) if user enters a landlord rep fee in $/SF
	Given a Tenant Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |
	And a house broker is added
	And an outside broker is added that doesn't share internal broker's commission
	And deal info <Start Date> <Lease Type> <Term> <Space Required> is entered
	And lease rate calculator page is opened
	When the user selects $/SF Landlord Rep fee type
	And the user enters <price> $ per Sf for Landlord Rep Fee
	And the user generates schedule
	Then all rows (Annual Years) show $<Opposite Side Rep Fee> under the Outside column

	#ClientRepFee = Term * Tenant Rep Fee Price / rows = 500
Examples: 
	| Start Date | Lease Type | Term | Space Required | price | Opposite Side Rep Fee |
	| 01/01/2020 | Assignment | 24   | 100            | 10    | 500                   |

	#Since the term is 24 months it means it has 2 Annual Years and the Rents Grid table will
	#display 2 rows. So the number of rows for the Rents Grid table is equal to:
	#Term divided by 12 (rounded up) example: 
	#if Term= 28 then: 28 / 12 = 2.3 => 3 rows

	#The fields (client rep fee) under Gross Commission House column on the Rents Grid table are equal to:
	# $/Sf times Square Footage divided by all table rows
	# or $/Sf * SquareFootage / tableRows