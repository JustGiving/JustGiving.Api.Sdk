<?php
include_once '../ApiClients/Model/CreateAccountRequest.php';
include_once '../ApiClients/Model/AuthenticateCharityAccountRequest.php';

class CharityApiTests
{
	function Retrieve_WhenSuppliedWithValidCharityId_RetrievesCharity($client)
	{
		echo "<hr />";
		echo "<b>Retrieve_WhenSuppliedWithValidCharityId_RetrievesCharity</b><br/>";
		
		$response = $client->Charity->Retrieve(2050);
		
		WriteLine("Charity Name: " . $response->name);
	}

	function RetriveAccount_WhenSuppliedCorrectRequest_ReturnValid($client)
	{
		echo "<hr />";
		echo "<b>RetriveAccount_WhenSuppliedCorrectRequest_ReturnValid</b><br/>";

		$request = new AuthenticateCharityAccountRequest();
		$request->password = "badPassword";
		$request->pin = "1111";
		$request->username = "apiunittest_charity@justgiving.com";

		$response = $client->Charity->Authenticate($request);

		if($response->isValid == 1 && $response->charityId > 0 && $response->error == null)
		{
			WriteLine("TEST PASSED");
		}
		else
		{
			WriteLine("TEST FAILED");
		}
	}

	function RetriveAccount_WhenSuppliedInCorrectRequest_ReturnInValid($client)
	{
		echo "<hr />";
		echo "<b>RetriveAccount_WhenSuppliedCorrectRequest_ReturnValid</b><br/>";

		$request = new AuthenticateCharityAccountRequest();
		$request->password = "badPassword";
		$request->pin = "1111";
		$request->username = "apiunittest_charity@justgiving.com";

		$response = $client->Charity->Authenticate($request);

		if($response->isValid == 0)
		{
			WriteLine("TEST PASSED");
		}
		else
		{
			WriteLine("TEST FAILED");
		}
	}

	function GetEventsByCharityId_WhenSuppliedCorrectCharityId_ReturnEvents($client)
	{
		echo "<hr />";
		echo "<b>GetEventsByCharityId_WhenSuppliedCorrectCharityId_ReturnEvents</b><br/>";

		$response = $client->Charity->GetEventsByCharityId(2050);

		if($response != null && $response->events != null)
		{
			WriteLine("TEST PASSED");
		}
		else
		{
			WriteLine("TEST FAILED");
		}
	}

	function GetDonations_WhenSuppliedCorrectCharityId_ReturnDonations($client)
	{
		echo "<hr />";
		echo "<b>GetDonations_WhenSuppliedCorrectCharityId_ReturnDonations</b><br/>";
		
		$response = $client->Charity->GetDonations(2050);

		if($response != null && $response->donations != null)
		{
			WriteLine("TEST PASSED");
		}
		else
		{
			WriteLine("TEST FAILED");
		}
	}

	function GetCategories_ReturnCategories($client)
	{
		echo "<hr />";
		echo "<b>GetCategories_ReturnCategories</b><br/>";
		
		$response = $client->Charity->Categories();

		if($response != null)
		{
			WriteLine("TEST PASSED");
		}
		else
		{
			WriteLine("TEST FAILED");
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

$tests = new CharityApiTests();
$tests->Retrieve_WhenSuppliedWithValidCharityId_RetrievesCharity($client);
$tests->RetriveAccount_WhenSuppliedCorrectRequest_ReturnValid($client);
$tests->RetriveAccount_WhenSuppliedInCorrectRequest_ReturnInValid($client);
$tests->GetEventsByCharityId_WhenSuppliedCorrectCharityId_ReturnEvents($client);
$tests->GetDonations_WhenSuppliedCorrectCharityId_ReturnDonations($client);
$tests->GetCategories_ReturnCategories($client);