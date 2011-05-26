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
include_once 'TestContext.php';

$testContext = new TestContext();
$client = new JustGivingClient($testContext->ApiLocation, $testContext->ApiKey, $testContext->ApiVersion, $testContext->TestUsername, $testContext->TestValidPassword);
$client->debug = $testContext->Debug;

function WriteLine($string)
{
	echo $string . "<br/>";
}

echo "<h1>Executing Test Cases</h1>";

$tests = new EventApiTests();
$tests->RetrieveEvent_IssuedWithKnownId_ReturnsEvent($client);
$tests->RetrievePages_IssuedWithKnownId_ReturnsPages($client);
