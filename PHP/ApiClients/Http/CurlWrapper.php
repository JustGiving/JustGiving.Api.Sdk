<?php
class CurlWrapper
{
	public function __construct()
	{
		if (!function_exists('curl_init'))
		{ 
			die('CURL is not installed!');
		}
	}
	
	public function Get($url, $base64Credentials = "")
	{
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);		
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);

		$this->SetCredentials($ch, $base64Credentials);
		
		$buffer = curl_exec($ch);
		$info = curl_getinfo($ch);
		curl_close($ch);
		return $buffer;
	}	
	
	public function Put($url, $base64Credentials = "", $payload)
	{	
		$fh = fopen('php://temp', 'r+');
		fwrite($fh, $payload);
		rewind($fh);
	
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
		curl_setopt($ch, CURLOPT_PUT, true);
		curl_setopt($ch, CURLOPT_INFILE, $fh);
		curl_setopt($ch, CURLOPT_INFILESIZE, strlen($payload));
		
		$this->SetCredentials($ch, $base64Credentials);
		
		$buffer = curl_exec($ch);
		$info = curl_getinfo($ch);
		curl_close($ch);
		return $buffer;
	}
	
	public function Head($url, $base64Credentials = "")
	{
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
		curl_setopt($ch, CURLOPT_NOBODY, true);
		
		$this->SetCredentials($ch, $base64Credentials);
		
		$buffer = curl_exec($ch);
		$info = curl_getinfo($ch);		
		curl_close($ch);
		
		return $info;
	}	
	
	public function Post($url, $base64Credentials = "", $payload, $contentType="application/json")
	{
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
		curl_setopt($ch, CURLOPT_POST, true);
		curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
		
		$this->SetCredentials($ch, $base64Credentials, $contentType);
		
		$buffer = curl_exec($ch);
		$info = curl_getinfo($ch);
		curl_close($ch);
		return $info;
	}
	
	private function SetCredentials($ch, $base64Credentials = "", $contentType="application/json")
	{		
		if($base64Credentials != null && $base64Credentials != "")
		{			
			curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-type: '.$contentType, 'Authorize: Basic '.$base64Credentials, 'Authorization: Basic '.$base64Credentials ));
		}
		else
		{
			curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-type: '.$contentType));
		}
	}
}
