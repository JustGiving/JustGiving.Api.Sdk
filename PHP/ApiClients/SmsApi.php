<?php
include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';

class SmsApi extends ClientBase
{
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}

	public function RetrievePageSmsCode($pageShortName)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/". $pageShortName ."/sms";
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url);
		return json_decode($json);
	}

	public function UpdatePageSmsCode($pageShortName, $updatePageSmsCodeRequest)
	{
		$requestBody = $updatePageSmsCodeRequest;
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/fundraising/pages/". $pageShortName ."/sms";
		$payload = json_encode($requestBody);
		$url = $this->BuildUrl($locationFormat);
		$httpInfo = $this->curlWrapper->Put($url, $this->BuildAuthenticationValue(), $payload, true);
		if($httpInfo['http_code'] == 201)
		{
			return true;
		}
		else if($httpInfo['http_code'] == 404)
		{
			return false;
		}
	}

	public function CheckSmsCodeAvailability($urn)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/sms/urn/". $urn . "/check";
		$url = $this->BuildUrl($locationFormat);
		$httpInfo = $this->curlWrapper->Post($url);
		if($httpInfo['http_code'] == 200)
		{
			return true;
		}
		else if($httpInfo['http_code'] == 400)
		{
			return false;
		}
	}
}