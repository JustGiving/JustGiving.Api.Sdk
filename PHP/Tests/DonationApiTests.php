<?php
include_once '../ApiClients/Model/CreateAccountRequest.php';
class DonationApiTests
{
	function Retrieve_WhenSuppliedWithKnownExistingDonationId_ReturnsDonation($client)
	{
		echo "<hr />";
		echo "<b>Retrieve_WhenSuppliedWithKnownExistingDonationId_ReturnsDonation</b><br/><br/>";
		
		$response = $client->Donation->Retrieve(21303723);
		
		WriteLine("Donation amount: " . $response->amount);
		WriteLine("Donation date: " . $response->donationDate);
		WriteLine("Donation Donor: " . $response->donorDisplayName);
	}	
	
	function RetrieveStatus_WhenSuppliedWithKnownExistingDonationId_ReturnsDonationStatus($client)
	{
		echo "<hr />";
		echo "<b>RetrieveStatus_WhenSuppliedWithKnownExistingDonationId_ReturnsDonationStatus</b><br/><br/>";
		
		$response = $client->Donation->RetrieveStatus(21303723);
		
		WriteLine("Donation ref: " . $response->ref);
		WriteLine("Donation donationId: " . $response->donationId);
		WriteLine("Donation donationRef: " . $response->donationRef);
		WriteLine("Donation status: " . $response->status);
		WriteLine("Donation amount: " . $response->amount);
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

$tests = new DonationApiTests();
$tests->Retrieve_WhenSuppliedWithKnownExistingDonationId_ReturnsDonation($client);
$tests->RetrieveStatus_WhenSuppliedWithKnownExistingDonationId_ReturnsDonationStatus($client);
