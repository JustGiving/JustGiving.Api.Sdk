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
	
	function IsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse($client)
	{
		echo "<hr />";
		echo "<b>IsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse</b><br/><br/>";
		
        $booleanResponse = $client->Account->IsEmailRegistered(uniqid() + "@justgiving.com");
				
		if($booleanResponse)
		{
			WriteLine("Email address listed as registered - TEST FAILED");	
		}
		else
		{
			WriteLine("Email address listed as available - TEST PASSED");
		}
	}
	
	function IsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue($client, $knownEmail)
	{
		echo "<hr />";
		echo "<b>IsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue</b><br/><br/>";
		
        $booleanResponse = $client->Account->IsEmailRegistered($knownEmail);
				
		if($booleanResponse)
		{
			WriteLine("Email address listed as registered - TEST PASSED");	
		}
		else
		{
			WriteLine("Email address listed as available - TEST FAILED");
		}
	}
}

///############### RUN TESTS	

include_once '../JustGivingClient.php';
include_once 'TestContext.php';

$testContext = new TestContext();
$client = new JustGivingClient($testContext->ApiLocation, $testContext->ApiKey, $testContext->ApiVersion, $testContext->TestUsername, $testContext->TestValidPassword);
$client->debug = $testContext->Debug;

function WriteLine($string)
{
	echo $string . "<br/>";
}

echo "<h1>Executing Test Cases</h1>";

$pageTests = new AccountApiTests();
$pageTests->Create_WhenSuppliedWithValidNewAccountDetails_CreatesAccount($client);
$pageTests->ListAllPages_WhenSuppliedWithAValidAccount_RetrievesPages($client);
$pageTests->IsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse($client);
$pageTests->IsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue($client, $testContext->TestUsername);
