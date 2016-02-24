<?php

include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';
include_once 'Model/RegisterCampaignRequest.php';

class CampaignApi extends ClientBase
{		
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}
	public function Retrieve($charityName, $campaignName)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/campaigns/". $charityName ."/". $campaignName;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json);
	}
	public function RetrieveV2($charityName, $campaignName)
	{
		$httpResponse = new HTTPResponse();
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/campaigns/". $charityName ."/". $campaignName;
		$url = $this->BuildUrl($locationFormat);
		$result = $this->curlWrapper->GetV2($url, $this->BuildAuthenticationValue());
		$httpResponse->bodyResponse = json_decode($result->bodyResponse);
		$httpResponse->httpStatusCode = $result->httpStatusCode;	
		return $httpResponse;
	}
	public function Create($campaignCreationRequest)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/campaigns";
		$url = $this->BuildUrl($locationFormat);
		$payload = json_encode($campaignCreationRequest);
		$json = $this->curlWrapper->Put($url, $this->BuildAuthenticationValue(), $payload);
		return json_decode($json);
	}
	public function CreateV2($campaignCreationRequest)
	{
		$httpResponse = new HTTPResponse();
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/campaigns";
		$url = $this->BuildUrl($locationFormat);
		$payload = json_encode($campaignCreationRequest);
		$result = $this->curlWrapper->PutV2($url, $this->BuildAuthenticationValue(), $payload);
		$httpResponse->bodyResponse = json_decode($result->bodyResponse);
		$httpResponse->httpStatusCode = $result->httpStatusCode;	
		return $httpResponse;
	}
	public function PagesForCampaign($charityShortName, $campaignShortUrl)
	{
		$httpResponse = new HTTPResponse();
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/campaigns/". $charityShortName ."/". $campaignShortUrl . "/pages";
		$url = $this->BuildUrl($locationFormat);
		$result = $this->curlWrapper->GetV2($url, $this->BuildAuthenticationValue());
		$httpResponse->bodyResponse = json_decode($result->bodyResponse);
		$httpResponse->httpStatusCode = $result->httpStatusCode;	
		return $httpResponse;
	}
	public function CampaignsByCharityId($charityId)
	{
		$httpResponse = new HTTPResponse();
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/campaigns/". $charityId;
		$url = $this->BuildUrl($locationFormat);
		$result = $this->curlWrapper->GetV2($url, $this->BuildAuthenticationValue());
		$httpResponse->bodyResponse = json_decode($result->bodyResponse);
		$httpResponse->httpStatusCode = $result->httpStatusCode;	
		return $httpResponse;
	}
	public function RegisterCampaignFundraisingPage($registerCampaignFundraisingPageRequest)
	{
		$httpResponse = new HTTPResponse();
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/campaigns";
		$url = $this->BuildUrl($locationFormat);
		$payload = json_encode($registerCampaignFundraisingPageRequest);
		$result = $this->curlWrapper->PostV2($url, $this->BuildAuthenticationValue(), $payload);
		$httpResponse->bodyResponse = json_decode($result->bodyResponse);
		$httpResponse->httpStatusCode = $result->httpStatusCode;	
		return $httpResponse;
	}
}
