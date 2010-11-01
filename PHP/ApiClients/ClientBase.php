<?php

class ClientBase
{		
	public $debug;
	
	public function __construct()
	{		
		$this->debug = false;
	}
	
	public function BuildUrl($locationFormat)
	{	
		$url = $locationFormat;
		$url = str_replace("{apiKey}", $this->Parent->ApiKey, $url);
		$url = str_replace("{apiVersion}", $this->Parent->ApiVersion, $url);
		return $url;
	}
	
	public function BuildAuthenticationValue()
	{
		$stringForEnc = $this->Parent->Username.":".$this->Parent->Password;
		return base64_encode($stringForEnc);
	}
	
	public function WriteLine($string)
	{
		echo $string . "<br/>";
	}	
}
