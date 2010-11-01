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
	
	function ListAll($client)
	{		
		echo "<hr />";
		echo "<b>ListAll</b><br/><br/>";
		
		$pages = $client->Page->ListAll();		
		
		foreach ($pages as $page) {
		   echo 'Page:' . $page->pageShortName . ' status: ' . $page->pageStatus ."<br/>". PHP_EOL;
		}		
	}
	
	function Create($client)
	{		
		echo "<hr />";
		echo "<b>Create</b><br/><br/>";
		
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
	
	function IsShortNameRegistered($client)
	{		
		echo "<hr />";
		echo "<b>IsShortNameRegistered</b><br/><br/>";
		
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
}


?>