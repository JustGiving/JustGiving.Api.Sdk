<?php

class HTTPRawResponse
{
	public $httpInfo;
	public $httpStatusCode;
	public $bodyResponse;
}

class CurlWrapper
{
	public function __construct()
	{
		if (!function_exists('curl_init'))
		{ 
			die('CURL is not installed!');
		}
	}

	public function GetV2($url, $base64Credentials = "")
	{
		$ch = curl_init();
		$httpResponse = new HTTPRawResponse();

		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);		
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);

		$this->SetCredentials($ch, $base64Credentials);
		
		$buffer = curl_exec($ch);
		$info = curl_getinfo($ch);
		curl_close($ch);
		$httpResponse->httpInfo = $info;
		$httpResponse->bodyResponse = $buffer;
		$httpResponse->httpStatusCode = $info['http_code'];
		return $httpResponse;
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

	public function PutV2($url, $base64Credentials = "", $payload)
	{	
		$httpResponse = new HTTPRawResponse();
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
		$httpResponse->httpInfo = $info;
		$httpResponse->bodyResponse = $buffer;
		$httpResponse->httpStatusCode = $info['http_code'];
		return $httpResponse;
		
	}	
	
	public function Put($url, $base64Credentials = "", $payload, $getHttpStatus = false)
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
		if($getHttpStatus == 1)
		{
			return $info;
		}
		else
		{
			return $buffer;
		}
		
	}

	public function PostBinary($url, $base64Credentials = "", $filename, $imageContentType)
	{	
		$fh = fopen($filename, 'rb');
		$imageBytes = fread($fh, filesize($filename));		
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_FOLLOWLOCATION, true);
		curl_setopt($ch, CURLOPT_MAXREDIRS, 10 );
		curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-type: '.$imageContentType, 'Authorize: Basic '.$base64Credentials, 'Authorization: Basic '.$base64Credentials));
		curl_setopt($ch, CURLOPT_POST, true);
		curl_setopt($ch, CURLOPT_POSTFIELDS, $imageBytes);
		curl_setopt($ch, CURLOPT_INFILE, $fh);
		curl_setopt($ch, CURLOPT_INFILESIZE, filesize($filename));
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		curl_setopt($ch, CURLOPT_TIMEOUT, 10);
		curl_setopt($ch, CURLOPT_VERBOSE, true);
		curl_setopt($ch, CURLINFO_HEADER_OUT, true);			
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);		
		$buffer = curl_exec($ch);	
		$info = curl_getinfo($ch);
		curl_close($ch);		
		return $info;
	}

	public function HeadV2($url, $base64Credentials = "")
	{
		$ch = curl_init();
		$httpResponse = new HTTPRawResponse();

		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
		curl_setopt($ch, CURLOPT_NOBODY, true);
		
		$this->SetCredentials($ch, $base64Credentials);
		
		$buffer = curl_exec($ch);
		$info = curl_getinfo($ch);		
		curl_close($ch);
		$httpResponse->httpInfo = $info;
		$httpResponse->bodyResponse = $buffer;
		$httpResponse->httpStatusCode = $info['http_code'];
		return $httpResponse;		
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

	public function PostV2($url, $base64Credentials = "", $payload, $contentType="application/json")
	{
		$httpResponse = new HTTPRawResponse();
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
		$httpResponse->httpInfo = $info;
		$httpResponse->bodyResponse = $buffer;
		$httpResponse->httpStatusCode = $info['http_code'];
		return $httpResponse;

	}	

	public function PostAndGetResponse($url, $base64Credentials = "", $payload, $contentType="application/json")
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
		return $buffer;
	}

	public function Delete($url, $base64Credentials = "", $contentType="application/json")
	{
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		curl_setopt($ch, CURLOPT_CUSTOMREQUEST, 'DELETE');

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
			curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-type: '.$contentType, 'Accept: '.$contentType, 'Authorize: Basic '.$base64Credentials, 'Authorization: Basic '.$base64Credentials ));
		}
		else
		{
			curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-type: '.$contentType, 'Accept: '.$contentType));
		}
	}
}
