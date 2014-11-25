<?php
include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';

class CharityApi extends ClientBase
{		
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}
	
	public function Retrieve($charityId)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/charity/" . $charityId;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json); 
	}

	public function Authenticate($authenticateCharityAccountRequest)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/charity/authenticate";
		$url = $this->BuildUrl($locationFormat);
		$payload = json_encode($authenticateCharityAccountRequest);
		$json = $this->curlWrapper->PostAndGetResponse($url, "", $payload);
		return json_decode($json);
	}

	public function GetEventsByCharityId($charityId)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/charity/" . $charityId . "/events";
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url);
		return json_decode($json);
	}

	public function GetDonations($charityId)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/charity/" . $charityId . "/donations";
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url);
		return json_decode($json);
	}
}
