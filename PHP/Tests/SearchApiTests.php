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

$tests = new SearchApiTests();
$tests->CharitySearch_KeywordWithKnownResults_SearchResultsPresent($client);
