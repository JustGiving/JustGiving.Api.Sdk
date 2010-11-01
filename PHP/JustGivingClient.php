<?php

include_once 'ApiClients/PageApi.php';
include_once 'ApiClients/AccountApi.php';
include_once 'ApiClients/CharityApi.php';

class JustGivingClient
{	
	public $ApiKey;
	public $ApiVersion;
	public $Username;
	public $Password;
	public $RootDomain;
	
	public $Page;
	public $Account;
	public $Charity;

	public function __construct($rootDomain, $apiKey, $apiVersion, $username="", $password="")
	{
		$this->RootDomain   	= (string) $rootDomain; 
		$this->ApiKey     		= (string) $apiKey;
		$this->ApiVersion     	= (string) $apiVersion;
		$this->Username     	= (string) $username;
		$this->Password     	= (string) $password;
		$this->curlWrapper		= new CurlWrapper();
		$this->debug			= false;
		
		// Init API clients
		$this->Page				= new PageApi($this);
		$this->Account			= new AccountApi($this);
		$this->Charity			= new CharityApi($this);
	}
}