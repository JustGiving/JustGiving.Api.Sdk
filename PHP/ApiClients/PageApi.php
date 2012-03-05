<?php

include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';
include_once 'Model/RegisterPageRequest.php';
include_once 'Model/StoryUpdateRequest.php';

class PageApi extends ClientBase
{		
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}
	
	public function Create($pageCreationRequest)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages";
		$url = $this->BuildUrl($locationFormat);
		$payload = json_encode($pageCreationRequest);		
		$json = $this->curlWrapper->Put($url, $this->BuildAuthenticationValue(), $payload);
		return json_decode($json); 
	}
	
	public function IsShortNameRegistered($pageShortName)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/" . $pageShortName;
		$url = $this->BuildUrl($locationFormat);		
		$httpInfo = $this->curlWrapper->Head($url, $this->BuildAuthenticationValue());		
		
		if($httpInfo['http_code'] == 200)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public function ListAll()
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages";
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json);
	}
	
	public function Retrieve($pageShortName)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/" . $pageShortName;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json);
	}	
	
	public function SuggestPageShortNames($preferredName)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/suggest?preferredName=" . urlencode ($preferredName);
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json);
	}	
	
	public function RetrieveDonationsForPage($pageShortName, $pageSize=50, $pageNumber=1)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/".$pageShortName."/donations"."?PageSize=".$pageSize."&PageNum=".$pageNumber;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json);
	}
	
	public function UpdateStory($pageShortName, $storyUpdate)
	{		
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/" . $pageShortName;
		$url = $this->BuildUrl($locationFormat);
		$storyUpdateRequest = new StoryUpdateRequest();
		$storyUpdateRequest->storySupplement = $storyUpdate;
		$payload = json_encode($storyUpdateRequest);		
		$httpInfo = $this->curlWrapper->Post($url, $this->BuildAuthenticationValue(), $payload);
		
		if($httpInfo['http_code'] == 200)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public function UploadImage($pageShortName, $caption, $filename, $imageContentType)
	{            
		$fh = fopen($filename, 'r');
		$imageBytes = fread($fh, filesize($filename));
		fclose($fh);
	
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/" . $pageShortName . "/images?caption=" . urlencode ($caption);
		$url = $this->BuildUrl($locationFormat);
		$httpInfo = $this->curlWrapper->Post($url, $this->BuildAuthenticationValue(), $imageBytes, $imageContentType);
		
		if($httpInfo['http_code'] == 200)
		{
			return true;
		}
		else
		{
			return $httpInfo;
		}
	}
}
