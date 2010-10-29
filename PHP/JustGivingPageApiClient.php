<?php

include_once 'JustGivingPageApiClient.php';
include_once 'ClientBase.php';
include_once 'CurlWrapper.php';

class JustGivingPageApiClient extends ClientBase
{		
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}
	
	public function ListAll()
	{		
		$verb = "GET";
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages";
		$url = $this->BuildUrl($locationFormat);
		return $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
	}
	
	public function Retrieve($pageShortName)
	{	
		$verb = "GET";
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/" . $pageShortName;
		$url = $this->BuildUrl($locationFormat);
		return $this->curlWrapper->Get($url);
	}
	
	public function RetrieveDonationsForPage($pageShortName, $pageSize=50, $pageNumber=1)
	{	
		$verb = "GET";
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/".$pageShortName."/donations"."?PageSize=".$pageSize."&PageNum=".$pageNumber;
		$url = $this->BuildUrl($locationFormat);
		return $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
	}

}
?>