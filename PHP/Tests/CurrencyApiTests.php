<?php


class CurrencyApiTests
{
	function GetValidCurrencies_ReturnCurrencies($client)
	{
		echo "<hr />";
		echo "<b>GetValidCurrencies_ReturnCurrencies</b><br/><br/>";
		
		$response = $client->Currency->ValidCurrencies();

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
$pageTests = new CurrencyApiTests();
$pageTests->GetValidCurrencies_ReturnCurrencies($client);
