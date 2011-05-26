<?php
class PageApiTests
{
	function Retrieve_WhenSuppliedWithValidPage_ReturnsPageData($client)
	{
		echo "<hr />";
		echo "<b>Retrieve_WhenSuppliedWithValidPage_ReturnsPageData</b><br/><br/>";
		
		$json = $client->Page->Retrieve("rasha25");
		
		WriteLine("pageId - " . $json->pageId);
		WriteLine("activityId - " . $json->activityId);
		WriteLine("eventName - " . $json->eventName);
		WriteLine("pageShortName - " . $json->pageShortName);
		WriteLine("status - " . $json->status);
		WriteLine("owner - " . $json->owner);
		WriteLine("event date - " . $json->eventDate);
		WriteLine("branding->buttonColour - " . $json->branding->buttonColour);
		WriteLine("branding->buttonTextColour - " . $json->branding->buttonTextColour);
		WriteLine("branding->headerTextColour - " . $json->branding->headerTextColour);		
		WriteLine("Story - " . strip_tags($json->story));
	}
	
	function ListAll_WithValidCredentials_ReturnsListOfUserPages($client)
	{		
		echo "<hr />";
		echo "<b>ListAll_WithValidCredentials_ReturnsListOfUserPages</b><br/><br/>";
		
		$pages = $client->Page->ListAll();		
		
		foreach ($pages as $page) {
		   echo 'Page:' . $page->pageShortName . ' status: ' . $page->pageStatus ."<br/>". PHP_EOL;
		}		
	}
	
	function Create_ValidCredentials_CreatesNewPage($client)
	{		
		echo "<hr />";
		echo "<b>Create_ValidCredentials_CreatesNewPage</b><br/><br/>";
		
		$dto = new RegisterPageRequest();
		$dto->reference = "12345";
		$dto->pageShortName = "api-test-" . uniqid();
		$dto->activityType = "OtherCelebration";
		$dto->pageTitle = "api test";
		$dto->eventName = "The Other Occasion of ApTest and APITest";
		$dto->charityId = 2050;
		$dto->targetAmount = 20;
		$dto->eventDate = "/Date(1235764800000)/";
		$dto->justGivingOptIn = true;
		$dto->charityOptIn = true;
		$dto->charityFunded = false;
			
		$page = $client->Page->Create($dto);
		
		WriteLine("Created page url - " . $page->next->uri);	
	}
	
	function Create_ValidCredentials_PageIdReturnedInResponse($client)
	{		
		echo "<hr />";
		echo "<b>Create_ValidCredentials_PageIdReturnedInResponse</b><br/><br/>";
		
		$dto = new RegisterPageRequest();
		$dto->reference = "12345";
		$dto->pageShortName = "api-test-" . uniqid();
		$dto->activityType = "OtherCelebration";
		$dto->pageTitle = "api test";
		$dto->eventName = "The Other Occasion of ApTest and APITest";
		$dto->charityId = 2050;
		$dto->targetAmount = 20;
		$dto->eventDate = "/Date(1235764800000)/";
		$dto->justGivingOptIn = true;
		$dto->charityOptIn = true;
		$dto->charityFunded = false;
			
		$page = $client->Page->Create($dto);
		
		WriteLine("Created page id - " . $page->pageId);	
	}
	
	function IsShortNameRegistered_KnownPage_ReturnsTrue($client)
	{		
		echo "<hr />";
		echo "<b>IsShortNameRegistered_KnownPage_ReturnsTrue</b><br/><br/>";
		
		$pageShortName = "rasha25";
			
		$booleanResponse = $client->Page->IsShortNameRegistered($pageShortName);
		
		if($booleanResponse)
		{
			WriteLine($pageShortName . " is registered");	
		}
		else
		{
			WriteLine($pageShortName . " is NOT registered");
		}
	}	
	
	function IsShortNameRegistered_ForUnregisteredPage_ReturnsFalse($client)
	{		
		echo "<hr />";
		echo "<b>IsShortNameRegistered_ForUnregisteredPage_ReturnsFalse</b><br/><br/>";
					
		$pageShortName = uniqid();
		$booleanResponse = $client->Page->IsShortNameRegistered($pageShortName);
		
		if($booleanResponse)
		{
			WriteLine($pageShortName . " is registered");	
		}
		else
		{
			WriteLine($pageShortName . " is NOT registered");
		}
	}	
	
	function UpdatePageStory_ForKnownPageWithValidCredentials_UpdatesStory($client)
	{		
		echo "<hr />";
		echo "<b>UpdatePageStory_ForKnownPageWithValidCredentials_UpdatesStory</b><br/><br/>";
		
		$dto = new RegisterPageRequest();
		$dto->reference = "12345";
		$dto->pageShortName = "api-test-" . uniqid();
		$dto->activityType = "OtherCelebration";
		$dto->pageTitle = "api test";
		$dto->eventName = "The Other Occasion of ApTest and APITest";
		$dto->charityId = 2050;
		$dto->targetAmount = 20;
		$dto->eventDate = "/Date(1235764800000)/";
		$dto->justGivingOptIn = true;
		$dto->charityOptIn = true;
		$dto->charityFunded = false;			
		$page = $client->Page->Create($dto);	
		
		// Act
		$update = "Updated this story with update - " . uniqid();
		$booleanResponse = $client->Page->UpdateStory($dto->pageShortName, $update);
		
		if($booleanResponse)
		{
			WriteLine("Story updated for " . $dto->pageShortName . " with '" . $update . "'");	
		}
		else
		{
			WriteLine("Story update failed for " . $dto->pageShortName);
		}
	}
	
	function UploadImage_ForKnownPageWithValidCredentials_UploadsImageWithExpectedCaption($client)
	{		
		echo "<hr />";
		echo "<b>UpdatePageStory_ForKnownPageWithValidCredentials_UpdatesStory</b><br/><br/>";
		
		$dto = new RegisterPageRequest();
		$dto->reference = "12345";
		$dto->pageShortName = "api-test-" . uniqid();
		$dto->activityType = "OtherCelebration";
		$dto->pageTitle = "api test";
		$dto->eventName = "The Other Occasion of ApTest and APITest";
		$dto->charityId = 2050;
		$dto->targetAmount = 20;
		$dto->eventDate = "/Date(1235764800000)/";
		$dto->justGivingOptIn = true;
		$dto->charityOptIn = true;
		$dto->charityFunded = false;			
		$page = $client->Page->Create($dto);	
		
		// Act
		$caption = "PHP Image Caption - " . uniqid();
		$filename = "jpg.jpg";
		$imageContentType =  "image/jpeg";
		$booleanResponse = $client->Page->UploadImage($dto->pageShortName, $caption, $filename, $imageContentType);
		
		if($booleanResponse)
		{
			WriteLine("Image updated for " . $dto->pageShortName);	
		}
		else
		{
			WriteLine("Image update failed for " . $dto->pageShortName);
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

$tests = new PageApiTests();
$tests->Retrieve_WhenSuppliedWithValidPage_ReturnsPageData($client);
$tests->ListAll_WithValidCredentials_ReturnsListOfUserPages($client);
$tests->Create_ValidCredentials_CreatesNewPage($client);
$tests->Create_ValidCredentials_PageIdReturnedInResponse($client);
$tests->IsShortNameRegistered_KnownPage_ReturnsTrue($client);
$tests->IsShortNameRegistered_ForUnregisteredPage_ReturnsFalse($client);
$tests->UpdatePageStory_ForKnownPageWithValidCredentials_UpdatesStory($client);
$tests->UploadImage_ForKnownPageWithValidCredentials_UploadsImageWithExpectedCaption($client);
