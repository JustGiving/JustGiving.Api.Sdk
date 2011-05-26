<?php
class SearchApiTests
{
	function CharitySearch_KeywordWithKnownResults_SearchResultsPresent($client)
	{
		echo "<hr />";
		echo "<b>Retrieve_WhenSuppliedWithKnownExistingDonationId_ReturnsDonation</b><br/><br/>";
		
		$response = $client->Search->CharitySearch('the demo charity');
		
		foreach ($response->charitySearchResults as $charity) {
		   echo 'id:' . $charity->charityId . ' name: ' . $charity->name ."<br/>". PHP_EOL;
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

$tests = new SearchApiTests();
$tests->CharitySearch_KeywordWithKnownResults_SearchResultsPresent($client);
