Feature: RouteSearch
	
Scenario: Verify the Route Search Functionality 
	Given I navigate to page "https://routemewebsite-staging.azurewebsites.net" 
	Given I have entered "Auckland" in From Listbox
	Given I have entered "Hamilton" in To Listbox
	When I press search Button
	Then the result route path should be visible on the screen