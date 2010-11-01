<?php
class EventApiTests
{
	function RetrieveEvent_IssuedWithKnownId_ReturnsEvent($client)
	{
		echo "<hr />";
		echo "<b>RetrieveEvent_IssuedWithKnownId_ReturnsEvent</b><br/><br/>";
		
		$response = $client->Event->Retrieve(479546);
		
		WriteLine("Event name: " . $response->name);	
		WriteLine("Event description: " . $response->description);	
		WriteLine("Event startDate: " . $response->startDate);	
	}	
	
	function RetrievePages_IssuedWithKnownId_ReturnsPages($client)
	{
		echo "<hr />";
		echo "<b>RetrievePages_IssuedWithKnownId_ReturnsPages</b><br/><br/>";
		
		$response = $client->Event->RetrievePages(479546);		
		
		foreach ($response->fundraisingPages as $page) {
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

$client = new JustGivingClient($ApiLocation, $ApiKey, 1, $TestUsername, $TestValidPassword);
$client->debug = true;

function WriteLine($string)
{
	echo $string . "<br/>";
}

echo "<h1>Executing Test Cases</h1>";

$tests = new EventApiTests();
$tests->RetrieveEvent_IssuedWithKnownId_ReturnsEvent($client);
$tests->RetrievePages_IssuedWithKnownId_ReturnsPages($client);
