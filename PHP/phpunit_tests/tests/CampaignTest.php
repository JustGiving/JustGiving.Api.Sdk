<?php
include_once 'Base.php';

class CampaignTest extends Base
{
	public function testRetrieveV2_WhenSuppliedCharityNameAndCampaignName_ReturnsCampaignDetails()
	{
		//arrange
		$charityName = "jgdemo";
		$campaignName = "testsandboxcampaign";
		$expectedCampaignPageName = "The name of my campaign";

		//act
		$response = $this->client->Campaign->RetrieveV2($charityName, $campaignName);

		//assert
		$this->assertEquals($response->httpStatusCode, "200");
		$this->assertEquals($response->bodyResponse->campaignPageName, $expectedCampaignPageName);
	}

	public function testCampaignsByCharityId_WhenSuppliedCharityId_ReturnsCampaigns()
	{
		//arrange
		$charityId = "2050";		
		$expectedCampaignPageName = "The name of my campaign";

		//act
		$response = $this->client->Campaign->CampaignsByCharityId($charityId);

		//assert
		$this->assertEquals($response->httpStatusCode, "200");
		$this->assertEquals($response->bodyResponse->campaignsDetails[0]->campaignPageName, $expectedCampaignPageName);
		$this->assertNotEmpty($response->bodyResponse->campaignsDetails);
	}

	public function testPagesForCampaign_WhenSuppliedCharityShortNameAndCampaignShortUrl_ReturnsPages()
	{
		//arrange
		$charityShortName = "jgdemo";
		$campaignShortUrl = "testsandboxcampaign";	
		$expectedCampaignPageName = "The name of my campaign";

		//act
		$response = $this->client->Campaign->PagesForCampaign($charityShortName, $campaignShortUrl);

		//assert
		$this->assertEquals($response->httpStatusCode, "200");
		$this->assertEquals($response->bodyResponse->campaignShortUrl, $campaignShortUrl);
		$this->assertEquals($response->bodyResponse->charityShortName, $charityShortName);
	}
}