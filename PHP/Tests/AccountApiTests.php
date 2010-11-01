<?php
include_once '../ApiClients/Model/CreateAccountRequest.php';
class AccountApiTests
{
	function Create_WhenSuppliedWithValidNewAccountDetails_CreatesAccount($client)
	{
		echo "<hr />";
		echo "<b>Create_WhenSuppliedWithValidNewAccountDetails_CreatesAccount</b><br/><br/>";
		
		$uniqueId = uniqid();
		$request = new CreateAccountRequest();
		$request->email = "test+".$uniqueId."@test.com";
		$request->firstName = "first".$uniqueId;
		$request->lastName = "last".$uniqueId;
		$request->password = "testpassword";
		$request->title = "Mr";
		$request->address->line1 = "testLine1".$uniqueId;
		$request->address->line2 = "testLine2".$uniqueId;
		$request->address->country = "testCountry".$uniqueId;
		$request->address->countyOrState = "testCountyOrState".$uniqueId;
		$request->address->townOrCity = "testTownOrCity".$uniqueId;
		$request->address->postcodeOrZipcode = "M130EJ";
		$request->acceptTermsAndConditions = true;
		
		$response = $client->Account->Create($request);
		
		WriteLine("Created accounts email/login: " . $response->email);
	}
	
	function ListAllPages_WhenSuppliedWithAValidAccount_RetrievesPages($client)
	{
		echo "<hr />";
		echo "<b>ListAll_WhenSuppliedWithAValidAccount_RetrievesPages</b><br/><br/>";
		
		$response = $client->Account->ListAllPages("apiunittests@justgiving.com");
		
		foreach ($response as $page) {
		   echo 'Page:' . $page->pageShortName . ' status: ' . $page->pageStatus ."<br/>". PHP_EOL;
		}	
	}
}

///############### RUN TESTS	

include_once '../JustGivingClient.php';

// Test context
$ApiLocation = "http://api.local.justgiving.com/";
$ApiKey = "decbf1d2";
$TestUsername = "apiunittests@justgiving.com";
$TestValidPassword = "password";
$TestInvalidPassword = "badPassword";

$client = new JustGivingClient($ApiLocation, $ApiKey, 1, $TestUsername, $TestValidPassword);
$client->debug = true;

function WriteLine($string)
{
	echo $string . "<br/>";
}

echo "<h1>Executing Test Cases</h1>";

$pageTests = new AccountApiTests();
$pageTests->Create_WhenSuppliedWithValidNewAccountDetails_CreatesAccount($client);
$pageTests->ListAllPages_WhenSuppliedWithAValidAccount_RetrievesPages($client);
