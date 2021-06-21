Feature: BuyerRep


Background: 
	Given a Buyer Rep Deal is created
	| Company Name | DealName | EstCloseDate |
	| NewCompany   | AutoDeal | 03.20.2021   |

Scenario: create a new buyer rep deal
	And property information are entered
	And seller commpany name is entered
	And a house broker is added with commission percentage 100%
	And financial details are added
	| PurchasePrice | BuyerRepFee |
	| 200000        | 5           |
	And buyer rep deal payment is added


	#also check the dates (important dates & payment date)