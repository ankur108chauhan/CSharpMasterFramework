Feature: Invalid Login

	@InvalidLogin
	Scenario: Invalid Login
		Given User select English language and click Login Button
		When User enters phone number "9042798783" and password "password123"
		Then Error message "Sorry, we could not recognise you" should be displayed

	@ValidLogin
	Scenario: Valid Login
		Given User select English language and click Login Button
		When User enters phone number "9042798783" and password "Test@9876"
		Then User should successfully login and logout

	@ContextSwitch
	Scenario: Context Switch
		Given User select English language and click Login Button
		When User clicks on "Privacy Policy" button
		And User clicks on "www.dogovorilis24.ru" link
		Then User is able to switch to webview and back to native app

	@Swipe
	Scenario: Swipe
		Given User select English language and click Login Button
		When User enters phone number "9042798783" and password "Test@9876"
		Then User is able to swipe cards