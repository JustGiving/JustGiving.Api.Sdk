<?php
include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';

class AccountApi extends ClientBase
{		
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}
	
	public function Create($createAccountRequest)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/account";
		$url = $this->BuildUrl($locationFormat);
		$payload = json_encode($createAccountRequest);		
		$json = $this->curlWrapper->Put($url, $this->BuildAuthenticationValue(), $payload);
		return json_decode($json); 
	}
	
	public function ListAllPages($email)
	{		
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/account/" . $email . "/pages";
		$url = $this->BuildUrl($locationFormat);		
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json); 
	}
	
	public function IsEmailRegistered($email)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/account/" . $email;
		$url = $this->BuildUrl($locationFormat);		
		$httpInfo = $this->curlWrapper->Head($url, $this->BuildAuthenticationValue());		
		
		if($httpInfo['http_code'] == 200)
		{
			return true;
		}
		else if($httpInfo['http_code'] == 404)
		{
			return false;
		}
		else		
		{
			throw new Exception('IsEmailRegistered returned a status code it wasn\'t expecting. Returned ' . $httpInfo['http_code']);
		}
	}
	
	public function RequestPasswordReminder($email)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/account/" . $email . "/requestpasswordreminder";
		$url = $this->BuildUrl($locationFormat);		
		$json = $this->curlWrapper->Get($url, $this->BuildAuthenticationValue());
		return json_decode($json); 		
	}
}
