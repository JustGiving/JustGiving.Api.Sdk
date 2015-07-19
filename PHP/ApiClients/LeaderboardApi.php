<?php
include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';

class LeaderboardApi extends ClientBase
{
	public $Parent;
	public $curlWrapper;

	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}

	public function GetCharityLeaderboard($charityId)
	{
		$httpResponse = new HTTPResponse();
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/charity/" . $charityId . "/leaderboard";
		$url = $this->BuildUrl($locationFormat);
		$result = $this->curlWrapper->GetV2($url, $this->BuildAuthenticationValue());
		$httpResponse->bodyResponse = json_decode($result->bodyResponse);
		$httpResponse->httpStatusCode = $result->httpStatusCode;	
		return $httpResponse;
	}

	public function GetEventLeaderboard($eventId)
	{
		$httpResponse = new HTTPResponse();
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/event/" . $eventId . "/leaderboard";
		$url = $this->BuildUrl($locationFormat);
		$result = $this->curlWrapper->GetV2($url, $this->BuildAuthenticationValue());
		$httpResponse->bodyResponse = json_decode($result->bodyResponse);
		$httpResponse->httpStatusCode = $result->httpStatusCode;	
		return $httpResponse;
	}
}

?>