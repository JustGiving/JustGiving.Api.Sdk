<?php

class TestContext
{	
	public $ApiLocation;
	public $ApiKey;
	public $TestUsername;
	public $TestValidPassword;
	public $TestInvalidPassword;
	public $ApiVersion;
	public $Debug;
	
	public function __construct()
	{
		$this->ApiLocation = "https://api-staging.justgiving.com/";
		$this->ApiKey = "decbf1d2";
		$this->TestUsername = "apiunittests@justgiving.com";
		$this->TestValidPassword = "password";
		$this->TestInvalidPassword = "badPassword";
		$this->ApiVersion = 1;
		
		$this->Debug = true;	

			
		echo  "Test Context<br/>";
		echo  "=================<br/>";
		echo  "You can modify this context by editing /JustGiving.Api.Sdk/PHP/Tests/TestContext.php<br/>";
		echo  "This will allow you to verify you have a working API key and setup.<br/>";
		echo  "ApiLocation: " . $this->ApiLocation . "<br/>";
		echo  "ApiKey: " . $this->ApiKey . "<br/>";
		echo  "TestUsername: " . $this->TestUsername . "<br/>";
		echo  "TestValidPassword: " . $this->TestValidPassword . "<br/>";
		echo  "TestInvalidPassword: " . $this->TestInvalidPassword . "<br/>";
		echo  "ApiVersion: " . $this->ApiVersion . "<br/>";
		echo  "=================<br/>";
	}
	
}