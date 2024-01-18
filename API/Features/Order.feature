Feature: Validate Order API

	@api
	Scenario: Validate the get order API request
		When I send GET API request for "Order" API
		Then I got "200" status code
