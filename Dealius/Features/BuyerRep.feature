﻿Feature: BuyerRep


Background: 
	Given a Buyer Rep Deal is created

Scenario: create a new buyer rep deal
	And property information are entered
	And seller commpany name is entered
	And a house broker is added
	And financial details are added
	| PurchasePrice | BuyerRepFee |
	| 200000        | 5           |
	And buyer rep deal payment is added


	#also check the dates (important dates & payment date)