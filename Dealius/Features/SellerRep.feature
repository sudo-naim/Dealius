Feature: SellerRep

Background: 
	Given a Seller Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |

Scenario: Seller Rep Commission input calculates rep fee amount for Seller Rep Deal Type
	And property information are entered
	And seller commpany name is entered
	And a house broker is added with commission percentage 100%
	And financial details are added
	| PurchasePrice | ClientRepFee | OppositeSideRepFee |
	| 200000        | 5            | 6                  |
	And buyer rep deal payment is added
	Then Client Rep Commission is 10000$

Scenario: Buyer Rep Commission input calculates rep fee amount for Seller Rep Deal Type
	And property information are entered
	And seller commpany name is entered
	And a house broker is added with commission percentage 100%
	And financial details are added
	| PurchasePrice | ClientRepFee | OppositeSideRepFee |
	| 200000        | 5            | 6                  |
	And buyer rep deal payment is added
	Then Opposite Side Rep Commission is 12000$