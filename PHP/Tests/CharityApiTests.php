<?php
include_once '../ApiClients/Model/CreateAccountRequest.php';
class CharityApiTests
{
	function Retrieve_WhenSuppliedWithValidCharityId_RetrievesCharity($client)
	{
		echo "<hr />";
		echo "<b>Retrieve_WhenSuppliedWithValidCharityId_RetrievesCharity</b><br/><br/>";
		
		$response = $client->Charity->Retrieve(2050);
		
		WriteLine("Charity Name: " . $response->name);
	}
}

///############### RUN TESTS	

include_once '../JustGivingClient.php';

// Test context
$ApiLocation = "http://api.local.justgiving.com/";
$ApiKey = "decbf1d2";
$TestUsername = "apiunittests@justgiving.com";
$TestValidPassword = "password";

$client = new JustGivingClient($ApiLocation, $ApiKey, 1, $TestUsername, $TestValidPassword);
$client->debug = true;

function WriteLine($string)
{
	echo $string . "<br/>";
}

echo "<h1>Executing Test Cases</h1>";

$tests = new CharityApiTests();
$tests ->Retrieve_WhenSuppliedWithValidCharityId_RetrievesCharity($client);
