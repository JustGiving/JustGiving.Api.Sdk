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
		$curl_handle = curl_init();
		curl_setopt($curl_handle,CURLOPT_URL, $url);
		curl_setopt($curl_handle,CURLOPT_RETURNTRANSFER, true);
		
		if($base64Credentials != null && $base64Credentials != "")
		{			
			curl_setopt($curl_handle,CURLOPT_HTTPHEADER, array('Content-type: application/xml', 'Authorize: Basic '.$base64Credentials, 'Authorization: Basic '.$base64Credentials ));
		}
		else
		{
			curl_setopt($curl_handle,CURLOPT_HTTPHEADER, array('Content-type: application/xml'));
		}
		
		$buffer = curl_exec($curl_handle);
		curl_close($curl_handle);
		return $buffer;
	}
}
?>