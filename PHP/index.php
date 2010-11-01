<?php

include 'JustGivingClient.php';

// Test context
$ApiLocation = "http://api.local.justgiving.com/";
$ApiKey = "decbf1d2";
$TestUsername = "apiunittests@justgiving.com";
$TestValidPassword = "password";
$TestInvalidPassword = "badPassword";

$client = new JustGivingClient($ApiLocation, $ApiKey, 1, $TestUsername, $TestValidPassword);
$client->debug = true;

function WriteLine($string)
{
	echo $string . "<br/>";
}

echo "<h1>Executing Test Cases</h1>";

include_once 'Tests/PageApiTests.php';
$pageTests = new PageApiTests();
$pageTests->Retrieve_WhenSuppliedWithValidPage_ReturnsPageData($client);
$pageTests->ListAll($client);
$pageTests->Create($client);
$pageTests->IsShortNameRegistered($client);

?>