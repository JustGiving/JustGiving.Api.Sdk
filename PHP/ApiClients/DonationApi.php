<?php
include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';

class DonationApi extends ClientBase
{		
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}
	
	public function Retrieve($donationId)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/donation/" . $donationId;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json); 
	}	

	public function RetrieveV2($donationId)
	{
		$httpResponse = new HTTPResponse();
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/donation/" . $donationId;
		$url = $this->BuildUrl($locationFormat);
		$result = $this->curlWrapper->GetV2($url, $this->BuildAuthenticationValue());
		$httpResponse->bodyResponse = json_decode($result->bodyResponse);
		$httpResponse->httpStatusCode = $result->httpStatusCode;	
		return $httpResponse;
	}
	
	public function RetrieveStatus($donationId)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/donation/" . $donationId . "/status";
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json); 
	}

	public function RetrieveDetails($thirdPartReference)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/donation/ref/" . $thirdPartReference;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url);
		return json_decode($json);
	}
}
