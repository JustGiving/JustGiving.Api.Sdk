<?php
include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';

class SearchApi extends ClientBase
{		
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}
	
	public function CharitySearch($searchTerms, $pageSize=50, $pageNumber=1)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/charity/search?q=".urlencode($searchTerms)."&PageSize=".$pageSize."&PageNum=".$pageNumber;
		$url = $this->BuildUrl($locationFormat);	
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json); 
	}

	public function EventSearch($searchTerms, $pageSize=50, $pageNumber=1)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/event/search?q=".urlencode($searchTerms)."&PageSize=".$pageSize."&PageNum=".$pageNumber;
		$url = $this->BuildUrl($locationFormat);	
		$json = $this->curlWrapper->Get($url);
		return json_decode($json); 
	}

	public function FundraiserSearch($searchTerms, $charityId, $pageSize=50, $pageNumber=1)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/search?q=".urlencode($searchTerms)."&PageSize=".$pageSize."&PageNum=".$pageNumber."&charityId=".$charityId;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url);
		return json_decode($json); 
	}	

	public function InMemorySearch($searchTerms, $pageSize=50, $pageNumber=1)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/remember/search?q=".urlencode($searchTerms)."&PageSize=".$pageSize."&PageNum=".$pageNumber;
		$url = $this->BuildUrl($locationFormat);	
		$json = $this->curlWrapper->Get($url);
		return json_decode($json); 
	}

	public function TeamSearch($teamShortName)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/team/search?teamname=". $teamShortName;
		$url = $this->BuildUrl($locationFormat);	
		$json = $this->curlWrapper->Get($url);
		return json_decode($json); 
	}
}
