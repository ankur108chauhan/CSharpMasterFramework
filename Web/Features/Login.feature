Feature: Login to Rahul Shetty Academy

@login
Scenario: Login to Rahul Shetty Academy
	Given user navigates to "https://rahulshettyacademy.com/loginpagePractise/"
	When user login with valid credentials
	Then user should be able to successfully login