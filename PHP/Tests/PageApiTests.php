<?php

class PageApiTests
{
	function Test_Page_Retrieve_WhenSuppliedWithValidPage_ReturnsPageData($client)
	{
		echo "<hr />";
		echo "<b>Test_Page_Retrieve_WhenSuppliedWithValidPage_ReturnsPageData</b><br/><br/>";
		
		$rasha25 = $client->Page->Retrieve("rasha25");
		
		$xml = new SimpleXMLElement($rasha25);
		WriteLine("pageId - " . $xml->pageId);
		WriteLine("activityId - " . $xml->activityId);
		WriteLine("eventName - " . $xml->eventName);
		WriteLine("pageShortName - " . $xml->pageShortName);
		WriteLine("status - " . $xml->status);
		WriteLine("owner - " . $xml->owner);
		WriteLine("branding->buttonColour - " . $xml->branding->buttonColour);
		WriteLine("branding->buttonTextColour - " . $xml->branding->buttonTextColour);
		WriteLine("branding->headerTextColour - " . $xml->branding->headerTextColour);		
		WriteLine("Story - " . strip_tags($xml->story));
	}
	
	function Test_Page_ListAll($client)
	{		
		echo "<hr />";
		echo "<b>Test_Page_Retrieve_WhenSuppliedWithValidPage_ReturnsPageData</b><br/><br/>";
		
		$pages = $client->Page->ListAll();
		
		$xml = new SimpleXMLElement($pages);
		foreach ($xml->fundraisingPage as $page) {
		   echo 'Page:' . $page->pageShortName . ' status: ' . $page->pageStatus ."<br/>". PHP_EOL;
		}		
	}
}


?>