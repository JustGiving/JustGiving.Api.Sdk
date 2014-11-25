<?php


class CountriesApiTests
{
	function GetCountries_ReturnCountries($client)
	{
		echo "<hr />";
		echo "<b>GetCountries_ReturnCountries</b><br/><br/>";
		
		$response = $client->Countries->Countries();

		if($response)
		{
			WriteLine("TEST PASSED");
		}
		else
		{
			WriteLine("TEST FAILED");
		}

	}
}

##Run Tests

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
$pageTests = new CountriesApiTests();
$pageTests->GetCountries_ReturnCountries($client);
