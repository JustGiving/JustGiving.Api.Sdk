<?php
include_once '../ApiClients/Model/CreateAccountRequest.php';
include_once '../ApiClients/Model/ValidateAccountRequest.php';
include_once '../ApiClients/Model/ChangePasswordRequest.php';

class AccountApiTests
{
	function Create_WhenSuppliedWithValidNewAccountDetails_CreatesAccount($client)
	{
		echo "<hr />";
		echo "<b>Create_WhenSuppliedWithValidNewAccountDetails_CreatesAccount</b><br/><br/>";
		
		$uniqueId = uniqid();
		$request = new CreateAccountRequest();
		$request->email = "test+".$uniqueId."@test.com";
		$request->firstName = "first".$uniqueId;
		$request->lastName = "last".$uniqueId;
		$request->password = "testpassword";
		$request->title = "Mr";
		$request->address->line1 = "testLine1".$uniqueId;
		$request->address->line2 = "testLine2".$uniqueId;
		$request->address->country = "United Kingdom";
		$request->address->countyOrState = "testCountyOrState".$uniqueId;
		$request->address->townOrCity = "testTownOrCity".$uniqueId;
		$request->address->postcodeOrZipcode = "M130EJ";
		$request->acceptTermsAndConditions = true;
		
		$response = $client->Account->Create($request);
		
		WriteLine("Created accounts email/login: " . $response->email);
	}
	
	function ListAllPages_WhenSuppliedWithAValidAccount_RetrievesPages($client)
	{
		echo "<hr />";
		echo "<b>ListAll_WhenSuppliedWithAValidAccount_RetrievesPages</b><br/><br/>";
		
		$response = $client->Account->ListAllPages("apiunittest@justgiving.com");
		
		foreach ($response as $page) {
		   echo 'Page:' . $page->pageShortName . ' status: ' . $page->pageStatus ."<br/>". PHP_EOL;
		}	
	}
	
	function IsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse($client)
	{
		echo "<hr />";
		echo "<b>IsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse</b><br/><br/>";
		
        $booleanResponse = $client->Account->IsEmailRegistered(uniqid() + "@justgiving.com");
				
		if($booleanResponse)
		{
			WriteLine("Email address listed as registered - TEST FAILED");	
		}
		else
		{
			WriteLine("Email address listed as available - TEST PASSED");
		}
	}
	
	function IsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue($client, $knownEmail)
	{
		echo "<hr />";
		echo "<b>IsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue</b><br/><br/>";
		
        $booleanResponse = $client->Account->IsEmailRegistered($knownEmail);
				
		if($booleanResponse)
		{
			WriteLine("Email address listed as registered - TEST PASSED");	
		}
		else
		{
			WriteLine("Email address listed as available - TEST FAILED");
		}
	}

	function IsAccountValid_WhenSuppliedKnownEmailAndPassword_ReturnsValid($client, $knownEmail, $knownPassword)
	{
		echo "<hr />";
		echo "<b>IsAccountValid_WhenSuppliedKnownEmailAndPassword_ReturnsValid</b><br /><br />";

		$request = new ValidateAccountRequest();
		$request->email = $knownEmail;
		$request->password = $knownPassword;

		$response = $client->Account->IsValid($request);
		WriteLine ($response->consumerId);
		WriteLine ($response->isValid);
		if($response->consumerId > 0 && $response->isValid == 1)
		{
			WriteLine("Account credentials are correct and account exist - TEST PASSED");
		}
		else
		{
			WriteLine("Account credentials are incorrect - TEST FAILED");
		}
	}

	function IsAccountValid_WhenSuppliedKnownEmailAndPassword_ReturnsInValid($client, $knownEmail, $knownPassword)
	{
		echo "<hr />";
		echo "<b>IsAccountValid_WhenSuppliedKnownEmailAndPassword_ReturnsInValid</b><br /><br />";

		$request = new ValidateAccountRequest();
		$request->email = $knownEmail;
		$request->password = $knownPassword;

		$response = $client->Account->IsValid($request);
		WriteLine ($response->consumerId);
		WriteLine ($response->isValid);
		if($response->consumerId == 0 && $response->isValid == 0)
		{
			WriteLine("Account credentials are incorrect or accound doesn't exist - TEST PASSED");
		}
		else
		{
			WriteLine("Account credentials are correct - TEST FAILED");
		}
	}

	function GetAccountDetails_WhenSuppliedAuthentication_RetriveAccountDetails($client)
	{		
		echo "<hr />";
		echo "<b>GetAccountDetails_WhenSuppliedAuthentication_RetriveAccountDetails</b><br/>";
		
		$response = $client->Account->AccountDetails();
		if($response->email != null)
		{
			WriteLine ($response->email);
			WriteLine ($response->firstName);
			WriteLine ($response->lastName);
			WriteLine("Account credentials are correct can get account details for " . $response->email ." - TEST PASSED");
		}
		else
		{
			WriteLine("Account credentials are incorrect can't get account details - TEST FAILED");
		}
	}

	function ChangeAccountPassword_WhenSuppliedCorrectCurrentPasswordAndNewPassword_ReturnSuccess_True($client, $knownEmail, $knownPassword)
	{
		echo "<hr />";
		echo "<b>ChangeAccountPassword_WhenSuppliedCorrectCurrentPasswordAndNewPassword_ReturnSuccess_True</b><br/>";
		$request = new ChangePasswordRequest();
		$request->emailaddress = $knownEmail;
		$request->newpassword = $knownPassword;
		$request->currentpassword = $knownPassword;

		$response = $client->Account->ChangePassword($request);
		WriteLine ($request->emailaddress);
		WriteLine ($request->newpassword);
		WriteLine ($request->currentpassword);
		WriteLine ($response->success);
		if($response->success == 1)
		{
			WriteLine("Password was changed for : " .  $request->emailaddress . " and new is password : " . $request->newpassword . " - TEST PASSED");
		}
		else{
			WriteLine("Password wasn't changed for : " .  $request->emailaddress . " and new is password : " . $request->newpassword . " - TEST FAILED");
		}
	}

	function ChangeAccountPassword_WhenSuppliedInCorrectCurrentPasswordAndNewPassword_ReturnSuccess_False($client, $knownEmail, $badPassword)
	{
		echo "<hr />";
		echo "<b>ChangeAccountPassword_WhenSuppliedInCorrectCurrentPasswordAndNewPassword_ReturnSuccess_False</b><br/>";
		$request = new ChangePasswordRequest();
		$request->emailaddress = $knownEmail;
		$request->newpassword = $badPassword;
		$request->currentpassword = $badPassword;

		$response = $client->Account->ChangePassword($request);
		if($response->success == 0){
			WriteLine("Password wasn't changed for : " .  $request->emailaddress . " and new is password : " . $request->newpassword . " - TEST PASSED");
		}
		else{
			WriteLine("Password was changed for : " .  $request->emailaddress . " and new is password : " . $request->newpassword . " - TEST FAILED");
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

$pageTests = new AccountApiTests();
$pageTests->Create_WhenSuppliedWithValidNewAccountDetails_CreatesAccount($client);
$pageTests->ListAllPages_WhenSuppliedWithAValidAccount_RetrievesPages($client);
$pageTests->IsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse($client);
$pageTests->IsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue($client, $testContext->TestUsername);
$pageTests->IsAccountValid_WhenSuppliedKnownEmailAndPassword_ReturnsValid($client, $testContext->TestUsername, $testContext->TestValidPassword);
$pageTests->IsAccountValid_WhenSuppliedKnownEmailAndPassword_ReturnsInValid($client, $testContext->TestUsername, $testContext->TestInvalidPassword);
$pageTests->GetAccountDetails_WhenSuppliedAuthentication_RetriveAccountDetails($client);
$pageTests->ChangeAccountPassword_WhenSuppliedCorrectCurrentPasswordAndNewPassword_ReturnSuccess_True($client, $testContext->TestUsername, $testContext->TestValidPassword);
$pageTests->ChangeAccountPassword_WhenSuppliedInCorrectCurrentPasswordAndNewPassword_ReturnSuccess_False($client, $testContext->TestUsername, $testContext->TestInvalidPassword);