<?php
include_once 'ClientBase.php';

class CountriesApi extends ClientBase
{
	public $Parent;
	public $curlWrapper;

	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}

	public function Countries()
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/countries";
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url);
		return json_decode($json);
	}
}