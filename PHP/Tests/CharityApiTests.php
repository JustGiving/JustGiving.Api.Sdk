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
